using HLGranite.Nisan;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HLGranite.Nisan.Test
{
    /// <summary>
    ///This is a test class for UserTest and is intended
    ///to contain all UserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserTest
    {
        [TestMethod()]
        public void CustomerAddressSaveTest()
        {
            Customer target = new Customer("Ahmad bin talib");
            target.Email = "ahmad@hotmail.com";

            Address address = new Address();
            address.Street = "963 Jalan 6 Machang Bubok";
            address.Postal = "14020";
            address.State = "Penang";
            target.Address = address;

            Assert.IsTrue(target.Save());
        }
        [TestMethod()]
        public void AgentSaveTest()
        {
            Agent target = new Agent("W001");
            target.Name = "Fatimah bt Ali";
            target.Email = "fatimah@hotmail.com";

            Address address = new Address();
            address.Street = "963 Jalan 6 Machang Bubok";
            address.Postal = "33320";
            address.State = "Kuala Lumpur";
            target.Address = address;

            Assert.IsTrue(target.Save());
        }
        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void DesignerSaveTest()
        {
            Designer target = new Designer("efa");
            target.Password = "efa123";
            target.Email = "efa@hotmail.com";
            Assert.IsTrue(target.Save());
        }
    }
}