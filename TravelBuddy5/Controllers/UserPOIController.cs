using System;
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
        private readonly IUserTourRepo _userTourRepo;

        public UserPOIController(IUserPOIRepo userPOIRepo, IUserTourRepo userTourRepo)
        {
            _repo = userPOIRepo;
            _userTourRepo = userTourRepo;
        }

        [HttpGet]
        [Route("api/UserPOI/CheckUserTourPOI")]
        public HttpResponseMessage CheckUserTourPOI(int poiID, int userTourID)
        {
            try
            { 
                _repo.SetPOIAsVisited(poiID, userTourID);
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
                _repo.SetPOIAsVisited(poiID, tourID, userID);
            }
            catch (Exception ex)
            {
                HttpError err = new HttpError(ex.Message);
                return Request.CreateResponse(HttpStatusCode.Conflict, err);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/UserPOI/SetNextPOIAsVisited")]
        public HttpResponseMessage SetNextPOIAsVisited(int userID)
        {
            try
            {
                UserTour userTour = _userTourRepo.GetActiveTour(userID).Value.First();
                POI currentPoi = _repo.GetNextPOI(userTour).Value.First();
                _repo.SetPOIAsVisited(currentPoi.Id, userTour.Id);
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
        public HttpResponseMessage GetNextPOI(int userId)
        {
            var userTour = _userTourRepo.GetActiveTour(userId).Value.First();
            var nextPOI = _repo.GetNextPOI(userTour);
            if(nextPOI.Value == null)
            {
                HttpError err = new HttpError(nextPOI.Message);
                return Request.CreateErrorResponse(HttpStatusCode.Conflict, err);
            }

            PointOfInterestDTO res = nextPOI.Value.Select(POIMapper.CreatePOIDTO()).First();
            return Request.CreateResponse(HttpStatusCode.OK, res);

        }
    }
}
