using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trim.DTOs.AtristServiceDTO
{
public class ArtistSummaryDto
{
    public int ArtistId { get; set; }
    public string FullName { get; set; }
    public string Category { get; set; }
    public decimal? AverageRating { get; set; }
    public decimal LocationLat { get; set; }
    public decimal LocationLng { get; set; }
    public bool IsVerified { get; set; }
}
}