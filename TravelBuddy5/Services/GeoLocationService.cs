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
        /// <summary>
        /// Gets the route from one point to another by latitude and longitude.
        /// </summary>
        /// <param name="originLatitude">The origin latitude.</param>
        /// <param name="originLongitude">The origin longitude.</param>
        /// <param name="destinationLatitude">The destination latitude.</param>
        /// <param name="destinationLongitude">The destination longitude.</param>
        /// <returns></returns>
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