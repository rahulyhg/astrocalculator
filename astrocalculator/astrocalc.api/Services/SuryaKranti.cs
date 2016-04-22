

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
        public static DateTime LocalSunrise(this DateTime dt, double longitude, double latitude, double degZenith, bool rising = true) {

            //calculation for the julian day
            double n1 = Math.Floor((double)dt.Month * 275 / 9);
            double n2 = Math.Floor((double)(dt.Month + 9) / 12);
            double n3 = 1 + (Math.Floor((dt.Year - 4 * Math.Floor((double)dt.Year / 4) + 2) / 3));
            var jualianday= n1 - (n2 * n3) + dt.Day - 30;
            Trace.WriteLine(String.Format(
                "Julian Day: {0}", jualianday));
            //the place has its longitude and thus an offset in the time then would have to be considered
            var solarnoon = jualianday+((6 - (longitude / 15)) / 24);//jualianday - (longitude / 360);
            Trace.WriteLine(String.Format(
                "Solar Noon {0}", solarnoon));

            //now calculating the Sun's anomaly
            var solaranolamy = (0.9856 * solarnoon) - 3.289;

            //var trusolarlongitude = solaranolamy + 
            //    (1.916 * Sine(solaranolamy)) + 
            //    (0.020 * Sine(2 * solaranolamy)) + 
            //    282.634;
            var trusolarlongitude = solaranolamy +
                (1.916 * Sine(solaranolamy)) +
                (0.020 * Sine(2 * solaranolamy)) +
                (0.0003 * Sine(3 * solaranolamy))+ 282.634;


            var solarRightAscension = TangentInv(0.91764 * Tangent(trusolarlongitude));
            Trace.WriteLine(String.Format("Solar Right Ascension(deg) {0}", solarRightAscension));
            //normalizing the quadrants of solar longitude and right ascension
            var Lquadrant = (Math.Floor(trusolarlongitude / 90)) * 90;
            var RAquadrant = (Math.Floor(solarRightAscension / 90)) * 90;
            solarRightAscension = solarRightAscension + (Lquadrant - RAquadrant);
            //right ascension value being converted to hours
            solarRightAscension = solarRightAscension / 15;

            var sinDec = 0.39782 * Sine(trusolarlongitude); //0.39782 = sin(23.44)
            var cosDec = Cosine(SineInv(sinDec));
            var dec = SineInv(sinDec);
            var dec_deg = Math.Floor(dec);
            var dec_mins = Math.Floor((dec - dec_deg) * 60);
            Trace.WriteLine(String.Format("Solar Declination (deg) :{0}{1} {2}'", dec_deg, (char)176 ,dec_mins));

            //then we calculate the sun's local hour angle
            
            var cosH = (Cosine(degZenith)) - 
                (sinDec * Sine(latitude)) / (cosDec * Cosine(latitude));

            //local rising time
            var H = 360 - CosineInv(cosH);
            H = H / 15;
           
            var sunrise = H + solarRightAscension - (0.06571 * solarnoon) - 6.622; ;
            
            sunrise  = sunrise <0 ? (-1)*sunrise : sunrise;
            var mins = (sunrise - Math.Floor(sunrise))*60;
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.AddHours(Math.Floor(sunrise)).Hour, dt.AddMinutes(mins).Minute, 0) ;
        }
        
    }
}