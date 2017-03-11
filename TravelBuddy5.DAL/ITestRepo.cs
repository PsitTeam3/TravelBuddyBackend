using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy5.DAL
{
    public interface ITestRepo
    {

        int GetTestNumber();
        void SetTestNumber(int number);

    }
}
