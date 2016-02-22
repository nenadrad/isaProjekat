using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RestBiz.DataLayer.Entities
{
    [Table("PozicijeStolova")]
    public class PozicijaStola
    {
        [Key]
        public int PozicijaId { get; set; }

        public int Pozicija { get; set; }
        public int BrojStola { get; set; }

        public virtual KonfiguracijaSedenja KonfiguracijaSedenja { get; set; } 

        public virtual ICollection<Rezervacija> Rezervacije { get; set; }
    }
}
