using astrocalc.app.repos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using astrocalc.app.storemodels;
using MongoDB.Driver;

namespace astrocalc.app.repos {
    public class MonthRepo : AnyRepo, IMonth {
        protected IMongoCollection<Month> _months;
        public MonthRepo() {
            if (_dbase!=null) {
                _months = _dbase.GetCollection<Month>("months");
            }
        }
        public async Task<IEnumerable<Month>> Index(int skip = 0, int top = 20) {
            //this is just to get all the months from the database
            return await _months.Find(Builders<Month>.Filter.Empty).ToListAsync<Month>();
        }

        public Task<IEnumerable<Month>> Likely(string phrase) {
            throw new NotImplementedException();
        }

        public Task<Month> OfId(int id) {
            throw new NotImplementedException();
        }

        public Task<Month> OfId(string id) {
            throw new NotImplementedException();
        }
    }
}
