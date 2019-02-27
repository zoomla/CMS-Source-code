using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.BLL.Helper;
using ZoomLa.Model;
using ZoomLa.Model.User;

/// <summary>
///VisitCounter 的摘要说明
/// </summary>
public class VisitCounter
{
    
    public static void AddVisitCounter()
    {
    }
    public class User//获取用户操作系统和浏览器版本
    {
        public static string Agent(int intNum)
        {
            string strResult = null;
            strResult = "";

            switch (intNum)
            {
                case 1:
                    strResult = HttpContext.Current.Request.Browser.Type;//Browser(strResult);
                    break;
                case 2:
                    strResult = System(HttpContext.Current.Request.UserAgent);
                    break;
                case 3:
                    strResult = Device(HttpContext.Current.Request.UserAgent);
                    break;
            }
            return strResult;
        }
        /// <summary>
        /// 获得系统图标
        /// </summary>
        /// <param name="os"></param>
        /// <returns></returns>
        public static string SystemIcon(string os)
        {
            switch (os)
            {
                case "Windows 10":
                    return "<span class='fa fa-windows'></span> Windows 10";
                case "Windows 8.1/Windows Server 2012":
                    return "<span class='fa fa-windows'></span> Windows 8.1";
                case "Windows 8":
                    return "<span class='fa fa-windows'></span> Windows 8";
                case "Windows 7":
                    return "<span class='fa fa-windows'></span> Windows 7";
                case "Windows Vista/Server 2008":
                    return "<span class='fa fa-windows'></span> Windows Vista";
                case "Windows XP":
                    return "<span class='fa fa-windows'></span> Windows XP";
                case "IOS":
                    return "<span class='fa fa-mobile'></span> IOS";
                case "Mac OS":
                    return "<span class='fa fa-apple'></span> Mac OS";
                case "Android OS":
                    return "<span class='fa fa-android'></span> Android";
                case "UNIX":
                    return "<span class='fa fa-underline'></span> UNIX";
                case "Linux":
                    return "<span class='fa fa-linux'></span> Linux";
                default:
                    return "<span class='fa fa-cog'></span> 其它系统";
            }
        }
        /// <summary>
        /// 获得浏览器图标
        /// </summary>
        /// <param name="browser"></param>
        /// <returns></returns>
        public static string BrowserIcon(string browser)
        {
            if (browser.Contains("Chrome"))
                return browser;
            if (browser.Contains("InternetExplorer"))
                return browser;
            if (browser.Contains("Firefox"))
                return browser;
            return "其它浏览器";

        }
        public static string System(string userAgent)
        {
            string osVersion = "其他系统";
            if (userAgent.Contains("NT 10.0"))
                osVersion = "Windows 10";
            else if (userAgent.Contains("NT 6.3"))
                osVersion = "Windows 8.1/Windows Server 2012";
            else if (userAgent.Contains("NT 6.2"))
                osVersion = "Windows 8";
            else if (userAgent.Contains("NT 6.1"))
                osVersion = "Windows 7";
            else if (userAgent.Contains("NT 6.0"))
                osVersion = "Windows Vista/Server 2008";
            else if (userAgent.Contains("NT 5.2"))
                osVersion = "Windows Server 2003";
            else if (userAgent.Contains("NT 5.1"))
                osVersion = "Windows XP";
            else if (userAgent.Contains("iPhone")|| userAgent.Contains("iPad"))
                osVersion = "IOS";
            else if (userAgent.Contains("Mac"))
                osVersion = "Mac OS";
            else if (userAgent.Contains("Android"))
                osVersion = "Android OS";
            else if (userAgent.Contains("Unix"))
                osVersion = "UNIX";
            else if (userAgent.Contains("Linux"))
                osVersion = "Linux";
            else if (userAgent.Contains("SunOS"))
                osVersion = "SunOS";
            return osVersion;

            //过时操作系统
            //else if (userAgent.Contains("NT 5"))
            //    osVersion = "Windows 2000";
            //else if (userAgent.Contains("NT 4"))
            //    osVersion = "Windows NT4";
            //else if (userAgent.Contains("Me"))
            //    osVersion = "Windows Me";
            //else if (userAgent.Contains("98"))
            //    osVersion = "Windows 98";
            //else if (userAgent.Contains("95"))
            //    osVersion = "Windows 95";
        }
        /// <summary>
        /// 获取客户端设备
        /// </summary>
        /// <param name="useragent"></param>
        /// <returns></returns>
        public static string Device(string useragent)
        {
            string osVersion = "其他设备";
            if (useragent.Contains("Windows"))
                osVersion = "PC";
            else if (useragent.Contains("iPad"))
                osVersion = "iPad";
            else if (useragent.Contains("iPhone"))
                osVersion = "iPhone";
            else if (useragent.Contains("Android"))
                osVersion = "Android";
            else if (useragent.Contains("Windows Phone"))
                osVersion = "Windows Phone";
            return osVersion;
        }

        public static string Browser(string strPara)
        {
            string strResult = null;
            strResult = strPara.Replace("MSIE", "Internet Explorer");
            return strResult;
        }
    }
}