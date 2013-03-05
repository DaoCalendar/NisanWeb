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
            Agent target = new Agent("W002");
            target.Name = "roslan bt Ali";
            target.Email = "roslan@hotmail.com";

            Address address = new Address();
            address.Street = "963 Jalan 6 Machang Bubok";
            address.Postal = "33320";
            address.State = "Kuala Lumpur";
            target.Address = address;

            Assert.IsTrue(target.Save());
        }
        [TestMethod()]
        public void AgentLoadTest()
        {
            Agent target = new Agent("W002");
            Assert.IsTrue(target.Code.Length > 0);
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
        [TestMethod()]
        public void AdminSaveTest()
        {
            Admin target = new Admin("hlgranite");
            target.Name = "hlgranite";
            target.Email = "hlgranite@gmail.com";
            Assert.IsTrue(target.Save());
        }
        [TestMethod()]
        public void LoginPassTest()
        {
            User user = new User("agent");
            bool expected = true;
            bool actual = user.Login("agent");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void LoginFailTest()
        {
            User user = new User("A999");
            bool expected = false;
            bool actual = user.Login("W002");
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void GetRoleTest()
        {
            User user = new User("agent");
            bool expected = true;
            bool actual = user.Login("agent");
            Assert.AreEqual(expected, actual);

            user = user.Find();
            if (user is Agent)
                Assert.IsTrue(true);
            else
                Assert.IsTrue(false);
        }
        [TestMethod()]
        public void CastRoleTest()
        {
            User user = new User("agent");
            bool expected = true;
            bool actual = user.Login("agent");
            Assert.AreEqual(expected, actual);

            user = user.Find();
            User target = (User)user;
            if (target is Agent)
                Assert.IsTrue(true);
            else
                Assert.IsTrue(false);
        }
        [TestMethod()]
        public void IsExistTest()
        {
            User user = new User();
            user.Code = "W002";

            bool expected = true;
            bool actual = user.IsExist;
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void ValidateTest()
        {
            User user = new User();
            user.Validate();

            bool expected = false;
            bool actual = (user.Message.Length > 0) ? false : true;
            Assert.AreEqual(expected, actual);
        }
    }
}