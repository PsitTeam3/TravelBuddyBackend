using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class POIRepo:RepoBase, IPOIRepo
    {
        public POIRepo(Entities db) : base(db)
        {
        }

        public IQueryable<POI> GetPOIs()
        {
            return DB.POI;
        }

        public IQueryable<POI> GetPOIsByTour(int tourID)
        {
            return DB.TourPOI.Where(tp => tp.FK_Tour == tourID).Select(tp => tp.POI);
        }

        public POI GetPOI(int poiID)
        {
            return DB.POI.FirstOrDefault(p => p.Id == poiID);
        }
    }
}
