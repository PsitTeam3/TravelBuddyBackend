using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBuddy5.DAL.Interfaces;
using TravelBuddy5.DAL.Repositories;

namespace TravelBuddy5.DAL.Repositories
{
    public class UserTourRepo: RepoBase, IUserTourRepo
    {

        public void StartUserTour(int userID, int tourID)
        {
            // This is wrong, no? User should be able to do the same tour multiple times if he likes... 
            /*if(DB.UserTour.Where(ut => ut.FK_Tour == tourID && ut.FK_User == userID).Count() > 0)
            {
                throw new Exception("User has already started this tour");
            }*/

            //Constraint that user can only start one tour at the time
            if(GetActiveTour(userID) != null)
            {
                throw new Exception("User has already started a tour");
            }

            DB.UserTour.Add(new UserTour() { StartDate = DateTime.Now, FK_Tour = tourID, FK_User = userID });
            DB.SaveChanges();
        }

        public void EndUserTour(int userID)
        {
            var tour = DB.UserTour.FirstOrDefault(ut => ut.FK_User == userID && ut.EndDate == null);

            if (tour == null)
            {
                throw new Exception("User hasn't an active tour");
            }

            tour.EndDate = DateTime.Now;

            DB.SaveChanges();
        }

        public bool CheckTourComplete(int userTourId)
        {
            var userPOIRepo = new UserPOIRepo();
            return userPOIRepo.GetNextPOI(userTourId) == null;
        }

        public UserTour GetUserTour(int userTour)
        {
            return DB.UserTour.FirstOrDefault(ut => ut.Id == userTour);
        }

        public UserTour GetUserTour(int tourID, int userID)
        {
            return DB.UserTour.FirstOrDefault(ut => ut.FK_Tour == tourID && ut.FK_User == userID);
        }

        public UserTour GetActiveTour(int userID)
        {
            return DB.UserTour.FirstOrDefault(ut => ut.EndDate == null && ut.FK_User == userID);
        }
    }
}
