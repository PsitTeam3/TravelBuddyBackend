using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
                Longitude = poi.Coordinates.Longitude.Value,
                Latitude = poi.Coordinates.Latitude.Value,
                VisitDuration = poi.VisitDuration
            };
        }



    }
}
