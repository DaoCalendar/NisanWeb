using HLGranite.Nisan;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HLGranite.Nisan.Test
{
    /// <summary>
    /// This is a test class for OrderTest and is intended
    /// to contain all OrderTest Unit Tests
    /// </summary>
    [TestClass()]
    public class OrderTest
    {
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
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
        public void CustomerSaveTest()
        {
            Customer customer = new Customer("Ali");
            customer.Email = "ali@gmail.com";

            Address address = new Address();
            address.Street = "2534 Lorong 2 Taman India";
            address.Postal = "12345";
            address.State = "Melaka";

            Stock stock = new Stock(3);
            Nisan nisan = new Nisan(stock);
            nisan.Name = "Ramli" + new System.Random().Next(100) + " bin Taib";
            nisan.Death = RandomDate();

            Order target = new Order();
            target.Status = TransactionStage.Confirmed;
            target.Agent = new Agent("W002");
            target.Amount = stock.Price;
            target.Quantity = 1;
            target.Stock = nisan;
            target.ShipTo = address;
            target.Parent.CreatedBy = customer;//can be an agent
            Assert.IsTrue(target.Save());
        }
        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void AgentSaveTest()
        {
            Address address = new Address();
            address.Street = "1234 Lorong 2 Taman Bangla";
            address.Postal = "12345";
            address.State = "Pahang";

            Stock stock = new Stock(3);
            Nisan nisan = new Nisan(stock);
            nisan.Name = "Che Som" + new System.Random().Next(100) + " bin Said";
            nisan.Death = RandomDate();

            Agent agent = new Agent("W002");

            Order target = new Order();
            target.Status = TransactionStage.Confirmed;
            target.Agent = agent;
            target.Amount = stock.Price;
            target.Quantity = 1;
            target.Stock = nisan;
            target.ShipTo = address;
            Assert.IsTrue(target.Save());
        }
        private System.DateTime RandomDate()
        {
            System.Random random = new System.Random();
            int year = 2011;
            int month = 1;
            month += random.Next(12);
            int date = 1;
            date += random.Next(27);
            return new System.DateTime(year, month, date);
        }
    }
}
