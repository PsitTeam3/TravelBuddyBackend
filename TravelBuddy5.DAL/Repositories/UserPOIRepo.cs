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
        public void SetPOIAsVisited(int poiID, int userTourID)
        {
            //TODO: Check if POI belongs to tour
            if(DB.UserPOI.Where(up => up.FK_POI == poiID && up.FK_UserTour == userTourID).Count()>0)
            {
                throw new Exception("POI already checked");
            }

            DB.UserPOI.Add(new UserPOI() { Date = DateTime.Now, FK_POI = poiID, FK_UserTour = userTourID });
        }

        public void SetPOIAsVisited(int poiID, int tourID, int userID)
        {
            var userTourRepo = new UserTourRepo();
            var userTour = userTourRepo.GetUserTour(tourID, userID);
            SetPOIAsVisited(poiID, userTour.Value.First().Id);
        }

        public RepoObject<POI> GetNextPOI(int userTourId)
        {
            var usertour = DB.UserTour.FirstOrDefault(t => t.Id == userTourId);
            return GetNextPOI(usertour);
        }

        public RepoObject<POI> GetNextPOI(UserTour usertour)
        {
            //Get TourPOIs for tour and for all UserPOIs
            var userpois =
                DB.TourPOI.Join(DB.UserPOI, tp => tp.FK_POI, up => up.FK_POI, (tp, up) => new {TourPOI = tp, UserPOI = up})
                    .Where((d) => d.TourPOI.FK_POI == d.UserPOI.FK_POI && d.UserPOI.FK_UserTour == usertour.Id)
                    .Select(d => d.TourPOI);
            var tourpois = DB.TourPOI.Where(tp => tp.FK_Tour == usertour.FK_Tour);

            //Get the next POI by order that is in the tour but not in the UserPOIs yet
            var poi = Enumerable.Except<TourPOI>(tourpois, userpois).OrderBy(tp => tp.Order);

            if (poi.Count() == 0)
            {
                return new RepoObject<POI>(null, "No unchecked POI in current tour left");
            }

            var poiid = poi.First().FK_POI;
            //Get POI
            var res = DB.POI.Where(p => p.Id == poiid);
            if (res.Count() == 0)
            {
                return new RepoObject<POI>(res, "POI not found");
            }

            return new RepoObject<POI>(res);
        }
    }
}
