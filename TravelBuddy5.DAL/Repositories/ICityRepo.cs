using System.Linq;

namespace TravelBuddy5.DAL.Repositories
{
    public interface ICityRepo
    {
        IQueryable<City> GetCities();
        IQueryable<City> GetCitiesByCountryId(int countryId);
    }
}