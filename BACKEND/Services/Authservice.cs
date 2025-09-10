using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.Interfaces;
using Microsoft.EntityFrameworkCore;
using CrispCut.Data;
using CrispCut.Models;
using CrispCut.DTOs.UserDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using CrispCut.DTOs.ArtistServiceDTO;
using CrispCut.DTOs.AtristServiceDTO;

namespace CrispCut.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> RegisterUserAsync(UserForRegistrationDto dto)
        {
            // Normalize and check the email for existence to prevent duplicates
            var emailExists = await _context.Users.AnyAsync(u => u.Email.ToLower().Trim() == dto.Email.ToLower().Trim());
            if (emailExists)
            {
                return new AuthResponseDto { Success = false, Message = "Email already in use." };
            }

            var newUser = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role // Now uses the role from the DTO
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(newUser, false);
            var userDto = new UserDto
            {
                UserId = newUser.UserId,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                Role = newUser.Role
            };

            return new AuthResponseDto { Success = true, Message = "Registration successful.", Token = token, User = userDto, IsArtist = false };
        }

        public async Task<AuthResponseDto> OnboardArtistAsync(UserForRegistrationDto dto, ArtistOnBoardingDto artistDto)
        {
            // Normalize and check the email for existence to prevent duplicates
            var emailExists = await _context.Users.AnyAsync(u => u.Email.ToLower().Trim() == dto.Email.ToLower().Trim());
            if (emailExists)
            {
                return new AuthResponseDto { Success = false, Message = "Email already in use." };
            }
            Console.WriteLine(artistDto.Role);

            var newUser = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = artistDto.Role 
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var newArtist = new Artist
            {
                UserId = newUser.UserId,
                Address = artistDto.Address,
                Bio = artistDto.Bio,
                Category = artistDto.Category,
                Certificate = artistDto.Certificate,
                LocationLat = artistDto.LocationLat,
                LocationLng = artistDto.LocationLng,
                OperatingHours = artistDto.OperatingHours,
                IsVerified = false
            };
            
            _context.Artists.Add(newArtist);
            await _context.SaveChangesAsync();
            
            var token = GenerateJwtToken(newUser, true);
            var userDto = new UserDto
            {
                UserId = newUser.UserId,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                Role = newUser.Role
            };

            return new AuthResponseDto { Success = true, Message = "Artist onboarding successful.", Token = token, User = userDto, IsArtist = true };
        }

        public async Task<AuthResponseDto> LoginAsync(UserForLoginDto dto)
        {
            // Normalize and find the user for login
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Trim() == dto.Email.ToLower().Trim());

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return new AuthResponseDto { Success = false, Message = "Invalid email or password." };
            }
            
            var isArtist = user.Role == "Artist";
            
            var token = GenerateJwtToken(user, isArtist);
            var userDto = new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role
            };
            
            return new AuthResponseDto { Success = true, Message = "Login successful.", Token = token, User = userDto, IsArtist = isArtist };
        }
        
        public async Task<CurrentUserDto?> GetCurrentUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return null;
            }

            bool isArtist = user.Role == "Artist";

            return new CurrentUserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsArtist = isArtist
            };
        }

        private string GenerateJwtToken(User user, bool isArtist)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            if (isArtist)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Artist"));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
