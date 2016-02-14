using RestBiz.DataLayer;
using RestBiz.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestBiz
{
    public partial class ProfilRestorana : System.Web.UI.Page
    {
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

                    JelovnikRepeater.DataSource = restoran.Jelovnik.Stavke.ToList();
                    JelovnikRepeater.DataBind();
                }
            }
        }
    }
}