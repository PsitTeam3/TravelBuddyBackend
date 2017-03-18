using System.Linq;

namespace TravelBuddy5.DAL.Repositories
{
    public interface ICountryRepo
    {
        IQueryable<Country> GetCountries();
    }
}