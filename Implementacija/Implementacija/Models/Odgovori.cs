using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacija.Models
{
    public class Odgovori
    {
        [Key]
        public int Id { get; set; } 
        public int KomentarId { get; set; } 
        public string Autor { get; set; }
        public string Tekst { get; set; }
        public Odgovori() { }
    }
}
