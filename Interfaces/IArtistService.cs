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
        Task<ArtistDto> RegisterArtistAsync(ArtistRegistrationDto artistRegistrationDto);
        Task<ArtistDto> OnboardArtistAsync(ArtistOnBoardingDto onboardingDto);
        Task<IEnumerable<ArtistMapPinDto>> GetArtistMapPinsAsync();
    }
}