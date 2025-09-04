using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.DTOs.ArtistServiceDTO;

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
        public ArtistSummaryDto Artist { get; set; }
        public ArtistServiceDto Service { get; set; }
    }
}