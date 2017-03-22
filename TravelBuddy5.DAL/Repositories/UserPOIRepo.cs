using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class UserPOIRepo : RepoBase, IUserPOIRepo
    {

        public void CheckPOI(int poiID, int userTourID)
        {
            //TODO: Check if POI belongs to tour
            if(DB.UserPOI.Where(up => up.FK_POI == poiID && up.FK_UserTour == userTourID).Count()>0)
            {
                throw new Exception("POI already checked");
            }

            DB.UserPOI.Add(new UserPOI() { Date = DateTime.Now, FK_POI = poiID, FK_UserTour = userTourID });
        }

        public void CheckPOI(int poiID, int tourID, int userID)
        {
            var userTourRepo = new UserTourRepo();
            var userTour = userTourRepo.GetUserTour(tourID, userID);
            CheckPOI(poiID, userTour.Id);
        }
    }
}
