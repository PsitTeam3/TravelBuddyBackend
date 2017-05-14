using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TravelBuddy5.DAL;

namespace TravelBuddy5.Models
{
    public class TourDTO
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the detail description.
        /// </summary>
        /// <value>
        /// The detail description.
        /// </value>
        public string DetailDescription { get; set; }
        
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }
        
        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }
        
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public string Image { get; set; }

        /// <summary>
        /// Creates the mapping lambda expression.
        /// </summary>
        /// <returns>Lambda expression for the mapping</returns>
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