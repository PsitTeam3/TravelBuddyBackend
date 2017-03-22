using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy.DAL
{
    public class CoordinatesHelper
    {
        public static DbGeography CreatePoint(double lat, double lon, int srid = 4326)
        {
            string wkt = String.Format("POINT({0} {1})", lon, lat);
            return DbGeography.PointFromText(wkt, srid);
        }

    }
}
