using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy5.Models
{
    public class UserTourDTO
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the fk tour.
        /// </summary>
        /// <value>
        /// The fk tour.
        /// </value>
        public int FK_Tour { get; set; }
        
        /// <summary>
        /// Gets or sets the fk user.
        /// </summary>
        /// <value>
        /// The fk user.
        /// </value>
        public int FK_User { get; set; }
        
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public System.DateTime StartDate { get; set; }
        
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public Nullable<System.DateTime> EndDate { get; set; }

    }
}
