using System;
using System.Web;
using System.Xml;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Security;
using System.Linq;
using System.Text.RegularExpressions;
using System.Configuration;
using log4net;

namespace ZoomLa.Web.HttpModule
{
    public class IPHttpModule : IHttpModule
    {
        #region 支持方法
        //与UpdatePanel冲突，所以有些页面不让其输出,判断页面是否不用输出
        protected static string[] pageArr = new string[] { "createhtml.aspx", "gethits.aspx", "callcounter.aspx" };    
        //Url是否包含目标页面
        protected static bool PageIsContain(string url) 
        {
            try
            {
                string pageName = url.Split('/')[url.Split('/').Length - 1];
                return pageArr.Where(p => p.Contains(pageName)).ToArray().Length > 0;
            }
            catch { return true; }
        }
        protected static bool isAjax(HttpRequest r)
        {
            if (r == null)
            {
                throw new ArgumentNullException("request");
            }
            return ((r["X-Requested-With"] == "XMLHttpRequest") || ((r.Headers != null) && (r.Headers["X-Requested-With"] == "XMLHttpRequest")));
        }
        protected static void ShowErr(HttpContext ctx, string title, string msg)
        {
            ctx.Items["Message"] = HttpUtility.HtmlEncode(msg);
            ctx.Items["ReturnUrl"] = "/";
            ctx.Items["MessageTitle"] = title;
            ctx.Items["autoSkipTime"] = "0";
            ctx.Server.Transfer("~/Prompt/ShowError.aspx");
            ctx.Response.End();
        }
        protected static void Log(HttpContext ctx, string msg)
        {
            ILog logs = LogManager.GetLogger("exception");
            string result = "";
            result += "操作人：(用户名:,管理员名:,IP:" + ctx.Request.UserHostAddress + ")\r\n";
            result += "时间：(" + DateTime.Now + "),操作：(),来源：(" + ctx.Request.RawUrl + ")\r\n";
            result += "详情：(" + msg + ")\r\n";
            result += "/*-----------------------------------------------------------------------------------*/";
            logs.Info(result);
        }
        #endregion
        public void Application_BeginRequest(object source, EventArgs e)
        {
            HttpRequest req = HttpContext.Current.Request;
            string path = req.Url.AbsolutePath.ToLower();
            //域名归并,支持带端口跳转
            if (SiteConfig.SiteOption.DomainMerge)
            {
                string domain = SiteConfig.SiteInfo.SiteUrl;
                string orgin = Regex.Split(req.Url.AbsoluteUri, "://")[1].Split('/')[0];
                if (SiteConfig.SiteInfo.SiteUrl.Contains("://"))
                {
                    domain = Regex.Split(domain, "://")[1];
                }
                if (!orgin.ToLower().Equals(domain.ToLower()))
                {
                    string url = req.Url.AbsoluteUri.Replace(orgin, domain);
                    HttpContext.Current.Response.Redirect(url);
                }
            }
            //强制Https
            if (!SiteConfig.SiteOption.SafeDomain.Equals("1") || path.Equals("/tools/default.aspx") || req.IsSecureConnection) { }
            else
            {
                HttpContext.Current.Response.Redirect("https://" + req.Url.Host + req.Url.PathAndQuery); return;
            }
            //AppSettings版本设置判断
            //bool versionOK = false;//如果通过,则只检测一次,重启服务器后再重新检测
            //if (HttpContext.Current.Application["VersionOK"] == null)
            //{
            //    HttpContext current = HttpContext.Current;
            //    XmlDocument xmlDoc2 = new XmlDocument();
            //    xmlDoc2.Load(AppDomain.CurrentDomain.BaseDirectory + @"\Config\AppSettings.config");
            //    XmlNodeList amde = xmlDoc2.SelectSingleNode("appSettings").ChildNodes;
            //    foreach (XmlNode xn in amde)
            //    {
            //        XmlElement xe = (XmlElement)xn;
            //        if (xe.GetAttribute("key").ToString() == "Version" && xe.GetAttribute("value").ToString() == "X3.120160109")
            //        {
            //            current.Application["VersionOK"] = 1;
            //            versionOK = true; 
            //            break;
            //        }
            //    }
            //    if (!versionOK)
            //    {
            //        throw new Exception("您的版本不正确！当前为X3.1 Beta版，请检查您的AppSetting中Version版本设置");
            //    }
            //}
        }
        public void Application_PreSendContentRequest(object source, EventArgs e)
        {
            HttpContext context = ((HttpApplication)source).Context;
            RequestDeal(context);
        }
        protected void context_Error(object sender, EventArgs e)
        {
            //此处处理异常,不会截获ValidRequest等异常(需去除Application_Error)
            HttpContext ctx = HttpContext.Current;
            Exception ex = ctx.Server.GetLastError();
            Exception iex = ex.InnerException;
            Log(ctx, iex.Message);
            //ctx.Server.ClearError();
            //ShowErr(ctx, "<i class='fa fa-lightbulb-o'></i> 信息提示", iex.Message);//注意所放位置
        }
        private static void RequestDeal(HttpContext context)
        {
            string url = context.Request.Url.AbsolutePath.ToLower();
            if (!string.IsNullOrEmpty(SiteConfig.SiteInfo.AllSiteJS))//有内容才输出
            {
                //必须过滤下，否则会在js文件等中也会输出,另AJAX也不输出
                if (url.StartsWith("/api/") || isAjax(context.Request) || IPHttpModule.PageIsContain(url)) { }
                else if (url.Contains(".aspx") || url.Contains(".htm") || url.Contains(".html") || url.Contains(".shtml"))
                {
                    context.Response.Write("\r\n<!--全局统计代码开始-->\r\n");
                    context.Response.Write(SiteConfig.SiteInfo.AllSiteJS);
                    context.Response.Write("\r\n<!--全局统计代码结束-->");
                }
            }
            string closeSite = ConfigurationManager.AppSettings["SYSCloseSite"];
            if (!string.IsNullOrEmpty(closeSite) && closeSite.Equals("1"))
            {
                //除首页外,全部禁用
                if (url.Equals("/default.aspx") || url.Contains("/index.")) { }
                else if ((url.Contains(".aspx") || url.Contains(".htm") || url.Contains(".html") || url.Contains(".shtml")) && !isAjax(context.Request))
                {
                    context.Response.Clear();
                    function.WriteErrMsg("[仅提供首页访问]-该站点因违约欠费空间到期或其它原因已经关停,请联系网管或相应服务商咨询服务.");
                }
            }
            #region 简转繁(不用，使用流过滤),使用前台JS实现,主要是流过滤,对Html页面支持不好,
            //object o = System.Configuration.ConfigurationManager.AppSettings["TraditionalChinese"];
            //if (o != null && o.ToString().ToLower().Equals("true"))//html等静态页面，引入相应的JS，读Cookies进行处理,主要为了IE，
            //{
            //    context.Response.Cookies["SimpToTrad"]["Zhen"] = "t";
            //    if (url.Contains(".aspx"))
            //    {
            //        context.Response.Write("\r\n<script type='text/javascript' src='/JS/SimpToTrad.js'></script><script type='text/javascript'>zh_tran('t');</script>\r\n");
            //    }
            //}
            //else { context.Response.Cookies["SimpToTrad"]["Zhen"] = "n"; }
            #endregion
        }
        #region disuse
        //private static bool CheckIPlock(int lockType, string lockWhiteList, string lockBlackList, double userIP)
        //{
        //    bool flag = false; //访问的IP是否被限制 true被限制 false不限制
        //    if ((lockType != 0) && (userIP != 0.0))
        //    {
        //        //同时启用白名单与黑名单，先判断IP是否在黑名单中，如果不在，则允许访问；
        //        //如果在则再判断是否在白名单中，如果IP在白名单中则允许访问，否则禁止访问。
        //        if (lockType == 4)
        //        {
        //            flag = GetAnalysisResult(lockBlackList, userIP); //是否在黑名单内
        //            if (flag)
        //            {
        //                flag = !GetAnalysisResult(lockWhiteList, userIP); //是否不在白名单内
        //            }
        //            return flag;
        //        }
        //        //仅仅启用白名单，只允许白名单中的IP访问本站。
        //        if (lockType == 1)
        //        {
        //            flag = !GetAnalysisResult(lockWhiteList, userIP); //是否不在白名单内 
        //            return flag;
        //        }
        //        //仅仅启用黑名单，只禁止黑名单中的IP访问本站。
        //        if (lockType == 2) //是否在黑名单内
        //        {
        //            flag = GetAnalysisResult(lockBlackList, userIP);
        //            return flag;
        //        }
        //        //同时启用白名单与黑名单，先判断IP是否在白名单中，如果不在，则禁止访问；
        //        //如果在则再判断是否在黑名单中，如果IP在黑名单中则禁止访问，否则允许访问。
        //        if ((lockType == 3))
        //        {
        //            flag = !GetAnalysisResult(lockWhiteList, userIP); //是否不在白名单内
        //            if (!flag)
        //            {
        //                flag = GetAnalysisResult(lockBlackList, userIP); //是否在黑名单内
        //            }
        //            return flag;
        //        }                
        //    }
        //    return flag;
        //}
        //private static string GetBasePath(HttpRequest request)
        //{
        //    if (request == null)
        //    {
        //        return "/";
        //    }
        //    return VirtualPathUtility.AppendTrailingSlash(request.ApplicationPath);
        //}
        //private static bool IsRefusedVisitAdminPage(HttpContext context, int lockAdminType, string lockAdminWhiteList, string lockAdminBlackList, double userIP)
        //{
        //    string absolutePath = context.Request.Url.AbsolutePath.ToLower();
        //    string str2 = GetBasePath(context.Request) + SiteConfig.SiteOption.ManageDir + "/";
        //    //context.Response.Write(CheckIPlock(lockAdminType, lockAdminWhiteList, lockAdminBlackList, userIP).ToString());
        //    //context.Response.End();
        //    return (absolutePath.StartsWith(str2.ToLower(), StringComparison.CurrentCultureIgnoreCase) && CheckIPlock(lockAdminType, lockAdminWhiteList, lockAdminBlackList, userIP));
        //}
        // // 判断IP是否在IP范围内
        //private static bool GetAnalysisResult(string lockList, double userIP)
        //{
        //    bool flag = false;
        //    if (!string.IsNullOrEmpty(lockList))
        //    {
        //        string[] strArray = lockList.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
        //        for (int i = 0; i < strArray.Length; i++)
        //        {
        //            if (!string.IsNullOrEmpty(strArray[i]))
        //            {
        //                try
        //                {                            
        //                    string[] strArray2 = strArray[i].Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
        //                    flag = (DataConverter.CDouble(strArray2[0]) <= userIP) && (userIP <= DataConverter.CDouble(strArray2[1]));
        //                    if (flag)
        //                    {
        //                        return true;
        //                    }                            
        //                }
        //                catch (IndexOutOfRangeException)
        //                {
        //                    return false;
        //                }
        //            }
        //        }
        //    }
        //    return flag;
        //}
        #endregion
        void IHttpModule.Dispose()
        {
            
        }
        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(this.Application_BeginRequest);
            context.PreSendRequestContent += new EventHandler(this.Application_PreSendContentRequest);
            context.Error += new EventHandler(context_Error);
        }
    }
}
