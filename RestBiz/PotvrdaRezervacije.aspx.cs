using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestBiz.DataLayer;
using RestBiz.DataLayer.Entities;
using System.Web.Services;
using System.Web.Script.Serialization;
using RestBiz.Ajax;

namespace RestBiz
{
    public partial class PotvrdaRezervacije : System.Web.UI.Page
    {

        private int IdPoziva
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["id"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetValues();
            }
        }

        private void SetValues()
        {
            using (var ctx = new RestBizContext())
            {
                var poziv = ctx.Pozivi.Find(IdPoziva);
                Name.Text = poziv.Rezervacija.Restoran.Naziv;
                Date.Value = poziv.Rezervacija.Pocetak.ToShortDateString();
                DateTime.Value = poziv.Rezervacija.Pocetak.TimeOfDay.ToString();
                Time.Text = (poziv.Rezervacija.Zavrsetak - poziv.Rezervacija.Pocetak).TotalHours.ToString();
                foreach (Korisnik prijatelj in poziv.Rezervacija.Prijatelji)
                    Prijatelji.Text += prijatelj.Ime + ", ";
                Prijatelji.Text += poziv.Rezervacija.Korisnik.Ime;

                if (poziv.Potvrdio)
                {
                    controlsDiv.Visible = false;
                    if (poziv.Ocenjeno)
                    {
                        ocenaDiv.Visible = false;
                        controlDivOcena.Visible = false;
                    }
                    else
                    {
                        ocenaDiv.Visible = true;
                        controlDivOcena.Visible = true;
                    }
                }
                
                    
      
            }
        }

        protected void AcceptButton_Click(object sender, EventArgs e)
        {
            using (var ctx = new RestBizContext())
            {
                Poziv poziv = ctx.Pozivi.Single(p => p.PozivId == IdPoziva);
                
                Poziv pozivNew = new Poziv()
                {
                    Korisnik = poziv.Korisnik,
                    Ocena = poziv.Ocena,
                    Potvrdio = true,
                    Dolazi = true,
                    Ocenjeno = poziv.Ocenjeno,
                    PozivId = poziv.PozivId,
                    Rezervacija = poziv.Rezervacija

                };

                ctx.Entry(poziv).CurrentValues.SetValues(pozivNew);
                ctx.SaveChanges();
                
            }
            controlsDiv.Visible = false;
            controlDivOcena.Visible = true;
            ocenaDiv.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "toastr.success('Poziv prihvaćen.')", true);
        }

        protected void DeclineButton_Click(object sender, EventArgs e)
        {
            using (var ctx = new RestBizContext())
            {
                Poziv poziv = ctx.Pozivi.Single(p => p.PozivId == IdPoziva);

                Poziv pozivNew = new Poziv()
                {
                    Korisnik = poziv.Korisnik,
                    Ocena = poziv.Ocena,
                    Potvrdio = false,
                    Dolazi = false,
                    Ocenjeno = poziv.Ocenjeno,
                    PozivId = poziv.PozivId,
                    Rezervacija = poziv.Rezervacija

                };

                ctx.Entry(poziv).CurrentValues.SetValues(pozivNew);
                ctx.SaveChanges();

            }
            controlsDiv.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "toastr.success('Poziv odbijen.')", true);
        }

        [WebMethod]
        public static string Oceni(decimal ocena, int idPoz)
        {
            string retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, "greska"));

            using (var ctx = new RestBizContext()) 
            {
                Poziv poziv = ctx.Pozivi.Find(idPoz);
                Poziv pozivNew = new Poziv()
                {
                    Korisnik = poziv.Korisnik,
                    Ocena = ocena,
                    Potvrdio = poziv.Potvrdio,
                    Dolazi = poziv.Dolazi,
                    Ocenjeno = true,
                    PozivId = poziv.PozivId,
                    Rezervacija = poziv.Rezervacija

                };

                ctx.Entry(poziv).CurrentValues.SetValues(pozivNew);
                ctx.SaveChanges();

                retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(1, "Ocenjivanje uspešno"));
            }

            return retVal;
       
        }
    }
}