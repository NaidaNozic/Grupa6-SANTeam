using System.ComponentModel.DataAnnotations;

namespace Implementacija.Models
{
    public class Obavijest
    {
        [Key]
        public int Id { get; set; }
        public string Tekst { get; set; }
        public VrstaObavijesti Vrsta { get; set; }
        public Obavijest() { }
    }
}
