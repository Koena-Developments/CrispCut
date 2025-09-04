using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrispCut.DTOs.BookingsDTO
{
    public class CreateBookingDto
{
    [Required]
    public int ArtistServiceId { get; set; }

    [Required]
    public DateTime AppointmentTime { get; set; }

    public bool RequestRide { get; set; } = false; // Flag to trigger ride booking
}
}