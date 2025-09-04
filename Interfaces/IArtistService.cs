using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.DTOs.ArtistServiceDTO;

namespace CrispCut.Interfaces
{
    public interface IArtistService
    {
        Task<ArtistDto> RegisterArtistAsync(ArtistRegistrationDto artistRegistrationDto);
        Task<ArtistDto> OnboardArtistAsync(ArtistOnBoardingDto onboardingDto);
    }
}