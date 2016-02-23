using RestBiz.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace RestBiz
{
    public partial class Restorani : System.Web.UI.Page
    {

        private HtmlTableCell RezColumn
        {
            get
            {
                ContentPlaceHolder cph1 = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                return (HtmlTableCell)cph1.FindControl("thRez");
            }
        }

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

                if (Roles.IsUserInRole("Korisnik"))
                    RezColumn.Visible = true;
            }
        }

        protected void RestoraniRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HtmlTableCell cell = (HtmlTableCell)e.Item.FindControl("tdRez");
            if (Roles.IsUserInRole("Korisnik"))
                cell.Visible = true;
        }
    }
}