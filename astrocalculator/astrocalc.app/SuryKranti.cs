using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astrocalc.app.services
{
    public struct Time {
        public int hours { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }
    }
    public static  class SuryKranti
    {
        public static double Radians(double angle) {
            return (Math.PI / 180) * angle;
        }

        public static double Degrees(double angle) {
            return angle * 180 / Math.PI;
        }
        public static Time Time(double time) {

            Time result = new Time() { hours = Convert.ToInt32(Math.Truncate(Convert.ToDecimal(time))) };
            result.minutes = Convert.ToInt32((time - result.hours) * 60);
            return result;
        }
        public static decimal SolarDeclinationApprox(int julday) {
            decimal maxAscension = (decimal)Math.Sin(Radians(23.45));
            Trace.WriteLine(String.Format("max declination of sun is {0}", maxAscension));
            decimal currentAscension = (decimal)Math.Sin(Radians((360 * (julday - 81)) /365));
            Trace.WriteLine(String.Format("current declination of sun is {0}", currentAscension));
            return (decimal) Degrees(Math.Asin(Convert.ToDouble(
                maxAscension * currentAscension)));
        }
        public static decimal SolarDeclinationExact(double jd) {
            decimal anomaly = 357.5291M + (0.98560028M * (decimal)jd);
            decimal correction = (decimal)1.19148M * 
                (decimal)Math.Sin(Radians(Convert.ToDouble(anomaly))) +
                0.002M * (decimal)Math.Sin(2 * Radians(Convert.ToDouble(anomaly))) +
                0.0003M * (decimal)Math.Sin(3 * Radians(Convert.ToDouble(anomaly)));

            decimal eclipticLongitude = anomaly + correction + 180M + 102.9372M;
            return (decimal)Degrees(Math.Asin(
                Math.Sin(Radians(Convert.ToDouble(eclipticLongitude))) *
                Math.Sin(Radians(23.45))));
        }
        public static int JulianDayApprox(int year, int month, int day, double longitude, bool west) {
            List<int> regular = new List<int>() {
                31, 28, 31, 30, 31, 30 , 31, 31, 30, 31, 30, 31
            };
            List<int> leap = new List<int>() {
                31, 29, 31, 30, 31, 30 , 31, 31, 30, 31, 30, 31
            };
            int result;
            Math.DivRem(year, 4, out result);
            List<int> selected = result != 0 ? regular : leap;
            int julian = 0;
            selected.Take(month - 1).Select(x => x).ToList<int>().ForEach(x => {
                julian = julian + x;
            });
            julian = julian + day;
            return julian;
        }
        public static double JulianDayExact(int year, int month, int day, double longitude, bool west) {
            List<int> regular = new List<int>() {
                31, 28, 31, 30, 31, 30 , 31, 31, 30, 31, 30, 31
            };
            List<int> leap = new List<int>() {
                31, 29, 31, 30, 31, 30 , 31, 31, 30, 31, 30, 31
            };
            int result;
            Math.DivRem(year, 4, out result);
            List<int> selected = result != 0 ? regular : leap;
            int julian = 0;
            selected.Take(month-1).Select(x => x).ToList<int>().ForEach(x => {
                julian  =julian+x;
            });
            julian = julian + day;
            return julian+ ((west == true ? 1 : -1) * (longitude / 360));
        }
        //public static decimal JulianDay(int day, int month, int year) {
        //    return (decimal)(day - 32075 +
        //         (1461 * (year + 4800 + (month - 14) / 12) / 4) +
        //         367 * (month - 2 - (month - 14) / 12 * 12) / 12 -
        //         3 * ((year + 4900 + (month - 14) / 12) / 100) / 4);
        //}
        //public static decimal JulianDay2000(decimal day) {
        //    return day - 2451545.0M + 0.0008M;
        //}

      
       
        public static double SunriseApprox(double declination, double latitude, double solarNoon) {
            double hourAngle = Math.Acos((-1) * Math.Tan(Radians(latitude)) * Math.Tan(Radians(declination)));
            return Math.Abs((solarNoon - Degrees(hourAngle)) / 15);
        }
        public static double SunriseExact(double declination, double latitude, double solarNoon) {
            double value = (Math.Sin(Radians(-0.83)) - (Math.Sin(Radians(declination)) * Math.Sin(Radians(latitude)))) / (Math.Cos(Radians(declination)) * Math.Cos(Radians(latitude)));
            double hourAngle = Math.Acos(value);
            return Math.Abs((solarNoon - Degrees(hourAngle)) / 15);
        }
    }
}
