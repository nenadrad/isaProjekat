using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RestBiz.DataLayer.Entities
{
    [Table("Rezervacije")]
    public class Rezervacija
    {
        [Key]
        public int RezervacijaId { get; set; }

        [Required]
        public Restoran Restoran { get; set; }

        [Required]
        public DateTime Pocetak { get; set; }

        [Required]
        public DateTime Zavrsetak { get; set; }

        public virtual ICollection<PozicijaStola> Stolovi { get; set; }

        public virtual ICollection<Korisnik> Prijatelji { get; set; }

    }
}
