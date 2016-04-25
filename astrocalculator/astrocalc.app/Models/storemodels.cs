using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace astrocalc.app.storemodels {
    public class City {
        [BsonId]
        public BsonObjectId _id { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string nation { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }

    }
    public class Month {
        [BsonId]
        public string id { get; set; }
        public int pos { get; set; }
    }
}