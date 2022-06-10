using System.ComponentModel.DataAnnotations;

namespace Implementacija.Models
{
    public class Obavijest
    {
        [Key]
        public int Id { get; set; }
        public string Tekst { get; set; }

        [EnumDataType(typeof(VrstaObavijesti))]
        public VrstaObavijesti Vrsta { get; set; }
        public Obavijest() { }
    }
}
