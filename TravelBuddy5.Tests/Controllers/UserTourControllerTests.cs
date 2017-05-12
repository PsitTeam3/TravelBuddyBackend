using System;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelBuddy5.Controllers;
using TravelBuddy5.DAL;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.Interfaces;
using TravelBuddy5.Models;

namespace TravelBuddy5.Tests.Controllers
{
    [TestClass]
    public class UserTourControllerTests
    {
        private UserTourController _target;
        private Mock<IUserTourRepo> _userTourRepoMock;
        private Mock<IUserPOIRepo> _userPoiRepoMock;
        private Mock<IGeoLocationService> _geoLocationServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _userTourRepoMock = new Mock<IUserTourRepo>(MockBehavior.Strict);
            _userPoiRepoMock = new Mock<IUserPOIRepo>(MockBehavior.Strict);
            _geoLocationServiceMock = new Mock<IGeoLocationService>(MockBehavior.Strict);

            _target = new UserTourController(_userTourRepoMock.Object, _userPoiRepoMock.Object, _geoLocationServiceMock.Object);
        }

        [TestMethod]
        public void TestThatTourGetsStartedCorrectly()
        {
            UserTour activeTour = null;
            POI nextPOI = new POI
            {
                Id = 4,
                Name = "POIName",
                Description = "POIDesc",
                VisitDuration = 23,
                Coordinates = DbGeography.PointFromText("POINT(49 23)", 4326)
            };
            _userTourRepoMock.Setup(m => m.GetActiveTour(1)).Returns((int userId) => activeTour);
            _userTourRepoMock.Setup(m => m.StartUserTour(1, 2)).Callback(() => activeTour = new UserTour {Id = 3});
            _userPoiRepoMock.Setup(m => m.GetNextPOI(3)).Returns(nextPOI);
            CoordinateDTO coordinate1 = new CoordinateDTO {Latitude = 14, Longitude = 39};
            CoordinateDTO coordinate2 = new CoordinateDTO {Latitude = 16, Longitude = 40};
            _geoLocationServiceMock.Setup(m => m.GetRoute(It.IsInRange(7.9, 8.1, Range.Inclusive),
                It.IsInRange(84.9, 85.1, Range.Inclusive), It.IsInRange(22.9, 23.1, Range.Inclusive),
                It.IsInRange(48.9, 49.1, Range.Inclusive)))
                .Returns(new CoordinateDTO[] {coordinate1, coordinate2});

            RouteToPointOfInterestDTO routeToPointOfInterestDto = _target.StartUserTour(1, 2, 8, 85);
            Assert.IsNotNull(routeToPointOfInterestDto);
            Assert.IsNotNull(routeToPointOfInterestDto.NextPOI);
            Assert.IsNotNull(routeToPointOfInterestDto.RouteToNextPOI);
            Assert.AreEqual(nextPOI.Id, routeToPointOfInterestDto.NextPOI.Id);
            Assert.AreEqual(nextPOI.Description, routeToPointOfInterestDto.NextPOI.Description);
            Assert.AreEqual(nextPOI.VisitDuration, routeToPointOfInterestDto.NextPOI.VisitDuration);
            Assert.AreEqual(23.0, routeToPointOfInterestDto.NextPOI.Latitude, 0.1);
            Assert.AreEqual(49.0, routeToPointOfInterestDto.NextPOI.Longitude, 0.1);
            Assert.AreEqual(2.0, routeToPointOfInterestDto.RouteToNextPOI.Count());
            Assert.AreEqual(14.0, routeToPointOfInterestDto.RouteToNextPOI.ElementAt(0).Latitude, 0.1);
            Assert.AreEqual(39.0, routeToPointOfInterestDto.RouteToNextPOI.ElementAt(0).Longitude, 0.1);
            Assert.AreEqual(16.0, routeToPointOfInterestDto.RouteToNextPOI.ElementAt(1).Latitude, 0.1);
            Assert.AreEqual(40.0, routeToPointOfInterestDto.RouteToNextPOI.ElementAt(1).Longitude, 0.1);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void TestThatExceptionIsThrownWhenTourIsStartedAndUserHasAlreadyAnActiveTour()
        {
            _userTourRepoMock.Setup(m => m.GetActiveTour(1)).Returns(new UserTour());
            _target.StartUserTour(1, 2, 34, 59);
        }

        [TestMethod]
        public void TestThatTourIsCorrectlyEnded()
        {
            _userTourRepoMock.Setup(m => m.GetActiveTour(1)).Returns(new UserTour {Id = 23});
            _userTourRepoMock.Setup(m => m.EndUserTour(1));
            _target.EndUserTour(1);
            _userTourRepoMock.Verify(m => m.EndUserTour(1), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void TestThatExceptionIsThrownWhenTourGetsEndedButUserHasNoActiveTour()
        {
            _userTourRepoMock.Setup(m => m.GetActiveTour(1)).Returns((UserTour)null);
            _target.EndUserTour(1);
        }
    }
}
