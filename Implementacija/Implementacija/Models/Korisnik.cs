using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacija.Models
{
    public class Korisnik
    {   [Key]
        public int Id { get;set; }
        [ForeignKey("Osoba")]
        public int osobaId { get; set; }
        public Osoba osoba { get; set; }
        public int Vip { get; set; }
        public Korisnik() { }
    }
}
