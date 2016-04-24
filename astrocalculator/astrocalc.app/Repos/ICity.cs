using astrocalc.app.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astrocalc.app.repos {
    public interface ICity : IGet<City>, IQueryable {
        Task<List<City>> OfState(string state);
    }
}
