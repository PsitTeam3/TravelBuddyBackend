using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy5.DAL.Repositories
{
    public class RepoObject<T>
    {

        public IQueryable<T> Value { get;}
        public string Message {get; }

        public RepoObject(IQueryable<T> value)
        {
            Value = value;
            Message = string.Empty;
        }

        public RepoObject(IQueryable<T> value, string message)
        {
            Value = value;
            Message = message;
        }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(Message); }
        }

    }
}
