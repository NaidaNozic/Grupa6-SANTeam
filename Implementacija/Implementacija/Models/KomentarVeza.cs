using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacija.Models
{
    public class KomentarVeza
    {
        [Key]
        public int Id { get; set; } 
        [ForeignKey("Film")]
        public int FilmId { get; set; } 
        public Film Film { get; set; }
        [ForeignKey("Komentar")]
        public int KomentarId { get; set; } 
        public Komentar Komentar { get; set; }
        public KomentarVeza() { }
    }
}
