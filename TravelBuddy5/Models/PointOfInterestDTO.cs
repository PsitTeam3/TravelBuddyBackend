using System;
using System.Linq.Expressions;
using TravelBuddy5.DAL;

namespace TravelBuddy5.Models
{
    public class PointOfInterestDTO
    {
        public int Id { get; set; } 
        public string Description { get; set; }
        public int VisitDuration { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

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