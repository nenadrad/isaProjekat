using RestBiz.DataLayer;
using RestBiz.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestBiz
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Roles.IsUserInRole("MenadzerSistema"))
                {
                    MenadzerSistemaDiv.Visible = true;
                    BindRestorani();
                }
                
            }
        }

        protected void SubmitRestoran_Click(object sender, EventArgs e)
        {
            using(var ctx = new RestBizContext())
            {
                Restoran restoran = new Restoran()
                {
                    Jelovnik = new Jelovnik(),
                    Naziv = Naziv.Text,
                    Opis = Opis.InnerText
                };

                ctx.Restorani.Add(restoran);
                ctx.SaveChanges();
            }

            Naziv.Text = string.Empty;
            Opis.InnerText = string.Empty;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "toastr.success('Novi restoran uspešno dodat.')", true);

        }

        private void BindRestorani()
        {
            using(var ctx = new RestBizContext())
            {
                Restorani.DataSource = ctx.Restorani.ToList();
                Restorani.DataTextField = "Naziv";
                Restorani.DataValueField = "RestoranId";
                Restorani.DataBind();
            }
        }

        protected void SubmitMenadzer_Click(object sender, EventArgs e)
        {
            using(var ctx = new RestBizContext())
            {
                MenadzerRestorana menadzer = new MenadzerRestorana()
                {
                    Adresa = Adresa.Text,
                    Email = Email.Text,
                    Ime = Ime.Text,
                    Lozinka = Lozinka.Text,
                    Prezime = Prezime.Text
                };

                var restoran = ctx.Restorani.Find(Convert.ToInt32(Restorani.SelectedValue));

                restoran.Menadzeri.Add(menadzer);

                ctx.SaveChanges();
            }

            Adresa.Text = string.Empty;
            Ime.Text = string.Empty;
            Prezime.Text = string.Empty;
            Email.Text = string.Empty;
            Lozinka.Text = string.Empty;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "toastr.success('Novi menadžer uspešno dodat.')", true);

        }
    }
}