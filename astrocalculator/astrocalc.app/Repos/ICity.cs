using astrocalc.app.storemodels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace astrocalc.app.repos {
    public interface ICity : IGet<City>, IPost<City>,IQueried {
        Task<List<City>> OfState(string state);
        Task<List<string>> States();
    }
}
