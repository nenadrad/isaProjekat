using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RestBiz.DataLayer;
using System.Web.Services;
using System.Web.Script.Serialization;
using RestBiz.Ajax;
using RestBiz.Utills;

namespace RestBiz
{
    public partial class Registracija : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        [WebMethod]
        public static string Register(string ime, string prezime, string email, string lozinka)
        {
            string retVal = "";

            using(var ctx = new RestBizContext())
            {
                if (ctx.MenadzeriSistema.Select(k => k.Email).Contains(email) || ctx.MenadzeriRestorana.Select(k => k.Email).Contains(email) || ctx.Korisnici.Select(k => k.Email).Contains(email))
                    retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(0, "Korisnik sa unetim email-om već postoji"));
                else
                {
                    Korisnik noviKorisnik = new Korisnik()
                    {
                        Ime = ime,
                        Prezime = prezime,
                        Email = email,
                        Lozinka = lozinka
                    };

                    ctx.Korisnici.Add(noviKorisnik);
                    ctx.SaveChanges();
                    retVal = new JavaScriptSerializer().Serialize(new AjaxCallStatus(1, noviKorisnik));

                    new Registration().SendActivationEmail(email);

                    

                }
            }

            return retVal;
        }
        

     
    }
}