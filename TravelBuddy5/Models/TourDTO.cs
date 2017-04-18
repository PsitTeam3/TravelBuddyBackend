using System.Collections.Generic;

namespace TravelBuddy5.Models
{
    public class TourDTO
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string DetailDescription { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Image { get; set; }
    }
}