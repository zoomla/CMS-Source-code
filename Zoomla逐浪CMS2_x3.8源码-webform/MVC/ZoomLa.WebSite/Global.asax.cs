using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.HtmlLabel;
using ZoomLa.Model;
namespace ZoomLaCMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");
            //二级域名路由
            if (SiteConfig.SiteOption.DomainRoute.Equals("1"))
            {
                routes.Add("SecondRoute", new SecondRoute(""));
            }
            //插件路由
            {
                var routeList = ZoomLa.Sns.RouteConfig.GetRoutes();
                for (int i = 0; i < routeList.Count; i++)
                {
                    var routeMod = routeList[i];
                    routes.MapRoute("ZLPlug" + i, routeMod.url, defaults: new { controller = routeMod.controller, action = routeMod.action });
                }
            }
            //区域路由
            AreaRegistration.RegisterAllAreas();
            //根目录下路由
            routes.MapRoute("D3", "D3/{action}", defaults: new { controller = "D3", action = "Default" });
            routes.MapRoute("Zone", "Zone/{action}", defaults: new { controller = "Zone", action = "ChatStart" });
            routes.MapRoute("RSS", "RSS/{action}", defaults: new { controller = "RSS", action = "Default" });
            routes.MapRoute("DAI", "DAI/{action}", defaults: new { controller = "DAI", action = "Default" });
            routes.MapRoute("FrontSpace", "Space/{action}", defaults: new { controller = "FrontSpace", action = "Default" });
            routes.MapRoute("FrontPage", "Page/{action}", defaults: new { controller = "FrontPage", action = "Default" });
            routes.MapRoute("FrontCom", "com/{action}", defaults: new { controller = "FrontCom" });
            routes.MapRoute("FrontStore", "store/{action}", defaults: new { controller = "FrontStore", action = "StoreIndex" });
            routes.MapRoute("FrontSearch", "Search/{action}", defaults: new { controller = "FrontSearch", action = "Default" });
            routes.MapRoute("Comments", "Comments/{action}", defaults: new { controller = "Comments", action = "CommentFor" });
            routes.MapRoute("Edit", "Edit/{action}", defaults: new { controller = "Edit", action = "Default" });
            routes.MapRoute("PostBar", "PostBar/{action}", defaults: new { controller = "PostBar", action = "Default" });
            routes.MapRoute("Ask", "Ask/{action}", defaults: new { controller = "Ask", action = "Default" });
            routes.MapRoute("Guest", "Guest/{action}",namespaces:new string[] { "ZoomLaCMS.Controllers" }, defaults: new { controller = "Guest", action = "Default" });
            routes.MapRoute("Site", "Site/{action}", defaults: new { controller = "Site", action = "Default" });
            routes.MapRoute("Baike_Front", "Baike/{action}", defaults: new { controller = "FrontBaike", action = "Default" });
            routes.MapRoute("App", "App/{action}", defaults: new { controller = "App", action = "Default" });

            routes.MapRoute("Bar_Index", "Index", defaults: new { controller = "PostBar", action = "Index" });
            routes.MapRoute("Bar_PClass", "PClass", defaults: new { controller = "PostBar", action = "PClass" });
            routes.MapRoute("Bar_PItem", "PItem", defaults: new { controller = "PostBar", action = "PItem" });
            routes.MapRoute("Bar_EditContent", "EditContent", defaults: new { controller = "PostBar", action = "EditContent" });
            routes.MapRoute("Bar_PostSearch", "PostSearch", defaults: new { controller = "PostBar", action = "PostSearch" });
            //提供中文节点/标题支持,内容|商品|列表页
            //routes.MapPageRoute("Chinese_Content", "Content/{A}/{B}/{ID}/", "~/BU/Front/Content.aspx");
            //routes.MapPageRoute("Chinese_Shop", "Shop/{A}/{B}/{ID}.aspx", "~/BU/Front/Shop.aspx");
            //routes.MapPageRoute("Chinese_ColumnList", "Class/{A}/{B}/{ID}.aspx", "~/BU/Front/ColumnList.aspx");
            //aspx路由
            B_Route.RegisterRoutes(routes);
        }
        protected void Application_Start()
        {
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            //-------------------------------------------------
            RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("/Config/log.config")));
            //--------
            try
            {
                TaskCenter.Init();
                TaskCenter.Start();
            }
            catch (Exception ex) { ZLLog.L("TaskCenter" + ex.Message); }
        }
        void Application_End(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
            string url = SiteConfig.SiteInfo.SiteUrl;
            System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse rsp = (System.Net.HttpWebResponse)req.GetResponse();
            string tmp = rsp.StatusDescription;
        }

        //void Application_Error(object sender, EventArgs e)
        //{
        //    //try
        //    //{
        //    //    Exception objErr = Server.GetLastError().GetBaseException();
        //    //    if (objErr.Message.Contains("不存在")) { return; }
        //    //    //实际发生的异常
        //    //    Exception iex = objErr.InnerException;
        //    //    ZLLog.L(ZLEnum.Log.exception, new M_Log()
        //    //    {
        //    //        Source = Request.Url.ToString(),
        //    //        Message = "来源：Application_Error,原因：" + iex.Message + "异常源：" + objErr.Source
        //    //    });
        //    //    Response.Write("sfsfsf"); Response.End();
        //    //    Server.ClearError();
        //    //}
        //    //catch { }
        //}
        void Application_AcquireRequestState(object sender, EventArgs e)
        {

        }
        void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.RequestType.ToUpper() == "GET")
            {
                if (ZoomlaSecurityCenter.GetData())
                {
                    ZLLog.L(ZLEnum.Log.safe, "安全:已拦截SQL注入");
                    //function.WriteErrMsg("产生错误的可能原因：你提交的参数不正确,包含恶意字符串,或检查系统是否开启了SQL防注入功能!", "", "非法SQL注入或存储!");
                    throw new Exception("产生错误的可能原因：你提交的参数不正确,包含恶意字符串,或检查系统是否开启了SQL防注入功能!");
                }
            }
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                if (System.Web.HttpContext.Current.Request.Files.Count > 0)
                {
                    ZoomlaSecurityCenter.CheckUpladFiles();
                }
            }
            // if (Application["safeDomain"] != null && !string.IsNullOrEmpty(Application["safeDomain"].ToString()) && Request.RawUrl.ToLower().Contains(".aspx"))
            //{
            //   if (!ZoomlaSecurityCenter.IsSafeDomain(Application["safeDomain"].ToString().ToLower()))
            //  {
            //     function.WriteErrMsg("非安全域名,无法访问管理目录!!!");
            //  }
            // }
        }

        void Application_EndRequest(Object sender, EventArgs e)
        {

        }
        void Session_Start(object sender, EventArgs e)
        { }
        void Session_End(object sender, EventArgs e)
        {
            try
            {
                string name = Session["M_LoginName"] as string;
                if (!string.IsNullOrEmpty(name))
                {
                    B_Admin.UpdateField("LastLogoutTime", DateTime.Now.AddMinutes(-10).ToString(), name);
                }
            }
            catch (Exception ex) { ZLLog.L(ZLEnum.Log.exception, "更新管理员最后登录时间失败,原因:" + ex.Message + "," + (Session["M_LoginName"] as string)); }
        }
    }
}