using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RestBiz.DataLayer.Entities
{
    [Table("KonfiguracijeSedenja")]
    public class KonfiguracijaSedenja
    {
        public KonfiguracijaSedenja()
        {

        }

        [Key, ForeignKey("Restoran")]
        public int IdRestorana { get; set; }

        public virtual Restoran Restoran { get; set; }

        public virtual ICollection<PozicijaStola> Stolovi { get; set; }
    }
}
