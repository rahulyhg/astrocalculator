using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace astrocalc.app.repos
{
    public class Repo
    {
        //this would store all the registered implementations
        protected List<IQueried> implementations  = new List<IQueried>();

        public Repo() {
            //registering all the implementations
            implementations.AddRange(new List<IQueried>() {
                new CityRepo(),
                new MonthRepo(),
                new ZenithRepo()
            });
        }
        public T QueryInterface<T>() {
            //we need to query the object for the implementation and then send back to the client
            if (typeof(T).GetInterfaces().Where(x => x.Name == "IQueried").FirstOrDefault() != null) {
               return (T)(implementations.Where(x => x.GetType().GetInterfaces().Where(i => i.Name == typeof(T).Name).Count() != 0).FirstOrDefault());
            }
            else {
                throw new ArgumentException(String.Format("Interface of type {0} not queryable over this object", typeof(T)));
            }
        }
    }
}
