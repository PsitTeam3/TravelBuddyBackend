using System.Linq;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class CityRepo : RepoBase, ICityRepo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CityRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public CityRepo(Entities db) : base(db)
        {
        }

        /// <summary>
        /// Gets all cities.
        /// </summary>
        /// <returns>
        /// Queryable for all cities
        /// </returns>
        public IQueryable<City> GetCities()
        {
            return DB.City;
        }

        /// <summary>
        /// Gets all cities by country identifier.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// Queryable for all cities
        /// </returns>
        public IQueryable<City> GetCitiesByCountryId(int countryId)
        {
            return DB.City.Where(city => city.FK_Country == countryId);
        }
    }
}