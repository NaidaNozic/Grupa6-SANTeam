using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacija.Models
{
    public class Korisnik
    {
        [ForeignKey("Osoba")]
        public int Id { get;set; }
        public Osoba osoba { get; set; }
        public int Vip { get; set; }
        public Korisnik() { }
    }
}
