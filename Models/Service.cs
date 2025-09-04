using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.Enums;

namespace CrispCut.Models
{
    public class Service
{
    [Key]
    public int ServiceId { get; set; }

    [Required]
    [StringLength(100)]
    public string ServiceName { get; set; }
    public ArtistCategory Category { get; set; }
    public string? Description { get; set; }
}
}