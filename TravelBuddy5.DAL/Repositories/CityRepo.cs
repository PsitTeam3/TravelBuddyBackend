using System.Linq;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class CityRepo : RepoBase, ICityRepo
    {
        public CityRepo(Entities db) : base(db)
        {
        }

        public IQueryable<City> GetCities()
        {
            return DB.City;
        }

        public IQueryable<City> GetCitiesByCountryId(int countryId)
        {
            return DB.City.Where(city => city.FK_Country == countryId);
        }
    }
}