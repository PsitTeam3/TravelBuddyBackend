using System;
using TravelBuddy5.DAL.Interfaces;

namespace TravelBuddy5.DAL.Repositories
{
    /// <summary>
    /// Only for testing the repo-database pipeline
    /// </summary>
    /// <seealso cref="TravelBuddy5.DAL.Interfaces.ITestRepo" />
    public class TestRepo:ITestRepo
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRepo"/> class.
        /// </summary>
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
