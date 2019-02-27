using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Configuration;
using ZoomLa.BLL.Helper;

public partial class manage_Common_SystemFinger : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Request.UrlReferrer.AbsoluteUri.ToString();
            }
            catch (NullReferenceException)
            {
                function.WriteErrMsg("错误的Url或非法的请求！", "");
            }
            lbServerName.Text = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
            lbIp.Text = Request.ServerVariables["LOCAl_ADDR"];
            DBIP_L.Text = GetDBIP();
            lbDomain.Text = Request.ServerVariables["SERVER_NAME"].ToString();
            lbPort.Text = Request.ServerVariables["Server_Port"].ToString();
            lbIISVer.Text = Request.ServerVariables["Server_SoftWare"].ToString();
            lbPhPath.Text = Request.PhysicalApplicationPath;
            lbOperat.Text = Environment.OSVersion.ToString();
            lbSystemPath.Text = Environment.SystemDirectory.ToString();
            lbTimeOut.Text = (Server.ScriptTimeout / 1000).ToString() + "秒";
            lbLan.Text = CultureInfo.InstalledUICulture.EnglishName;
            lbAspnetVer.Text = string.Concat(new object[] { Environment.Version.Major, ".", Environment.Version.Minor, Environment.Version.Build, ".", Environment.Version.Revision });
            lbCurrentTime.Text = DateTime.Now.ToString();
            OperatingSystem osinfo = Environment.OSVersion;
            SystemVersion_L.Text = GetSystemVersion();
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Version Vector");
            lbIEVer.Text = key.GetValue("IE", "未检测到").ToString();
            lbServerLastStartToNow.Text = ((Environment.TickCount / 0x3e8) / (60 * 60)).ToString() + "分钟";

            string[] achDrives = Directory.GetLogicalDrives();
            for (int i = 0; i < Directory.GetLogicalDrives().Length - 1; i++)
            {
                lbLogicDriver.Text = lbLogicDriver.Text + achDrives[i].ToString();
            }
            lbCpuNum.Text = Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS").ToString();
            lbCpuType.Text = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER").ToString();
            lbMemory.Text = (Environment.WorkingSet / 1024).ToString() + "M";
            lbMemoryPro.Text = ((Double)GC.GetTotalMemory(false) / 1048576).ToString("N2") + "M";
            lbMemoryNet.Text = ((Double)Process.GetCurrentProcess().WorkingSet64 / 1048576).ToString("N2") + "M";
            lbCpuNet.Text = ((TimeSpan)Process.GetCurrentProcess().TotalProcessorTime).TotalSeconds.ToString("N0");
            lbSessionNum.Text = Session.Contents.Count.ToString();
            lbSession.Text = Session.Contents.SessionID;
            lbUser.Text = "逐浪CMS2_X3.6 版";


        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Config/DatalistProfile.aspx'>扩展功能</a></li> <li><a href='" + CustomerPageAction.customPath2 + "Config/RunSql.aspx'>开发中心</a></li><li><a href='SystemFinger.aspx'>服务器信息总览</a></li>" + Call.GetHelp(72));
    }
    private string GetDBIP()
    {
        string constr = ZoomLa.SQLDAL.SqlHelper.ConnectionString;
        string dbip = StrHelper.GetAttrByStr(constr,"Data Source").ToLower().TrimStart('(').TrimEnd(')');
        string serverip = Request.ServerVariables["LOCAl_ADDR"]; 
        switch (dbip)
        {
            case "local":
            case ".":
            case "127.0.0.1":
                dbip = serverip + "<span style='color:green;'>(本机)</span>";
                break;
            default:
                break;
        }
        return dbip;
    }
    public string GetSystemVersion()
    {
        OperatingSystem osinfo = Environment.OSVersion;
        PlatformID platid = osinfo.Platform;
        int versionMajor = osinfo.Version.Major;
        int versionMinor = osinfo.Version.Minor;
        if (platid == PlatformID.Win32NT && versionMajor == 5 && versionMinor == 1)
            return "Windows XP";
        if (platid == PlatformID.Win32NT && versionMajor == 5 && versionMinor == 2)
            return "Windows 2003";
        if (platid == PlatformID.Win32NT && versionMajor == 6 && versionMinor == 0)
            return "Windows Vista";
        if (platid == PlatformID.Win32NT && versionMajor == 6 && versionMinor == 1)
            return "Windows 7";
        if (platid == PlatformID.Win32NT && versionMajor == 6 && versionMinor == 2)
            return "Windows 8";
        if (platid == PlatformID.Win32NT && versionMajor == 6 && versionMinor == 3)
            return "Windows 8.1";
        return "非Windows操作系统";
    }
}