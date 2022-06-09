using System.Collections.Generic;

namespace Implementacija.Models
{
    public class MoviesResponse
    {
        public int MovieId { get; set; }
        public int page { get; set; }
        public List<ApiMovie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
