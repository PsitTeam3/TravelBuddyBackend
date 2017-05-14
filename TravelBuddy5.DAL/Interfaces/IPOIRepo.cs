using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy5.DAL.Interfaces
{
    public interface IPOIRepo
    {

        /// <summary>
        /// Gets all POIs.
        /// </summary>
        /// <returns>Queryable for all POIs</returns>
        IQueryable<POI> GetPOIs();

        /// <summary>
        /// Gets one POI by ID.
        /// </summary>
        /// <param name="poiID">The poi identifier.</param>
        /// <returns>The POI object</returns>
        POI GetPOI(int poiID);

        /// <summary>
        /// Gets all POIs for a specific tour.
        /// </summary>
        /// <param name="tourID">The tour identifier.</param>
        /// <returns>ueryable for all POIs in the tour</returns>
        IQueryable<POI> GetPOIsByTour(int tourID);
    }
}
