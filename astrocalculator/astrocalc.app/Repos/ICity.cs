using astrocalc.app.storemodels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace astrocalc.app.repos {
    public interface ICity : IGet<City>, IQueried {
        Task<List<City>> OfState(string state);
    }
}
