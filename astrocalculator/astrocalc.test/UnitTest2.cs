using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using astrocalc.app.repos;
using System.Linq;
using System.Diagnostics;
using astrocalc.app.storemodels;

namespace astrocalc.test {
    [TestClass]
    public class AppTest {
        [TestMethod]
        public void TEST_IndexCities() {
            Repo r = new Repo();
            var  result = 
            (r.QueryInterface<ICity>()).Index(0, 20).Result.ToList<City>();
            Assert.IsNotNull(result, String.Format("the result was found to be null"));
            Assert.IsTrue(result.Count != 0, String.Format("the result was not found with any results"));
            result.ForEach(x => {
                Trace.WriteLine(x.city);
            });
        }
    }
}
