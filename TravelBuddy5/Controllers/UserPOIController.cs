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

        public void CheckPOI(int poiID, int tourID)
        {
            _repo.CheckPOI(poiID, tourID);
        }
    }
}
