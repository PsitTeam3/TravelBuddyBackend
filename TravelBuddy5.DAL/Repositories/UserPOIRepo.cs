﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    public class UserPOIRepo : RepoBase, IUserPOIRepo
    {
        public UserPOIRepo(Entities db) : base(db)
        {
        }

        public void SetPOIAsVisited(int poiID, int userTourID)
        {
            //TODO: Check if POI belongs to tour
            if(DB.UserPOI.Where(up => up.FK_POI == poiID && up.FK_UserTour == userTourID).Count()>0)
            {
                throw new Exception("POI already checked");
            }

            DB.UserPOI.Add(new UserPOI() { Date = DateTime.Now, FK_POI = poiID, FK_UserTour = userTourID });
            DB.SaveChanges();
        }

        public POI GetNextPOI(int userTourId)
        {
            var usertour = DB.UserTour.FirstOrDefault(t => t.Id == userTourId);
            return GetNextPOI(usertour);
        }

        public POI GetNextPOI(UserTour usertour)
        {
            //Get TourPOIs for tour and for all UserPOIs
            var userpois =
                DB.TourPOI.Join(DB.UserPOI, tp => tp.FK_POI, up => up.FK_POI, (tp, up) => new {TourPOI = tp, UserPOI = up})
                    .Where((d) => d.TourPOI.FK_POI == d.UserPOI.FK_POI && d.UserPOI.FK_UserTour == usertour.Id)
                    .Select(d => d.TourPOI);
            var tourpois = DB.TourPOI.Where(tp => tp.FK_Tour == usertour.FK_Tour);

            //Get the next POI by order that is in the tour but not in the UserPOIs yet
            var remainingTourPois = Enumerable.Except<TourPOI>(tourpois, userpois).OrderBy(tp => tp.Order);

            POI nextPoi;
            if (remainingTourPois.Any())
            {
                var poiID = remainingTourPois.First().FK_POI;

                //Get POI
                nextPoi = DB.POI.First(p => p.Id == poiID);
            }
            else
            {
                nextPoi = null;
            }

            return nextPoi;
        }
    }
}
