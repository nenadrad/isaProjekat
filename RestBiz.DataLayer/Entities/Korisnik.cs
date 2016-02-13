using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestBiz.DataLayer
{
    [Table("Korisnici")]
    public class Korisnik
    {
        
        public Korisnik()
        {
        }

        [Key]
        public int KorisnikId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Ime { get; set; }

        [Required]
        [MaxLength(20)]
        public string Prezime { get; set; }

        [MaxLength(50)]
        public string Adresa { get; set; }

        public int? BrojPoseta { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lozinka { get; set; }

        public virtual ICollection<Korisnik> Prijatelji { get; set; }

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
