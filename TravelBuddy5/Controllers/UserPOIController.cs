using GoogleMapsApi.Entities.Directions.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TravelBuddy5.DAL;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.Models;

namespace TravelBuddy5.Controllers
{
    public class UserPOIController : ApiController
    {

        private readonly IUserPOIRepo _repo;

        public UserPOIController(IUserPOIRepo userPOIRepo)
        {
            _repo = userPOIRepo;
        }

        [HttpGet]
        [Route("api/UserPOI/CheckUserTourPOI")]
        public HttpResponseMessage CheckUserTourPOI(int poiID, int userTourID)
        {
            try
            { 
                _repo.CheckPOI(poiID, userTourID);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.Conflict, err);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/UserPOI/CheckPOI")]
        public HttpResponseMessage CheckPOI(int poiID, int tourID, int userID)
        {
            try
            {
                _repo.CheckPOI(poiID, tourID, userID);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.Conflict, err);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/UserPOI/GetNextPOI")]
        public HttpResponseMessage GetNextPOI(int userTourID)
        {

            var nextPOI = _repo.GetNextPOI(userTourID);
            if(nextPOI.Value == null)
            {
                HttpError err = new HttpError(nextPOI.Message);
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, err);
            }

            var res = nextPOI.Value.Select(POIMapper.CreatePOIDTO());
            return Request.CreateResponse(HttpStatusCode.OK);

        }

    }
}
