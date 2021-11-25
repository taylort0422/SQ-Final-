using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TMSMainWindow;
namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CommTMSConstructorTest()
        {
            CommTMS obj = new CommTMS("root", "!Mustang123GT", "127.0.0.1", 3306, "tms");
            Order newOrder = new Order("Malmart", "London", "Hamilton", 0, 0);
            newOrder.Confirmed = 0;
            int result = obj.InsertOrder(newOrder);
            Assert.AreEqual(43, result);
        }
    }
}
