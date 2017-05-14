using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy.DAL
{
    /// <summary>
    /// Helper class for all coordinate functionality
    /// </summary>
    public class CoordinatesHelper
    {
        /// <summary>
        /// Creates a mssql compatible coordinate point.
        /// </summary>
        /// <param name="lat">The lat.</param>
        /// <param name="lon">The lon.</param>
        /// <param name="srid">The srid.</param>
        /// <returns></returns>
        public static DbGeography CreatePoint(double lat, double lon, int srid = 4326)
        {
            string wkt = String.Format("POINT({0} {1})", lon, lat);
            return DbGeography.PointFromText(wkt, srid);
        }

        /// <summary>
        /// Creates a comma seperated string from a given latitude and longitude
        /// </summary>
        /// <param name="lat">The latitude</param>
        /// <param name="lon">The longitude/param>
        /// <returns>
        /// A comma seperated string from a given latitude and longitude
        /// </returns>
        public static string ToString(double lat, double lon)
        {
            return string.Format("{0},{1}", lat, lon);
        }

        /// <summary>
        /// Creates a comma seperated string from a given db coordinate point
        /// </summary>
        /// <param name="geo">The geo db data object</param>
        /// <returns>
        /// A comma seperated string from a given  given db coordinate point
        /// </returns>
        public static string ToString(DbGeography geo)
        {
            return ToString(geo.Latitude.Value, geo.Longitude.Value);
        }

    }
}
