using HLGranite.Nisan;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HLGranite.Nisan.Test
{
    /// <summary>
    ///This is a test class for MuslimCalendarTest and is intended
    ///to contain all MuslimCalendarTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MuslimCalendarTest
    {
        /// <summary>
        ///A test for GetDate
        ///</summary>
        [TestMethod()]
        public void MuslimMonthTest()
        {
            int expected = 1;
            int actual = (int)MuslimMonth.Muharram;
            Assert.AreEqual(expected, actual);

            expected = 7;
            actual = (int)MuslimMonth.Rejab;
            Assert.AreEqual(expected, actual);
        }
    }
}