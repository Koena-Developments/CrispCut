using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.Data;
using CrispCut.DTOs.ArtistServiceDTO;
using CrispCut.DTOs.BookingsDTO;
using CrispCut.DTOs.UserDTO;
using CrispCut.Enums;
using CrispCut.Interfaces;
using CrispCut.Models;
using Microsoft.EntityFrameworkCore;

namespace CrispCut.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookingDetailsDto> CreateBookingAsync(int userId, CreateBookingDto dto)
        {
            // 1. Find the service being booked and include related data
            var artistService = await _context.ArtistServices
                .Include(s => s.Artist)
                    .ThenInclude(a => a.User)
                .Include(s => s.Service)
                .FirstOrDefaultAsync(s => s.ArtistServiceId == dto.ArtistServiceId);

            if (artistService == null)
            {
                // The requested service doesn't exist
                return null!;
            }

            // 2. Validation Checks
            if (artistService.Artist.UserId == userId)
            {
                // Prevent artists from booking their own services
                return null!;
            }

            if (dto.AppointmentTime <= DateTime.UtcNow)
            {
                // Booking must be in the future
                return null!;
            }

            // (Future Enhancement: Add logic here to check for artist availability and prevent double-booking)

            // 3. Create the Booking Entity
            var newBooking = new Booking
            {
                UserId = userId,
                ArtistId = artistService.ArtistId,
                ArtistServiceId = dto.ArtistServiceId,
                AppointmentTime = dto.AppointmentTime,
                Status = BookingStatus.Confirmed, // Default to confirmed for simplicity
                ServicePrice = artistService.Price,
                RideFare = 0, // Placeholder for ride-hailing integration
                TotalPrice = artistService.Price // Placeholder
            };

            // 4. Save to Database
            _context.Bookings.Add(newBooking);
            await _context.SaveChangesAsync();

            // 5. Map to DTO and Return
            // We need to fetch the customer's user details for the DTO
            var customer = await _context.Users.FindAsync(userId);
            if(customer == null) return null!; // Should never happen if JWT is valid

            return new BookingDetailsDto
            {
                BookingId = newBooking.BookingId,
                AppointmentTime = newBooking.AppointmentTime,
                Status = newBooking.Status.ToString(),
                ServicePrice = newBooking.ServicePrice,
                RideFare = newBooking.RideFare,
                TotalPrice = newBooking.TotalPrice,
                CreatedAt = newBooking.CreatedAt,
                Artist = new ArtistDto
                {
                    ArtistId = artistService.Artist.ArtistId,
                    UserId = artistService.Artist.UserId,
                    Bio = artistService.Artist.Bio,
                    Category = artistService.Artist.Category,
                    Address = artistService.Artist.Address,
                    LocationLat = artistService.Artist.LocationLat,
                    LocationLng = artistService.Artist.LocationLng,
                    IsVerified = artistService.Artist.IsVerified,
                    OperatingHours = artistService.Artist.OperatingHours,
                    AverageRating = artistService.Artist.AverageRating
                },
                Service = new ArtistServiceDto // Assuming you have or will create this DTO
                {
                    ArtistServiceId = artistService.ArtistServiceId,
                    ServiceName = artistService.Service.ServiceName,
                    Price = artistService.Price,
                    DurationMinutes = artistService.DurationMinutes
                },
                User = new UserDto
                {
                    UserId = customer.UserId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber
                }
            };
        }

    }
}