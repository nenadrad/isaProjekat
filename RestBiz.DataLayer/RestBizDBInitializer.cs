using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace RestBiz.DataLayer
{
    public class RestBizDBInitializer : DropCreateDatabaseIfModelChanges<RestBizContext>
    {
        Korisnik admin = new Korisnik
        {
            Ime = "Nenad",
            Prezime = "Rad",
            Adresa = "Kisacka 51",
            Email = "nenad.rad933@gmail.com",
            Lozinka = "admin"
        };

        Korisnik korisnik1 = new Korisnik
        {
            Ime = "Sonja",
            Prezime = "Mijatović",
            Adresa = "Adresa 1",
            Email = "sonja.mijatovic@gmail.com",
            Lozinka = "maslacak"
        };

        Korisnik korisnik2 = new Korisnik
        {
            Ime = "Ivana",
            Prezime = "Tesanovic",
            Adresa = "Kralja Petra I B3",
            Email = "ivana.tesanovic@gmail.com",
            Lozinka = "ivana"
        };

        protected override void Seed(RestBizContext context)
        {
            context.Korisnici.Add(admin);
            context.Korisnici.Add(korisnik1);
            context.Korisnici.Add(korisnik2);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
