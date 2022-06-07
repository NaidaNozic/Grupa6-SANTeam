using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacija.Models
{
    public class KolekcijaVeza
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; } 
        public Korisnik Korisnik { get; set; }  
        [ForeignKey("Kolekcija")]
        public int Kolekcijaid { get; set; }   
        public Kolekcija Kolekcija { get; set; }

        public KolekcijaVeza() { }
    }
}
