using HLGranite.Nisan;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace HLGranite.Nisan.Test
{
    /// <summary>
    ///This is a test class for JawiTranslatorTest and is intended
    ///to contain all JawiTranslatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class JawiTranslatorTest
    {
        /// <summary>
        ///A test for Translate
        ///</summary>
        [TestMethod()]
        public void TranslateTest()
        {
            JawiTranslator target = new JawiTranslator();
            string rumi = "ahmad";
            string expected = "احمد";
            string actual = target.Translate(rumi);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod()]
        public void TranslateFullNameTest()
        {
            JawiTranslator target = new JawiTranslator();
            string rumi = "ali bin ahmad";
            string expected = "علي بن احمد";
            string actual = target.Translate(rumi);
            Assert.AreEqual(expected, actual);
        }
    }
}