using System.Web;
using WeiXin.Framework.Model;
using WeiXin.Framework.Utilities;

namespace WeiXin.Framework.WeiXinUtilities
{
    public class AccessTokenHelper
    {
        public static string GetNewAccessToken()
        {
            // 先从本地读取

            // 再从接口获取
            string res = HttpRequestHelper.PostOrGet("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx8400b7574c25093a&secret=2cde02c90d42310ebbd6c530b97a6743");
            if (!string.IsNullOrEmpty(res))
            {
                FileHelper.AppendText(HttpContext.Current.Server.MapPath("~/GetNewAccessToken.txt"), res);
                WeiXinAccessToken token = JsonEntityExchange<WeiXinAccessToken>.ConvertJsonToEntity(res);

                return token.access_token;
            }
            return null;
        }
    }
}
