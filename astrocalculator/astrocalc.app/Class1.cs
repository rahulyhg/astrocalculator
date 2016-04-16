using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astrocalc.app.services
{
    public static  class DateServices
    {
        public static float JulianDay(int year, int month, int day, float longitude, bool west) {
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

        public static double Radians(double angle) {
            return (Math.PI / 180) * angle;
        }

        public static double Degrees(double angle) {
            return angle * 180 / Math.PI;
        }
        public static double SolarDeclination(float jd) {
            double anomaly =  357.5291 + (0.98560028 * jd);
            
            double correction  = 1.19148 * Math.Sin(Radians(anomaly)) +
                0.002 * Math.Sin(2 * Radians(anomaly)) +
                0.0003 * Math.Sin(3 * Radians(anomaly));

            double eclipticLongitude  = anomaly + correction + 180 + 102.9372;
            return Degrees(Math.Asin(
                Math.Sin(Radians(eclipticLongitude)) *
                Math.Sin(Radians(23.45))));
        }

        public static double Sunrise(double declination, double latitude, double solarNoon) {
            double hourAngle = Math.Acos((-1) * Math.Tan(Radians(latitude)) * Math.Tan(Radians(declination)));
            return (solarNoon - Degrees(hourAngle)) / 15;
        }
    }
}
