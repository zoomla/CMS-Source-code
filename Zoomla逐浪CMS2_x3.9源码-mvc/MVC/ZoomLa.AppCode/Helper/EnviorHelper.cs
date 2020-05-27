using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using ZoomLa.Common;
using System.Linq;
using ZoomLa.BLL.Helper;
using System.Data;

/// <summary>
/// 获取所在服务器的各种信息，和web.config中的信息,后期移入Common中
/// 该类主要用于站群管理，操作本地web.config请用VS提供的类
/// 此是对Environment和Request.ServerVariables["Server_Port"]的补充或扩展。
/// </summary>
public class EnviorHelper
{
    private XmlDocument xmlDoc = new XmlDocument();
    /// <summary>
    /// 返回当前服务器的操作系统版本
    /// Microsoft Windows Server 2012 Datacenter
    /// </summary>
    /// <returns></returns>
    public string GetOSVersion() 
    {
        //System.Management.SelectQuery query = new System.Management.SelectQuery("Win32_OperatingSystem");
        //System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query);
        //System.Management.ManagementObjectCollection objs = searcher.Get();
        //string s = "";
        //foreach (System.Management.ManagementObject obj in objs)
        //    s += obj.GetPropertyValue("Caption").ToString();
        //return s;
        return "";
    }
    /// <summary>
    /// 返回当前服务端IP，包含IPV6与IPV4,筛选返回IPV4格式IP
    /// </summary>
    /// <returns></returns>
    public string GetServerIP() 
    {
        //注意返回的信息有IPV6的IP也有IPV4的，都包含在返回的数组当中
        //对于 IPv4，返回 InterNetwork；对于 IPv6，返回 InterNetworkV6。
        IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
        string myip = "";
        for (int i = 0; i < IpEntry.AddressList.Length; i++)
        {
            if (IpEntry.AddressList[i].AddressFamily.ToString() == "InterNetwork")
            {
                myip += IpEntry.AddressList[i].ToString() + ",";
            }
        }
            return myip.TrimEnd(',');
    }
    public static string GetUserIP()
    {
        return IPScaner.GetUserIP();
    }
    //---------------------------------------------WebConfig信息读取与操作
    //<customErrors mode="Off" defaultRedirect="~/Prompt/GenericError.htm">
    //<error statusCode="403" redirect="~/Prompt/NoAccess.html" />
    //<error statusCode="404" redirect="~/Prompt/FileNotFound.html" />
    //<error statusCode="500" redirect="~/Prompt/GenericError.html" />
    //</customErrors>
    public XmlDocument GetWebConfig(string ppath = "")
    {
        if (string.IsNullOrEmpty(ppath))
            ppath = function.VToP("/Web.config");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(ppath);
        return xmlDoc;
    }
    /// <summary>
    /// 如无customErrors则创建
    /// </summary>
    public void IsExistCustomErrorTag(string path)
    {
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(path);
        if (xmldoc.SelectSingleNode("/configuration/system.web/customErrors") == null)
        {
            XmlNode parent = xmldoc.SelectSingleNode("/configuration/system.web");
            XmlElement child = xmldoc.CreateElement("customErrors");
            child.SetAttribute("mode", "off");
            parent.AppendChild(child);
        }
        xmldoc.Save(path);
    }
    /// <summary>
    /// Update The CustomeError
    /// </summary>
    public void UpdateCustomError(XmlDocument xdoc, int statusCode, string url)
    {
        //无则插入，先判断有无customErrors,再判断有无对应error如无，则加上
        //必须保证有Error标记
        XmlDocument xmldoc = xdoc;
        XmlNode node = xmldoc.SelectSingleNode("/configuration/system.web/customErrors/error[@statusCode=" + statusCode + "]");
        if (node != null)//如果节点存在，则更新
        {
            node.Attributes["redirect"].Value = url;
        }
        else //节点不存在,创建
        {
            node = xmldoc.SelectSingleNode("/configuration/system.web/customErrors");//父节点
            XmlElement childNode = xmldoc.CreateElement("error");
            childNode.SetAttribute("statusCode", "" + statusCode);
            childNode.SetAttribute("redirect", url);
            node.AppendChild(childNode);
        }
    }
    public DataTable SelAllErrorCode(string ppath = "")
    {
        //注意大小写
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ErrCode", typeof(int)));
        dt.Columns.Add(new DataColumn("Url", typeof(string)));
        XmlDocument xmldoc = GetWebConfig(ppath); 
        XmlNode node = xmldoc.SelectSingleNode("/configuration/system.web/customErrors");
        if (node == null) return dt; 
        foreach (XmlNode child in node.ChildNodes)
        {
            try
            {
                DataRow dr = dt.NewRow();
                dr["ErrCode"] = child.Attributes["statusCode"].Value;
                dr["Url"] = child.Attributes["redirect"].Value;
                dt.Rows.Add(dr);
            }
            catch { }
        } 
        return dt;
    }
    /// <summary>
    /// iis.Sites[siteName].Applications[0].VirtualDirectories[0].PhysicalPath + "\\web.config"
    /// </summary>
    /// <param name="path"></param>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public CustomError GetCustomError(string ppath, int statusCode)
    {
        CustomError ce = new CustomError(statusCode, "<系统默认>");
        DataTable dt = SelAllErrorCode(ppath);
        dt.DefaultView.RowFilter = "ErrCode=" + statusCode;
        if (dt.DefaultView.ToTable().Rows.Count > 0)
        {
            ce.Redirect = dt.DefaultView.ToTable().Rows[0]["Url"].ToString();
        }
        return ce;
    }
    /// <summary>
    /// 最大上传容量
    /// </summary>
    public string GetMaxFile()
    {
        string value = "";
        xmlDoc = GetWebConfig();
        XmlNode node = xmlDoc.SelectSingleNode("/configuration/system.webServer/security/requestFiltering/requestLimits");
        if (node == null || node.Attributes.Count < 1)
        {
            value = "30";//30M
        }
        else
        {
            value = (DataConverter.CDouble(node.Attributes[0].Value) / (1024 * 1024)).ToString("f0");
        }
        return value;
    }
    public void UpdateMaxFile(string filesize)
    {
        if (Convert.ToInt64(filesize) != (Convert.ToInt64(GetMaxFile()) * 1024 * 1024))
        {
            XmlDocument xmldoc = GetWebConfig();
            XmlNode webserver = xmldoc.SelectSingleNode("/configuration/system.webServer");
            XmlNode security = xmldoc.SelectSingleNode("/configuration/system.webServer/security");
            security = security == null ? webserver.AppendChild(xmldoc.CreateElement("security")) : security;
            XmlNode requestFiltering = xmldoc.SelectSingleNode("/configuration/system.webServer/security/requestFiltering");
            requestFiltering = requestFiltering == null ? security.AppendChild(xmldoc.CreateElement("requestFiltering")) : requestFiltering;
            XmlNode requestLimits = xmldoc.SelectSingleNode("/configuration/system.webServer/security/requestFiltering/requestLimits");
            if (requestLimits == null)
            {
                XmlElement child = xmldoc.CreateElement("requestLimits");
                child.SetAttribute("maxAllowedContentLength", filesize);
                requestLimits = requestFiltering.AppendChild(child);
            }
            else
            {
                requestLimits.Attributes["maxAllowedContentLength"].Value = filesize;
            }
            xmldoc.Save(function.VToP("/Web.config"));
        }
    }
    //更新最大上传容量
    public void SetMaxContent(int maxlen)
    {
        if (maxlen / (1024 * 1024) < 1) function.WriteSuccessMsg("设置容量过少,最低不能小于1M");
        xmlDoc = GetWebConfig();
    }
    //---------------------------------------------

    /// <summary>
    /// 依据Config/Appsetting判断目标是否为逐浪站点，是的话返回版本号,使用时物理路径+/Config/AppSettings.config
    /// </summary>
    /// <returns></returns>
    public string IsZoomlaWebSite(string path) 
    {
        //异常太消耗资源，所以不用异常跳出，用IO判断
        //目标下存在Config/Appsetting,并且其中有<add key="Version" value="X1.120131106" />格式的数据才会认为是Zoomla,
        //包含X是为与PowerEasy区分
        string result = "";
        if (File.Exists(path))//如果文件不存在，则直接返回
        {
            xmlDoc.Load(path);
            XmlNodeList nodeList = xmlDoc.SelectNodes("appSettings/add");
            if (nodeList != null && nodeList.Count > 0)// 因用Xpath属性筛选,取不到值,循环方式取出值
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    if (nodeList[i].Attributes["key"].Value.ToString().ToLower().Equals("version") && nodeList[i].Attributes["value"].Value.ToString().ToLower().Contains("x")) 
                    { result = nodeList[i].Attributes["value"].Value.ToString(); break; }
                }
            }
        }
        return result;
    }
}