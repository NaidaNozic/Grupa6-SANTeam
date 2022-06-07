using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacija.Models
{
    public class Zaposlenik
    {

        [ForeignKey("Osoba")]
        public int Id { get; set; }
        public Zaposlenik() { }
    }
}
