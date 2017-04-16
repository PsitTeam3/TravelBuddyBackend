using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
            
            return _repo.GetPOIs().Select(CreatePOIDTO());

        }

        [HttpGet]
        [Route("api/POI/GetPOIsByTour")]
        public IQueryable<PointOfInterestDTO> GetPOIsByTour(int id)
        {
            return _repo.GetPOIsByTour(id).Select(CreatePOIDTO());
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

        private Expression<Func<POI, PointOfInterestDTO>> CreatePOIDTO()
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
