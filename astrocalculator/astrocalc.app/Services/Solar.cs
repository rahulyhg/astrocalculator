using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using astrocalc.app.services.algebric;
using astrocalc.app.services;
using astrocalc.app.httpmodels;

namespace astrocalc.app.services.solar {
    public static class Solar {
       
        public static SolarClock VedicShuddhi(SolarClock sc) {
            double solarArcTan = 0.53; //this is the diameter of the sun as see from the earth in degrees
            double earthAngVelo = 0.25; //degrees per minute, this is the angular velocity of of the earth 
            double vedic_shudhi = (solarArcTan /2) * earthAngVelo; //this is the correction when doin vedic calculations
            sc.sunrise = sc.sunrise.AddMinutes(vedic_shudhi);
            sc.sunset = sc.sunset.AddMinutes(-1 * vedic_shudhi);
            return sc;
        }
        public static SolarClock SolarClock(this DateTime dt, double latitude, double longitude, 
            double gmtoffset, bool atmRefrac) {

            SolarClock sc = new SolarClock();
            sc.gregoriandate = dt;
            //this is where we calcuclated the julian day, day index of the year
            double n1 = Math.Floor((double)dt.Month * 275 / 9);
            double n2 = Math.Floor((double)(dt.Month + 9) / 12);
            double n3 = 1 + (Math.Floor((dt.Year - 4 * Math.Floor((double)dt.Year / 4) + 2) / 3));
            sc.julian = (int)(n1 - (n2 * n3) + dt.Day - 30);
            double B = ((double)(sc.julian - 81) * 360 / 365);

            sc.declination = Algebric.SineInv(Algebric.Sine(23.45) * Algebric.Sine(B));

            var lstm = 15 * gmtoffset; //local standard time meridian
            var eot = 9.87 * (Algebric.Sine(2 * B)) - 7.53 * Algebric.Cosine(B) - 1.5 * Algebric.Sine(B);//equation of time

            var tc = (4 * (longitude - lstm)) + eot; //total time correction in minutes
   
            sc.noon = dt.AddHours(12-(tc / 60));
            var noonHourAngle = (tc / 60) * 15; //this is the time corection in degrees

            //this is to consider the atmospheric refraction , but not all methods would consider atm refraction 
            //so this param is kept as conditional one
            var refrac = atmRefrac==true? Algebric.Sine(-0.83): 0;
            var lngdecleffect = (refrac - (Algebric.Sine(sc.declination) * Algebric.Sine(latitude))) /
                (Algebric.Cosine(sc.declination) * Algebric.Cosine(latitude));
            var sunrise = 12 - (Algebric.CosineInv(lngdecleffect) / 15) - (tc / 60);

            sc.sunrise = dt.AddHours(sunrise);
            TimeSpan halfday = sc.noon.Subtract(sc.sunrise);
            sc.sunset = sc.noon.AddHours(halfday.Hours).AddMinutes(halfday.Minutes).AddSeconds(halfday.Seconds);
            sc.daylength = sc.sunset.Subtract(sc.sunrise);
            return sc;
        }
       
