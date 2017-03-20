﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy5.DAL.Interfaces
{
    public interface IPOIRepo
    {

        IQueryable<POI> GetPOIs();
        IQueryable<POI> GetPOIsByTour(int tourID);
        double GetPOIDistance(int poiID, double longitude, double latitude);


    }
}