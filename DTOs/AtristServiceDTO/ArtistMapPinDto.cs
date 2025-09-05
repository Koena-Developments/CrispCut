using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.Enums;

namespace CrispCut.DTOs.AtristServiceDTO
{
     public class ArtistMapPinDto
    {
        public int ArtistId { get; set; }
        public string FullName { get; set; }
        public ArtistCategory Category { get; set; }
        public decimal LocationLat { get; set; }
        public decimal LocationLng { get; set; }
    }
}