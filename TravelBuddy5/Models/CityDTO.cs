using System;
using System.Linq.Expressions;
using TravelBuddy5.DAL;

namespace TravelBuddy5.Models
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public static Expression<Func<City, CityDTO>> Create()
        {
            return city => new CityDTO { Id = city.Id, Name = city.Name };
        }
    }
}