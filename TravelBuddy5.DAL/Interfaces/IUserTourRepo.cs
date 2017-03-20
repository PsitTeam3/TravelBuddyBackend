using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy5.DAL.Interfaces
{
    public interface IUserTourRepo
    {

        void StartUserTour(int userID, int tourID);
        void EndUserTour(int userID, int tourID);
        UserTour GetUserTour(int tourID, int userID);
    }
}
