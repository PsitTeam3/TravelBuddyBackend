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

        void StartUserTour(int userID, int tourID);
        void EndUserTour(int userID);
        RepoObject<UserTour> GetUserTour(int tourID, int userID);
        RepoObject<UserTour> GetActiveTour(int userID);
    }
}
