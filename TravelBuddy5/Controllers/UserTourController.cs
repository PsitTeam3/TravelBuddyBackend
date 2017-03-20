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

        public void StartUserTour(int userID, int tourID)
        {
            _repo.StartUserTour(userID, tourID);
        }

        public void EndUserTour(int userID, int tourID)
        {
            _repo.EndUserTour(userID, tourID);
        }
    }
}
