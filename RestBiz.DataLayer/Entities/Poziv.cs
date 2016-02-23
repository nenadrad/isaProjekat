using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestBiz.DataLayer.Entities
{
    [Table("Poziv")]
    public class Poziv
    {
        [Key]
        public int PozivId { get; set; }

        [Required]
        public virtual Korisnik Korisnik { get; set; }

        [Required]
        public virtual Rezervacija Rezervacija { get; set; }

        [Required]
        public bool Potvrdio { get; set; }

        [Required]
        public bool Dolazi { get; set; }

        [Required]
        public bool Ocenjeno { get; set; }

        public decimal Ocena { get; set; }
    }
}
