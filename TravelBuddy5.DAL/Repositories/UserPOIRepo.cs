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
        /// <summary>
        /// Initializes a new instance of the <see cref="UserPOIRepo"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public UserPOIRepo(Entities db) : base(db)
        {
        }

        /// <summary>
        /// Sets the poi as visited.
        /// </summary>
        /// <param name="poiID">The poi identifier.</param>
        /// <param name="userTourID">The user tour identifier.</param>
        /// <exception cref="System.Exception">POI already checked</exception>
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

        /// <summary>
        /// Gets the next poi.
        /// </summary>
        /// <param name="userTourId">The user tour identifier.</param>
        /// <returns></returns>
        public POI GetNextPOI(int userTourId)
        {
            var usertour = DB.UserTour.FirstOrDefault(t => t.Id == userTourId);
            return GetNextPOI(usertour);
        }

        /// <summary>
        /// Gets the next poi.
        /// </summary>
        /// <param name="usertour">The usertour.</param>
        /// <returns></returns>
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
