using System.ComponentModel.DataAnnotations;

namespace Implementacija.Models
{
    public class Ocjena
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Rank")]
        public int OcjenaKorisnika { get; set; }

        public string userId { get; set; }
        public int movieId { get; set; }
        public Ocjena() { }
    }
}
