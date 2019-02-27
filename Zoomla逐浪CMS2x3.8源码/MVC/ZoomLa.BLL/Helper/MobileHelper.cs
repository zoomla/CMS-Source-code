using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ZoomLa.BLL.Helper
{
    public class MobileHelper
    {
        /// <summary>
        /// UnKnown可能是新设备或Linux等操作系统的浏览器
        /// </summary>
        public enum Agent { WindowsPhone, Android, iPhone, iPad, PC, UnKnown };
        public static bool IsMobile
        {
            get
            {
                HttpRequest r = HttpContext.Current.Request;
                if (r.Browser.IsMobileDevice || r.Browser.Browser.Equals("Unknown") || r.Browser.Browser.Equals("Mozilla"))
                    return true;
                else
                { return false; }
            }
        }
        public Agent GetAgent()
        {
            string agent = HttpContext.Current.Request.UserAgent;
            if (agent.Contains("Windows NT") || agent.Contains("compatible; MSIE"))
            {
                return Agent.PC;
            }
            else if (agent.Contains("Macintosh"))//苹果桌面系统
            {
                return Agent.PC;
            }
            else//移动设备类型判断
            {
                if (agent.Contains("Windows Phone"))//必须在Android前,否则会误判
                {
                    return Agent.WindowsPhone;
                }
                else if (agent.Contains("Android"))
                {
                    return Agent.Android;
                }
                else if (agent.Contains("iPhone"))
                {
                    return Agent.iPhone;
                }
                else if (agent.Contains("iPad"))
                {
                    return Agent.iPad;
                }
            }
            return Agent.UnKnown;
        }
    }
}
