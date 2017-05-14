using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class POIRepo:RepoBase, IPOIRepo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="POIRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public POIRepo(Entities db) : base(db)
        {
        }

        /// <summary>
        /// Gets all POIs.
        /// </summary>
        /// <returns>
        /// Queryable for all POIs
        /// </returns>
        public IQueryable<POI> GetPOIs()
        {
            return DB.POI;
        }

        /// <summary>
        /// Gets all POIs for a specific tour.
        /// </summary>
        /// <param name="tourID">The tour identifier.</param>
        /// <returns>
        /// ueryable for all POIs in the tour
        /// </returns>
        public IQueryable<POI> GetPOIsByTour(int tourID)
        {
            return DB.TourPOI.Where(tp => tp.FK_Tour == tourID).Select(tp => tp.POI);
        }

        /// <summary>
        /// Gets one POI by ID.
        /// </summary>
        /// <param name="poiID">The poi identifier.</param>
        /// <returns>
        /// The POI object
        /// </returns>
        public POI GetPOI(int poiID)
        {
            return DB.POI.FirstOrDefault(p => p.Id == poiID);
        }
    }
}
