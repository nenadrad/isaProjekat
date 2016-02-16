using RestBiz.Ajax;
using RestBiz.DataLayer;
using RestBiz.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestBiz
{
    public partial class ProfilRestorana : System.Web.UI.Page
    {

        private int RestoranId
        {
            get
            {
                return Convert.ToInt32(ViewState["RestoranId"]);
            }
            set
            {
                ViewState["RestoranId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int idRestorana = Convert.ToInt32(Request.QueryString["id"]);
                using(var ctx = new RestBizContext())
                {
                    Restoran restoran = ctx.Restorani.Find(idRestorana);
                    PodaciRestorana.DataSource = new List<Restoran>() { restoran };
                    PodaciRestorana.DataBind();
                    ProfileHeader.Text = "<h2>" + restoran.Naziv + "</h2>";
                    RestoranId = restoran.RestoranId;

                    JelovnikRepeater.DataSource = restoran.Jelovnik.Stavke.ToList();
                    JelovnikRepeater.DataBind();
                }
            }
        }

        #region WebMethods

        [WebMethod]
        public static string Edit(int id, string naziv, string opis, decimal cena)
        {
            string retVal = "";
            using (var ctx = new RestBizContext())
            {
                StavkaJelovnika stavka = ctx.StavkeJelovnika.Find(id);
                if (stavka != null)
                {
                    stavka.Naziv = naziv;
                    stavka.Opis = opis;
                    stavka.Cena = cena;

                    ctx.SaveChanges();

                    retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(1, new StavkaJelovnika(stavka.Naziv, stavka.Opis, stavka.Cena)));
                }
                else
                    retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, ""));
            }

            return retVal;
        }

        [WebMethod]
        public static string Add(int idRest, string naziv, string opis, decimal cena)
        {
            string retVal = "";

            StavkaJelovnika novaStavka = new StavkaJelovnika()
            {
                Naziv = naziv,
                Opis = opis,
                Cena = cena
            };

            using (var ctx = new RestBizContext())
            {
                var restoran = ctx.Restorani.Find(idRest);
                if (restoran != null)
                {
                    restoran.Jelovnik.Stavke.Add(novaStavka);
                    ctx.SaveChanges();

                    StavkaJelovnika stavkaClone = new StavkaJelovnika
                    {
                        StavkaId = novaStavka.StavkaId,
                        Naziv = novaStavka.Naziv,
                        Opis = novaStavka.Opis,
                        Cena = novaStavka.Cena
                    };

                    retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(1, stavkaClone));
                }
                else
                {
                    retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, "greska"));
                }
            }   

            return retVal;
        }

        [WebMethod]
        public static string Remove(int id, int idRest)
        {
            string retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, null));

            using(var ctx = new RestBizContext())
            {
                var itemToRemove = ctx.StavkeJelovnika.Find(id);
                ctx.Restorani.Find(idRest).Jelovnik.Stavke.Remove(itemToRemove);

                ctx.SaveChanges();

                retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(1, itemToRemove));
            }

            return retVal;
        }

        [WebMethod]
        public static string EditRest(int id, string naziv, string opis)
        {
            string retVal = "";

            using(var ctx = new RestBizContext())
            {
                var restoran = ctx.Restorani.Find(id);
                if(restoran != null)
                {
                    restoran.Naziv = naziv;
                    restoran.Opis = opis;

                    ctx.SaveChanges();

                    retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(1, null));
                }
                else
                {
                    retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, null));
                }
            }
 
            return retVal;
        }

        #endregion

        protected void hiddenButton_Click(object sender, EventArgs e)
        {
            using (var ctx = new RestBizContext())
            {
                JelovnikRepeater.DataSource = ctx.Restorani.Find(RestoranId).Jelovnik.Stavke.ToList();
                JelovnikRepeater.DataBind();

            }
        }
    }



}