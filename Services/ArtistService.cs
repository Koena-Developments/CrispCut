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

{
        public class ArtistService : IArtistService
        {
            private readonly ApplicationDbContext _context;
            private readonly IEmailService _emailService;

            public ArtistService(ApplicationDbContext context, IEmailService emailService)
            {
                _context = context;
                _emailService = emailService;
            }

            public async Task<ArtistDto?> RegisterExistingUserAsArtistAsync(int userId, ArtistRegistrationDto dto)
            {
                var userIsAlreadyArtist = await _context.Artists.AnyAsync(a => a.UserId == userId);
                if (userIsAlreadyArtist)
                {
                    return null;
                }

                var newArtist = new Artist
                {
                    UserId = userId,
                    Bio = dto.Bio,
                    Category = dto.Category,
                    Address = dto.Address,
                    LocationLat = dto.LocationLat,
                    LocationLng = dto.LocationLng,
                    IsVerified = false,
                    OperatingHours = dto.OperatingHours
                };

                _context.Artists.Add(newArtist);
                await _context.SaveChangesAsync();

                return new ArtistDto
                {
                    ArtistId = newArtist.ArtistId,
                    UserId = newArtist.UserId,
                    Bio = newArtist.Bio,
                    Category = newArtist.Category,
                    Address = newArtist.Address,
                    LocationLat = newArtist.LocationLat,
                    LocationLng = newArtist.LocationLng,
                    IsVerified = newArtist.IsVerified,
                    OperatingHours = newArtist.OperatingHours,
                    AverageRating = null
                };
            }

            public async Task<ArtistDto?> OnboardArtistAsync(ArtistOnBoardingDto dto)
            {
                var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
                if (emailExists)
                {
                    return null;
                }

                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var newUser = new User
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.Email,
                        PhoneNumber = dto.PhoneNumber,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
                    };
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    var newArtist = new Artist
                    {
                        UserId = newUser.UserId,
                        Bio = dto.Bio,
                        Category = dto.Category,
                        Address = dto.Address,
                        LocationLat = dto.LocationLat,
                        LocationLng = dto.LocationLng,
                        IsVerified = false,
                        OperatingHours = dto.OperatingHours
                    };
                    _context.Artists.Add(newArtist);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    var emailSubject = "Your Application is Under Review";
                    var emailBody = $"<p>Hi {newUser.FirstName},</p><p>Thank you for registering. Your artist profile is now under review. We will contact you once it's approved.</p>";
                    await _emailService.SendEmailAsync(newUser.Email, emailSubject, emailBody);

                    return new ArtistDto
                    {
                        ArtistId = newArtist.ArtistId,
                        UserId = newUser.UserId,
                        Bio = newArtist.Bio,
                        Category = newArtist.Category,
                        Address = newArtist.Address,
                        LocationLat = newArtist.LocationLat,
                        LocationLng = newArtist.LocationLng,
                        IsVerified = newArtist.IsVerified,
                        OperatingHours = newArtist.OperatingHours,
                        AverageRating = null
                    };
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return null;
                }
            }

            public async Task<IEnumerable<ArtistMapPinDto>> GetArtistMapPinsAsync()
            {
                return await _context.Artists
                    .Where(a => a.IsVerified)
                    .Include(a => a.User)
                    .Select(a => new ArtistMapPinDto
                    {
                        ArtistId = a.ArtistId,
                        FullName = $"{a.User.FirstName} {a.User.LastName}",
                        Category = a.Category,
                        LocationLat = a.LocationLat,
                        LocationLng = a.LocationLng
                    })
                    .ToListAsync();
            }

            public async Task<ArtistServiceDto?> AddServiceToProfileAsync(int userId, AddArtistServiceDto dto)
            {
                var artist = await _context.Artists.FirstOrDefaultAsync(a => a.UserId == userId);

                // --- MODIFIED: Removed artist verification check ---
                if (artist == null || artist.Category != dto.Category)
                {
                    return null;
                }

                var service = await _context.Services.FirstOrDefaultAsync(s =>
                    s.ServiceName.ToLower() == dto.ServiceName.ToLower() && s.Category == dto.Category);

                if (service == null)
                {
                    service = new Service
                    {
                        ServiceName = dto.ServiceName,
                        Category = dto.Category,
                        Description = dto.Description
                    };
                    _context.Services.Add(service);
                    await _context.SaveChangesAsync();
                }

                var alreadyExists = await _context.ArtistServices.AnyAsync(a => a.ArtistId == artist.ArtistId && a.ServiceId == service.ServiceId);
                if (alreadyExists)
                {
                    return null;
                }

                var newArtistService = new CrispCut.Models.ArtistService
                {
                    ArtistId = artist.ArtistId,
                    ServiceId = service.ServiceId,
                    Price = dto.Price,
                    DurationMinutes = dto.DurationMinutes
                };

                _context.ArtistServices.Add(newArtistService);
                await _context.SaveChangesAsync();

                return new ArtistServiceDto
                {
                    ArtistServiceId = newArtistService.ArtistServiceId,
                    ServiceName = service.ServiceName,
                    Category = service.Category,
                    Price = newArtistService.Price,
                    DurationMinutes = newArtistService.DurationMinutes,
                    Description = service.Description
                };
            }
        }

    }