using System.Text.Json.Serialization;

namespace CrispCut.Interfaces
{
  public class BoltRideRequestDto
    {
        [JsonPropertyName("stops")]
        public List<BoltStopDto> Stops { get; set; }

        [JsonPropertyName("scheduled_time")]
        public long? ScheduledTimeUnix { get; set; } 
    }

    public class BoltStopDto
    {
        [JsonPropertyName("lat")]
        public decimal Latitude { get; set; }

        [JsonPropertyName("lng")]
        public decimal Longitude { get; set; }
    }

    public class BoltRideResponseDto
    {
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("price")]
        public BoltPriceDto Price { get; set; }
    }

    public class BoltPriceDto
    {
        // Price is given in cents, so we use an integer
        [JsonPropertyName("amount")]
        public int AmountInCents { get; set; }

        [JsonPropertyName("currency_code")]
        public string CurrencyCode { get; set; }
    }
}