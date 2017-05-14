using System;

namespace TravelBuddy5.DAL.Repositories
{
    /// <summary>
    /// Base class for all repositories, defines fields and functionality for the db access
    /// </summary>
    public class RepoBase
    {
        private readonly Entities _db;
        
        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        protected Entities DB
        {
            get { return _db; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepoBase"/> class.
        /// </summary>
        /// <param name="db">The database.</param>
        public RepoBase(Entities db)
        {
            _db = db;
        }
    }
}
