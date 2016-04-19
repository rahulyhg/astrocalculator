

using System;
using System.Diagnostics;

namespace astrocalc.app.services.usnautical {
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

        public static DateTime LocalSunrise(this DateTime dt, double longitude, double latitude, double degZenith, bool rising = true) {

            //calculation for the julian day
            decimal n1 = Math.Floor((decimal)dt.Month * 275 / 9);
            decimal n2 = Math.Floor((decimal)(dt.Month + 9) / 12);
            decimal n3 = 1 + (Math.Floor((decimal)dt.Year - 4 * Math.Floor((decimal)dt.Year / 4) + 2) / 3);
            var jualianday= n1 - (n2 * n3) + dt.Day - 30;
            Trace.WriteLine(String.Format(
                "The julian day is calculated to be {0}", jualianday));
            //the place has its longitude and thus an offset in the time then would have to be considered
            var local_julianday = jualianday + (decimal)((6 - (longitude / 15)) / 24);

            //now calculating the Sun's anomaly
            var solaranolamy = (0.9586M * local_julianday) - 3.289M;
            var rad_solaranomaly = Radians(Convert.ToDouble(solaranolamy));
            var trusolarlongitude = solaranolamy + 
                (decimal)(1.916 * Math.Sin(rad_solaranomaly)) + 
                (decimal)(0.020 * Math.Sin(2 * rad_solaranomaly)) + 
                282.634M;
            var rad_truSolarLong = Radians(Convert.ToDouble(rad_solaranomaly));
            var solarRightAscension = Degrees(Math.Atan(0.91764 * Math.Tan(rad_truSolarLong)));
            Trace.WriteLine(String.Format("solar right ascension is {0}", solarRightAscension));

            //right ascension value being converted to hours
            solarRightAscension = solarRightAscension / 15;

            var sinDec = 0.39782 * Math.Sin(rad_truSolarLong);
            var cosDec = Math.Cos(Math.Asin(sinDec));

            //then we calculate the sun's local hour angle
            var rad_latitude = Radians(latitude);
            var cosH = (Math.Cos(Radians(degZenith)) - (sinDec * Math.Sin(rad_latitude))) / (cosDec * Math.Cos(rad_latitude));

            //local rising time
            var H = 360 - Degrees(Math.Acos(cosH));
            H = H / 15;
            var T = H + solarRightAscension - (double)(0.06571M * local_julianday) - 6.622;
            var sunrise = T;
            Trace.WriteLine(sunrise);
            sunrise  = sunrise <0 ? (-1)*sunrise : sunrise;
            var mins = (sunrise - Math.Floor(sunrise))*60;
            return dt.AddHours(Math.Floor(sunrise)).AddMinutes(mins);
        }
        
    }
}