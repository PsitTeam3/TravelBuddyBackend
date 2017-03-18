using System;

namespace TravelBuddy5.DAL.Repositories
{
    public class RepoBase : IDisposable
    {

        private TravelBuddyEntities _db;
        public TravelBuddyEntities DB
        {
            get { return _db; }
            set { _db = value; }
        }

        public RepoBase()
        {
            _db = new TravelBuddyEntities();
        }

        public void Dispose()
        {
            _db.SaveChanges();
            _db.Dispose();
        }
    }
}
