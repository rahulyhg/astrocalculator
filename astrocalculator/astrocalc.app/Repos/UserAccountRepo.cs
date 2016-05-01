using astrocalc.app.storemodels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astrocalc.app.repos {
   public class UserAccountRepo :AnyRepo, IUserAccount {
        //this is the collection that represents all the useraccounts from the database
        protected IMongoCollection<UserAccount> _accounts;

        public UserAccountRepo() {
            if (_dbase!=null) {
                //here is to getting the user accounts from the database
                _accounts = _dbase.GetCollection<UserAccount>("useraccounts");
            }
        }

        public Task<IEnumerable<UserAccount>> Index(int skip = 0, int top = 20) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserAccount>> Likely(string phrase) {
            throw new NotImplementedException();
        }

        public async Task<UserAccount> Login(string username, string pin) {
            //both the filters have to be matched in case you want the user to login successfully 
            var filter = Builders<UserAccount>.Filter.And(new List<FilterDefinition<UserAccount>>() {
                Builders<UserAccount>.Filter.Eq(x=>x.username,  username),
                Builders<UserAccount>.Filter.Eq(x=>x.pin,  pin),
            });
            try {
                return await _accounts.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex) {
                //this is the case when there was a problem on the store
                throw ex;
            }
        }
        public async Task<UserAccount> OfId(string id) {
            var filter = Builders<UserAccount>.Filter.Eq(x => x.username, id);
            try {
                return await _accounts.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex) {
                //this is the case when there was a problem on the store
                throw ex;
            }
        }
        public Task<UserAccount> OfId(int id) {
            throw new NotImplementedException();
        }

        
    }
}
