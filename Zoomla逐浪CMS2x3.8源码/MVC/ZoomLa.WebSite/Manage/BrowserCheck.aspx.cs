namespace ZoomLaCMS.Manage
{
    using System;
    using System.Collections.Generic;
    using System.Management;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    public partial class BrowserCheck : System.Web.UI.Page
    {
        public string browser = "";//浏览器
        public string currentIP = "";//当前IP
        public string currentWindow = "";//当前屏幕
        public string cookiesSurrport = "";//Cookies
        public string UserAgent = ""; //服务器语言种类
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged();
            if (!IsPostBack)
            {
                lbServerName.Text = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
                IISVersion_L.Text = Request.ServerVariables["Server_SoftWare"].ToString();
                NFVersion_L.Text = string.Concat(new object[] { Environment.Version.Major, ".", Environment.Version.Minor, Environment.Version.Build, ".", Environment.Version.Revision });
            }
            tdMac.InnerText = getMacs();
            HttpBrowserCapabilities brObject = Request.Browser;
            browser = brObject.Type + "  版本：" + brObject.Version;
            if (brObject.Cookies)
            {
                cookiesSurrport = "支持（<font color='red'>推荐</font>）";
            }
            else
            {
                cookiesSurrport = "不支持(请使用ie5以上的浏览器或检查Cookies设置)";
            }
            currentIP = Page.Request.UserHostAddress;
            UserAgent = Page.Request.UserAgent;
            currentWindow = "深度:" + brObject.ScreenBitDepth;// "  屏大小：(" + brObject.ScreenPixelsWidth + "px," + brObject.ScreenPixelsHeight + "px)";        
        }
        public string getMacs()
        {
            string mac = null;
            ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    mac = mo["MacAddress"].ToString();
            }
            return mac;
        }
    }
}