using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class UserTourRepo: RepoBase, IUserTourRepo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserTourRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public UserTourRepo(Entities db) : base(db)
        {
        }

        /// <summary>
        /// Starts a specific tour for a given user -&gt; creates a UserTour.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="tourID">The tour identifier.</param>
        /// <exception cref="System.Exception">User has already started a tour</exception>
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

        /// <summary>
        /// Ends the current active UserTour.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <exception cref="System.Exception">User hasn't an active tour</exception>
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

        /// <summary>
        /// Gets the user tour.
        /// </summary>
        /// <param name="userTour">The user tour.</param>
        /// <returns></returns>
        public UserTour GetUserTour(int userTour)
        {
            return DB.UserTour.FirstOrDefault(ut => ut.Id == userTour);
        }

        /// <summary>
        /// Gets the user tour.
        /// </summary>
        /// <param name="tourID">The tour identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// Data object of the specific UserTour for the given user
        /// </returns>
        public UserTour GetUserTour(int tourID, int userID)
        {
            return DB.UserTour.FirstOrDefault(ut => ut.FK_Tour == tourID && ut.FK_User == userID);
        }

        /// <summary>
        /// Gets the active tour of a given user
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// Data object of the active UserTour for the given user, or null if none is started
        /// </returns>
        public UserTour GetActiveTour(int userID)
        {
            return DB.UserTour.FirstOrDefault(ut => ut.EndDate == null && ut.FK_User == userID);
        }
    }
}
