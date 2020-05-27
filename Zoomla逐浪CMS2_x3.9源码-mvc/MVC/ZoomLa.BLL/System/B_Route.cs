using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using ZoomLa.Common;

namespace ZoomLa.BLL
{
    public class B_Route
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.MapPageRoute("Home", "Home", "~/Plat/Blog/Default.aspx");
            routes.MapPageRoute("Office", "Office", "~/Mis/OA/Default.aspx");
            routes.MapPageRoute("Baike", "Baike", "~/Guest/Baike/Default.aspx");
            routes.MapPageRoute("BaikePage", "Baike/{Page}", "~/Guest/Baike/{Page}");
            routes.MapPageRoute("Ask", "Ask", "~/Guest/Ask/Default.aspx");
            //贴吧
            routes.MapPageRoute("BarIndex", "Index", "~/Guest/Bar/Default.aspx");
            routes.MapPageRoute("Bar", "PClass", "~/Guest/Bar/PostList.aspx");
            routes.MapPageRoute("Bar1", "PItem", "~/Guest/Bar/PostContent.aspx");
            routes.MapPageRoute("BarEdit", "EditContent", "~/Guest/Bar/EditContent.aspx");
            routes.MapPageRoute("BarSearch", "PostSearch", "~/Guest/Bar/PostSearch.aspx");

            //栏目首页
            routes.MapPageRoute("ColumnList", "Class_{ID}/Default.aspx", "~/ColumnList.aspx");
            routes.MapPageRoute("ColumnList2", "Class_{ID}/Default_{CPage}.aspx", "~/ColumnList.aspx");
            //栏目列表页
            routes.MapPageRoute("NodePage", "Class_{ID}/NodePage.aspx", "~/NodePage.aspx");
            routes.MapPageRoute("NodePage2", "Class_{ID}/NodePage_{CPage}.aspx", "~/NodePage.aspx");
            //最新信息页
            routes.MapPageRoute("NodeNews", "Class_{ID}/NodeNews.aspx", "~/NodeNews.aspx");
            routes.MapPageRoute("NodeNews2", "Class_{ID}/NodeNews_{CPage}.aspx", "~/NodeNews.aspx");
            //最热信息页
            routes.MapPageRoute("NodeHot", "Class_{ID}/NodeHot.aspx", "~/NodeHot.aspx");
            routes.MapPageRoute("NodeHot2", "Class_{ID}/NodeHot_{CPage}.aspx", "~/NodeHot.aspx");
            //推荐信息页
            routes.MapPageRoute("NodeElite", "Class_{ID}/NodeElite.aspx", "~/NodeElite.aspx");
            routes.MapPageRoute("NodeElite2", "Class_{ID}/NodeElite_{CPage}.aspx", "~/NodeElite.aspx");

            routes.MapPageRoute("StoreList", "Store/StoreList_{CPage}.aspx", "~/Store/StoreList.aspx");
            routes.MapPageRoute("Page", "Page/PageList_{CPage}.aspx", "~/Page/PageList.aspx");
            //页面
            routes.MapPageRoute("Shop", "Shop/{ID}.aspx", "~/Shop.aspx");
            //内容页,为兼容保留.aspx
            routes.MapPageRoute("Content", "Item/{ID}_{CPage}.aspx", "~/Content.aspx");
            routes.MapPageRoute("Content2", "Item/{ID}.aspx", "~/Content.aspx");
            routes.MapPageRoute("Content3", "Item/{ID}", "~/Content.aspx");
            routes.MapPageRoute("Content4", "Item/{ID}_{CPage}", "~/Content.aspx");
            //-----------------
            //routes.MapPageRoute("Admin", "Admin/{Page}.aspx", "~/Manage/{Page}.aspx");
            //routes.MapPageRoute("Admin1", "Admin/{Dir}/{Page}.aspx", "~/Manage/{Dir}/{Page}.aspx");
            //routes.MapPageRoute("Admin2", "Admin/{Dir}/{Dir2}/{Page}.aspx", "~/Manage/{Dir}/{Dir2}/{Page}.aspx");
            //routes.MapPageRoute("Admin3", "Admin/{Dir}/{Dir2}/{Dir3}/{Page}.aspx", "~/Manage/{Dir}/{Dir2}/{Dir3}/{Page}.aspx");
            //routes.MapPageRoute("Admin4", "Admin/{Dir}/{Dir2}/{Dir3}/{Dir4}/{Page}.aspx", "~/Manage/{Dir}/{Dir2}/{Dir3}/{Dir4}/{Page}.aspx");
            //routes.MapPageRoute("Admin5", "Admin/{Dir}/{Dir2}/{Dir3}/{Dir4}/{Dir5}/{Page}.aspx", "~/Manage/{Dir}/{Dir2}/{Dir3}/{Dir4}/{Dir5}/{{Page}.aspx");


            //routes.MapPageRoute("Bar2", "{Page}", "~/Guest/Bar/{Page}.aspx");
            //routes.MapPageRoute("Font", "t/{Flow}/{File}", "~/WebFont/t.aspx");
            //也可后带?传参
            //routes.MapPageRoute("S1", "SH/{Type}.html", "~/Search/SearchList.aspx");
            //WebAPI
            //routes.MapHttpRoute(
            //      name: "WebAPI",
            //      routeTemplate: "WebAPI/{controller}/{action}",
            //      defaults: new { id = System.Web.Http.RouteParameter.Optional }
            //  );
        }
        //获取路由或地址栏参数
        public static string GetParam(string pname, System.Web.UI.Page page)
        {
            string item = "";
            HttpRequest req = HttpContext.Current.Request;
            if (page.RouteData.Values[pname] != null)
            {
                item = page.RouteData.Values[pname].ToString();
            }
            else if (!string.IsNullOrEmpty(req.QueryString[pname]))
            {
                item = req.QueryString[pname];
            }
            return item;
        }
    }
}
