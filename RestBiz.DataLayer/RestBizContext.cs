using System;
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


            base.OnModelCreating(modelBuilder);
        }
    }
}
