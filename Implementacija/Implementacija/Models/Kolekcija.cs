using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Implementacija.Models
{
    public class Kolekcija
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public Kolekcija() { }

    }
}
