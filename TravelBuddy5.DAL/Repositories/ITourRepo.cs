using System.Linq;

namespace TravelBuddy5.DAL.Repositories
{
    public interface ITourRepo
    {
        IQueryable<Tour> GetToursByCity(int cityId);
        IQueryable<Tour> GetTours();
    }

}
