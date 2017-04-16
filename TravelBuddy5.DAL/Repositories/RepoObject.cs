using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy5.DAL.Repositories
{
    public class RepoObject<T>
    {

        public T Value { get;}
        public string Message {get; }

        public RepoObject(T value, string message)
        {
            Value = value;
            Message = message;
        }

    }
}
