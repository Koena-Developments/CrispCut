using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrispCut.Interfaces;
using CrispCut.DTOs.UserDTO;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CrispCut.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegistrationDto dto)
        {
            var result = await _authService.RegisterUserAsync(dto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.User);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
            {
                return Unauthorized(result.Message);
            }

            return Ok(new { result.Token, result.User });
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                return Unauthorized("User ID not found or invalid in token.");
            }

            var currentUser = await _authService.GetCurrentUserAsync(userId);
            if (currentUser == null)
            {
                return NotFound("User associated with this token not found.");
            }

            // For testing purposes, extract the token from the header to return it.
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // Construct the final response object as requested
            var response = new
            {
                Token = token,
                currentUser.UserId,
                currentUser.IsArtist,
                UserDetails = new 
                {
                    currentUser.FirstName,
                    currentUser.LastName,
                    currentUser.Email,
                    currentUser.PhoneNumber
                }
            };

            return Ok(response);
        }
    }
}