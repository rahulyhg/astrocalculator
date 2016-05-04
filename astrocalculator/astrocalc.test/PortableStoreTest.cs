using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using astrocalc.app.Migration;
namespace astrocalc.test {
    [TestClass]
    public class PortableStoreTest {
        [TestMethod]
        public void TransferDatabase() {
            PortableStore ps = new PortableStore();
            var task =ps.TransferDatabase();
            task.Wait();
        }
    }
}
