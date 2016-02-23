using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestBiz.DataLayer.Entities;

namespace RestBiz.DataLayer
{
    [Table("MenadzeriSistema")]
    public class MenadzerSistema
    {
        
        public MenadzerSistema()
        {
        }

        [Key]
        public int MenadzerSistemaId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Ime { get; set; }

        [Required]
        [MaxLength(20)]
        public string Prezime { get; set; }

        [MaxLength(50)]
        public string Adresa { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lozinka { get; set; }

        [NotMapped]
        public string ImePrezime
        {
            get
            {
                return Ime + " " + Prezime;
            }
            set
            {
                ImePrezime = value;
            }
        }
        
    }
}
