using RestBiz.Ajax;
using RestBiz.DataLayer;
using RestBiz.DataLayer.Entities;
using RestBiz.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

        private bool UserIsManager { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int idRestorana = Convert.ToInt32(Request.QueryString["id"]);
                using(var ctx = new RestBizContext())
                {

                    if (Roles.IsUserInRole("MenadzerRestorana"))
                    {
                        MenadzerRestorana currentUser = null;
                        string userEmail = HttpContext.Current.User.Identity.Name;
                        currentUser = (from k in ctx.MenadzeriRestorana where k.Email == userEmail select k).FirstOrDefault();
                        if ((currentUser.Restoran.RestoranId == idRestorana))
                            UserIsManager = true;
                        else
                            UserIsManager = false;
                    }
                    else
                        UserIsManager = false;

                    Restoran restoran = ctx.Restorani.Find(idRestorana);
                    PodaciRestorana.DataSource = new List<Restoran>() { restoran };
                    PodaciRestorana.DataBind();
                    ProfileHeader.Text = "<h2>" + restoran.Naziv + "</h2>";
                    RestoranId = restoran.RestoranId;

                    JelovnikRepeater.DataSource = restoran.Jelovnik.Stavke.ToList();
                    JelovnikRepeater.DataBind();

                    if (UserIsManager)
                    {
                        ButtonConf.Visible = true;
                        thEdit.Visible = true;
                        thDel.Visible = true;
                        dodajBtn.Visible = true;
                    }

                    if (ctx.KonfiguracijeSedenja.Select(k => k.IdRestorana).Contains(RestoranId))
                        ButtonConf.Visible = false;
                    else
                        ButtonConf.NavigateUrl = "KonfiguracijaSedenja.aspx?idRest=" + idRestorana;

                    
                   
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

        protected void PodaciRestorana_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HtmlAnchor btn = (HtmlAnchor)e.Item.FindControl("editRestBtn");
            if (UserIsManager)
                btn.Visible = true;
        }

        protected void JelovnikRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HtmlTableCell cellEdit = (HtmlTableCell)e.Item.FindControl("editCell");
            HtmlTableCell cellDel = (HtmlTableCell)e.Item.FindControl("delCell");
            if(UserIsManager)
            {
                cellEdit.Visible = true;
                cellDel.Visible = true;
            }
        }
    }



}