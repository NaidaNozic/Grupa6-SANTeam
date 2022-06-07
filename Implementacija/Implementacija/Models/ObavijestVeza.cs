using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacija.Models
{
    public class ObavijestVeza
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }  
        [ForeignKey("Obavijest")]
        public int ObavijestId { get; set; }
        public Obavijest Obavijest { get; set; }
        public ObavijestVeza() { }
    }
}
