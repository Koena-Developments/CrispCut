using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.Interfaces;
using CrispCut.Data;
using CrispCut.DTOs.ArtistServiceDTO;
using CrispCut.DTOs.AtristServiceDTO;
using Microsoft.EntityFrameworkCore;
using CrispCut.Models;

namespace CrispCut.Services
{
     public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;

        public ArtistService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ArtistDto> OnboardArtistAsync(ArtistOnBoardingDto dto)
        {
            // Use a transaction to ensure both user and artist are created, or neither is.
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Step 1: Check if the email is already in use.
                if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                {
                    throw new InvalidOperationException("A user with this email already exists.");
                }

                // Step 2: Create the User entity.
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password, 12);
                var newUser = new User
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    PasswordHash = hashedPassword
                };
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync(); // Save to get the new UserId

                // Step 3: Create the Artist entity, linking it to the new user.
                var newArtist = new Artist
                {
                    UserId = newUser.UserId, // Link to the user we just created
                    Bio = dto.Bio,
                    Category = dto.Category,
                    Address = dto.Address,
                    LocationLat = dto.LocationLat,
                    LocationLng = dto.LocationLng,
                    OperatingHours = dto.OperatingHours,
                    IsVerified = false // Onboarding starts as unverified.
                };
                await _context.Artists.AddAsync(newArtist);
                await _context.SaveChangesAsync();

                // If everything was successful, commit the transaction.
                await transaction.CommitAsync();

                // Step 4: Return the combined data in a DTO.
                return new ArtistDto
                {
                    ArtistId = newArtist.ArtistId,
                    UserId = newUser.UserId,
                    FullName = $"{newUser.FirstName} {newUser.LastName}",
                    Bio = newArtist.Bio,
                    Category = newArtist.Category,
                    Address = newArtist.Address,
                    IsVerified = newArtist.IsVerified
                };
            }
            catch (Exception)
            {
                // If any step fails, roll back the entire transaction.
                await transaction.RollbackAsync();
                throw; // Re-throw the exception to be handled by the controller.
            }
        }


       

        public async Task<ArtistDto> RegisterArtistAsync(ArtistRegistrationDto dto)
        {
            // ... existing code for registering an artist profile for an existing user
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found. Cannot create an artist profile.");
            }
            var existingArtistProfile = await _context.Artists.FirstOrDefaultAsync(a => a.UserId == dto.UserId);
            if (existingArtistProfile != null)
            {
                throw new InvalidOperationException("This user already has an artist profile.");
            }
            var newArtist = new Artist
            {
                UserId = dto.UserId,
                Bio = dto.Bio,
                Category = dto.Category,
                Address = dto.Address,
                LocationLat = dto.LocationLat,
                LocationLng = dto.LocationLng,
                OperatingHours = dto.OperatingHours,
                IsVerified = false, 
                AverageRating = null 
            };
            await _context.Artists.AddAsync(newArtist);
            await _context.SaveChangesAsync();
            var artistDto = new ArtistDto
            {
                ArtistId = newArtist.ArtistId,
                UserId = user.UserId,
                FullName = $"{user.FirstName} {user.LastName}",
                Bio = newArtist.Bio,
                Category = newArtist.Category,
                Address = newArtist.Address,
                IsVerified = newArtist.IsVerified,
                AverageRating = newArtist.AverageRating
            };
            return artistDto;
        }

    }
}