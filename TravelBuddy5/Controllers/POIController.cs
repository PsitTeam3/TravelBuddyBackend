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

        public POIController(IPOIRepo poiRepo, IUserTourRepo userTourRepo, IUserPOIRepo userPOIRepo,
            IGeoLocationService geoLocationService)
        {
            _poiRepo = poiRepo;
            _userTourRepo = userTourRepo;
            _userPOIRepo = userPOIRepo;
            _geoLocationService = geoLocationService;
        }

        [HttpGet]
        [Route("api/POI/GetPOIs")]
        public IEnumerable<PointOfInterestDTO> GetPOIs()
        {
            return _poiRepo.GetPOIs().Select(PointOfInterestDTO.Create());
        }

        [HttpGet]
        [Route("api/POI/GetPOIsByTour")]
        public IEnumerable<PointOfInterestDTO> GetPOIsByTour(int tourID)
        {
            return _poiRepo.GetPOIsByTour(tourID).Select(PointOfInterestDTO.Create());
        }

        [HttpPost]
        [Route("api/POI/SetNextPOIAsVisited")]
        public void SetNextPOIAsVisited(int userID)
        {
            var userTour = GetActiveTour(userID);
            var nextPoi = GetNextPOIInternal(userTour);
            _userPOIRepo.SetPOIAsVisited(nextPoi.Id, userTour.Id);
        }

        [HttpGet]
        [Route("api/POI/GetNextPOI")]
        public PointOfInterestDTO GetNextPOI(int userID)
        {
            var nextPOI = GetNextPOIInternal(userID);
            return PointOfInterestDTO.Create().Compile()(nextPOI);
        }

        [HttpGet]
        [Route("api/POI/GetDistanceToNextPOI")]
        public double GetDistanceToNextPOI(int userID, double latitude, double longitude)
        {
            POI nextPoi = GetNextPOIInternal(userID);
            return nextPoi.Coordinates.Distance(CoordinatesHelper.CreatePoint(latitude, longitude)).Value;
        }

        [HttpGet]
        [Route("api/POI/IsNextPOIInRange")]
        public bool IsNextPOIInRange(int userID, double longitude, double latitude, float allowedDistance = 3)
        {
            return GetDistanceToNextPOI(userID, latitude, longitude) <= allowedDistance;
        }

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
