using System.Data.Entity;
using System.Linq;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class TourRepo: RepoBase, ITourRepo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TourRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public TourRepo(Entities db) : base(db)
        {
        }

        /// <summary>
        /// Gets all tours.
        /// </summary>
        /// <returns>
        /// Queryable for all tours
        /// </returns>
        public IQueryable<Tour> GetTours()
        {
            return DB.Tour.Include(tour => tour.City).Include(tour => tour.City.Country);
        }

        /// <summary>
        /// Gets all tours by city.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>
        /// Queryable for all tours in the given city
        /// </returns>
        public IQueryable<Tour> GetToursByCity(int cityId)
        {
            return DB.Tour.Where(tour => tour.FK_City == cityId).Include(tour => tour.City).Include(tour => tour.City.Country);
        }
    }
}
