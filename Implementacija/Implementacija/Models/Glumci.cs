using System.ComponentModel.DataAnnotations;

namespace Implementacija.Models
{
    public class Glumci
    {
        [Key]
        public int Id { get; set; } 
        public string ImePrezime { get; set; }
        public Glumci() { }

    }
}
