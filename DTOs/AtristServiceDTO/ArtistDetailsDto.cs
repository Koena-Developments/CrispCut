using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trim.DTOs.ArtistServiceDTO;


namespace Trim.DTOs.AtristServiceDTO
{
    public class ArtistDetailsDto : ArtistSummaryDto 
{
    public string Bio { get; set; }
    public string Address { get; set; }
    public string OperatingHours { get; set; }
    public List<ArtistServiceDto> Services { get; set; } = new();
}
}