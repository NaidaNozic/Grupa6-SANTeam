using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacija.Models
{
    public class OcjenaVeza
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Film")]
        public int FilmId { get; set; }
        public Film Film { get; set; }  
        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }  
        [ForeignKey("Ocjena")]
        public int OcjenaId { get; set; }
        public Ocjena OcjenaKorisnika { get; set; }
        public OcjenaVeza() { }
    }
}
