using System;
using System.Collections.Generic;
using System.Linq;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using TravelBuddy.DAL;
using TravelBuddy5.Interfaces;
using TravelBuddy5.Models;

namespace TravelBuddy5.Services
{
    public class GeoLocationService : IGeoLocationService
    {
        public IEnumerable<CoordinateDTO> GetRoute(double originLatitude, double originLongitude, double destinationLatitude,
            double destinationLongitude)
        {
            DirectionsRequest directionsRequest = new DirectionsRequest()
            {
                Origin = CoordinatesHelper.ToString(originLatitude, originLongitude),
                Destination = CoordinatesHelper.ToString(destinationLatitude, destinationLongitude),
                TravelMode = TravelMode.Walking
            };

            DirectionsResponse directions = GoogleMaps.Directions.Query(directionsRequest);
            IEnumerable<Step> steps = directions.Routes.First().Legs.First().Steps;
            return
                steps.SelectMany(step => step.PolyLine.Points)
                    .Select(step => new CoordinateDTO() {Latitude = step.Latitude, Longitude = step.Longitude});
        }
    }
}