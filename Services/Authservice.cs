using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.Interfaces;
using Microsoft.EntityFrameworkCore;
using CrispCut;
using CrispCut.Data;
using CrispCut.DTOs.UserDto;
using BCrypt.Net;
using CrispCut.Models;

namespace CrispCut.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

            public Task<UserDto> LoginAsynce(UserForLoginDto userForLogin)
            {
                throw new NotImplementedException();
            }



        // REGISTRATION SERVICE
        public async Task<UserDto> RegisterAsync(UserForRegistrationDto userForRegistrationDto)
        {
            // 1. Check if a user with the same email already exists to prevent duplicates.
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userForRegistrationDto.Email);
            if (existingUser != null)
            {
                // Throw a specific exception that the controller can catch and handle.
                throw new InvalidOperationException("User with this email already exists.");
            }

            // 2. Hash the user's password using BCrypt. Never store plain-text passwords.
            // The "work factor" (12) determines the complexity of the hash.
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userForRegistrationDto.Password, 12);

            // 3. Create a new User domain model from the incoming DTO.
            var newUser = new User
            {
                FirstName = userForRegistrationDto.FirstName,
                LastName = userForRegistrationDto.LastName,
                Email = userForRegistrationDto.Email,
                PhoneNumber = userForRegistrationDto.PhoneNumber,
                PasswordHash = hashedPassword
                // The CreatedAt property is set by default in the User model.
            };

            // 4. Add the new user to the database context and save the changes.
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            // 5. Map the newly created user to a UserDto to safely return its public information.
            // This prevents sensitive data like the password hash from being sent back to the client.
            var userDto = new UserDto
            {
                UserId = newUser.UserId,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email
            };

            return userDto;
        }

    }
}