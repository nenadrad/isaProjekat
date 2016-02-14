using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RestBiz.DataLayer.Entities
{
    [Table("Jelovnici")]
    public class Jelovnik
    {
        public Jelovnik()
        {

        }

        [Key, ForeignKey("Restoran")]
        public int RestoranId { get; set; }

        public virtual Restoran Restoran { get; set; }

        public virtual ICollection<StavkaJelovnika> Stavke { get; set; }

    }
}
