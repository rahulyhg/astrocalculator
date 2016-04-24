using System.Collections.Generic;
using System.Threading.Tasks;

namespace astrocalc.app.repos {
    public interface IGet<T>
    {
        Task<IEnumerable<T>> Index(int skip = 0, int top = 20);
        Task<T> OfId(string id);
        Task<T> OfId(int id);
        /// <summary>
        /// find the likely entity
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> Likely(string phrase);
    }
}
