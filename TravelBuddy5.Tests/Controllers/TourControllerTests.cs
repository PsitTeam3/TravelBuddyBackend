using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TravelBuddy5.Controllers;
using TravelBuddy5.DAL;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.Models;

namespace TravelBuddy5.Tests.Controllers
{
    [TestClass]
    public class TourControllerTests
    {
        private ToursController _target;
        private Mock<ITourRepo> _tourRepoMock;
        private IList<Tour> _tours;
        private Tour _tour1;

        [TestInitialize]
        public void Initialize()
        {
            _tourRepoMock = new Mock<ITourRepo>(MockBehavior.Strict);
            _target = new ToursController(_tourRepoMock.Object);

            // Create mock data
            var country1 = new Country() {Id = 1, Name = "Switzerland"};
            var country2 = new Country() { Id = 2, Name = "Germany" };
            var city1 = new City() {Id = 3, Name = "Zuerich", Country = country1};
            var city2 = new City() {Id = 4, Name = "Berlin", Country = country2};
            _tour1 = new Tour()
            {
                Id = 5, Name = "Tour 1", Description = "Desc Tour 1",
                DetailDescription = "Detail Desc Tour 1", City = city1, Image = "Image Tour 1"
            };
            var tour2 = new Tour()
            {
                Id = 6, Name = "Tour 2", Description = "Desc Tour 2",
                DetailDescription = "Detail Desc Tour 2", City = city2, Image = "Image Tour 2"
            };
            _tours = new List<Tour>() {_tour1, tour2};
        }

        [TestMethod]
        public void TestThatAllToursAreReturned()
        {
            _tourRepoMock.Setup(m => m.GetTours()).Returns(_tours.AsQueryable());
            IQueryable<TourDTO> tourDTOs = _target.GetTours();
            Assert.AreEqual(2, tourDTOs.Count());
        }

        [TestMethod]
        public void TestThatCorrectToursByCityAreReturned()
        {
            _tourRepoMock.Setup(m => m.GetToursByCity(3)).Returns(_tours.AsQueryable());
            IQueryable<TourDTO> tourDTOs = _target.GetToursByCity(3);
            Assert.AreEqual(2, tourDTOs.Count());
        }

        [TestMethod]
        public void TestThatDTOContainsCorrectData()
        {
            _tourRepoMock.Setup(m => m.GetTours()).Returns(new Tour[] {_tour1}.AsQueryable());
            IQueryable<TourDTO> tourDTOs = _target.GetTours();
            Assert.AreEqual(1, tourDTOs.Count());
            TourDTO tourDTO = tourDTOs.First();
            Assert.AreEqual(_tour1.Id, tourDTO.Id);
            Assert.AreEqual(_tour1.Name, tourDTO.Name);
            Assert.AreEqual(_tour1.City.Name, tourDTO.City);
            Assert.AreEqual(_tour1.City.Country.Name, tourDTO.Country);
            Assert.AreEqual(_tour1.Description, tourDTO.Description);
            Assert.AreEqual(_tour1.DetailDescription, tourDTO.DetailDescription);
            Assert.AreEqual(_tour1.Image, tourDTO.Image);
        }
    }
}
