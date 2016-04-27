using astrocalc.app.storemodels;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace astrocalc.app.repos {
    public class CityRepo : AnyRepo, ICity {
        protected IMongoCollection<City> _cities;
        public CityRepo() {
            if (_dbase != null) {
                _cities = _dbase.GetCollection<City>("cities");
            }
        }
        /// <summary>
        /// this would allow us to create a new location based on the user input
        /// </summary>
        /// <param name="toCreate"></param>
        /// <returns></returns>
        public async Task<City> Create(City toCreate) {
            var likely = _cities.Find(Builders<City>.Filter.Regex(x => x.city, new BsonRegularExpression(new Regex(toCreate.city, RegexOptions.IgnoreCase)))).ToList<City>();
            var duplicate = likely.Where(x => x.city.ToLower() == toCreate.city.ToLower()).FirstOrDefault();
            duplicate = await _cities.Find(Builders<City>.Filter.And(new List<FilterDefinition<City>>() {
                Builders<City>.Filter.Eq(x=>x.latitude, toCreate.latitude),
                Builders<City>.Filter.Eq(x=>x.longitude, toCreate.longitude)
            })).FirstOrDefaultAsync();
            if (duplicate == null) {
                //this where we can go ahead to create the city in the database
                try {
                    await _cities.InsertOneAsync(toCreate);
                    return await _cities.Find(Builders<City>.Filter.Eq(x => x.city, toCreate.city)).FirstOrDefaultAsync();
                }
                catch (Exception ex) {

                    throw ex;
                }   
            }
            else {
                throw new ArgumentException(String.Format("We already have a location for the co-ordinates, you can pick the same by searching for it"));
            }
        }

        public async Task<IEnumerable<City>> Index(int skip = 0, int top = 20) {
            if (skip >= 0 && top >= 0) {
                var filter = Builders<City>.Filter.Empty;
                try {
                    return await _cities.Find(filter).Skip(skip).Limit(top).ToListAsync<City>();
                }
                catch (Exception ex) {
                    throw ex;
                }
            }
            else {
                throw new ArgumentException(String.Format("Pagination params cannot be null"));
            }
        }
        public async Task<IEnumerable<City>> Likely(string phrase) {
            if (!string.IsNullOrEmpty(phrase)) {
                var filter = Builders<City>.Filter.Regex(x => x.city, new BsonRegularExpression(new Regex(phrase, RegexOptions.IgnoreCase)));
                var fltState = Builders<City>.Filter.Regex(x => x.state, new BsonRegularExpression(new Regex(phrase, RegexOptions.IgnoreCase)));
                var orFilter = Builders<City>.Filter.Or(new List<FilterDefinition<City>>() {
                    filter, fltState
                });
                try {
                    return await _cities.Find(orFilter).ToListAsync();
                }
                catch (Exception ex) {
                    throw ex; 
                }
            }
            else {
                throw new ArgumentException(String.Format("phrase to search for cannot be null or empty"));
            }
        }
        public City OfId(int id) {
            throw new NotImplementedException();
        }
        public async Task<City> OfId(string id) {
            var filter = Builders<City>.Filter.Eq(x => x.id, new BsonObjectId(new ObjectId(id)));
            try {
                return await _cities.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public async Task<List<City>> OfState(string state) {
            //this woudl form the approximate filter of the state
            var stateFilter = Builders<City>.Filter.Regex(x => x.state, new BsonRegularExpression(new Regex(state, RegexOptions.IgnoreCase)));
            var approxResults = await _cities.Find(stateFilter).ToListAsync<City>();
            var exactResults = approxResults.Where(x => x.state.ToLower() == state.ToLower()).ToList();
            return exactResults;
        }
        //gets all the states from the database of the locations
        public async Task<List<string>> States() {
            //this is when we are getting the states all unique grouped
            var states= await _cities.Aggregate()
                .Group(new BsonDocument { new BsonElement("_id", "$state")})
                .ToListAsync<BsonDocument>();
           return states.Select(x => x.Elements.First().Value.ToString()).OrderBy(x=>x).ToList<string>();
        }

        Task<City> IGet<City>.OfId(int id) {
            throw new NotImplementedException();
        }
    }
}
