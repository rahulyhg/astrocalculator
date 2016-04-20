using astrocalc.api.models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astrocalc.api.Controllers {
    [Route("cities")]
    public class CitiesController {
        [Route("")]
        public List<City> Index() {
            return new List<City>() {
                new City() {Coordinates = new double[] { 18.5204,73.8567},Title = "Pune" },
                new City() {Coordinates = new double[] { 17.3850 ,78.4867},Title = "Hyderabad" },
                new City() {Coordinates = new double[] { 30.7333,76.7794},Title = "Chandigarh" },
                new City() {Coordinates = new double[] { 30.6544, 76.8452 },Title = "Dhakoli" }
            };
        }
    }
}
