using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CrispCut.DTOs.BookingsDTO;
using CrispCut.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrispCut.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Protect all endpoints in this controller
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("User ID not found in token.");
            }

            var userId = int.Parse(userIdString);

            var result = await _bookingService.CreateBookingAsync(userId, dto);

            if (result == null)
            {
                return BadRequest("Failed to create booking. Please check the service ID and appointment time.");
            }

            return Ok(result);
        }
    }
}