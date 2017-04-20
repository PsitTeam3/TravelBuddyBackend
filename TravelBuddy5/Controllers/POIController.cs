using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TravelBuddy5.DAL;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.Models;
using TravelBuddy5.Services;

namespace TravelBuddy5.Controllers
{
    public class POIController : ApiController
    {

        private readonly IPOIRepo _repo;
        private readonly IUserTourRepo _userTourRepo;
        private readonly IUserPOIRepo _userPoiRepo;
        private readonly IGeoLocationService _geoLocationService;

        public POIController(IPOIRepo poiRepo, IUserTourRepo userTourRepo, IUserPOIRepo userPoiRepo, 
            IGeoLocationService geoLocationService)
        {
            _repo = poiRepo;
            _userTourRepo = userTourRepo;
            _userPoiRepo = userPoiRepo;
            _geoLocationService = geoLocationService;
        }

        [HttpGet]
        [Route("api/POI/GetPOIs")]
        public IQueryable<PointOfInterestDTO> GetPOIs()
        {
            return _repo.GetPOIs().Select(POIMapper.CreatePOIDTO());
        }

        [HttpGet]
        [Route("api/POI/GetPOIsByTour")]
        public IQueryable<PointOfInterestDTO> GetPOIsByTour(int id)
        {
            return _repo.GetPOIsByTour(id).Select(POIMapper.CreatePOIDTO());
        }

        [HttpGet]
        [Route("api/POI/GetDistanceToPOI")]
        public double GetDistanceToPOI(int poiID, double latitude, double longitude)
        {
            return _repo.GetPOIDistance(poiID, latitude, longitude);
        }

        [HttpGet]
        [Route("api/POI/GetDistanceToNextPOI")]
        public double GetDistanceToNextPOI(int userID, double latitude, double longitude)
        {
            POI nextPoi = GetNextPOI(userID);
            return _repo.GetPOIDistance(nextPoi.Id, latitude, longitude);
        }

        [HttpGet]
        [Route("api/POI/IsPOIInRange")]
        public bool IsPOIInRange(int poiID, double longitude, double latitude, float allowedDistance = 3)
        {
            return GetDistanceToPOI(poiID, longitude, latitude) <= allowedDistance;
        }

        [HttpGet]
        [Route("api/POI/IsNextPOIInRange")]
        public bool IsNextPOIInRange(int userID, double longitude, double latitude, float allowedDistance = 3)
        {
            return GetDistanceToNextPOI(userID, longitude, latitude) <= allowedDistance;
        }

        [HttpGet]
        [Route("api/POI/GetRouteToNextPOI")]
        public HttpResponseMessage GetRouteToNextPOI(int userId, double currentLatitude, double currentLongitude)
        {
            try
            {
                var poi = GetNextPOI(userId);
                IEnumerable<CoordinateDTO> route = _geoLocationService.GetRoute(currentLatitude, currentLongitude, poi.Coordinates.Latitude.Value,
                    poi.Coordinates.Longitude.Value);
                return Request.CreateResponse(HttpStatusCode.OK, route);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, err);
            }
        }

        private POI GetNextPOI(int userId)
        {
            var userTour = _userTourRepo.GetActiveTour(userId).Value.First();
            var poi = _userPoiRepo.GetNextPOI(userTour).Value.First();
            return poi;
        }
    }
}
