using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBuddy5.DAL.Repositories;

namespace TravelBuddy5.DAL.Interfaces
{
    public interface IUserPOIRepo
    {
        void SetPOIAsVisited(int poiID, int tourID);
        void SetPOIAsVisited(int poiID, int tourID, int userID);

        RepoObject<POI> GetNextPOI(int userTourId);
        RepoObject<POI> GetNextPOI(UserTour usertour);
    }
}
