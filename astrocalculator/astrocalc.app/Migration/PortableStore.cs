using astrocalc.app.storemodels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astrocalc.app.Migration {
    public class PortableStore {
        protected IMongoClient targetclient = new MongoClient("mongodb://vmkneeruazure.cloudapp.net:27017");
        protected IMongoClient sourceclient = new MongoClient("mongodb://localhost:27017");

        public async Task TransferDatabase() {
            var targetdb = targetclient.GetDatabase("astrocalc");
            var sourcedb = sourceclient.GetDatabase("astrocalc");
            //getting all the cities uploaded
            List<City> allcities =  await sourcedb.GetCollection<City>("cities").Find(Builders<City>.Filter.Empty).ToListAsync();
            await targetdb.GetCollection<City>("cities").InsertManyAsync(allcities);

            List<UserAccount> allUserAccounts = await sourcedb.GetCollection<UserAccount>("useraccounts").Find(Builders<UserAccount>.Filter.Empty).ToListAsync();
            await targetdb.GetCollection<UserAccount>("useraccounts").InsertManyAsync(allUserAccounts);

            List<Zenith> allZeniths = await sourcedb.GetCollection<Zenith>("zeniths").Find(Builders<Zenith>.Filter.Empty).ToListAsync();
            await targetdb.GetCollection<Zenith>("zeniths").InsertManyAsync(allZeniths);
        }
    }
}
