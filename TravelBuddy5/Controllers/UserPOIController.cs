using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TravelBuddy5.DAL.Interfaces;

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
        [Route("api/UserPOI/CheckPOI")]
        public HttpResponseMessage CheckPOI(int poiID, int userTourID)
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
    }
}
