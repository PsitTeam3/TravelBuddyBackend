using System.Collections.Generic;

namespace TravelBuddy5.Models
{
    public class RouteToPointOfInterestDTO
    {
        /// <summary>
        /// Gets or sets the next poi.
        /// </summary>
        /// <value>
        /// The next poi.
        /// </value>
        public PointOfInterestDTO NextPOI { get; set; }
       
        /// <summary>
        /// Gets or sets the route to next poi.
        /// </summary>
        /// <value>
        /// The route to next poi.
        /// </value>
        public IEnumerable<CoordinateDTO> RouteToNextPOI { get; set; }
    }
}