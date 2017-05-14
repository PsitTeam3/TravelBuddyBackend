using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TravelBuddy.DAL;
using TravelBuddy5.DAL;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.Interfaces;
using TravelBuddy5.Models;

namespace TravelBuddy5.Controllers
{
    public class POIController : ApiController
    {
        private readonly IPOIRepo _poiRepo;
        private readonly IUserTourRepo _userTourRepo;
        private readonly IUserPOIRepo _userPOIRepo;
        private readonly IGeoLocationService _geoLocationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="POIController"/> class.
        /// </summary>
        /// <param name="poiRepo">The poi repo.</param>
        /// <param name="userTourRepo">The user tour repo.</param>
        /// <param name="userPOIRepo">The user poi repo.</param>
        /// <param name="geoLocationService">The geo location service.</param>
        public POIController(IPOIRepo poiRepo, IUserTourRepo userTourRepo, IUserPOIRepo userPOIRepo,
            IGeoLocationService geoLocationService)
        {
            _poiRepo = poiRepo;
            _userTourRepo = userTourRepo;
            _userPOIRepo = userPOIRepo;
            _geoLocationService = geoLocationService;
        }

        /// <summary>
        /// Gets all POIs.
        /// </summary>
        /// <returns>Enumerable of all POIs</returns>
        [HttpGet]
        [Route("api/POI/GetPOIs")]
        public IEnumerable<PointOfInterestDTO> GetPOIs()
        {
            return _poiRepo.GetPOIs().Select(PointOfInterestDTO.Create());
        }

        /// <summary>
        /// Gets the POI by tour.
        /// </summary>
        /// <param name="tourID">The tour identifier.</param>
        /// <returns>Enumerable of all POIs for the given tour</returns>
        [HttpGet]
        [Route("api/POI/GetPOIsByTour")]
        public IEnumerable<PointOfInterestDTO> GetPOIsByTour(int tourID)
        {
            return _poiRepo.GetPOIsByTour(tourID).Select(PointOfInterestDTO.Create());
        }

        /// <summary>
        /// Sets the next POI as visited.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        [HttpPost]
        [Route("api/POI/SetNextPOIAsVisited")]
        public void SetNextPOIAsVisited(int userID)
        {
            var userTour = GetActiveTour(userID);
            var nextPoi = GetNextPOIInternal(userTour);
            _userPOIRepo.SetPOIAsVisited(nextPoi.Id, userTour.Id);
        }

        /// <summary>
        /// Gets the next POI.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>The next POI object</returns>
        [HttpGet]
        [Route("api/POI/GetNextPOI")]
        public PointOfInterestDTO GetNextPOI(int userID)
        {
            var nextPOI = GetNextPOIInternal(userID);
            return PointOfInterestDTO.Create().Compile()(nextPOI);
        }

        /// <summary>
        /// Calculates the distance to next POI in the current started tour for the given user.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns>Distance to the next POI in the current started tour for the given user</returns>
        [HttpGet]
        [Route("api/POI/GetDistanceToNextPOI")]
        public double GetDistanceToNextPOI(int userID, double latitude, double longitude)
        {
            POI nextPoi = GetNextPOIInternal(userID);
            return nextPoi.Coordinates.Distance(CoordinatesHelper.CreatePoint(latitude, longitude)).Value;
        }

        /// <summary>
        /// Determines whether the next POI is in Range.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="longitude">The longitude.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="allowedDistance">The allowed distance (optional).</param>
        /// <returns>
        ///   <c>true</c> if next POI is in range; otherwise, <c>false</c>.
        /// </returns>
        [HttpGet]
        [Route("api/POI/IsNextPOIInRange")]
        public bool IsNextPOIInRange(int userID, double longitude, double latitude, float allowedDistance = 3)
        {
            return GetDistanceToNextPOI(userID, latitude, longitude) <= allowedDistance;
        }

        /// <summary>
        /// Gets the route to next poi.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="currentLatitude">The current latitude.</param>
        /// <param name="currentLongitude">The current longitude.</param>
        /// <returns>Enumerable of all waypoint coordinates to the next POI using the google maps API</returns>
        [HttpGet]
        [Route("api/POI/GetRouteToNextPOI")]
        public IEnumerable<CoordinateDTO> GetRouteToNextPOI(int userID, double currentLatitude, double currentLongitude)
        {
            var poi = GetNextPOIInternal(userID);
            IEnumerable<CoordinateDTO> route = _geoLocationService.GetRoute(currentLatitude, currentLongitude,
                poi.Coordinates.Latitude.Value,
                poi.Coordinates.Longitude.Value);
            return route;
        }

        private POI GetNextPOIInternal(int userID)
        {
            var userTour = GetActiveTour(userID);
            return GetNextPOIInternal(userTour);
        }

        private UserTour GetActiveTour(int userID)
        {
            UserTour userTour = _userTourRepo.GetActiveTour(userID);
            if (userTour == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("User doesn't have a started tour")
                };
                throw new HttpResponseException(resp);
            }
            return userTour;
        }

        private POI GetNextPOIInternal(UserTour userTour)
        {
            POI nextPOI = _userPOIRepo.GetNextPOI(userTour);
            if (nextPOI == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("Tour doesn't have remaining POIs")
                };
                throw new HttpResponseException(resp);
            }
            return nextPOI;
        }
    }
}
