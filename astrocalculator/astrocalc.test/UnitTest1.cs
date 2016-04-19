using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using astrocalc.app.services;
using System.Diagnostics;
using astrocalc.app.services.usnautical;

namespace astrocalc.test {
    [TestClass]
    public class UnitTest1 {

        double[] pune = new double[] { 73.8567, 18.5204 };
        double[] hyderabad = new double[] { 78.4867, 17.3850 };
        double[] chandigarh = new double[] { 76.7794, 30.7333 };
        double[] dhakoli = new double[] { 76.8452, 30.6544 };

        [TestMethod]
        public void ApproximateTest() {

            int julianDay = SuryKranti.JulianDayApprox(2016, 4,16, pune[0] , false);
            Trace.WriteLine(string.Format("Julian day requested is {0}", julianDay));

            decimal  declination =SuryKranti.SolarDeclinationApprox(julianDay);
            Trace.WriteLine(string.Format("solar declination is  {0}", declination));
        }
        [TestMethod]
        public void ServicesTest() {
            DateTime dt = new DateTime(2016, 4, 7, 0,0,0);
            Trace.WriteLine(String.Format(
                "Local sunrise is {0}", dt.LocalSunrise(73.8567, 18.5204, 90.3)));
        }
    }
}
