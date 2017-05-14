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

        /// <summary>
        /// Initializes a new instance of the <see cref="ToursController"/> class.
        /// </summary>
        /// <param name="tourRepo">The tour repo.</param>
        public ToursController(ITourRepo tourRepo)
        {
            _tourRepo = tourRepo;
        }

        /// <summary>
        /// Gets all tours.
        /// </summary>
        /// <returns>Enumerable for all tours</returns>
        [HttpGet]
        [Route("api/Tours/GetTours")]
        public IQueryable<TourDTO> GetTours()
        {
            return _tourRepo.GetTours().Select(TourDTO.Create());
        }

        /// <summary>
        /// Gets all tours by city.
        /// </summary>
        /// <param name="cityID">The city identifier.</param>
        /// <returns>Enumerable for all tours in the given city</returns>
        [HttpGet]
        [Route("api/Tours/GetToursByCity")]
        public IQueryable<TourDTO> GetToursByCity(int cityID)
        {
            return _tourRepo.GetToursByCity(cityID).Select(TourDTO.Create());
        }
    }
}