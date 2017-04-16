using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.DAL.Repositories;

namespace TravelBuddy5.Controllers
{
    public class UserTourController : ApiController
    {

        private readonly IUserTourRepo _repo;

        public UserTourController(IUserTourRepo tourRepo)
        {
            _repo = tourRepo;
        }

        [HttpGet]
        [Route("api/UserTour/StartUserTour")]
        public HttpResponseMessage StartUserTour(int userID, int tourID)
        {
            try
            {
                _repo.StartUserTour(userID, tourID);
            }
            catch(Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.Forbidden, err);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/UserTour/EndUserTour")]
        public HttpResponseMessage EndUserTour(int userID, int tourID)
        {
            try
            {
                _repo.EndUserTour(userID, tourID);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/UserTour/GetActiveTour")]
        public HttpResponseMessage GetActiveTour(int userID)
        {
            try
            {
                _repo.GetActiveTour(userID);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, err);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
