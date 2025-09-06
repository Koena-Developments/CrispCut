using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrispCut.Interfaces
{
  public interface IRideHailingService
    {
        Task<(decimal Fare, string RideId)> RequestRideAsync(decimal pickupLat, decimal pickupLng, decimal dropoffLat, decimal dropoffLng);
    }
}