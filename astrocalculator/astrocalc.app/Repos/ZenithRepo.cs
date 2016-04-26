using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using astrocalc.app.storemodels;
using MongoDB.Driver;

namespace astrocalc.app.repos {
    public class ZenithRepo : AnyRepo, IZenith {
        IMongoCollection<Zenith> _zeniths;
        public ZenithRepo() {
            if (_dbase != null) {
                _zeniths = _dbase.GetCollection<Zenith>("zeniths");
            }
        }
        public async Task<IEnumerable<Zenith>> Index(int skip = 0, int top = 20) {
            return await _zeniths.Find(Builders<Zenith>.Filter.Empty).ToListAsync<Zenith>();
        }

        public Task<IEnumerable<Zenith>> Likely(string phrase) {
            throw new NotImplementedException();
        }

        public Task<Zenith> OfId(int id) {
            throw new NotImplementedException();
        }

        public Task<Zenith> OfId(string id) {
            throw new NotImplementedException();
        }
    }
}
