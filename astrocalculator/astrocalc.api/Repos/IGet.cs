using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astrocalc.api.Repos
{
    public interface IGet<T>
    {
        /// <summary>
        /// this woudl get the index of all the items of the type
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Index();
        /// <summary>
        /// this gets the entity of the type given the id of the entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T OfId(string id);
        T OfId(int id);
        /// <summary>
        /// find the likely entity
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        IEnumerable<T> Likely(string phrase);
    }
}
