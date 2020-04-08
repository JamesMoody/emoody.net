using Microsoft.VisualStudio.TestTools.UnitTesting;
using eMoody.net.biz;

namespace eMoody.UnitTests
{
    [TestClass]
    public class HelperTests
    {

        #region unit tests - RazorHelpers.BindToInt()
        
        [TestMethod]
        public void RazorHelpers_BindToInt_Typcial() {
            Assert.AreEqual(5, RazorHelpers.bindToInt("5"));
        }

        [TestMethod]
        public void RazorHelpers_BindToInt_Empty() {
            Assert.AreEqual(-1, RazorHelpers.bindToInt("",-1));
        }

        [TestMethod]
        public void RazorHelpers_BindToInt_Negative()
        {
            Assert.AreEqual(-5, RazorHelpers.bindToInt("-5"));
        }

        [TestMethod]
        public void RazorHelpers_BindToInt_Zero()
        {
            Assert.AreEqual(0, RazorHelpers.bindToInt("0"));
        }

        [TestMethod]
        public void RazorHelpers_BindToInt_MaxInt()
        {
            Assert.AreEqual(int.MaxValue, RazorHelpers.bindToInt(int.MaxValue.ToString()));
        }

        [TestMethod]
        public void RazorHelpers_BindToInt_MinInt()
        {
            Assert.AreEqual(int.MinValue, RazorHelpers.bindToInt(int.MinValue.ToString()));
        }

        [TestMethod]
        public void RazorHelpers_BindToInt_IntOverflow() 
        {
            // 2147483647 + 1
            Assert.AreEqual(-1, RazorHelpers.bindToInt("2147483648"));
        }

        [TestMethod]
        public void RazorHelpers_BindToInt_IntUnderflow()
        {
            // -2147483648 - 1
            Assert.AreEqual(-1, RazorHelpers.bindToInt("-2147483649"));
        }

        #endregion

    }
}
