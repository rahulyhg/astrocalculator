using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using astrocalc.api.models;

namespace astrocalc.api.Repos {
    public class CityRepo : ICity {

        public IEnumerable<City> Index() {
            return new List<City>() {
                new City() {Coordinates = new double[] { 18.5204, 73.8567},Title = "Pune" },
                new City() {Coordinates = new double[] { 17.3850 ,78.4867},Title = "Hyderabad" },
                new City() {Coordinates = new double[] { 30.7333, 76.7794},Title = "Chandigarh" },
                new City() {Coordinates = new double[] { 30.6544, 76.8452 },Title = "Dhakoli" },
                new City() {Coordinates = new double[] { 19.0760, 72.8777 },Title = "Mumbai" },
                new City() {Coordinates = new double[] { 30.7046, 76.7179 },Title = "Mohali" },
                new City() {Coordinates = new double[] { 12.2958, 76.6394 },Title = "Mysore" },
                new City() {Coordinates = new double[] { 12.9716, 77.5946 },Title = "Bangalore" },
                new City() {Coordinates = new double[] { 13.0827, 80.2707 },Title = "Chennai" },
                new City() {Coordinates = new double[] { 26.9124, 75.7873 },Title = "Jaipur" }
            };
        }

        public IEnumerable<City> Likely(string phrase) {
            return this.Index().Where(x => x.Title.ToLower().Contains(phrase.ToLower())).ToList();
        }

        public City OfId(int id) {
            throw new NotImplementedException();
        }

        public City OfId(string id) {
            return this.Index().Where(x => x.Title.ToLower() == id.ToLower()).FirstOrDefault();
        }
    }
}
