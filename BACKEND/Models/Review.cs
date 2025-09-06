using System.ComponentModel.DataAnnotations;


namespace CrispCut.Models
{
    public class Review
{
    [Key]
    public int ReviewId { get; set; }
    public int BookingId { get; set; } // FK
    public int UserId { get; set; } // FK
    public int ArtistId { get; set; } // FK

    [Range(1, 5)]
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual Booking Booking { get; set; }
    public virtual User User { get; set; }
    public virtual Artist Artist { get; set; }
}
}