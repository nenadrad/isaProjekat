using RestBiz.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestBiz
{
    public partial class Restorani : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Site)this.Master).MarkAsSelected("RestoraniLink");

                using(var ctx = new RestBizContext())
                {
                    RestoraniRepeater.DataSource = ctx.Restorani.ToList();
                    RestoraniRepeater.DataBind();

                }
            }
        }
    }
}