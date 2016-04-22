using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using astrocalc.api.models;

namespace astrocalc.api.Repos {
    public class CityRepo : ICity {

        public IEnumerable<City> Index() {
            return new List<City>() {
                new City() {coordinates = new double[] { 18.5204, 73.8567},title = "Pune" },
                new City() {coordinates = new double[] { 17.3850 ,78.4867},title = "Hyderabad" },
                new City() {coordinates = new double[] { 30.7333, 76.7794},title = "Chandigarh" },
                new City() {coordinates = new double[] { 30.6544, 76.8452 },title = "Dhakoli" },
                new City() {coordinates = new double[] { 19.0760, 72.8777 },title = "Mumbai" },
                new City() {coordinates = new double[] { 30.7046, 76.7179 },title = "Mohali" },
                new City() {coordinates = new double[] { 12.2958, 76.6394 },title = "Mysore" },
                new City() {coordinates = new double[] { 12.9716, 77.5946 },title = "Bangalore" },
                new City() {coordinates = new double[] { 13.0827, 80.2707 },title = "Chennai" },
                new City() {coordinates = new double[] { 26.9124, 75.7873 },title = "Jaipur" }
            };
        }

        public IEnumerable<City> Likely(string phrase) {
            return this.Index().Where(x => x.title.ToLower().Contains(phrase.ToLower())).ToList();
        }

        public City OfId(int id) {
            throw new NotImplementedException();
        }

        public City OfId(string id) {
            return this.Index().Where(x => x.title.ToLower() == id.ToLower()).FirstOrDefault();
        }
    }
}
