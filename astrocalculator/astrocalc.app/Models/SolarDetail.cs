using System;

namespace astrocalc.app.Models {
    public class SolarDetail {
        public double julianday { get; set; }
        public DateTime gregorianday { get; set; }
        public DateTime sunrise { get; set; }
        public DateTime sunset { get; set; }
        public double declination { get; set; }
    }
}

