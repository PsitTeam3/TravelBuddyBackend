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

        /// <summary>
        /// Maps the specified data.
        /// </summary>
        /// <typeparam name="T">Data object type to be created</typeparam>
        /// <param name="data">The data to be mapped to the given type T</param>
        /// <returns></returns>
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

        /// <summary>
        /// Type safe generic variant of the map method.
        /// </summary>
        /// <typeparam name="T">Data object type to be created</typeparam>
        /// <typeparam name="S">Data object type of the input value</typeparam>
        /// <param name="data">The mapped data.</param>
        /// <returns></returns>
        public static T Map<T, S>(this S data) where T : class, new() where S : class, new()
        {
            return Map<T>(data);
        }

        /// <summary>
        /// Type safe generic variant of the map method for a enumerable collection of data objects.
        /// </summary>
        /// <typeparam name="T">Data object type to be created</typeparam>
        /// <typeparam name="S">Data object type of the input value</typeparam>
        /// <param name="data">The mapped data.</param>
        /// <returns></returns>
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
