using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trim.Models
{
    public class ArtistService
{
    [Key]
    public int ArtistServiceId { get; set; }
    public int ArtistId { get; set; } // FK
    public int ServiceId { get; set; } // FK

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; }

    // Navigation Properties
    public virtual Artist Artist { get; set; }
    public virtual Service Service { get; set; }
}

}