using System.Linq;
using System.Web.Http;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.Models;

namespace TravelBuddy5.Controllers
{
    public class CountriesController : ApiController
    {
        private readonly ICountryRepo _countryRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CountriesController"/> class.
        /// </summary>
        /// <param name="countryRepo">The country repo.</param>
        public CountriesController(ICountryRepo countryRepo)
        {
            _countryRepo = countryRepo;
        }

        /// <summary>
        /// Gets all countries.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Countries/GetCountries")]
        // GET: api/Countries
        public IQueryable<CountryDTO> GetCountries()
        {
            return _countryRepo.GetCountries().Select(country => new CountryDTO {Id = country.Id, Name = country.Name});
        }
    }
}