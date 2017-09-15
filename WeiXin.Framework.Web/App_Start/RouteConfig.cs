using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WeiXin.Framework.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(name: "web", url: "web/{controller}/{action}.do",namespaces: new[] { "WeiXin.Framework.Web.Controllers.UI" });
            routes.MapRoute(name: "api", url: "api/{controller}/{action}.api", namespaces: new[] { "WeiXin.Framework.Web.Controllers.API" });
        }
    }
}
