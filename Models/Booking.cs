using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.Enums;

namespace CrispCut.Models
{
    public class Booking
{
    [Key]
    public int BookingId { get; set; }
    public int UserId { get; set; } // Customer FK
    public int ArtistId { get; set; } // Artist FK
    public int ArtistServiceId { get; set; } // Service FK

    public DateTime AppointmentTime { get; set; }
    public BookingStatus Status { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal ServicePrice { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? RideFare { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual User User { get; set; }
    public virtual Artist Artist { get; set; }
    public virtual ArtistService ArtistService { get; set; }
    public virtual Review? Review { get; set; }
    public virtual Ride? Ride { get; set; }
}
}