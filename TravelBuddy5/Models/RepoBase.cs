using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBuddy5.DAL;

namespace TravelBuddy5.Models
{
    public class RepoBase
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


        ~RepoBase()
        {
            _db.SaveChanges();
            _db.Dispose();

        }

    }
}
