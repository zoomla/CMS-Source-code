using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Components;

namespace ZoomLaCMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new ZLFilterAttribute());
        }
    }
    public class ZLFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //filterContext.HttpContext.Response.Write("Action执行之前");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            //filterContext.HttpContext.Response.Write("Action执行之后");
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            //filterContext.HttpContext.Response.Write("返回Result之前");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //System.Web.Mvc.ViewResult|System.Web.Mvc.JavaScriptResult|System.Web.Mvc.ContentResult|System.Web.Mvc.PartialViewResult
            base.OnResultExecuted(filterContext);
            string atype = filterContext.Result.GetType().ToString();
            if (!string.IsNullOrEmpty(SiteConfig.SiteInfo.AllSiteJS) && atype == "System.Web.Mvc.ViewResult")
            {
                filterContext.HttpContext.Response.Write("\r\n<!--全局统计代码开始-->\r\n");
                filterContext.HttpContext.Response.Write(SiteConfig.SiteInfo.AllSiteJS);
                filterContext.HttpContext.Response.Write("\r\n<!--全局统计代码结束-->");
            }
        }
    }
}