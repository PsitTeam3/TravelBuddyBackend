using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy5.Models
{
    public class UserTourDTO
    {

        public int Id { get; set; }
        public int FK_Tour { get; set; }
        public int FK_User { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }

    }
}
