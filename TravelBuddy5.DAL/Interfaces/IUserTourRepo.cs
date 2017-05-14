using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBuddy5.DAL.Repositories;

namespace TravelBuddy5.DAL.Interfaces
{
    public interface IUserTourRepo
    {

        /// <summary>
        /// Starts a specific tour for a given user -> creates a UserTour.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="tourID">The tour identifier.</param>
        void StartUserTour(int userID, int tourID);
        
        /// <summary>
        /// Ends the current active UserTour.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        void EndUserTour(int userID);

        /// <summary>
        /// Gets the user tour.
        /// </summary>
        /// <param name="tourID">The tour identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>Data object of the specific UserTour for the given user</returns>
        UserTour GetUserTour(int tourID, int userID);

        /// <summary>
        /// Gets the active tour of a given user
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>Data object of the active UserTour for the given user, or null if none is started</returns>
        UserTour GetActiveTour(int userID);
    }
}
