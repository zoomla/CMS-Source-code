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
            routes.MapPageRoute("Home", "Home", "~/Plat/Blog/Default.aspx");
            //routes.MapPageRoute("Office", "Office", "~/Mis/OA/Default.aspx");
            routes.MapPageRoute("Baike", "Baike", "~/Guest/Baike/Default.aspx");
            routes.MapPageRoute("BaikePage", "Baike/{Page}", "~/Guest/Baike/{Page}");
            //routes.MapPageRoute("Ask", "Ask", "~/Guest/Ask/Default.aspx");
            //贴吧
            //routes.MapPageRoute("BarIndex", "Index", "~/Guest/Bar/Default.aspx");
            //routes.MapPageRoute("BarClass", "PClass", "~/Guest/Bar/PostList.aspx");
            //routes.MapPageRoute("BarItem", "PItem", "~/Guest/Bar/PostContent.aspx");
            //routes.MapPageRoute("BarEdit", "EditContent", "~/Guest/Bar/EditContent.aspx");
            //routes.MapPageRoute("BarSearch", "PostSearch", "~/Guest/Bar/PostSearch.aspx");

            //栏目首页
            routes.MapPageRoute("ColumnList", "Class_{ID}/Default.aspx", "~/BU/Front/ColumnList.aspx");
            routes.MapPageRoute("ColumnList2", "Class_{ID}/Default_{CPage}.aspx", "~/BU/Front/ColumnList.aspx");
            //栏目列表
            routes.MapPageRoute("NodePage", "Class_{ID}/NodePage.aspx", "~/BU/Front/NodePage.aspx");
            routes.MapPageRoute("NodePage2", "Class_{ID}/NodePage_{CPage}.aspx", "~/BU/Front/NodePage.aspx");
            //最新信息
            routes.MapPageRoute("NodeNews", "Class_{ID}/NodeNews.aspx", "~/BU/Front/NodeNews.aspx");
            routes.MapPageRoute("NodeNews2", "Class_{ID}/NodeNews_{CPage}.aspx", "~/BU/Front/NodeNews.aspx");
            //最热信息
            routes.MapPageRoute("NodeHot", "Class_{ID}/NodeHot.aspx", "~/BU/Front/NodeHot.aspx");
            routes.MapPageRoute("NodeHot2", "Class_{ID}/NodeHot_{CPage}.aspx", "~/BU/Front/NodeHot.aspx");
            //推荐信息
            routes.MapPageRoute("NodeElite", "Class_{ID}/NodeElite.aspx", "~/BU/Front/NodeElite.aspx");
            routes.MapPageRoute("NodeElite2", "Class_{ID}/NodeElite_{CPage}.aspx", "~/BU/Front/NodeElite.aspx");
            //商品浏览
            routes.MapPageRoute("Shop", "Shop/{ID}.aspx", "~/BU/Front/Shop.aspx");
            //内容页,为兼容保留.aspx
            routes.MapPageRoute("Content", "Item/{ID}_{CPage}.aspx", "~/BU/Front/Content.aspx");
            routes.MapPageRoute("Content2", "Item/{ID}.aspx", "~/BU/Front/Content.aspx");
            routes.MapPageRoute("Content3", "Item/{ID}", "~/BU/Front/Content.aspx");
            routes.MapPageRoute("Content4", "Item/{ID}_{CPage}", "~/BU/Front/Content.aspx");
            //routes.MapPageRoute("Chinese_Content", "Item/{Title}", "~/BU/Front/Content.aspx");
            //专题
            routes.MapPageRoute("Special1", "Special_{ID}/Default", "~/BU/Front/Special.aspx");
            routes.MapPageRoute("Special2", "Special_{ID}/Default_{CPage}", "~/BU/Front/Special.aspx");
            routes.MapPageRoute("Special_List1", "Special_{ID}/List", "~/BU/Front/SpecialList.aspx");
            routes.MapPageRoute("Special_List2", "Special_{ID}/List_{CPage}", "~/BU/Front/SpecialList.aspx");
            //routes.MapPageRoute("SpecialList", "Item/{ID}_{CPage}.aspx", "~/BU/Front/Special.aspx");
            ////店铺与黄页(已转为MVC)
            //routes.MapPageRoute("StoreList", "Store/StoreList_{CPage}.aspx", "~/Store/StoreList.aspx");
            //routes.MapPageRoute("Page", "Page/PageList_{CPage}.aspx", "~/Page/PageList.aspx");
            //Pub提交与预览
            routes.MapPageRoute("PreView", "PreView.aspx", "~/BU/Front/PreView.aspx");
            routes.MapPageRoute("PubAction", "PubAction.aspx", "~/BU/Front/PubAction.aspx");
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
        public static string GetParam(string pname, HttpRequestBase req)
        {
            string item = "";
            //if (page.RouteData.Values[pname] != null)
            //{
            //    item = page.RouteData.Values[pname].ToString();
            //}
            //else 
            if (!string.IsNullOrEmpty(req.QueryString[pname]))
            {
                item = req.QueryString[pname];
            }
            return item;
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
