using System.Security.Claims;
using CrispCut.DTOs.ArtistServiceDTO;
using CrispCut.DTOs.AtristServiceDTO;
using CrispCut.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrispCut.Controller                                                                                                                                                                                                                                                                                               
{
   [ApiController]
    [Route("api/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpPost("register")]
        [Authorize]
        public async Task<IActionResult> RegisterExistingUserAsArtist([FromBody] ArtistRegistrationDto dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = int.Parse(userIdString);
            var createdArtist = await _artistService.RegisterExistingUserAsArtistAsync(userId, dto);

            if (createdArtist == null)
            {
                return BadRequest("Failed to register artist. The user may already be an artist.");
            }

            return Ok(createdArtist);
        }

        [HttpPost("onboard")]
        public async Task<IActionResult> OnboardArtist([FromBody] ArtistOnBoardingDto dto)
        {
            var createdArtist = await _artistService.OnboardArtistAsync(dto);
            if (createdArtist == null)
            {
                return BadRequest("Failed to onboard artist. Email may already be in use.");
            }
            return Ok(createdArtist);
        }
        
        [HttpGet("map-pins")]
        public async Task<IActionResult> GetMapPins()
        {
            var pins = await _artistService.GetArtistMapPinsAsync();
            return Ok(pins);
        }

        [HttpPost("services")]
        [Authorize]
        public async Task<IActionResult> AddServiceToProfile([FromBody] AddArtistServiceDto dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = int.Parse(userIdString);
            var newService = await _artistService.AddServiceToProfileAsync(userId, dto);

            if (newService == null)
            {
                return BadRequest("Failed to add service. Ensure you are a registered artist and the service does not already exist for your profile.");
            }

            return Ok(newService);
        }
    }
}