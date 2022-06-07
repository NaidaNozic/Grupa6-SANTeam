using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacija.Models
{
    public class GlumciVeza
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Film")]
        public int FilmID { get; set; } 
        public Film Film { get; set; }  
        [ForeignKey("Glumci")]
        public int GlumacId { get; set; }
        public Glumci Glumac { get; set; }
        public GlumciVeza() { }
    }
}
