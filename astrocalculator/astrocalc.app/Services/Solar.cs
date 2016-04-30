using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using astrocalc.app.services.algebric;
namespace astrocalc.app.services.solar {
    public static class Solar {
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
