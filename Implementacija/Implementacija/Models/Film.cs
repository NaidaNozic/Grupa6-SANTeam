using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Implementacija.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }   
        public int GodinaObjave { get; set; }
        public string Zanr { get; set; }
        public string Trajanje { get; set; }
        public string Sinopsis { get; set; }
        public string Direktor { get; set; }
        public double OcjenaIMDb { get; set; }

        public Film() { }

    }
}
