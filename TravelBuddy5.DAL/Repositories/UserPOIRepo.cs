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

        public RepoObject<POI> GetNextPOI(int userTourId)
        {
            var usertour = DB.UserTour.FirstOrDefault(t => t.Id == userTourId);

            //Get TourPOIs for tour and for all UserPOIs
            var userpois = DB.TourPOI.Join(DB.UserPOI, tp => tp.FK_POI, up => up.FK_POI, (tp, up) => new { TourPOI = tp, UserPOI = up }).Where((d) => d.TourPOI.FK_POI == d.UserPOI.FK_POI && d.UserPOI.FK_UserTour == usertour.Id).Select(d => d.TourPOI);
            var tourpois = DB.TourPOI.Where(tp => tp.FK_Tour == usertour.FK_Tour);

            //Get the next POI by order that is in the tour but not in the UserPOIs yet
            var poi = Enumerable.Except<TourPOI>(tourpois, userpois).OrderBy(tp => tp.Order).FirstOrDefault();

            if(poi == null)
            {
                return new RepoObject<POI>(null, "No unchecked POI in current tour left");
            }

            //Get POI
            var res = DB.POI.Where(p => p.Id == poi.FK_POI).FirstOrDefault();
            if (res == null)
            {
                return new RepoObject<POI>(null, "POI not found");
            }

            return new RepoObject<POI>(res, string.Empty);
        }


    }
}
