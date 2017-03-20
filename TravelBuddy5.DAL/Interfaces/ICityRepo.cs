using System.Linq;

namespace TravelBuddy5.DAL.Interfaces
{
    public interface ICityRepo
    {
        IQueryable<City> GetCities();
        IQueryable<City> GetCitiesByCountryId(int countryId);
    }
}