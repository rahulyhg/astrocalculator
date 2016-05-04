using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using astrocalc.app.services;
using astrocalc.app.services.solar;
using System.Diagnostics;
using astrocalc.app.httpmodels;

namespace astrocalc.test {
    [TestClass]
    public class SolarTest {
        [TestMethod]
        public void TestSolarClock() {
            var chandigarh = new double[] { 30.7333, 76.7794 };
            var pune = new double[] { 18.5204, 73.8567 };
            SolarClock sc = new DateTime(2016, 4, 1).SolarClock(chandigarh[0],chandigarh[1], 5.5, true); //since this is astronomical
            Debug.WriteLine(String.Format("Sunrise would be at {0}", sc.sunrise));
            Debug.WriteLine(String.Format("Sunset would be at {0}", sc.sunset));
            Debug.WriteLine(String.Format("Total duration of the day woudl be {0}", sc.daylength));
            Debug.WriteLine(String.Format("Solar noon would be at {0}", sc.noon));
            Debug.WriteLine(String.Format("Julian day would be  {0}", sc.julian));
            Debug.WriteLine(String.Format("Solar declination would be  {0}", sc.declination));

            Debug.WriteLine("");
            Debug.WriteLine("");

            Debug.WriteLine(String.Format("We are now outputting the vedic solar clock "));

            sc = Solar.VedicShuddhi(new DateTime(2016, 4, 1).SolarClock(chandigarh[0],chandigarh[1], 5.5, false));
            Debug.WriteLine(String.Format("Sunrise would be at {0}", sc.sunrise));
            Debug.WriteLine(String.Format("Sunset would be at {0}", sc.sunset));
            Debug.WriteLine(String.Format("Total duration of the day woudl be {0}", sc.daylength));
            Debug.WriteLine(String.Format("Solar noon would be at {0}", sc.noon));
            Debug.WriteLine(String.Format("Julian day would be  {0}", sc.julian));
            Debug.WriteLine(String.Format("Solar declination would be  {0}", sc.declination));
        }
    }
}
