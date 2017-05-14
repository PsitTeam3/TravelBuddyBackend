using System.Linq;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class CountryRepo : RepoBase, ICountryRepo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CountryRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public CountryRepo(Entities db) : base(db)
        {
        }

        /// <summary>
        /// Gets all countries.
        /// </summary>
        /// <returns>
        /// Queryable for all countries
        /// </returns>
        public IQueryable<Country> GetCountries()
        {
            return DB.Country;
        }
    }
}