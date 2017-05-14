using System.Linq;

namespace TravelBuddy5.DAL.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITourRepo
    {
        /// <summary>
        /// Gets all tours by city.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>Queryable for all tours in the given city</returns>
        IQueryable<Tour> GetToursByCity(int cityId);

        /// <summary>
        /// Gets all tours.
        /// </summary>
        /// <returns>Queryable for all tours</returns>
        IQueryable<Tour> GetTours();
    }

}
