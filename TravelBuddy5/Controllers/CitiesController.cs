﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using TravelBuddy5.DAL;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.DAL.Repositories;
using TravelBuddy5.Models;

namespace TravelBuddy5.Controllers
{
    public class CitiesController : ApiController
    {
        private readonly ICityRepo _cityRepo;

        public CitiesController(ICityRepo cityRepo)
        {
            _cityRepo = cityRepo;
        }

        [HttpGet]
        [Route("api/Cities/GetCities")]
        public IQueryable<CityDTO> GetCities()
        {
            return _cityRepo.GetCities().Select(CreateCityDTO());
        }

        [HttpGet]
        [Route("api/Cities/GetCitiesByCountry")]
        public IQueryable<CityDTO> GetCitiesByCountry(int id)
        {
            return _cityRepo.GetCitiesByCountryId(id).Select(CreateCityDTO());
        }

        private Expression<Func<City, CityDTO>> CreateCityDTO()
        {
            return city => new CityDTO {Id = city.Id, Name = city.Name};
        }
    }
}
