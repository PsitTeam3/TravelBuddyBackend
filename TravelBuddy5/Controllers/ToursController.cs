using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using TravelBuddy5.DAL;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.Models;

namespace TravelBuddy5.Controllers
{
    public class ToursController : ApiController
    {
        private readonly ITourRepo _repo;

        public ToursController(ITourRepo tourRepo)
        {
            _repo = tourRepo;
        }

        [HttpGet]
        [Route("api/Tours/GetTours")]
        public IQueryable<TourDTO> GetTours()
        {
            return _repo.GetTours().Select(CreateTourDTO());
        }

        [HttpGet]
        [Route("api/Tours/GetToursByCity")]
        // GET: api/Tours
        public IQueryable<TourDTO> GetToursByCity(int cityID)
        {
            return _repo.GetToursByCity(cityID).Include(tour => tour.City).Include(tour => tour.City.Country).Select(CreateTourDTO());
        }

        private Expression<Func<Tour, TourDTO>> CreateTourDTO()
        {
            return tour => new TourDTO
            {
                Id = tour.Id,
                Name = tour.Name,
                City = tour.City.Name,
                Country = tour.City.Country.Name,
                Description = tour.Description,
                DetailDescription = tour.DetailDescription,
                Image = tour.Image
            };
        }
    }
}