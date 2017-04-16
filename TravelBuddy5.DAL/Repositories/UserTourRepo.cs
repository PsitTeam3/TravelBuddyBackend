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
            if(DB.UserTour.Where(ut => ut.FK_Tour == tourID && ut.FK_User == userID).Count() > 0)
            {
                throw new Exception("User has already started this tour");
            }

            //Constraint that user can only start one tour at the time
            if(GetActiveTour(userID) != null)
            {
                throw new Exception("User has already started a tour");
            }

            DB.UserTour.Add(new UserTour() { StartDate = DateTime.Now, FK_Tour = tourID, FK_User = userID });
            DB.SaveChanges();
        }

        public void EndUserTour(int userID, int tourID)
        {
            var tour = DB.UserTour.FirstOrDefault(ut => ut.FK_Tour == tourID && ut.FK_User == tourID);

            if (tour == null)
            {
                throw new Exception("User hasn't started this tour yet");
            }

            tour.EndDate = DateTime.Now;

            DB.SaveChanges();
        }

        public bool CheckTourComplete(int userTourId)
        {
            var userPOIRepo = new UserPOIRepo();
            return !(userPOIRepo.GetNextPOI(userTourId).Value == null);
           
        }

        public RepoObject<UserTour> GetUserTour(int userTour)
        {
            return new RepoObject<UserTour>(DB.UserTour.Where(ut => ut.Id == userTour));
        }


        public RepoObject<UserTour> GetUserTour(int tourID, int userID)
        {
            return new RepoObject<UserTour>(DB.UserTour.Where(ut => ut.FK_Tour == tourID && ut.FK_User == userID));
        }

        public RepoObject<UserTour> GetActiveTour(int userID)
        {
            return new RepoObject<UserTour>(DB.UserTour.Where(ut => ut.EndDate == null && ut.FK_User == userID));
        }
    }
}
