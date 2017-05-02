﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TravelBuddy5.DAL;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.Interfaces;
using TravelBuddy5.Models;

namespace TravelBuddy5.Controllers
{
    public class UserTourController : ApiController
    {

        private readonly IUserTourRepo _userTourRepo;
        private readonly IUserPOIRepo _userPOIRepo;
        private readonly IGeoLocationService _geoLocationService;

        public UserTourController(IUserTourRepo userTourRepo, IUserPOIRepo userPOIRepo, IGeoLocationService geoLocationService)
        {
            _userTourRepo = userTourRepo;
            _userPOIRepo = userPOIRepo;
            _geoLocationService = geoLocationService;
        }

        /*[HttpPost]
        [Route("api/UserTour/StartUserTour")]
        public HttpResponseMessage StartUserTour(int userID, int tourID)
        {
            try
            {
                _userTourRepo.StartUserTour(userID, tourID);
            }
            catch(Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.Forbidden, err);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }*/

        [HttpPost]
        [Route("api/UserTour/StartUserTour")]
        public RouteToPointOfInterestDTO StartUserTour(int userID, int tourID, double currentLatitude, double currentLongitude)
        {
            VerifyNoTourIsStarted(userID);
            _userTourRepo.StartUserTour(userID, tourID);
            UserTour activeTour = _userTourRepo.GetActiveTour(userID);
            POI nextPOI = _userPOIRepo.GetNextPOI(activeTour.Id).Value.First();
            IEnumerable<CoordinateDTO> route = _geoLocationService.GetRoute(currentLatitude, currentLongitude,
                nextPOI.Coordinates.Latitude.Value,
                nextPOI.Coordinates.Longitude.Value);
            return new RouteToPointOfInterestDTO {NextPOI = PointOfInterestDTO.Create(nextPOI), RouteToNextPOI = route};
        }

        private void VerifyNoTourIsStarted(int userID)
        {
            if (_userTourRepo.GetActiveTour(userID) != null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("User has already a started tour"),
                    ReasonPhrase = "Other tour already active"
                };
                throw new HttpResponseException(resp);
            }
        }

        [HttpPost]
        [Route("api/UserTour/EndUserTour")]
        public HttpResponseMessage EndUserTour(int userID)
        {
            VerifyAnyTourIsStarted(userID);
            _userTourRepo.EndUserTour(userID);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private void VerifyAnyTourIsStarted(int userID)
        {
            if (_userTourRepo.GetActiveTour(userID) == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("User not started any tour"),
                    ReasonPhrase = "No tour is active"
                };
                throw new HttpResponseException(resp);
            }
        }


        /*[HttpGet]
        [Route("api/UserTour/EndUserTour")]
        public HttpResponseMessage EndUserTour(int userID, int tourID)
        {
            try
            {
                _userTourRepo.EndUserTour(userID, tourID);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }*/

        [HttpGet]
        [Route("api/UserTour/GetActiveTour")]
        public HttpResponseMessage GetActiveTour(int userID)
        {
            UserTour tour = _userTourRepo.GetActiveTour(userID);
            if(tour == null)
            {
                HttpError err = new HttpError("No tour is active for the defined user");
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, err);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, Mapper.Map<UserTourDTO>(tour));
        }
    }
}
