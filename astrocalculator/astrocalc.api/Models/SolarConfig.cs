using astrocalc.api.models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace astrocalc.api.Models {
    public class SolarConfig {
        public List<City> citychoices { get; set; }
        public string[] monthchoices
        {
            get
            {
                return new string[] {
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
               };
            }
        }
        public int[] yearchoices
        {
            get
            {
                return new int[] {
                    2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019,2020, 2021, 2022, 2023
                };
            }
        }
        public City city { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public double zenith { get; set; }
    }
}
