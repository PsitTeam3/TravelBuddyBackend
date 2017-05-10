using System.Linq;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class CountryRepo : RepoBase, ICountryRepo
    {
        public CountryRepo(Entities db) : base(db)
        {
        }

        public IQueryable<Country> GetCountries()
        {
            return DB.Country;
        }
    }
}