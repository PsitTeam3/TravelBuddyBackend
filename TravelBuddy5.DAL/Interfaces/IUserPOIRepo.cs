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
        void CheckPOI(int poiID, int tourID);
        void CheckPOI(int poiID, int tourID, int userID);

        RepoObject<POI> GetNextPOI(int userTourId);
    }
}
