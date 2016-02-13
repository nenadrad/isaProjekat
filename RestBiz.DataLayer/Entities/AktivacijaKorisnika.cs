using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RestBiz.DataLayer.Entities
{
    [Table("AktivacijaKorisnika")]
    public class AktivacijaKorisnika
    {
        [Key, ForeignKey("Korisnik")]

        public int KorisnikId { get; set; }

        public virtual Korisnik Korisnik { get; set; }

        public Guid ActivationCode { get; set; }
    }
}
