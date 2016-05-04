
using MongoDB.Driver;

namespace astrocalc.app.repos {
    public class AnyRepo
    {
        protected IMongoClient _client;
        protected IMongoDatabase _dbase;
        public AnyRepo() {
            _client = new MongoClient("mongodb://localhost:27017");
            //_client = new MongoClient("mongodb://vmkneeruazure.cloudapp.net:27017");
            _dbase = _client.GetDatabase("astrocalc");  
        }
    }
}