        //these are deprecated methods since the formula is foudn to be not correct
        public static DateTime Rise(double latitude, double longitude, DateTime dt, double zenith, double localOffset) {
            //this would get the julian day for us
            double n1 = Math.Floor((double)dt.Month * 275 / 9);
            double n2 = Math.Floor((double)(dt.Month + 9) / 12);
            double n3 = 1 + (Math.Floor((dt.Year - 4 * Math.Floor((double)dt.Year / 4) + 2) / 3));
            var N = n1 - (n2 * n3) + dt.Day - 30;
            //once the julian day is calculated then the longitudinal correction for the julian day is made
            var lngHour = longitude / 15;
            var t = N + ((6 - lngHour) / 24);

            var M = (0.9856 * t) - 3.289; //suns mean anomaly
            var L = M + (1.916 * Algebric.Sine(M)) + (0.020 * Algebric.Sine(2 * M)) + 282.634; //this is the tru solar longitude
            L = L < 0 ? L + 360 : L > 360 ? L - 360 : L; //adjusting the value of solar longitude to [0, 360) domain
            //this is where we calculate the solar right ascension
            var RA = Algebric.TangentInv(0.91764 * Algebric.Tangent(L));
            var Lquadrant = (Math.Floor(L / 90)) * 90;
            var RAquadrant = (Math.Floor(RA / 90)) * 90;
            RA = RA + (Lquadrant - RAquadrant);
            RA = RA / 15;//right ascensionin hours from longitude
            //this is where we calcualate the declination
            var sinDec = 0.39782 * Algebric.Sine(L);
            var cosDec = Algebric.Cosine(Algebric.SineInv(sinDec));
            var cosH = (Algebric.Cosine(zenith) - (sinDec * Algebric.Sine(latitude))) / (cosDec * Algebric.Cosine(latitude));
            var H = 360 - Algebric.CosineInv(cosH);//this is the suns local hour angle
            H = H / 15;
            var T = H + RA - (0.06571 * t) - 6.622;//this is the local mean time of rising and setting
            var UT = T - lngHour; //getting the UTC mean time for rising
            UT = UT < 0 ? UT + 24 : UT > 24 ? UT - 24 : UT;//this is adjustment for [0., 24) 
            var localT = UT + localOffset;//local time for rise 
            return new DateTime(dt.Year, dt.Month, dt.Day, 
                dt.AddHours(localT).Hour, dt.AddHours(localT).Minute, dt.AddHours(localT).Second);
        }
        public static DateTime Set(double latitude, double longitude, DateTime dt, double zenith, double localOffset) {
            //this would get the julian day for us
            double n1 = Math.Floor((double)dt.Month * 275 / 9);
            double n2 = Math.Floor((double)(dt.Month + 9) / 12);
            double n3 = 1 + (Math.Floor((dt.Year - 4 * Math.Floor((double)dt.Year / 4) + 2) / 3));
            var N = n1 - (n2 * n3) + dt.Day - 30;
            //once the julian day is calculated then the longitudinal correction for the julian day is made
            var lngHour = longitude / 15;
            var t = N + ((18 - lngHour) / 24);

            var M = (0.9856 * t) - 3.289; //suns mean anomaly
            var L = M + (1.916 * Algebric.Sine(M)) + (0.020 * Algebric.Sine(2 * M)) + 282.634; //this is the tru solar longitude
            L = L < 0 ? L + 360 : L > 360 ? L - 360 : L; //adjusting the value of solar longitude to [0, 360) domain
            //this is where we calculate the solar right ascension
            var RA = Algebric.TangentInv(0.91764 * Algebric.Tangent(L));
            var Lquadrant = (Math.Floor(L / 90)) * 90;
            var RAquadrant = (Math.Floor(RA / 90)) * 90;
            RA = RA + (Lquadrant - RAquadrant);
            RA = RA / 15;//right ascensionin hours from longitude
            //this is where we calcualate the declination
            var sinDec = 0.39782 * Algebric.Sine(L);
            var cosDec = Algebric.Cosine(Algebric.SineInv(sinDec));
            var cosH = (Algebric.Cosine(zenith) - (sinDec * Algebric.Sine(latitude))) / (cosDec * Algebric.Cosine(latitude));
            var H =  Algebric.CosineInv(cosH);//this is the suns local hour angle
            H = H / 15;
            var T = H + RA - (0.06571 * t) - 6.622;//this is the local mean time of rising and setting
            var UT = T - lngHour; //getting the UTC mean time for rising
            UT = UT < 0 ? UT + 24 : UT > 24 ? UT - 24 : UT;//this is adjustment for [0., 24) 
            var localT = UT + localOffset;//local time for rise 
            return new DateTime(dt.Year, dt.Month, dt.Day,
                dt.AddHours(localT).Hour, dt.AddHours(localT).Minute, dt.AddHours(localT).Second);
        }
    }
}
