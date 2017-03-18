using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TravelBuddy5.DAL;
using TravelBuddy5.DAL.Repositories;

namespace TravelBuddy5.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {

        private ITestRepo _testRepo;

        //lothnic1: Ctor without DI for Unit-Tests
        public ValuesController()
        {
            _testRepo = new TestRepo();
        }

        /*lothnic1: DI Ctor for loose coupling of Database Repository, theoretically a hard dependency would make sense for data repositories -> 
        rebuild to CTOR-DI. (Lazy-Binding Data Repository Manager that delivers needed Repo-Ctor).*/
        public ValuesController(ITestRepo testRepo)
        {
            _testRepo = testRepo;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { _testRepo.GetTestNumber().ToString() };
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
