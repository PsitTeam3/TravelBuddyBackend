using System;
using System.Linq.Expressions;
using TravelBuddy5.DAL;

namespace TravelBuddy5.Models
{
    public class CityDTO
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
        /// Creates the mapping lambda expression.
        /// </summary>
        /// <returns>Lambda expression for the mapping</returns>
        public static Expression<Func<City, CityDTO>> Create()
        {
            return city => new CityDTO { Id = city.Id, Name = city.Name };
        }
    }
}