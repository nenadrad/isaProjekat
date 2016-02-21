using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RestBiz.DataLayer.Entities
{
    [Table("Restorani")]
    public class Restoran
    {
        public Restoran()
        {
        }

        [Key]
        public int RestoranId { get; set; }

        [Required]
        public string Naziv { get; set; }

        [Required]
        [MaxLength(200)]
        public string Opis { get; set; }

        public virtual Jelovnik Jelovnik { get; set; }

        public virtual KonfiguracijaSedenja KonfiguracijaSedenja { get; set; }

    }
}
