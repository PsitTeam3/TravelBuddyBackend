using System.Linq;

namespace TravelBuddy5.DAL.Interfaces
{
    public interface ICountryRepo
    {
        /// <summary>
        /// Gets all countries.
        /// </summary>
        /// <returns>Queryable for all countries</returns>
        IQueryable<Country> GetCountries();
    }
}