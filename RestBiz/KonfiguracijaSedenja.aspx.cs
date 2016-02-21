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
    public partial class KonfiguracijaSedenja : System.Web.UI.Page
    {

        public int IdRestorana
        {
            get
            {
                return Convert.ToInt32(Request.QueryString["idRest"]);
            }
            set
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string nazivRestorana;
                using (var ctx = new RestBizContext())
                {
                    nazivRestorana = ctx.Restorani.Find(IdRestorana).Naziv;
                }


                ProfileHeader.Text = "<h2>" + nazivRestorana + "</h2>";

                RasporediStolove();

            }

        }

        private void RasporediStolove()
        {
            RowsRepeater.DataSource = new int[5];
            RowsRepeater.DataBind();
        }

        protected void RowsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater ColsRepeater = (Repeater)e.Item.FindControl("ColsRepeater");
            ColsRepeater.DataSource = new int[8];
            ColsRepeater.DataBind();
        }

       

        #region WebMethods

        [WebMethod]
        public static string Snimi(List<int> brojeviStolova, List<int> pozicijeStolova, int idRest)
        {
            string retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, "Greška pri konfigurisanju sedenja."));

            RestBiz.DataLayer.Entities.KonfiguracijaSedenja konfiguracije = new DataLayer.Entities.KonfiguracijaSedenja();
            konfiguracije.Stolovi = new List<PozicijaStola>();

           
            foreach (int brojStola in brojeviStolova)
            {
                PozicijaStola pozicija = new PozicijaStola()
                {
                    BrojStola = brojStola,
                    Pozicija = pozicijeStolova[brojeviStolova.IndexOf(brojStola)]
                };
                konfiguracije.Stolovi.Add(pozicija);                
            }

            using(var ctx = new RestBizContext())
            {
                var stolovi = ctx.Restorani.Find(idRest).KonfiguracijaSedenja = konfiguracije;
                ctx.SaveChanges();

                retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(1, "Konfigurisanje sedenja uspešno."));

            }

            return retVal;

        }

        #endregion
    }


}