using RestBiz.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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


                ProfileHeader.Text = "<h2>" + nazivRestorana + " - konfiguracija sedenja</h2>";
            }

        }
    }
}