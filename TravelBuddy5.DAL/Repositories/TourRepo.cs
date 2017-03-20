using System.Linq;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class TourRepo: RepoBase, ITourRepo
    {
        public IQueryable<Tour> GetTours()
        {
            return DB.Tour;
        }

        public IQueryable<Tour> GetToursByCity(int cityId)
        {
            return DB.Tour.Where(tour => tour.FK_City == cityId);
        }
    }
}
