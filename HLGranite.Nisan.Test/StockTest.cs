using HLGranite.Nisan;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace HLGranite.Nisan.Test
{
    /// <summary>
    ///This is a test class for StockTest and is intended
    ///to contain all StockTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StockTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            string name = "4' Batu Batik(L)";

            Stock target = new Stock();
            target.Type = name;
            target.Price = 300;
            target.Save();

            Stock actual = new Stock(name);
            Assert.AreEqual(name, actual.Type);
        }

        /// <summary>
        ///A test for Load
        ///</summary>
        [TestMethod()]
        public void LoadTest()
        {
            Stock target = new Stock();
            target.Id = 1;
            target.Load();

            Assert.IsTrue(target.Type.Length > 0);
        }

        /// <summary>
        ///A test for LoadAll
        ///</summary>
        [TestMethod()]
        public void LoadAllTest()
        {
            int expected = 0;
            int actual = Stock.LoadAll().Count;
            Assert.AreNotEqual(expected, actual);
        }
    }
}