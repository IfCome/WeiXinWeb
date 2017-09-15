using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WeiXin.Framework.Model;
using WeiXin.Framework.Utilities;
using WeiXin.Framework.WeiXinUtilities;

namespace WeiXin.Framework.Web.Controllers.API
{
    public class WeiXinAccessController : Controller
    {

        public ActionResult WeiXinRequest()
        {
            string postString = string.Empty;
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                using (Stream stream = Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = Encoding.UTF8.GetString(postBytes);
                }
                WeiXinRequestContent sendContent = new WeiXinRequestContent();
                if (!string.IsNullOrEmpty(postString))
                {
                    FileHelper.AppendText(Server.MapPath("~/requestContent.txt"), postString + Environment.NewLine);
                    WeiXinRequestContent receveContent = XmlEntityExchange<WeiXinRequestContent>.ConvertXmlToEntity(postString);
                    sendContent = new WeiXinRequestContent()
                    {
                        ToUserName = receveContent.FromUserName,
                        FromUserName = receveContent.ToUserName,
                        MsgType = "text",
                        CreateTime = DateTime.Now.Ticks.ToString()
                    };

                    if (receveContent.MsgType == "text")
                    {
                        sendContent.Content = getUserInfo(receveContent.FromUserName);
                    }
                    else
                    {
                        sendContent.Content = "只要文字，不要发起他的";
                    }
                    Response.Write(XmlEntityExchange<WeiXinRequestContent>.ConvertEntityToXml(sendContent));
                }
                else
                {
                    Response.Write("");
                }
                Response.End();
            }
            else
            {
                FileHelper.AppendText(Server.MapPath("~/requestContent.txt"), "校验微信接入");
                CheckSignature(); //微信接入的测试
            }
            return null;
        }

        private string getUserInfo(string openId)
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", AccessTokenHelper.GetNewAccessToken(), openId);
            FileHelper.AppendText(Server.MapPath("~/getUserInfo.txt"), url + Environment.NewLine);
            return HttpRequestHelper.PostOrGet(url);
        }

        private void CheckSignature()
        {
            string token = "mumuyiyiaiaiai";
            string echoString = Request.QueryString["echoStr"];
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            if (CheckSignature(token, signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echoString))
                {
                    Response.Write(echoString);
                    Response.End();
                }
            }

        }
        private bool CheckSignature(string token, string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}