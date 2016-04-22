using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astrocalc.api.Models {
    public class SolarDetail {
        public double julianday { get; set; }
        public DateTime gregorianday { get; set; }
        public DateTime sunrise { get; set; }
        public DateTime sunset { get; set; }
        public double declination { get; set; }
    }
}

