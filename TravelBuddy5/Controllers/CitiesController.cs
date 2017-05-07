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

        public CitiesController(ICityRepo cityRepo)
        {
            _cityRepo = cityRepo;
        }

        [HttpGet]
        [Route("api/Cities/GetCities")]
        public IQueryable<CityDTO> GetCities()
        {
            return _cityRepo.GetCities().Select(CityDTO.Create());
        }

        [HttpGet]
        [Route("api/Cities/GetCitiesByCountry")]
        public IQueryable<CityDTO> GetCitiesByCountry(int countryId)
        {
            return _cityRepo.GetCitiesByCountryId(countryId).Select(CityDTO.Create());
        }
    }
}
