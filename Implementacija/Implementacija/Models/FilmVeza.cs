using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacija.Models
{
    public class FilmVeza
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Kolekcija")]
        public int KolekcijaId { get; set; }    
        public Kolekcija Kolekcija { get; set; }    
        [ForeignKey("Film")]
        public int FilmId { get; set; } 
        public Film Film { get; set; }
        public FilmVeza() { }
    }
}
