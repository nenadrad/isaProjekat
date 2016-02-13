using RestBiz.DataLayer;
using RestBiz.DataLayer.Entities;
using RestBiz.DataLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace RestBiz.Utills
{
    public class Registration
    {
        public void SendActivationEmail(string userEmail)
        {

            var korisnik = new KorisnikService().FindUserByEmail(userEmail);

            Guid activationCode = Guid.NewGuid();
            string activationCodeString = activationCode.ToString();

            using (var ctx = new RestBizContext())
            {
                var aktivacijaKorisnika = new AktivacijaKorisnika()
                {
                    KorisnikId = korisnik.KorisnikId,
                    ActivationCode = activationCode
                };

                ctx.AktivacijeKorisnika.Add(aktivacijaKorisnika);
                ctx.SaveChanges();
            }

            using (MailMessage mm = new MailMessage("nenad.rad933@gmail.com", userEmail))
            {
                mm.Subject = "Aktivacija naloga";
                string body = "Pozdrav, " + korisnik.Ime + ",";
                body += "<br/><br/>Molim Vas kliknete na link radi aktivacije vašeg naloga";
                body += "<br/><a href ='" + HttpContext.Current.Request.Url.AbsoluteUri.Replace("Registracija.aspx/Register", "Info.aspx?type=akt&ActivationCode=" + activationCodeString) + "'>Kliknite ovde za aktivaciju.</a>";
                body += "<br/><br/>Hvala";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("nenad.rad933@gmail.com", "pitajmamu");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }

        }
    }
}