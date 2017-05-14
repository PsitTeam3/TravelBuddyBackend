using System.Collections.Generic;
using TravelBuddy5.Models;

namespace TravelBuddy5.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGeoLocationService
    {
        /// <summary>
        /// Gets the route from one point to another by latitude and longitude.
        /// </summary>
        /// <param name="originLatitude">The origin latitude.</param>
        /// <param name="originLongitude">The origin longitude.</param>
        /// <param name="destinationLatitude">The destination latitude.</param>
        /// <param name="destinationLongitude">The destination longitude.</param>
        /// <returns></returns>
        IEnumerable<CoordinateDTO> GetRoute(double originLatitude, double originLongitude, double destinationLatitude,
            double destinationLongitude);
    }
}