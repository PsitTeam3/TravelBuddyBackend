using System;

namespace TravelBuddy5.DAL.Repositories
{
    public class RepoBase
    {
        private readonly Entities _db;
        protected Entities DB
        {
            get { return _db; }
        }

        public RepoBase(Entities db)
        {
            _db = db;
        }
    }
}
