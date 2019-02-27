using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ZoomLa.BLL.Helper
{
    //用于判断设备与浏览器类型
    public class DeviceHelper
    {
        /// <summary>
        /// 浏览器,建议先判断设备,再判断浏览器
        /// </summary>
        public enum Brower { Micro, IE, Chrome, Webkit, Unknown };
        /// <summary>
        /// 设备
        /// </summary>
        public enum Agent { WindowsPhone, Android, iPhone, iPad, PC, UnKnown };
        public static DeviceHelper.Agent GetAgent() { return GetAgent(HttpContext.Current.Request.UserAgent); }
        public static DeviceHelper.Agent GetAgent(string agent)
        {
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
        public static DeviceHelper.Brower GetBrower() { return GetBrower(HttpContext.Current.Request.UserAgent); }
        public static DeviceHelper.Brower GetBrower(string agent)
        {
            if (agent.Contains("MicroMessenger"))
            {
                return Brower.Micro;
            }
            else if (agent.Contains("Chrome/"))
            {
                //360等使用了chrome核心的会识别为该项
                return Brower.Chrome;
            }
            else if (agent.Contains("MSIE") || agent.Contains("Trident/"))
            {
                //IE核心的Edge可检测,但win10版Edge不可检测
                return Brower.IE;
            }
            else
            {
                return Brower.Unknown;
            }
        }
        /// <summary>
        /// 是否移动设备True:是
        /// </summary>
        public static bool IsMobile
        {
            get
            {
                HttpRequest r = HttpContext.Current.Request;
                if (r.Browser.IsMobileDevice || r.Browser.Browser.Equals("Unknown") || r.Browser.Browser.Equals("Mozilla"))
                    return true;
                else{ return false; }
            }
        }
    }
}
