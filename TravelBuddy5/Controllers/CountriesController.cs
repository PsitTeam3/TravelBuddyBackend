using System.Linq;
using System.Web.Http;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.DAL.Repositories;
using TravelBuddy5.Models;

namespace TravelBuddy5.Controllers
{
    public class CountriesController : ApiController
    {
        private readonly ICountryRepo _countryRepo;

        public CountriesController(ICountryRepo countryRepo)
        {
            _countryRepo = countryRepo;
        }

        // GET: api/Countries
        public IQueryable<CountryDTO> GetCountry()
        {
            return _countryRepo.GetCountries().Select(country => new CountryDTO {Id = country.Id, Name = country.Name});
        }
    }
}