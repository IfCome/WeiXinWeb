using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeiXin.Framework.WeiXinUtilities;

namespace WeiXin.Framework.Test
{
    [TestClass]
    public class WeiXinUtilitiesTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            AccessTokenHelper.GetNewAccessToken();
        }
    }
}
