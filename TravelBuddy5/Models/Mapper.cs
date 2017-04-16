using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy5.Models
{
    public static class Mapper
    {

        public static T Map<T>(object data) where T : class, new()
        {
            T t = new T();
            foreach (PropertyInfo pi in data.GetType().GetProperties())
            {
                var prop = t.GetType().GetProperty(pi.Name);
                if (prop != null)
                {
                    if (prop.GetSetMethod() != null)
                    {
                        prop.SetValue(t, pi.GetValue(data));
                    }
                }
            }

            return t;
        }

        public static T Map<T, S>(this S data) where T : class, new() where S : class, new()
        {
            return Map<T>(data);
        }

        public static IEnumerable<S> Map<T, S>(this IEnumerable<T> data) where S : class, new()
        {
            List<S> result = new List<S>();
            foreach (T entry in data)
            {
                result.Add(Map<S>(entry));
            }
            return result as IEnumerable<S>;
        }
    }
}
