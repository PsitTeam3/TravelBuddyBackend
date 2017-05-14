using System;
using System.Linq.Expressions;
using TravelBuddy5.DAL;

namespace TravelBuddy5.Models
{
    public class PointOfInterestDTO
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the duration of the visit.
        /// </summary>
        /// <value>
        /// The duration of the visit.
        /// </value>
        public int VisitDuration { get; set; }
        
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double Longitude { get; set; }
        
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double Latitude { get; set; }

        /// <summary>
        /// Creates the mapping lambda expression.
        /// </summary>
        /// <returns>Lambda expression for the mapping</returns>
        public static Expression<Func<POI, PointOfInterestDTO>> Create()
        {
            return poi => new PointOfInterestDTO
            {
                Id = poi.Id,
                Description = poi.Description,
                VisitDuration = poi.VisitDuration,
                Longitude = poi.Coordinates.Longitude.Value,
                Latitude = poi.Coordinates.Latitude.Value
            };
        }
    }
}