using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Implementacija.Models
{
    public class Film
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("original_title")]
        public string Naziv { get; set; }
        [JsonPropertyName("release_date")]
        public int GodinaObjave { get; set; }
        public string Zanr { get; set; }
        public string Trajanje { get; set; }
        [JsonPropertyName("overview")]
        public string Sinopsis { get; set; }
        public string Direktor { get; set; }
        public double OcjenaIMDb { get; set; }
        [JsonPropertyName("poster_path")]
        public string Slika { get; set; }   
        public int tmbd_id { get; set; }
        public Film() { }

    }
}
