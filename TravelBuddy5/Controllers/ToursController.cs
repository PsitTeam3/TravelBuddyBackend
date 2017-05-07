using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.Models;

namespace TravelBuddy5.Controllers
{
    public class ToursController : ApiController
    {
        private readonly ITourRepo _tourRepo;

        public ToursController(ITourRepo tourRepo)
        {
            _tourRepo = tourRepo;
        }

        [HttpGet]
        [Route("api/Tours/GetTours")]
        public IQueryable<TourDTO> GetTours()
        {
            return _tourRepo.GetTours().Select(TourDTO.Create());
        }

        [HttpGet]
        [Route("api/Tours/GetToursByCity")]
        public IQueryable<TourDTO> GetToursByCity(int cityID)
        {
            return _tourRepo.GetToursByCity(cityID).Select(TourDTO.Create());
        }
    }
}