using System;

namespace TravelBuddy5.DAL.Repositories
{
    public class RepoBase : IDisposable
    {

        private Entities _db;
        public Entities DB
        {
            get { return _db; }
            set { _db = value; }
        }

        public RepoBase()
        {
            _db = new Entities();
        }

        public void Dispose()
        {
            _db.SaveChanges();
            _db.Dispose();
        }
    }
}
