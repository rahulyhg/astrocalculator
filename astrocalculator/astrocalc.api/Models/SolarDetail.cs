using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astrocalc.api.Models {
    public class SolarDetail {
        public int JulianDay { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        public double Declination { get; set; }
    }
}

