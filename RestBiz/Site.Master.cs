using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;

namespace RestBiz
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void MarkAsSelected(string linkId)
        {
            HtmlGenericControl link = (HtmlGenericControl)FindControl(linkId);
            link.Attributes.Remove("class");
            link.Attributes.Add("class", "pure-menu-item pure-menu-selected");
        }

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        
    }
}