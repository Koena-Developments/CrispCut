using System.Text.Json.Serialization;

namespace CrispCut.Enums
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum ArtistCategory
{
    Barber = 1,
    MakeupArtist,
    TattooArtist,
    Hairstylist
}
}