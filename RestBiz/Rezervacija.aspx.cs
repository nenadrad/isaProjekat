using RestBiz.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace RestBiz
{
    public partial class Rezervacija : System.Web.UI.Page
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

        public string NazivRestorana
        {
            get
            {
                using(var ctx = new RestBizContext())
                {
                    return ctx.Restorani.Find(IdRestorana).Naziv;
                }
            }
            set
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Name.Text = NazivRestorana; 
            }

        }
    }
}