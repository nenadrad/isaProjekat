using RestBiz.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestBiz
{
    public partial class Info : System.Web.UI.Page
    {
        private string Type
        {
            get
            {
                return Request.QueryString["type"];
            }
            set
            {

            }
        }

        private String ActivationCode
        {
            get
            {
                return Request.QueryString["ActivationCode"];
            }
            set
            {

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            switch(Type)
            {
                case "reg":
                    Message.Text = "Registracija uspešna. Link sa linkom za aktivaciju naloga je poslat na vašu email adresu.";
                    break;
                case "akt":
                    Activate();
                    Message.Text = "Aktivacija naloga uspešna.";
                    break;
                case "neakt":
                    Message.Text = "Vaš nalog nije aktiviran. Aktivirajte nalog i pokušajte ponovo.";
                    break;
                case "rez":
                    Message.Text = "Rezervacija uspešna.";
                    break;
            }
        }

        private void Activate()
        {
            using(var ctx = new RestBizContext())
            {
                var aktivacija = (from a in ctx.AktivacijeKorisnika where a.ActivationCode.ToString() == ActivationCode select a).FirstOrDefault();
                ctx.AktivacijeKorisnika.Remove(aktivacija);
                ctx.SaveChanges();
            }
        }
    }
}