using System;

namespace TravelBuddy5.Models
{
    public class PointOfInterestDTO
    {
        public int Id { get; set; } 
        public string Description { get; set; }
        public TimeSpan VisitDuration { get; set; }
    }
}