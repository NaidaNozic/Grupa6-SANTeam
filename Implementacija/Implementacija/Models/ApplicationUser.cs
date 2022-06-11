using Microsoft.AspNetCore.Identity;

namespace Implementacija.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
    }
}
