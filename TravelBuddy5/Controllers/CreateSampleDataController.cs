using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TravelBuddy5.DAL;

namespace TravelBuddy5.Controllers
{
    public class CreateSampleDataController : ApiController
    {
        public IQueryable<Tour> GetCreateSampleDataController()
        {
            using (TravelBuddyEntities db = new TravelBuddyEntities())
            {
                CreateSampleTours(db);
                db.SaveChanges();
            }

            return Enumerable.Empty<Tour>().AsQueryable();
        }

        private static void CreateSampleTours(TravelBuddyEntities db)
        {
            db.Country.RemoveRange(db.Country);
            db.City.RemoveRange(db.City);
            db.Tour.RemoveRange(db.Tour);
            db.POI.RemoveRange(db.POI);

            Country switzerland = new Country() {Name = "Switzerland"};
            City zurich = new City() {Name = "Zürich", Country = switzerland};
            db.Country.Add(switzerland);
            db.City.Add(zurich);

            POI poi1 = new POI()
            {
                Name = "Fraumünster Church",
                Description = "Old church, beautiful, has a tower to go up.",
                VisitDuration = (int) TimeSpan.FromMinutes(40).TotalSeconds,
            };

            POI poi2 = new POI()
            {
                Name = "Lake of Zürich",
                Description = "The lake of Zürich is a nice place for having a swim during summer time",
                VisitDuration = (int) TimeSpan.FromMinutes(60).TotalSeconds
            };

            POI poi3 = new POI()
            {
                Name = "Landesmeseum",
                Description = "This museum just next to the main station shows the history of Switzerland",
                VisitDuration = (int) TimeSpan.FromMinutes(150).TotalSeconds
            };

            db.POI.Add(poi1);
            db.POI.Add(poi2);
            db.POI.Add(poi3);

            Tour zurichTour = new Tour()
            {
                Name = "Zürich Historical City Tour",
                Description = "Tour through the main historical attractions in the city of Zürich",
                DetailDescription = "The Tour starts at Zürich's main station and heads to the old town. " +
                                    "The first destination inside the old town is the Fraumünster church. There you can climb up the 40 meters " +
                                    "high tower and enjoy a stunning view over Zürich. And so on and so on until it's night time to visit Langstrasse!!!",
                City = zurich,
            };

            TourPOI tourPoi1 = new TourPOI() {Tour = zurichTour, POI = poi1, Order = 1};
            TourPOI tourPoi2 = new TourPOI() {Tour = zurichTour, POI = poi2, Order = 2};
            TourPOI tourPoi3 = new TourPOI() {Tour = zurichTour, POI = poi3, Order = 3};
            db.TourPOI.Add(tourPoi1);
            db.TourPOI.Add(tourPoi2);
            db.TourPOI.Add(tourPoi3);
            zurichTour.TourPOI.Add(tourPoi1);
            zurichTour.TourPOI.Add(tourPoi2);
            zurichTour.TourPOI.Add(tourPoi3);
            db.Tour.Add(zurichTour);
        }
    }
}
