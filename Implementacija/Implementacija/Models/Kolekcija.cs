using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacija.Models
{
    public class Kolekcija
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik korisnik { get; set; }  
        public Kolekcija() { }

    }
}
