using System.ComponentModel.DataAnnotations;

namespace Implementacija.Models
{
    public class Osoba
    {
        [Key]
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string KorisnickoIme { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Osoba() { }


    }
}
