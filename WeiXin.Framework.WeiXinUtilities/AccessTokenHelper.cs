using WeiXin.Framework.Utilities;

namespace WeiXin.Framework.WeiXinUtilities
{
    public class AccessTokenHelper
    {
        public static string GetNewAccessToken()
        {
            string res = HttpRequestHelper.PostOrGet("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wxa77a673c1cdce2c1&secret=c506a69e809ece9fdbdbbdb0d821ec92");
            if (!string.IsNullOrEmpty(res))
            {
                //{"access_token":"LyuyuWj4T132LrhI8_3edyYYKmKXuVPqpdy5FXOV24TewNA9QmI_VwlnnAYUiup3XSohejXn24_6bgepwFpAQe3vjap8tSsvFS9dEHkAoS4SPAQ78oSyzrkwSg1f7x8iLJKaAAASQR","expires_in":7200}
                return res;
            }
            return null;
        }
    }
}
