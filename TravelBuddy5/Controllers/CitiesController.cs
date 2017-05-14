using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using TravelBuddy5.DAL;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.Models;

namespace TravelBuddy5.Controllers
{
    public class CitiesController : ApiController
    {
        private readonly ICityRepo _cityRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CitiesController"/> class.
        /// </summary>
        /// <param name="cityRepo">The city repo.</param>
        public CitiesController(ICityRepo cityRepo)
        {
            _cityRepo = cityRepo;
        }

        /// <summary>
        /// Gets all cities.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Cities/GetCities")]
        public IQueryable<CityDTO> GetCities()
        {
            return _cityRepo.GetCities().Select(CityDTO.Create());
        }

        /// <summary>
        /// Gets all cities by country.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Cities/GetCitiesByCountry")]
        public IQueryable<CityDTO> GetCitiesByCountry(int countryId)
        {
            return _cityRepo.GetCitiesByCountryId(countryId).Select(CityDTO.Create());
        }
    }
}
