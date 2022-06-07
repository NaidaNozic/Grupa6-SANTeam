using System.ComponentModel.DataAnnotations;

namespace Implementacija.Models
{
    public class Ocjena
    {
        [Key]
        public int Id { get; set; } 
        public int OcjenaKorisnika { get; set; }
        public Ocjena() { }
    }
}
