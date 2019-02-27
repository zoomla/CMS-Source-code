using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Model;

namespace ZoomLa.Sns
{
    public class RouteConfig
    {
        public static List<M_MVCRoute> GetRoutes()
        {
            List<M_MVCRoute> routeList = new List<M_MVCRoute>();
            //routeList.Add(new M_MVCRoute() { url = "User/Content/Test2", controller = "ZLPlug", action = "Test2" });
            //routeList.Add(new M_MVCRoute() { url = "ZLPlug/{action}", controller = "ZLPlug", action = "Test" });
            return routeList;
            ////控制器路由
            //routes.MapRoute("Plug1", "ZLPlug/{action}", defaults: new { controller = "ZLPlug", action = "Test" });
            ////页面路由
            //routes.MapRoute("Plug2", "User/Content/Test2", defaults: new { controller = "ZLPlug", action = "Test2" });
        }
    }
}
