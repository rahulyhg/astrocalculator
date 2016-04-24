using astrocalc.app.models;
using System.Collections.Generic;

namespace astrocalc.app.Models {
    public class SolarConfigChoices {
        public List<City> city { get; set; }
        public string[] month
        {
            get
            {
                return new string[] {
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
               };
            }
        }
        public int[] year
        {
            get
            {
                return new int[] {
                    2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019,2020, 2021, 2022, 2023
                };
            }
        }
    }
    public class SolarConfig {
        public SolarConfigChoices choices { get; set; }
        public City city { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public double zenith { get; set; }
    }
}
