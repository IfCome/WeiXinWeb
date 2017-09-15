using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace WeiXin.Framework.Utilities
{
    public class XmlEntityExchange<T> where T : new()
    {
        /// <summary>
        /// 将XML转换为对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T ConvertXmlToEntity(string xml)
        {
            XmlDocument doc = new XmlDocument();
            PropertyInfo[] propinfos = null;
            doc.LoadXml(xml);
            XmlNodeList nodelist = doc.SelectNodes("/xml");
            T entity = new T();
            foreach (XmlNode node in nodelist)
            {
                //初始化propertyinfo
                if (propinfos == null)
                {
                    Type objtype = entity.GetType();
                    propinfos = objtype.GetProperties();
                }
                //填充entity类的属性
                foreach (PropertyInfo pi in propinfos)
                {
                    XmlNode cnode = node.SelectSingleNode(pi.Name);
                    if (cnode != null)
                    {
                        pi.SetValue(entity, Convert.ChangeType(cnode.InnerText, pi.PropertyType), null);
                    }
                }
            }
            return entity;
        }

        /// <summary>
        /// 将对象转化为xml
        /// </summary>
        /// <param name="enitity"></param>
        /// <returns></returns>
        public static string ConvertEntityToXml(T enitity)
        {
            //初始化propertyinfo
            Type objtype = enitity.GetType();
            PropertyInfo[] propinfos = objtype.GetProperties();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<xml>");

            foreach (PropertyInfo propinfo in propinfos)
            {
                sb.Append("<");
                sb.Append(propinfo.Name);
                sb.Append(">");
                sb.Append(propinfo.GetValue(enitity, null));
                sb.Append("</");
                sb.Append(propinfo.Name);
                sb.AppendLine(">");
            }

            sb.AppendLine("</xml>"); 
            return sb.ToString();
        }
    }
}
