using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
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
    public class POIControllerTests
    {
        private POIController _target;
        private Mock<IPOIRepo> _poiRepoMock;
        private Mock<IUserTourRepo> _userTourRepoMock;
        private Mock<IUserPOIRepo> _userPoiRepoMock;
        private Mock<IGeoLocationService> _geoLocationServiceMock;

        [TestInitialize]
        public void Initialize()
        {
            _poiRepoMock = new Mock<IPOIRepo>(MockBehavior.Strict);
            _userTourRepoMock = new Mock<IUserTourRepo>(MockBehavior.Strict);
            _userPoiRepoMock = new Mock<IUserPOIRepo>(MockBehavior.Strict);
            _geoLocationServiceMock = new Mock<IGeoLocationService>(MockBehavior.Strict);

            _target = new POIController(_poiRepoMock.Object, _userTourRepoMock.Object,
                _userPoiRepoMock.Object, _geoLocationServiceMock.Object);
        }

        [TestMethod]
        public void TestThatNextPOIGetsMarkedAsVisited()
        {
            SetupActiveTourAndNextPOI(8, 13, 28);
            _userPoiRepoMock.Setup(m => m.SetPOIAsVisited(28, 13));

            _target.SetNextPOIAsVisited(8);

            _userPoiRepoMock.Verify(m => m.SetPOIAsVisited(28, 13), Times.Once());
        }

        private void SetupActiveTourAndNextPOI(int userID, int userTourID, int nextPOIID)
        {
            var activeTour = new UserTour {Id = userTourID};
            var nextPOI = new POI {Id = nextPOIID, Coordinates = DbGeography.PointFromText("POINT(34 3)", 4326) };
            _userTourRepoMock.Setup(m => m.GetActiveTour(userID)).Returns(activeTour);
            _userPoiRepoMock.Setup(m => m.GetNextPOI(activeTour)).Returns(nextPOI);
        }

        [TestMethod]
        public void TestThatRouteToNextPOIIsReturnedCorrectly()
        {
            SetupActiveTourAndNextPOI(8, 13, 28);
            var point1 = new CoordinateDTO {Latitude = 2.4, Longitude = 33.4};
            var point2 = new CoordinateDTO {Latitude = 2.7, Longitude = 33.7};
            var route = new CoordinateDTO[] {point1, point2};
            _geoLocationServiceMock.Setup(m => m.GetRoute(It.IsInRange(1.9, 2.1, Range.Inclusive),
                It.IsInRange(32.9, 33.1, Range.Inclusive), It.IsInRange(2.9, 3.1, Range.Inclusive),
                It.IsInRange(33.9, 34.1, Range.Inclusive))).Returns(route);

            IEnumerable<CoordinateDTO> result = _target.GetRouteToNextPOI(8, 2.0, 33.0);
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2.4, result.ElementAt(0).Latitude, 0.1);
            Assert.AreEqual(33.4, result.ElementAt(0).Longitude, 0.1);
            Assert.AreEqual(2.7, result.ElementAt(1).Latitude, 0.1);
            Assert.AreEqual(33.7, result.ElementAt(1).Longitude, 0.1);
        }
    }
}
