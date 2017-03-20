using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class UserPOIRepo: RepoBase, IUserPOIRepo
    {

        public void CheckPOI(int poiID, int tourID)
        {
            //Create UserPOI Entry

            var userTourRepo = new UserTourRepo();
            var userTour = userTourRepo.GetUserTour(tourID, poiID);
            DB.UserPOI.Add(new UserPOI() { Date = DateTime.Now, FK_POI = poiID, FK_UserTour = userTour.Id });
        }


    }
}
