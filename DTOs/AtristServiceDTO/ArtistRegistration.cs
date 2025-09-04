using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.Enums;

namespace CrispCut.DTOs.ArtistServiceDTO
{
      public class ArtistRegistrationDto
    {
        public int UserId { get; set; }
        public string Bio { get; set; }
        public ArtistCategory Category { get; set; }
        public string Address { get; set; }
        public decimal LocationLat { get; set; }
        public decimal LocationLng { get; set; }
        public string? OperatingHours { get; set; } 
    }
}