﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using RestBiz.DataLayer.Entities;

namespace RestBiz.DataLayer
{
    public class RestBizContext : DbContext
    {
        public RestBizContext() : base("name=ConnectionString")
        {
            Database.SetInitializer<RestBizContext>(new RestBizDBInitializer());
        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<AktivacijaKorisnika> AktivacijeKorisnika { get; set; }
        public DbSet<Restoran> Restorani { get; set; }
        public DbSet<Jelovnik> Jelovnici { get; set; }
        public DbSet <StavkaJelovnika> StavkeJelovnika { get; set; }
        public DbSet <KonfiguracijaSedenja> KonfiguracijeSedenja { get; set; }
        public DbSet<PozicijaStola> Stolovi { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<MenadzerRestorana> MenadzeriRestorana { get; set; }
        public DbSet<MenadzerSistema> MenadzeriSistema { get; set; }
        public DbSet<Poziv> Pozivi { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Korisnik>()
                .HasMany<Korisnik>(s => s.Prijatelji)
                .WithMany()
                .Map(cs =>
                    {
                        cs.MapLeftKey("KorisnikId");
                        cs.MapRightKey("PrijateljId");
                        cs.ToTable("Prijatelji");
                    });

            modelBuilder.Entity<StavkaJelovnika>().Property(c => c.Cena).HasPrecision(10, 2);

            modelBuilder.Entity<Rezervacija>()
                .HasMany<Korisnik>(r => r.Prijatelji)
                .WithMany(k => k.Rezervacije)
                .Map(cs =>
                {
                    cs.MapLeftKey("RezervacijaId");
                    cs.MapRightKey("KorisnikId");
                    cs.ToTable("PozvaniPrijatelji");
                });

            modelBuilder.Entity<Rezervacija>()
                .HasMany<PozicijaStola>(r => r.Stolovi)
                .WithMany(p => p.Rezervacije)
                .Map(cs =>
                {
                    cs.MapLeftKey("RezervacijaId");
                    cs.MapRightKey("PozicijaId");
                    cs.ToTable("RezervisaniStolovi");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
