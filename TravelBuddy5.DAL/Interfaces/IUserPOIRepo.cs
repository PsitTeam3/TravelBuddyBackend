using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy5.DAL.Interfaces
{
    public interface IUserPOIRepo
    {
        void CheckPOI(int poiID, int tourID);

    }
}
