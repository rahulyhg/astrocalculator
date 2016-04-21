using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astrocalc.api.Repos
{
    public interface IGet<T>
    {
        IEnumerable<T> Index();
        T OfId(string id);
        T OfId(int id);
        IEnumerable<T> Likely(string phrase);
    }
}
