using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CrispCut.Interfaces;

namespace CrispCut.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authservice;


        public AuthController(IAuthService authservice)
        {
            _authservice = authservice;
        }



        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = await _authservice.RegisterAsync(userForRegistrationDto);
            return CreatedAtAction(nameof(Register), new { userId = createdUser.UserId }, createdUser);
        }
    
    }
}