using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.DTOs.ArtistServiceDTO;
using CrispCut.DTOs.AtristServiceDTO;

namespace CrispCut.Interfaces
{
       public interface IArtistService
    {
        Task<ArtistDto?> RegisterExistingUserAsArtistAsync(int userId, ArtistRegistrationDto dto);
        Task<ArtistDto?> OnboardArtistAsync(ArtistOnBoardingDto dto);
        Task<IEnumerable<ArtistMapPinDto>> GetArtistMapPinsAsync();
        Task<ArtistServiceDto?> AddServiceToProfileAsync(int userId, AddArtistServiceDto dto);
    }
}