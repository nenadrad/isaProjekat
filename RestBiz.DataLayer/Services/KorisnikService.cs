using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestBiz.DataLayer.Services
{
    public class KorisnikService
    {
        public Korisnik FindUserByEmail(string email) 
        {
            Korisnik korisnik = null;
            using (var ctx = new RestBizContext())
            {
                korisnik = (from k in ctx.Korisnici where k.Email == email select k).FirstOrDefault<Korisnik>();
            }
            return korisnik;
        }

        public bool IsActivated(string email)
        {
            bool activated = false;
            using(var ctx = new RestBizContext())
            {
                var korisnik = (from k in ctx.Korisnici where k.Email == email select k).FirstOrDefault<Korisnik>();
                if (ctx.AktivacijeKorisnika.Select(k => k.KorisnikId).Contains(korisnik.KorisnikId))
                    activated = false;
                else
                    activated = true;
            }
            return activated;
        }

        public List<Korisnik> GetFriends(int userId)
        {
            List<Korisnik> friends = null;
            using(var ctx = new RestBizContext())
            {
                var korisnik = (from k in ctx.Korisnici where k.KorisnikId == userId select k).FirstOrDefault();
                friends = korisnik.Prijatelji.ToList<Korisnik>();
            }
            return friends;
        }

    }
}
