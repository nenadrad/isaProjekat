using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using RestBiz.DataLayer.Entities;

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

        Restoran restoran1 = new Restoran()
        {
            Naziv = "Dva stapica",
            Opis  = "Kineska hrana"

        };

        Restoran restoran2 = new Restoran()
        {
            Naziv = "Picnic",
            Opis = "Opis restorana2"
        };

        Jelovnik jelovnik1 = new Jelovnik();
        Jelovnik jelovnik2 = new Jelovnik();
        

        protected override void Seed(RestBizContext context)
        {
            jelovnik1.Stavke = new List<StavkaJelovnika>() { new StavkaJelovnika("Jelo1", "Opis1", 100), new StavkaJelovnika("Jelo2", "Opis2", 200) };

            jelovnik2.Stavke = new List<StavkaJelovnika>() { new StavkaJelovnika("Jelo3", "Opis3", 300), new StavkaJelovnika("Jelo4", "Opis4", 400) };

            restoran1.Jelovnik = jelovnik1;
            restoran2.Jelovnik = jelovnik2;

            context.Korisnici.Add(admin);
            context.Korisnici.Add(korisnik1);
            context.Korisnici.Add(korisnik2);
            context.Restorani.Add(restoran1);
            context.Restorani.Add(restoran2);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
