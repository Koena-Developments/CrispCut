using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.DTOs.ArtistServiceDTO;
using CrispCut.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrispCut.Controller
{
   public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        // Endpoint for Registering as a Artist
        [HttpPost("onboard")]
        public async Task<IActionResult> OnboardArtist([FromBody] ArtistOnBoardingDto onboardingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdArtist = await _artistService.OnboardArtistAsync(onboardingDto);
                return CreatedAtAction(nameof(OnboardArtist), new { artistId = createdArtist.ArtistId }, createdArtist);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        // This endpoint remains for an existing user who wants to become an artist.
        [HttpPost("register")]
        public async Task<IActionResult> RegisterArtist([FromBody] ArtistRegistrationDto artistRegistrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdArtist = await _artistService.RegisterArtistAsync(artistRegistrationDto);
                return CreatedAtAction(nameof(RegisterArtist), new { artistId = createdArtist.ArtistId }, createdArtist);
            }
            catch (InvalidOperationException ex)
            {
                // Catches errors like "User not found" or "Artist profile already exists".
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // General catch-all for other server errors.
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
    }
}