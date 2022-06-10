using System.ComponentModel.DataAnnotations;

namespace Implementacija.Models
{
    public enum VrstaObavijesti
    {
        [Display(Name = "Komentar obavijest")] KomentarObavijest,
        [Display(Name = "Film obavijest")] FilmObavijest
    }
}
