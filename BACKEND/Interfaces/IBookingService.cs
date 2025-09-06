using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrispCut.DTOs.BookingsDTO;

namespace CrispCut.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDetailsDto?> CreateBookingAsync(int userId, CreateBookingDto dto);
    }
}