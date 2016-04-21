using astrocalc.api.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astrocalc.api.Models
{
    public class SolarConfig
    {
        public List<City> CityChoices { get; set; }
        public City City { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
