using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Trim.Enums;

namespace Trim.Models
{
   
public class Artist
{
    [Key]
    public int ArtistId { get; set; }

    [Required]
    public int UserId { get; set; } // Foreign Key

    public string Bio { get; set; }
    public ArtistCategory Category { get; set; }

    [StringLength(255)]
    public string Address { get; set; }
    public decimal LocationLat { get; set; }
    public decimal LocationLng { get; set; }
    public bool IsVerified { get; set; } = false;
    public decimal? AverageRating { get; set; }
    public string? OperatingHours { get; set; } // Stored as JSON string

    // Navigation Properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    public virtual ICollection<ArtistService> ServicesOffered { get; set; } = new HashSet<ArtistService>();
    public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    public virtual ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
}
}