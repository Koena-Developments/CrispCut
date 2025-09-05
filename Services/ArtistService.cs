using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.Interfaces;
using CrispCut.Data;
using CrispCut.DTOs.ArtistServiceDTO;
using Microsoft.EntityFrameworkCore;
using CrispCut.Models;
using CrispCut.DTOs.AtristServiceDTO;

namespace CrispCut.Services

{ public class ArtistService : IArtistService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public ArtistService(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public Task<IEnumerable<ArtistMapPinDto>> GetArtistMapPinsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ArtistDto> OnboardArtistAsync(ArtistOnBoardingDto dto)
        {
            // Using a transaction to ensure both User and Artist are created successfully, or neither are.
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Check if a user with this email already exists to prevent duplicates
                if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
                {
                    throw new InvalidOperationException("A user with this email already exists.");
                }

                // Create and save the new User record
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
                await _context.SaveChangesAsync();

                // Create and save the new Artist record, linking it to the new User
                var newArtist = new Artist
                {
                    UserId = newUser.UserId,
                    Bio = dto.Bio,
                    Category = dto.Category,
                    Address = dto.Address,
                    LocationLat = dto.LocationLat,
                    LocationLng = dto.LocationLng,
                    OperatingHours = dto.OperatingHours,
                    IsVerified = false 
                };
                await _context.Artists.AddAsync(newArtist);
                await _context.SaveChangesAsync();
                
                // If both operations succeed, commit the transaction to the database
                await transaction.CommitAsync();

                // --- EMAIL TRIGGER ---
                // After a successful registration, this code is called.
                var emailSubject = "Your Trim Application is Under Review";
                var emailBody = $"<p>Hi {newUser.FirstName},</p>" +
                                "<p>Thank you for registering as an artist on Trim! Your profile has been received and is now pending verification.</p>" +
                                "<p>We will contact you as soon as your profile has been approved.</p>" +
                                "<p>Thank you,<br>The Trim Team</p>";
                await _emailService.SendEmailAsync(newUser.Email, emailSubject, emailBody);

                // Return the details of the newly created artist
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
                // If anything goes wrong, roll back the entire transaction
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<ArtistDto> RegisterArtistAsync(ArtistRegistrationDto dto)
        {
            // Implementation for an *existing* user who wants to become an artist
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

            // Also send an email in this flow
            var emailSubject = "Your Trim Application is Under Review";
            var emailBody = $"<p>Hi {user.FirstName},</p>" +
                            "<p>Thank you for registering as an artist on Trim! Your profile has been received and is now pending verification.</p>" +
                            "<p>We will contact you as soon as your profile has been approved.</p>" +
                            "<p>Thank you,<br>The Trim Team</p>";
            await _emailService.SendEmailAsync(user.Email, emailSubject, emailBody);
            
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