using System;

namespace CrispCut.DTOs.ArtistServiceDTO
{
    public class ArtistServiceDto
    {
        public int ArtistServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public int DurationMinutes { get; set; }
    }
}