using System.Linq;

namespace TravelBuddy5.DAL.Interfaces
{
    public interface ITourRepo
    {
        IQueryable<Tour> GetToursByCity(int cityId);
        IQueryable<Tour> GetTours();
    }

}
