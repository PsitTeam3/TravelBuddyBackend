using System;
using System.Linq.Expressions;
using TravelBuddy5.DAL;

namespace TravelBuddy5.Models
{
    public static class POIMapper
    {

        public static Expression<Func<POI, PointOfInterestDTO>> CreatePOIDTO()
        {
            return poi => new PointOfInterestDTO
            {
                Id = poi.Id,
                Description = poi.Description,
                Longitude = poi.Coordinates.Longitude.Value,
                Latitude = poi.Coordinates.Latitude.Value,
                VisitDuration = poi.VisitDuration
            };
        }
    }
}
