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

        [Key]
        public int StavkaId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Naziv { get; set; }

        public decimal Cena { get; set; }

        public virtual Jelovnik Jelovnik { get; set; }
    }
}
