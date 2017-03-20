using System;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
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
