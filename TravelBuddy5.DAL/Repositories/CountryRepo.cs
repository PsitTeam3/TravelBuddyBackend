using System.Linq;

namespace TravelBuddy5.DAL.Repositories
{
    public class CountryRepo : RepoBase, ICountryRepo
    {
        public IQueryable<Country> GetCountries()
        {
            return DB.Country;
        } 
    }
}