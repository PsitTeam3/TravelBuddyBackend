using System.Linq;

namespace TravelBuddy5.DAL.Interfaces
{
    public interface ICountryRepo
    {
        IQueryable<Country> GetCountries();
    }
}