using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestBiz.DataLayer;
using RestBiz.DataLayer.Entities;
using System.Net.Mail;
using System.Net;

namespace RestBiz.Utills
{
    public class Invitation
    {
        public void SendInvitation(RestBizContext ctx, RestBiz.DataLayer.Entities.Rezervacija rezervacija) {

            foreach (Korisnik prijatelj in rezervacija.Prijatelji)
            {
                Poziv poziv = new Poziv()
                {
                    Korisnik = prijatelj,
                    Rezervacija = rezervacija,
                    Potvrdio = false,
                    Dolazi = false,
                    Ocenjeno = false,
                    Ocena = -1
                };

                ctx.Pozivi.Add(poziv);
                ctx.SaveChanges();

                int id = poziv.PozivId;

                using (MailMessage mm = new MailMessage(new MailAddress("nenad.rad933@gmail.com", "RestBiz"), new MailAddress(prijatelj.Email)))
                {
                    mm.Subject = "Pozivnica";
                    string body = "Pozdrav, " + prijatelj.Ime + ",";
                    body += "<br/><br/>Molim Vas da kliknete na link radi potvrde rezervacije.";
                    body += "<br/><a href ='" + HttpContext.Current.Request.Url.AbsoluteUri.Replace("Rezervacija.aspx?idRest="+rezervacija.Restoran.RestoranId, "PotvrdaRezervacije.aspx?id=" + id.ToString()) + "'>Kliknite ovde.</a>";
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
}