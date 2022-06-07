using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Implementacija.Models
{
    public class Administrator
    {

        [ForeignKey("Osoba")]
        public int Id { get; set; }
        public Osoba osoba { get; set; }
        public Administrator() { }
    }
}
