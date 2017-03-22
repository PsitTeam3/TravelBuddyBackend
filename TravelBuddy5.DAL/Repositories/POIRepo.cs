using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBuddy.DAL;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class POIRepo:RepoBase, IPOIRepo
    {
        public IQueryable<POI> GetPOIs()
        {
            return DB.POI;
        }

        public IQueryable<POI> GetPOIsByTour(int tourID)
        {
            return DB.TourPOI.Where(tp => tp.FK_Tour == tourID).Select(tp => tp.POI);
        }

        public double GetPOIDistance(int poiID, double longitude, double latitude)
        {
            return DB.POI.FirstOrDefault(p => p.Id == poiID).Coordinates.Distance(CoordinatesHelper.CreatePoint(latitude, longitude)).Value;
        }

 

    }
}
