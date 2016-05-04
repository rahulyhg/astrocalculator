using astrocalc.app.storemodels;
using System;
using System.Collections.Generic;

namespace astrocalc.app.httpmodels {
    public class UserChoices {
        public string cities { get; set; }
        public string months { get; set; }
        public string years { get; set; }
    }
    public class SolarClock {
        public DateTime gregoriandate { get; set; }
        public DateTime sunrise { get; set; }
        public DateTime sunset { get; set; }
        public DateTime noon { get; set; }
        public TimeSpan daylength { get; set; }

        public int julian { get; set; }
        public double declination { get; set; }

    }

}