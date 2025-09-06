using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrispCut.Models
{
    public class Ride
{
    [Key]
    public int RideId { get; set; }

    [Required]
    public int BookingId { get; set; } // FK

    public string Provider { get; set; } // "Bolt" or "Uber"
    public string ExternalRideId { get; set; } // The ID from the provider's API
    public string Status { get; set; } // e.g., "Requested", "Completed"

    [Column(TypeName = "decimal(10, 2)")]
    public decimal FareEstimate { get; set; }

    // Navigation Property
    public virtual Booking Booking { get; set; }
}
}