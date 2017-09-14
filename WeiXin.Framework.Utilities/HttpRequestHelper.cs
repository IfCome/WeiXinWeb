using System;
using System.IO;
using System.Net;

namespace WeiXin.Framework.Utilities
{
    /// <summary>  
    /// 有关HTTP请求的辅助类  
    /// </summary>  
    public class HttpRequestHelper
    {
        public static string PostOrGet(string sUrl, string sParam = "", string sMethod = "get")
        {
            try
            {
                Uri uriurl = new Uri(sUrl);
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uriurl);
                req.Method = sMethod;
                req.Timeout = 120 * 1000;
                req.ContentType = "application/x-www-form-urlencoded;";
                if (!string.IsNullOrEmpty(sParam))
                {
                    byte[] bt = System.Text.Encoding.UTF8.GetBytes(sParam);
                    req.ContentLength = bt.Length;
                    using (Stream reqStream = req.GetRequestStream())//using 使用可以释放using段内的内存                                                                                                                                                                                                                                                                                                                                                                                                                                                           
                    {
                        reqStream.Write(bt, 0, bt.Length);
                        reqStream.Flush();
                    }
                }

                using (WebResponse res = req.GetResponse())
                {
                    //在这里对接收到的页面内容进行处理                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                    Stream resStream = res.GetResponseStream();
                    StreamReader resStreamReader = new StreamReader(resStream, System.Text.Encoding.UTF8);
                    string resLine;
                    System.Text.StringBuilder resStringBuilder = new System.Text.StringBuilder();
                    while ((resLine = resStreamReader.ReadLine()) != null)
                    {
                        resStringBuilder.Append(resLine + System.Environment.NewLine);
                    }
                    resStream.Close();
                    resStreamReader.Close();
                    return resStringBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                //WriteTxt_Log(ex);
                // BLL.BackgroundUserBll_log.AddLog("标记112", "错误内容：" + ex.Message, "0.0.0.0");
                return null;//url错误时候回报错
            }
        }

    }
}
