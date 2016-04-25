using astrocalc.app.storemodels;
using System;
using System.Collections.Generic;

namespace astrocalc.app.httpmodels {
    public class UserChoices {
        public string cities { get; set; }
        public string months { get; set; }
        public string years { get; set; }
    }
   
    public class SolarTime {
        public DateTime date { get; set; }
        public double julian { get; set; }
        public DateTime sunrise { get; set; }
        public DateTime sunset { get; set; }
        public double declination { get; set; }
    }
}