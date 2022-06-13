using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Implementacija.Models
{
    public class Komentar
    {
        [Key]
        public int Id { set; get; } 
        public string Tekst { set; get; }   
        public string Autor { set; get; }
        public int tmbd_id { set; get; }
        public Komentar() { }

    }
}
