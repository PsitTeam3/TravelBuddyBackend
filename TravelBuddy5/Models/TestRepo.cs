using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBuddy5.DAL;

namespace TravelBuddy5.Repositories
{
    public class TestRepo:ITestRepo
    {

        public TestRepo()
        {
                
        }

        public int GetTestNumber()
        {
            return new Random().Next(0, 10);
        }

        public void SetTestNumber(int number)
        {
            return;
        }

    }
}
