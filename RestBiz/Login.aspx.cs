using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using RestBiz.DataLayer;
using RestBiz.DataLayer.Services;

namespace RestBiz
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                /*using (RestBizContext ctx = new RestBizContext())
                {
                    new RestBizDBInitializer().InitializeDatabase(ctx);
                }*/

                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Home.aspx");
                }
            }

        }

        protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
        {
            bool exists = false;
            bool isActivated = false;
            exists = Membership.ValidateUser(LoginUser.UserName, LoginUser.Password);
           if (exists)
            {
                string[] roles = Roles.GetRolesForUser(LoginUser.UserName);
                if (roles.Contains("MenadzerSistema") || roles.Contains("MenadzerRestorana"))
                    isActivated = true;
                else 
                    isActivated = new KorisnikService().IsActivated(LoginUser.UserName);
            }
            if (exists && isActivated)
            {
                FormsAuthentication.RedirectFromLoginPage(LoginUser.UserName, false);
            }
            else if(exists && !isActivated)
            {
                Response.Redirect("Info.aspx?type=neakt");
            }
           
        }

        protected void RegButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registracija.aspx");
        }
    }
}