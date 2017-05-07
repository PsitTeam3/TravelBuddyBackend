using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TravelBuddy5.DAL;

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

        public static Expression<Func<Tour, TourDTO>> Create()
        {
            return tour => new TourDTO
            {
                Id = tour.Id,
                Name = tour.Name,
                City = tour.City.Name,
                Country = tour.City.Country.Name,
                Description = tour.Description,
                DetailDescription = tour.DetailDescription,
                Image = tour.Image
            };
        }

    }
}