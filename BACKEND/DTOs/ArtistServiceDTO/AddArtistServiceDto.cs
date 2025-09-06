using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.Enums;

namespace CrispCut.DTOs.AtristServiceDTO
{
     public class AddArtistServiceDto
    {
        [Required]
        [StringLength(100)]
        public string ServiceName { get; set; }

        [Required]
        public ArtistCategory Category { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 480)] // Duration in minutes (e.g., up to 8 hours)
        public int DurationMinutes { get; set; }
        
        public string? Description { get; set; }
    }
}