using System.Collections.Generic;

namespace TravelBuddy5.Models
{
    public class RouteToPointOfInterestDTO
    {
        public PointOfInterestDTO NextPOI { get; set; }
        public IEnumerable<CoordinateDTO> RouteToNextPOI { get; set; }
    }
}