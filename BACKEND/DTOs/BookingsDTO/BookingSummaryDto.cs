using System;
using System.ComponentModel.DataAnnotations;
using CrispCut.Enums;



namespace CrispCut.DTOs.BookingsDTO{
public class BookingSummaryDto
    {
        public int BookingId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        
        // Will contain the details of the OTHER party in the booking
        public string ArtistName { get; set; }
        public string UserName { get; set; }
        public string ServiceName { get; set; }
    }
}