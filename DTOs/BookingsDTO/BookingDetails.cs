using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.DTOs.ArtistServiceDTO;
using CrispCut.DTOs.UserDTO;

namespace CrispCut.DTOs.BookingsDTO
{
   public class BookingDetailsDto
    {
        public int BookingId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Status { get; set; }
        public decimal ServicePrice { get; set; }
        public decimal? RideFare { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Nested DTOs for rich information
        public ArtistDto Artist { get; set; }
        public ArtistServiceDto Service { get; set; }
        public UserDto User { get; set; }
    }
}