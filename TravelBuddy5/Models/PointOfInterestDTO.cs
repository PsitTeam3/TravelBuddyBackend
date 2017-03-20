using System;

namespace TravelBuddy5.Models
{
    public class PointOfInterestDTO
    {
        public int Id { get; set; } 
        public string Description { get; set; }
        public TimeSpan VisitDuration { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}