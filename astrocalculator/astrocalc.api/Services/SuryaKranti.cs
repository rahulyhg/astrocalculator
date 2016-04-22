

using System;
using System.Diagnostics;

namespace astrocalc.api.services.usnautical {
    /*Source:
	Almanac for Computers, 1990
	published by Nautical Almanac Office
	United States Naval Observatory
	Washington, DC 20392 http://williams.best.vwh.net/sunrise_sunset_algorithm.htm*/

    /*day, month, year:      date of sunrise/sunset
	latitude, longitude:   location for sunrise/sunset
	zenith:                Sun's zenith for sunrise/sunset
	  offical      = 90 degrees 50'
	  civil        = 96 degrees
	  nautical     = 102 degrees
	  astronomical = 108 degrees
	
	NOTE: longitude is positive for East and negative for West
        NOTE: the algorithm assumes the use of a calculator with the
        trig functions in "degree" (rather than "radian") mode. Most
        programming languages assume radian arguments, requiring back
        and forth convertions. The factor is 180/pi. So, for instance,
        the equation RA = atan(0.91764 * tan(L)) would be coded as RA
        = (180/pi)*atan(0.91764 * tan((pi/180)*L)) to give a degree
        answer with a degree input for L.*/
   
    public static class ServiceExtensions {
        public static double Radians(double angle) {
            return (Math.PI / 180) * angle;
        }

        public static double Degrees(double angle) {
            return angle * 180 / Math.PI;
        }

        public static double Cosine(double angle_deg) {
            return Math.Cos(Radians(angle_deg));
        }
        public static double Sine(double angle_deg) {
            return Math.Sin(Radians(angle_deg));
        }
        public static double Tangent(double angle_deg) {
            return Math.Tan(Radians(angle_deg));
        }
        public static double TangentInv(double value) {
            return Degrees(Math.Atan(value));
        }
        public static double SineInv(double value) {
            return Degrees(Math.Asin(value));
        }
        public static double CosineInv(double value) {
            return Degrees(Math.Acos(value));
        }
        public static double JulianDay(this DateTime dt) {
            double n1 = Math.Floor((double)dt.Month * 275 / 9);
            double n2 = Math.Floor((double)(dt.Month + 9) / 12);
            double n3 = 1 + (Math.Floor((dt.Year - 4 * Math.Floor((double)dt.Year / 4) + 2) / 3));
            return n1 - (n2 * n3) + dt.Day - 30;
        }
        public static double SolarNoon(this DateTime dt, double longitude) {
            return JulianDay(dt) + ((6 - (longitude / 15)) / 24);
        }
        public static double TrueSolarLongitude(this DateTime dt, double longitude) {
            var solarnoon = SolarNoon(dt, longitude);
            var solaranolamy = (0.9856 * solarnoon) - 3.289;
            return solaranolamy +
                (1.916 * Sine(solaranolamy)) +
                (0.020 * Sine(2 * solaranolamy)) +
                (0.0003 * Sine(3 * solaranolamy)) + 282.634;
        }
        public static double SolarRightAscension(double solarLongitude) {
            var ra= TangentInv(0.91764 * Tangent(solarLongitude));
            var Lquadrant = (Math.Floor(solarLongitude / 90)) * 90;
            var RAquadrant = (Math.Floor(ra / 90)) * 90;
            var ra_deg= ra + (Lquadrant - RAquadrant);
            return ra_deg / 15;
        }
        public static double SolarDeclination(double solarLongitude) {
            return SineInv(0.39782 * Sine(solarLongitude));
        }
        public static DateTime LocalSunrise(this DateTime dt, double longitude, double latitude, double degZenith, bool rising = true) {

            var trusolarlongitude = TrueSolarLongitude(dt, longitude);
            var solarRightAscension = SolarRightAscension(trusolarlongitude);

            var dec = SolarDeclination(trusolarlongitude);
            var sinDec = Sine(dec); //0.39782 = sin(23.44)
            var cosDec = Cosine(dec);
            
            var dec_deg = Math.Floor(dec);
            var dec_mins = Math.Floor((dec - dec_deg) * 60);
            Trace.WriteLine(String.Format("Solar Declination (deg) :{0}{1} {2}'", dec_deg, (char)176 ,dec_mins));

            //then we calculate the sun's local hour angle
            
            var cosH = (Cosine(degZenith)) - 
                (sinDec * Sine(latitude)) / (cosDec * Cosine(latitude));

            //local rising time
            var H = 360 - CosineInv(cosH);
            H = H / 15;
           
            var sunrise = H + solarRightAscension - (0.06571 * SolarNoon(dt, longitude)) - 6.622; ;
            
            sunrise  = sunrise <0 ? (-1)*sunrise : sunrise;
            var mins = (sunrise - Math.Floor(sunrise))*60;
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.AddHours(Math.Floor(sunrise)).Hour, dt.AddMinutes(mins).Minute, 0) ;
        }
        
    }
}