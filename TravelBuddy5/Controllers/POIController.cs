using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
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
using TravelBuddy5.DAL.Repositories;
using TravelBuddy5.Models;

namespace TravelBuddy5.Controllers
{
    public class POIController : ApiController
    {

        private readonly IPOIRepo _repo;

        public POIController(IPOIRepo poiRepo)
        {
            _repo = poiRepo;
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
        public double GetDistanceToPOI(int poiID, double longitude, double latitude)
        {
            return _repo.GetPOIDistance(poiID, longitude, latitude);
        }

        [HttpGet]
        [Route("api/POI/IsPOIInRange")]
        public bool IsPOIInRange(int poiID, double longitude, double latitude, float allowedDistance = 3)
        {
            return GetDistanceToPOI(poiID, longitude, latitude) <= allowedDistance;
        }

        [HttpGet]
        [Route("api/POI/GetRouteToNextPOI")]
        ///travelMode: 1 = Walking, 2 = Biking
        public HttpResponseMessage GetRouteToNextPOI(double currentLatitude, double currentLongitude, int poiID, int travelMode)
        {
            try
            {
                var poi = _repo.GetPOI(poiID);

                DirectionsRequest directionsRequest = new DirectionsRequest()
                {
                    Origin = CoordinatesHelper.ToString(currentLatitude, currentLongitude),
                    Destination = CoordinatesHelper.ToString(poi.Coordinates),
                    TravelMode = (TravelMode)travelMode
                };

                var directions = GoogleMaps.Directions.Query(directionsRequest);
                IEnumerable<Step> steps = directions.Routes.First().Legs.First().Steps;
                return Request.CreateResponse(HttpStatusCode.OK, steps);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, err);
            }
        }


    }
}
