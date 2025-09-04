using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.DTOs.ArtistServiceDTO;


namespace CrispCut.DTOs.ArtistServiceDTO
{
    public class ArtistDetailsDto : ArtistSummaryDto 
{
    public string Bio { get; set; }
    public string Address { get; set; }
    public string OperatingHours { get; set; }
    public List<ArtistServiceDto> Services { get; set; } = new();
}
}