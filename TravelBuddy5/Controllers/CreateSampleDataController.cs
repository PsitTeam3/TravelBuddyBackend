using System;
using System.Web.Http;
using TravelBuddy.DAL;
using TravelBuddy5.DAL;

namespace TravelBuddy5.Controllers
{
    public class CreateSampleDataController : ApiController
    {
        private Country _switzerland;
        private City _zurich;
        private Country _colombia;
        private POI _fraumuenster;
        private POI _lakeOfZurich;
        private POI _stateMuseumZurich;
        private City _lucerne;
        private City _bogota;
        private City _cartagena;
        private POI _oldCityWallCartagena;
        private POI _uetliberg;

        public IHttpActionResult GetCreateSampleData()
        {
            using (TravelBuddyEntities db = new TravelBuddyEntities())
            {
                CreateSampleData(db);
                db.SaveChanges();
            }

            return Ok("Sample data created");
        }

        private void CreateSampleData(TravelBuddyEntities db)
        {
            ClearExistingDBEntries(db);
            CreateCountriesAndCities(db);
            CreatePointsOfInterest(db);
            CreateTours(db);
        }

        private void ClearExistingDBEntries(TravelBuddyEntities db)
        {
            db.TourPOI.RemoveRange(db.TourPOI);
            db.Tour.RemoveRange(db.Tour);
            db.POI.RemoveRange(db.POI);
            db.City.RemoveRange(db.City);
            db.Country.RemoveRange(db.Country);
        }

        private void CreateCountriesAndCities(TravelBuddyEntities db)
        {
            _switzerland = new Country() {Name = "Switzerland"};
            db.Country.Add(_switzerland);

            _colombia = new Country() {Name = "Colombia"};
            db.Country.Add(_colombia);

            _lucerne = new City() {Name = "Lucerne", Country = _switzerland};
            db.City.Add(_lucerne);

            _zurich = new City() {Name = "Zürich", Country = _switzerland};
            db.City.Add(_zurich);

            _bogota = new City() {Name = "Bogota", Country = _colombia};
            db.City.Add(_bogota);

            _cartagena = new City() {Name = "Cartagena", Country = _colombia};
            db.City.Add(_cartagena);
        }

        private void CreatePointsOfInterest(TravelBuddyEntities db)
        {
            _fraumuenster = new POI()
            {
                Name = "Fraumünster Church",
                Description = "Old church, beautiful, has a tower to go up.",
                VisitDuration = (int) TimeSpan.FromMinutes(40).TotalSeconds,
                Coordinates = CoordinatesHelper.CreatePoint(47.369741, 8.541631),
            };
            db.POI.Add(_fraumuenster);

            _lakeOfZurich = new POI()
            {
                Name = "Lake of Zürich",
                Description = "The lake of Zürich is a nice place for having a swim during summer time",
                VisitDuration = (int) TimeSpan.FromMinutes(60).TotalSeconds,
                Coordinates = CoordinatesHelper.CreatePoint(47.366202,8.541840),
            };
            db.POI.Add(_lakeOfZurich);

            _stateMuseumZurich = new POI()
            {
                Name = "State Museum",
                Description = "This museum just next to the main station shows the history of Switzerland",
                VisitDuration = (int) TimeSpan.FromMinutes(150).TotalSeconds
            };
            db.POI.Add(_stateMuseumZurich);

            _uetliberg = new POI()
            {
                Name = "Üetliberg",
                Description =
                    "Walk up the house mountain of Zürich and enjoy the view over Zürich and perhaps the alps as well.",
                VisitDuration = (int) TimeSpan.FromMinutes(240).TotalSeconds,
                Coordinates = CoordinatesHelper.CreatePoint(47.351703, 8.492508),

            };
            db.POI.Add(_uetliberg);

            _oldCityWallCartagena = new POI
            {
                Name = "Old city wall",
                Description = "Visit the old city wall of cartagena. Bla bla bla.",
                VisitDuration = (int) TimeSpan.FromMinutes(70).TotalSeconds
            };
            db.POI.Add(_oldCityWallCartagena);
        }

        private void CreateTours(TravelBuddyEntities db)
        {
            Tour tour;
            
            tour = new Tour()
            {
                Name = "Zürich Historical City Tour",
                Description = "Tour through the main historical attractions in the city of Zürich",
                DetailDescription = "The Tour starts at Zürich's main station and heads to the old town. " +
                                    "The first destination inside the old town is the Fraumünster church. There you can climb up the 40 meters " +
                                    "high tower and enjoy a stunning view over Zürich. And so on and so on until it's night time to visit Langstrasse!!!",
                City = _zurich,
            };
            db.TourPOI.Add(new TourPOI { Tour = tour, POI = _fraumuenster, Order = 1 });
            db.TourPOI.Add(new TourPOI { Tour = tour, POI = _lakeOfZurich, Order = 2 });
            db.TourPOI.Add(new TourPOI { Tour = tour, POI = _stateMuseumZurich, Order = 3 });
            db.Tour.Add(tour);

            tour = new Tour
            {
                Name = "Full day tour: Old town and the mountain 'Üetliberg'",
                Description = "Lorem ipsum bla bla fasel fasel",
                DetailDescription = "Jetzt magi langsam nüm und mir gönd di guetä ideeä uuuus ;-) Und wenn du das lisisch, bisch au e armi Sau!!!",
                City = _zurich
            };
            db.TourPOI.Add(new TourPOI {Tour = tour, POI = _fraumuenster, Order = 1});
            db.TourPOI.Add(new TourPOI {Tour = tour, POI = _uetliberg, Order = 2});
            db.Tour.Add(tour);
        }
    }
}
