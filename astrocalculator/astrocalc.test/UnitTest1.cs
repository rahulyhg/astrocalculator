using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using astrocalc.app.services;
using System.Diagnostics;

namespace astrocalc.test {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
            float julianDay = DateServices.JulianDay(2016, 4,16, 78.4867f, false);
            Trace.WriteLine(julianDay);
            double solarDeclination = DateServices.SolarDeclination(julianDay);
            Trace.WriteLine(solarDeclination);
            double slNoon =  (julianDay > 90 ? julianDay - 90 : 90 - julianDay)/15;
            double sunrise = DateServices.Sunrise(solarDeclination, 17.3850, slNoon);
            Trace.WriteLine(sunrise);
        }
    }
}
