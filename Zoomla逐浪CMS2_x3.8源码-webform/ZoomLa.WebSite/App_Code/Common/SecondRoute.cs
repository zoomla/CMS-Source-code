using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.UI;
using ZoomLa.BLL;
using ZoomLa.BLL.Site;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Site;
using ZoomLa.SQLDAL;

/// <summary>
/// 网站路由器,负责二级域名导航
/// </summary>
public class SecondRoute:Route
{
    //要匹配的域名
    public string Domain { get; set; }
    //传入了参数,则全部由其处理了,不经GetRouteData了
    public SecondRoute(string url) : base(url, new ZLRouteHandler()) { }
    //重写该方法返回数据给Handler
    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
        string domain = httpContext.Request.Url.Host;
        string path = httpContext.Request.Path;
        RouteData data = null;//绑定Handler，这里完成后交给Handler
        M_IDC_DomainList model = RouteHelper.GetModelByDomain(domain);
        if (RouteHelper.RouteDT == null || RouteHelper.RouteDT.Rows.Count < 1 || model == null)
        {
            new PageRouteHandler("~" + httpContext.Request.FilePath);
        }
        else
        {
            switch (model.SType)
            {
                case 1://directory
                    #region 后台目录指向
                    {
                        data = new RouteData(this, RouteHandler);
                        //用户访问的路径
                        string filepath = httpContext.Request.FilePath.Equals("/") ? "default" : httpContext.Request.FilePath.TrimStart('/');
                        filepath = filepath.Split('.')[0];//不保留路径
                        //替换占位符为实际文件路径(/test/test.aspx)
                        model.Url = model.Url.Replace("*", filepath);
                        //将实际访问的Url写入
                        data.Values["url"] = model.Url;
                        PushParam(data, model.Url);
                        break;
                    }
                    #endregion
                case 2://Wix,  mydomain.z01.com/page
                    {
                        data = new RouteData(this, RouteHandler);
                        //string mydomain = domain.Split('.')[0];
                        string url = "/Design/PreView.aspx";//domain=mydomain&page=page
                        if (string.IsNullOrEmpty(path) || path.Equals("/")) { path = "/index"; }
                        data.Values["url"] = url;
                        PushParam(data, url + "?domain=" + domain + "&path=" + path);
                    }
                    break;
            }
        }
        return data;
    }
    /// <summary>
    /// 将地址的参数写入路由中,方便目标页面获取
    /// </summary>
    public void PushParam(RouteData data, string url)
    {
        if (url.IndexOf("?") < 0) return;
        string paramStr = url.Split('?')[1];
        if (string.IsNullOrEmpty(paramStr)) return;
        string[] paramArr = paramStr.Split('&');
        foreach (string param in paramStr.Split('&'))
        {
            string name = param.Split('=')[0];
            string value = param.Split('=')[1];
            if (string.IsNullOrEmpty(name)) continue;
            data.Values[name] = value;
        }
    }
}
//PageRouteHandler
public class ZLRouteHandler : IRouteHandler
{
    public IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
        return new SecoundDomainHanldler(requestContext.RouteData);
    }
    private class SecoundDomainHanldler : IHttpHandler, IRequiresSessionState
    {
        public RouteData Data { get; set; }
        public SecoundDomainHanldler(RouteData data)
        {
            Data = data;
        }
        public bool IsReusable { get { return false; } }
        public void ProcessRequest(HttpContext context)
        {
            context.Server.Execute(Data.Values["url"].ToString());
        }
    }
}
public class RouteHelper
{
    //附加的路由表
    //路由映射表
    public static DataTable RouteDT=new DataTable();
    static RouteHelper()
    {
        try { RouteDT = new B_IDC_DomainList().Sel(); }
        catch (Exception ex) { RouteDT = new DataTable(); ZLLog.L(ZLEnum.Log.exception, "域名路由加载失败,原因:" + ex.Message); }
    }
    public static M_IDC_DomainList GetModelByDomain(string domain)
    {
        domain = domain.Replace(" ", "").ToLower();
        M_IDC_DomainList model = null;
        for (int i = 0; i < RouteDT.Rows.Count; i++)
        {
            if (RouteDT.Rows[i]["DomName"].Equals(domain))
            { model = new M_IDC_DomainList().GetModelFromReader(RouteDT.Rows[i]); break; }
        }
        return model;
    }
    /// <summary>
    /// 根据域名返回Url
    /// </summary>
    public static string GetUrlByDomain(string domain)
    {
        string url = "";
        foreach (DataRow dr in RouteDT.Rows)
        {
            if (dr["domain"].ToString().Equals(domain))
            {
                url = dr["SendTo"].ToString();
                break;
            }
        }
        return url;
    }
    /// <summary>
    /// //检测是否有自己的二级域名信息,无则直接输出，有则处理后输出,主用于跳转时，其他Submit等时不会
    /// </summary>
    /// <param name="vpath">Route路径</param>
    public static string GetRouteUrl(string vpath)
    {
        string domain = HttpContext.Current.Request.Url.Host;
        string url = GetUrlByDomain(domain);
        if (!string.IsNullOrEmpty(url))
        { return vpath; }
        return HttpContext.Current.Request.FilePath;
    }
}