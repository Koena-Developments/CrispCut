using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.Enums;

namespace CrispCut.DTOs.ArtistServiceDTO
{
       public class ArtistDto
    {
        public int ArtistId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; } 
        public string Bio { get; set; }
        public ArtistCategory Category { get; set; }
        public string Address { get; set; }
        public bool IsVerified { get; set; }
        public decimal? AverageRating { get; set; }
    }
}