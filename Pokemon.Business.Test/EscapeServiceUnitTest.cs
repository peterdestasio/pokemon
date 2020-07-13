using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pokemon.Business.Interfaces;
using Pokemon.Business.Services;

namespace Pokemon.Business.Test
{
    [TestClass]
    public class EscapeServiceUnitTest
    {
        private IEscapeService _escapeService;
        [TestInitialize]
        public void Initialize()
        {
            _escapeService = new EscapeService();
        }

        [TestMethod]
        public void Test_EscapeString_Return_Escaped()
        {
            //arrange
            var text = "some text \n to escape";
            var expected = "some%20text%20%20%20to%20escape";

            //act
            var actual = _escapeService.EscapeString(text);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_EscapeString_Return_Empty()
        {
            //arrange
            string text = "";
            var expected = "";

            //act
            var actual = _escapeService.EscapeString(text);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}
