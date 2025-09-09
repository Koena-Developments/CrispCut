using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CrispCut.Enums;
using System.Text.Json.Serialization;
namespace CrispCut.DTOs.ArtistServiceDTO
{
    public class ArtistOnBoardingDto
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        public string Bio { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ArtistCategory Category { get; set; }
        [Required]
        public string Address { get; set; }
        public decimal LocationLat { get; set; }
        public decimal LocationLng { get; set; }
        public string? OperatingHours { get; set; }        
    }
}