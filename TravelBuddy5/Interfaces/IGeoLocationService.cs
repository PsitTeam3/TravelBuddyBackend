using System.Collections.Generic;
using TravelBuddy5.Models;

namespace TravelBuddy5.Interfaces
{
    public interface IGeoLocationService
    {
        IEnumerable<CoordinateDTO> GetRoute(double originLatitude, double originLongitude, double destinationLatitude,
            double destinationLongitude);
    }
}