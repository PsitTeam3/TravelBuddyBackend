using System.Linq;

namespace TravelBuddy5.DAL.Interfaces
{

    public interface ICityRepo
    {
        /// <summary>
        /// Gets all cities.
        /// </summary>
        /// <returns>Queryable for all cities</returns>
        IQueryable<City> GetCities();

        /// <summary>
        /// Gets all cities by country identifier.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>Queryable for all cities</returns>
        IQueryable<City> GetCitiesByCountryId(int countryId);
    }
}