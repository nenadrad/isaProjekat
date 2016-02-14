using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RestBiz.DataLayer.Entities
{
    [Table("StavkeJelovnika")]
    public class StavkaJelovnika
    {
        public StavkaJelovnika()
        {
        }

        public StavkaJelovnika(string naziv, string opis, decimal cena)
        {
            Naziv = naziv;
            Opis = opis;
            Cena = cena;
        }

        [Key]
        public int StavkaId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Naziv { get; set; }

        [Required]
        [MaxLength(50)]
        public string Opis { get; set; }

        public decimal Cena { get; set; }

        public virtual Jelovnik Jelovnik { get; set; }
    }
}
