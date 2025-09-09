using CrispCut.Interfaces;

namespace CrispCut.Services
{
    public class BoltRideHailingService : IRideHailingService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<BoltRideHailingService> _logger;

        public BoltRideHailingService(IHttpClientFactory httpClientFactory, ILogger<BoltRideHailingService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        
        /// <summary>
        /// fare estimator and the final booker, as per many business API designs.
        /// </summary>
        public async Task<(decimal Fare, string RideId)> RequestRideAsync(decimal pickupLat, decimal pickupLng, decimal destinationLat, decimal destinationLng)
        {
            var httpClient = _httpClientFactory.CreateClient("BoltApiClient");

            var requestBody = new BoltRideRequestDto
            {
                Stops = new List<BoltStopDto>
                {
                    new BoltStopDto { Latitude = pickupLat, Longitude = pickupLng },
                    new BoltStopDto { Latitude = destinationLat, Longitude = destinationLng }
                }
            };
            try
            {
                var response = await httpClient.PostAsJsonAsync("orders", requestBody);
                if (response.IsSuccessStatusCode)
                {
                    var bookingResponse = await response.Content.ReadFromJsonAsync<BoltRideResponseDto>();
                    if (bookingResponse?.Price != null && !string.IsNullOrEmpty(bookingResponse.OrderId))
                    {
                        decimal fare = bookingResponse.Price.AmountInCents / 100.0m;
                        return (Fare: fare, RideId: bookingResponse.OrderId);
                    }
                }
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to book Bolt ride. Status: {StatusCode}, Response: {Response}", response.StatusCode, errorContent);
                return (0, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred while booking a Bolt ride.");
                return (0, null); 
            }
        }
    }
}



















// namespace CrispCut.Services
// {
//     public class MockRideHailingService : IRideHailingService
//     {
//         public Task<decimal?> GetRideFareEstimateAsync(RidePointDto pickup, RidePointDto destination)
//         {
//             // Simulate a successful API call and return a random fare between 45 and 120.
//             var random = new Random();
//             decimal randomFare = random.Next(4500, 12000) / 100.0m;
//             return Task.FromResult<decimal?>(randomFare);
//         }

//         public Task<BoltBookingResponse?> BookRideAsync(RidePointDto pickup, RidePointDto destination)
//         {
//             // Simulate a successful booking.
//             var fakeResponse = new BoltBookingResponse
//             {
//                 OrderId = Guid.NewGuid().ToString(), // Generate a fake, unique order ID
//                 Status = "confirmed"
//             };
//             return Task.FromResult<BoltBookingResponse?>(fakeResponse);
//         }
//     }
// }

