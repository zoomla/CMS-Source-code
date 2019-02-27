using Microsoft.Web.Administration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;


/// <summary>
/// IISHelper提供各种管理
/// </summary>
public class IISHelper
{
    private ServerManager iis = new ServerManager();
    private EnviorHelper enHelper = new EnviorHelper();
    private B_Site_SiteList siteBll = new B_Site_SiteList();
    public IISHelper()
    {
        //_server = "localhost";
        //_website = "1";
    }
    /// <summary>
    /// 获取网站信息列表
    /// </summary>
    public DataTable GetWebSiteList()
    {
        DataTable endDT = siteBll.SelAll();
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("SiteID", typeof(string)));
        dt.Columns.Add(new DataColumn("SiteName", typeof(string)));
        dt.Columns.Add(new DataColumn("SiteState", typeof(string)));
        dt.Columns.Add(new DataColumn("SitePort", typeof(string)));
        dt.Columns.Add(new DataColumn("PhysicalPath", typeof(string)));
        dt.Columns.Add(new DataColumn("Domain", typeof(string)));
        dt.Columns.Add("AppPoolName", typeof(string));
        dt.Columns.Add("EndDate",typeof(DateTime));
        //dt.Columns.Add(new DataColumn("CreateTime",typeof(string)));
        try
        {
            foreach (Site s in iis.Sites)
            {
                DataRow dr = dt.NewRow();
                try
                {
                    dr["SiteID"] = s.Id;
                    dr["SiteName"] = s.Name;
                    dr["SiteState"] = s.State.ToString();
                    dr["PhysicalPath"] = s.Applications[0].VirtualDirectories[0].PhysicalPath;
                    dr["SitePort"] = s.Bindings[0].EndPoint.Port;
                    //dr["DomainName"] = s.Bindings[0].BindingInformation.Split(':')[2];
                    dr["Domain"] = s.Bindings[0].Host;
                    //if (s.Bindings.Count > 1) { dr["Domain"] += "(绑定多个，点击查看详细)"; }
                    dr["AppPoolName"] = s.Applications[0].ApplicationPoolName;
                    dr["EndDate"] = endDT.Select("SiteID="+s.Id)[0]["EndDate"].ToString();
                }
                catch { }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        catch (UnauthorizedAccessException) 
        {
            string remind = "当前管理帐户权限不足,无法完成指定操作,请确保你输入的是windows管理员帐户,并且拥有对目标文件的操作权!!!";
            HttpContext.Current.Response.Redirect(CustomerPageAction.customPath + "Site/SiteConfig.aspx?remind=" + HttpContext.Current.Server.UrlEncode(remind));
            return dt; 
        }
    }
    /// <summary>
    /// 获取逐浪网站列表,true只返回逐浪网站,false返回全部，并有逐浪版本号
    /// </summary>
    public DataTable GetWebSiteList(bool flag)
    {
        DataTable siteList = GetWebSiteList();
        siteList.Columns.Add("zoomlaVersion",typeof(string));
        for (int i = 0; i < siteList.Rows.Count; i++)
        {
          siteList.Rows[i]["zoomlaVersion"]=enHelper.IsZoomlaWebSite(siteList.Rows[i]["PhysicalPath"].ToString()+"/Config/AppSettings.config");
        }
        if(flag)
        siteList.DefaultView.RowFilter = "zoomlaVersion not in ('')";//为true则只返回逐浪网站
        return siteList.DefaultView.ToTable();
    }
    /// <summary>
    /// 用于域名管理界面,同步入数据库
    /// </summary>
    /// <returns></returns>
    public DataTable GetDomanList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("SiteID", typeof(string)));
        dt.Columns.Add(new DataColumn("SiteName", typeof(string)));
        dt.Columns.Add(new DataColumn("SitePort", typeof(string)));
        dt.Columns.Add(new DataColumn("Domain", typeof(string)));
        dt.Columns.Add(new DataColumn("EndDate", typeof(string)));
        //dt.Columns.Add(new DataColumn("CreateTime",typeof(string)));
        int index=1;
        try
        {
            //有域名才加入记录
            foreach (Site s in iis.Sites)
            {
                try
                {
                    for (int i = 0; i < s.Bindings.Count; i++)//有多个域名，则绑定多个
                    {
                        DataRow dr = dt.NewRow();
                       
                        dr["SiteID"] = s.Id;
                        dr["SiteName"] = s.Name;
                        dr["SitePort"] = s.Bindings[i].EndPoint.Port;
                        //域名为空,或不包含.
                        if (string.IsNullOrEmpty(s.Bindings[i].Host)||!s.Bindings[i].Host.ToLower().Contains("www.")) continue;
                        dr["id"] = index++;
                        dr["Domain"] = s.Bindings[i].Host;
                        dr["EndDate"] = DomNameHelper.GetEndDate(s.Bindings[i].Host.ToLower().Replace("www.",""));
                        dt.Rows.Add(dr);
                    }
                }
                catch { }
              
            }
        }
        catch { }
        return dt;
    }

    /// <summary>
    /// 获取指定网站的详细信息
    /// </summary>
    /// <param name="siteName"></param>
    /// <returns></returns>
    public DataTable GetSiteData(string siteName) 
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID",typeof(string)));//即其在数组中的序列
        dt.Columns.Add(new DataColumn("SiteName",typeof(string)));
        dt.Columns.Add(new DataColumn("SiteState", typeof(string)));
        dt.Columns.Add(new DataColumn("SitePort", typeof(string)));
        //dt.Columns.Add(new DataColumn("PhysicalPath", typeof(string)));
        dt.Columns.Add(new DataColumn("Domain", typeof(string)));
        int index = 1;
        foreach (Binding b in iis.Sites[siteName].Bindings)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = index++;
            dr["SiteName"] = siteName;
            dr["SiteState"] = iis.Sites[siteName].State.ToString();
            //dr["PhysicalPath"] = iis.Sites[siteName].Applications[0].VirtualDirectories[0].PhysicalPath;
            string[] temp=b.BindingInformation.Split(':');//EndPoint不论哪个Binds里都是第一位的接口,而不是自己拥有的
            dr["SitePort"] = temp.Length>1?temp[1]:"未绑定端口";
            dr["Domain"] = b.Host;
            dt.Rows.Add(dr);
        }
        return dt;
    }
    /// <summary>
    /// 返回站点模型，包含站点所有信息
    /// </summary>
    public IISWebSite GetSiteModel(string siteName)
    {
        IISWebSite siteModel = new IISWebSite();
        siteModel.SiteName = siteName;
        siteModel.SiteID = iis.Sites[siteName].Id.ToString();
        siteModel.State = iis.Sites[siteName].State.ToString();
        siteModel.AppPool = iis.Sites[siteName].Applications[0].ApplicationPoolName;
        foreach (Binding b in iis.Sites[siteName].Bindings)
        {
            siteModel.BindInfoList.Add(b.BindingInformation);
        }
        foreach (VirtualDirectory v in iis.Sites[siteName].Applications[0].VirtualDirectories)
        {
            siteModel.PhysicalPathList.Add(v.PhysicalPath);
        }
        return siteModel;
    }
    /// <summary>
    /// 获取虚拟路径列表
    /// </summary>
    public DataTable GetVDList(string siteName)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("Index", typeof(string)));//即其在数组中的序列
        dt.Columns.Add(new DataColumn("SiteName", typeof(string)));
        dt.Columns.Add(new DataColumn("VPath", typeof(string)));
        dt.Columns.Add(new DataColumn("PPath", typeof(string)));
        int index = 1;
        foreach (VirtualDirectory v in iis.Sites[siteName].Applications[0].VirtualDirectories)
        {
            DataRow dr = dt.NewRow();
            dr["Index"] = index++;
            dr["SiteName"] = siteName;
            dr["VPath"] = v.Path;
            dr["PPath"] = v.PhysicalPath;
            dt.Rows.Add(dr);
        }
        return dt;
    }
    /// <summary>
    /// 根据ID获取名字
    /// </summary>
    public string GetNameBySiteID(string siteID)
    {
        //siteID不对应iis的Sites数组，只能这样取得
        string siteName = "";
        for (int i = 0; i < iis.Sites.Count; i++)
        {
            if (iis.Sites[i].Id.ToString().Equals(siteID))
            {
                siteName = iis.Sites[i].Name;
                break;
            }
        }
        return siteName;
    }
    /// <summary>
    ///根据停止,运行等状态,获取站点数据,0:Started,1:Stopped,默认返回启动中
    /// </summary>
    public int GetSiteCountByState(int flag) 
    {
        string state="";
        switch(flag)
        {
            case 0:
                state=ObjectState.Started.ToString();
                break;
            case 1:
                state=ObjectState.Stopped.ToString();
                break;
            default:
                state=ObjectState.Started.ToString();
                break;
        }
        DataTable siteDT = GetWebSiteList();
        siteDT.DefaultView.RowFilter = "SiteState in ('" + state + "')";
        return siteDT.DefaultView.ToTable().Rows.Count;
    }
    /// <summary>
    /// 返回逐浪站点的数量
    /// </summary>
    /// <param name="flag">状态同上</param>
    /// <param name="b">true只返回逐浪,falsg返回全部</param>
    /// <returns></returns>
    public int GetSiteCountByState(int flag,bool b)
    {
        string state = "";
        switch (flag)
        {
            case 0:
                state = ObjectState.Started.ToString();
                break;
            case 1:
                state = ObjectState.Stopped.ToString();
                break;
            default:
                state = ObjectState.Started.ToString();
                break;
        }
        DataTable siteDT = GetWebSiteList(b);
        siteDT.DefaultView.RowFilter = "SiteState in ('" + state + "')";
        return siteDT.DefaultView.ToTable().Rows.Count;
    }
    /// <summary>
    /// 根据模式/版本/状态标记返回数量信息
    /// </summary>
    public int GetPoolCountByMNS(string field,string condition)
    {
        DataTable poolDT = GetAppPoolList();
        poolDT.DefaultView.RowFilter = field+" in ('" + condition + "')";
        return poolDT.DefaultView.ToTable().Rows.Count;
    }
    //--默认文档
    /// <summary>
    /// 获取默认文档链表
    /// </summary>
    public string[] GetDefaultDocBySiteName(string siteName)
    {
        ConfigurationElementCollection filesCollection = GetDefaultElementsBySiteName(siteName);
        string defaultDoc = "";
        foreach (ConfigurationElement ce in filesCollection)
        {
            defaultDoc += ce.Attributes["value"].Value + ",";
        }
        return defaultDoc.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    }
    /// <summary>
    /// 添加指定站点默认文档,参数为该站点全部默认文档数组
    /// </summary>
    public void AddDefaultDocBysiteName(string siteName,params string[] defaultDoc)
    {
        //无ID身份标志,所以全部清除后,再逐一添加
        ConfigurationElementCollection ces =GetDefaultElementsBySiteName(siteName);
        ces.Clear();
        for (int i = 0; i < defaultDoc.Length; i++)
        {
            ConfigurationElement ce = ces.CreateElement();
            ce.Attributes["value"].Value = defaultDoc[i];
            ces.Add(ce);
        }
        iis.CommitChanges();
    }
    /// <summary>
    /// 根据网站名称，返回所有默认文档配置
    /// </summary>
    /// <param name="siteName"></param>
    /// <returns></returns>
    public ConfigurationElementCollection GetDefaultElementsBySiteName(string siteName)
    {
        Configuration confg = iis.GetWebConfiguration(siteName);
        ConfigurationSection section;
        section = confg.GetSection("system.webServer/defaultDocument");    
        ConfigurationElement filesElement = section.GetCollection("files");
        ConfigurationElementCollection filesCollection = filesElement.GetCollection();
        return filesCollection;
    }
    //--MimeType
    public ConfigurationElementCollection GetMimeTypeBySiteName(string siteName)
    {
        Configuration confg = iis.GetWebConfiguration(siteName); //webSiteName站点名称
        ConfigurationSection section;
        section = confg.GetSection("system.webServer/staticContent");     //取得MimeMap所有节点(路径为:%windir%\Windows\System32\inetsrv\config\applicationHost.config)
        ConfigurationElement filesElement = section.GetCollection();
        ConfigurationElementCollection filesCollection = filesElement.GetCollection();
        return filesCollection;
    }
    /// <summary>
    /// 添加一个新的Mime映射,如果存在,则更新
    /// </summary>
    /// <param name="siteName"></param>
    /// <param name="ext"></param>
    /// <param name="mimeType"></param>
    /// <returns></returns>
    public bool AddMimeType(string siteName, string ext, string mimeType)
    {
        Configuration confg = iis.GetWebConfiguration(siteName); //webSiteName站点名称

        ConfigurationSection section;
        section = confg.GetSection("system.webServer/staticContent");    

        ConfigurationElement filesElement = section.GetCollection();
        ConfigurationElementCollection filesCollection = filesElement.GetCollection();

        ConfigurationElement newElement = filesCollection.CreateElement(); //新建MimeMap节点

        newElement.Attributes["fileExtension"].Value = ext;
        newElement.Attributes["mimeType"].Value = mimeType;

        if (CheckMimeTypeIsExist(newElement, filesCollection, true))//如果存在,则更新它
        {

        }
        else
        {
            filesCollection.Add(newElement);
        }
        iis.CommitChanges();
        return true;
    }
    /// <summary>
    /// 检查有无该映射存在,update为true则如果存在的话,更新其否则仅判断
    /// </summary>
    public bool CheckMimeTypeIsExist(ConfigurationElement newElement, ConfigurationElementCollection filesCollection, bool update) 
    {
        bool flag=false;
        foreach (ConfigurationElement ce in filesCollection)
        {
            if (ce.Attributes["fileExtension"].Value.ToString().ToLower().Equals(newElement.Attributes["fileExtension"].Value.ToString().ToLower()))
            { 
                flag = true;
                if (update)
                    ce.Attributes["mimeType"].Value = newElement.Attributes["mimeType"].Value;
                break;
            }
        }
        return flag;
    }
    /// <summary>
    /// 根据后缀名,匹配到指定Element,返回
    /// </summary>
    /// <param name="ext"></param>
    /// <returns></returns>
    public ConfigurationElement GetMimeElementByExt(string ext,ConfigurationElementCollection ces) 
    {
        ConfigurationElement result = null;
        foreach (ConfigurationElement ce in ces)
        {
            if (ce.Attributes["fileExtension"].Value.ToString().ToLower().Equals(ext.ToLower()))
            {
                result = ce;
                break;
            }
        }
        return result;
    }
    /// <summary>
    /// 给网站名称与后缀名
    /// </summary>
    public bool RemoveMimeElement(string siteName,string ext) 
    {
        ConfigurationElementCollection ces = GetMimeTypeBySiteName(siteName);
        ConfigurationElement ce=GetMimeElementByExt(ext,ces);
        ces.Remove(ce);
        iis.CommitChanges();
        return true;
    }
    //-----------站点创建与获取
    /// <summary>
    /// 创建一个新站点,注意如果端口相同,则新建的网站会停止
    /// </summary>
    public bool CreateSite(IISWebSite WebSite)
    {
        return CreateSite(WebSite.SiteName, "http", "*:" + WebSite.Port + ":" + WebSite.DomainName, WebSite.PhysicalPath, WebSite.AppPool);//+WebSite.DomainName
    }
    public bool ChangeSiteName(string oldName, string newName)
    {
        if (iis.Sites[oldName] == null) return false;
        iis.Sites[oldName].Name = newName;
        iis.CommitChanges();
        return true;
    }
    /// <summary>
    /// 站点名，协议，动态域名与端口，物理路径，应用程序池名称
    /// </summary>
    public bool CreateSite(string siteName, string protol, string port, string physicalPath, string poolName)
    {
        if (iis.Sites[siteName] != null) { return false; }
        //  iis.Sites.Add("NewSite", "http", "*:766:www.z01.com", @"D:\Web\125楼");
        if (!Directory.Exists(physicalPath)) { Directory.CreateDirectory(physicalPath); }
        iis.Sites.Add(siteName, protol, port, physicalPath);
        iis.CommitChanges();
        //判断程序池是否存在,如果不存在,则创建
        if (!string.IsNullOrEmpty(poolName))
        {
            if (iis.ApplicationPools[poolName] != null)
                ChangeAppPool(siteName, poolName);
            else
            {
                CreateAppPool(poolName);
                ChangeAppPool(siteName, poolName);
            }
        }
        //----默认为Net2.0,经典,将其改为Net4.0,集成
        ChangeNetVersion(poolName, "v4.0");
        ChangeMode(poolName,"Integrated");
        return true;
    }
    public bool DeleteSite(string siteName)
    {
        if (iis.Sites[siteName] == null) return false;
        iis.Sites.Remove(iis.Sites[siteName]);
        iis.CommitChanges();
        return true;
    }
    /// <summary>
    /// 返回绑定的,不为空的网址
    /// </summary>
    public string GetDomainsBySite(string siteName) 
    {
        string s = "";
        if (iis.Sites[siteName] == null) return s;
        foreach (Binding b in iis.Sites[siteName].Bindings)
        {
            if (!string.IsNullOrEmpty(b.Host))
                s += b.Host + ",";
        }
        return string.IsNullOrEmpty(s)?s:s.Remove(s.Length-1).Trim();
 
    }
    //核对索引器与信息均OK时再删除(端口是唯一的,以端口为准判断)
    public void RemoveBinding(string siteName, int index, string port)
    {
        try//避免无端口信息等导致的报错
        {
            if (iis.Sites[siteName] != null && iis.Sites[siteName].Bindings.Count > index && iis.Sites[siteName].Bindings[index].BindingInformation.Split(':')[1] == port)
            {
                iis.Sites[siteName].Bindings.Remove(iis.Sites[siteName].Bindings[index]);
                iis.CommitChanges();
            }
        }
        catch { }
    }
    /// <summary>
    /// 移除，绑定，不加端口验证
    /// </summary>
    public void RemoveBinding(string siteName, int index)
    {
            if (iis.Sites[siteName] != null && iis.Sites[siteName].Bindings.Count>index)
            {
                iis.Sites[siteName].Bindings.Remove(iis.Sites[siteName].Bindings[index]);
                iis.CommitChanges();
            }
    }
    //-----------Virtual Directory Mnage
    public bool AddVD(string siteName, VirtualDirectory v)
    {
        bool flag = false;
        if (iis.Sites[siteName] != null && !string.IsNullOrEmpty(v.PhysicalPath))//站点存在,并且绑定信息不为空
        {
            iis.Sites[siteName].Applications[0].VirtualDirectories.Add(v);
            iis.CommitChanges();
            flag = true;
        }
        return flag;
    }
    public bool AddVD(string siteName, string alias, string ppath)
    {
        VirtualDirectory v = iis.Sites[siteName].Applications[0].VirtualDirectories.CreateElement();
        v.Path = alias;
        v.PhysicalPath = ppath;
        return AddVD(siteName, v);
    }
    public void RemoveVD(string siteName, int index)
    {
        if (iis.Sites[siteName] != null && iis.Sites[siteName].Applications[0].VirtualDirectories.Count > index)
        {
            iis.Sites[siteName].Applications[0].VirtualDirectories.Remove(iis.Sites[siteName].Applications[0].VirtualDirectories[index]);
            iis.CommitChanges();
        }
    }
    public void RemoveVD(string siteName, VirtualDirectory v)
    {
        if (iis.Sites[siteName] != null )
        {
            iis.Sites[siteName].Applications[0].VirtualDirectories.Remove(v);
            iis.CommitChanges();
        }
    }
    //-----------AppPool Manage(Such as NetVersion,Mode..)(没有ID,只有程序池名)
    /// <summary>
    /// Create The pool on the iis
    /// </summary>
    public bool CreateAppPool(string poolName)
    {
        if (iis.ApplicationPools[poolName] != null) { return false; }
        iis.ApplicationPools.Add(poolName).AutoStart = true;
        //iis.ApplicationPools[0].ManagedPipelineMode = ManagedPipelineMode.Integrated;//经典或集成
        iis.CommitChanges();
        return true;
    }
    /// <summary>
    /// 更改网站的应用程序池
    /// </summary>
    public bool ChangeAppPool(string siteName, string poolName)
    {
        if (iis.ApplicationPools[poolName] == null || iis.Sites[siteName] == null) { return false; }
        //iis.Sites["newSite"].Applications[0].ApplicationPoolName="new Pool";
        iis.Sites[siteName].Applications[0].SetAttributeValue("applicationPool", poolName);
        iis.CommitChanges();
        return true;
    }
    /// <summary>
    /// 返回所有的应用程序池信息
    /// </summary>
    /// <returns></returns>
    public DataTable GetAppPoolList()
    {
        DataTable temp = GetWebSiteList();
        DataTable dt = new DataTable();
        dt.Columns.Add("Index", typeof(string));
        dt.Columns.Add("AppPoolName", typeof(string));
        dt.Columns.Add("State", typeof(string));
        dt.Columns.Add("NetVersion", typeof(string));
        dt.Columns.Add("Mode", typeof(string));
        dt.Columns.Add("AppNum", typeof(string));
        for (int i = 0; i < iis.ApplicationPools.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Index"] = i + 1;
            dr["AppPoolName"] = iis.ApplicationPools[i].Name;
            dr["State"] = iis.ApplicationPools[i].State.ToString();//Started
            dr["NetVersion"] = iis.ApplicationPools[i].ManagedRuntimeVersion;
            dr["Mode"] = iis.ApplicationPools[i].ManagedPipelineMode.ToString();
            temp.DefaultView.RowFilter = "AppPoolName in ('" + dr["AppPoolName"] + "')";
            dr["AppNum"] = temp.DefaultView.ToTable().Rows.Count;
            dt.Rows.Add(dr);
        }
        return dt;
    }
    /// <summary>
    /// v2.0||v4.0
    /// </summary>
    public void ChangeNetVersion(string appName,string versionName) 
    {
        iis.ApplicationPools[appName].ManagedRuntimeVersion = versionName;
        iis.CommitChanges();
    }
    /// <summary>
    /// Classic||Integrated
    /// </summary>
    public void ChangeMode(string appName, string mode)
    {
        if(mode=="Classic")
        iis.ApplicationPools[appName].ManagedPipelineMode = ManagedPipelineMode.Classic;
        else
            iis.ApplicationPools[appName].ManagedPipelineMode = ManagedPipelineMode.Integrated;
        iis.CommitChanges();
    }
    public void StopAppPool(string appName) 
    {
        if (iis.ApplicationPools[appName] != null && iis.ApplicationPools[appName].State.ToString() == "Started")
        iis.ApplicationPools[appName].Stop();
    }
    public void StartAppPool(string appName)
    {
        if (iis.ApplicationPools[appName] != null && iis.ApplicationPools[appName].State.ToString() != "Started")
        iis.ApplicationPools[appName].Start();
    }
    //----Application扩展管理
    //--Cpu
    /// <summary>
    /// 更改IIS的CPU最大占用率，不能超过1000
    /// </summary>
    public bool ChangeCpuLimit(string appName, int limitValue)
    {
        if (limitValue>1000||iis.ApplicationPools[appName] == null) return false;
        iis.ApplicationPools[appName].Cpu.Limit = limitValue;
        iis.CommitChanges();
        return true;
    }
    //--Recycle
    /// <summary>
    /// 在配置文件更改后,是否立即回收
    /// </summary>
    public bool ChangeRecycleOnConfigChange(string appName, bool value) 
    {
        if (iis.ApplicationPools[appName] == null) return false;
        iis.ApplicationPools[appName].Recycling.DisallowRotationOnConfigChange = value;//配置发生更改时禁止回收,可以更改web.config而不重启服务器
        iis.CommitChanges();
        return true;
    }
    /// <summary>
    /// 每隔多久,回收一次,默认1天5小时,即1740分钟,我们限制一下,最少一小时以上,可传分钟为参例:60
    /// </summary>
    public bool ChangeRecycleTimeSpan(string appName, int time)
    {
        TimeSpan ts = new TimeSpan(0,time,0);
        return ChangeRecycleTimeSpan(appName,ts);
    }
    /// <summary>
    /// 每隔多久,回收一次,默认1天5小时,即1740分钟,我们限制一下,最少一小时以上
    /// </summary>
    public bool ChangeRecycleTimeSpan(string appName, TimeSpan ts)
    {
        if (ts.TotalHours<1||iis.ApplicationPools[appName] == null) return false;
        iis.ApplicationPools[appName].Recycling.PeriodicRestart.Time = ts;
        iis.CommitChanges();
        return true;
    }
    /// <summary>
    /// 设定该应用程序池最大可用虚拟内存,达到则重启,为0则不限制
    /// </summary>
    public bool ChangeRecycleMemory(string appName, long size)
    {
        if (iis.ApplicationPools[appName] == null) return false;
        iis.ApplicationPools[appName].Recycling.PeriodicRestart.Memory = size;
        iis.CommitChanges();
        return true;
    }
    /// <summary>
    /// 设定该应用程序池最大可用内存,达到则重启,为0则不限制
    /// </summary>
    public bool ChangeRecyclePrivateMemory(string appName, long size)
    {

        if (iis.ApplicationPools[appName] == null) return false;
        iis.ApplicationPools[appName].Recycling.PeriodicRestart.PrivateMemory = size;
        iis.CommitChanges();
        return true;
    }
    //---------------操作服务器
    public ObjectState StopSite(string siteName)
    {
        return iis.Sites[siteName].Stop();
    }
    public ObjectState StartSite(string siteName)
    {
        return iis.Sites[siteName].Start();
    }
    public ObjectState RestartSite(string siteName)
    {
        iis.Sites[siteName].Stop();
        return iis.Sites[siteName].Start();
    }
    //----如未绑定多个,默认更新第一个,绑定多个,则根据索引更新指定项
    public bool ChangeSitePort(string siteName, string port) //http:*:86:www.
    {
        return ChangeSitePort(siteName, port, 0);
    }
    public bool ChangeSitePort(string siteName, string port, int index)
    {
        if (iis.Sites[siteName] == null) return false;//下面需要处理为空的问题
        string[] temp = iis.Sites[siteName].Bindings[index].BindingInformation.Split(':');
        iis.Sites[siteName].Bindings[index].BindingInformation = temp[0] + ":" + port + ":" + (temp.Length > 2 ? temp[2] : "");
        iis.CommitChanges();
        return true;
    }
    public bool ChangeSitePath(string siteName, string path)
    {
        if (iis.Sites[siteName] == null) return false;
        iis.Sites[siteName].Applications[0].VirtualDirectories[0].PhysicalPath = path;
        iis.CommitChanges();
        return true;
    }
    public bool ChangeSiteDomain(string siteName, string Domain)
    {
        return ChangeSiteDomain(siteName, Domain, 0);
    }
    public bool ChangeSiteDomain(string siteName, string Domain, int index)
    {
        if (iis.Sites[siteName] == null) return false;
        //iis.Sites[siteName].Bindings[0].SetAttributeValue("Host",hostName);
        string[] temp = iis.Sites[siteName].Bindings[index].BindingInformation.Split(':');
        iis.Sites[siteName].Bindings[index].BindingInformation = temp[0] + ":" + temp[1] + ":" + Domain;//Host是只读的
        iis.CommitChanges();
        return true;
    }
    //-----------Binding Info Manage(Such as Domain,IP,Port)
    public bool AddSiteBindInfo(string siteName, Binding b)//*:888:www.baba 
    {
        bool flag = false;
        if (iis.Sites[siteName] != null && !string.IsNullOrEmpty(b.BindingInformation))//站点存在,并且绑定信息不为空
        {
            iis.Sites[siteName].Bindings.Add(b);
            iis.CommitChanges();
            flag = true;
        }
        return flag;
    }
    public bool AddSiteBindInfo(string siteName,string bindInfo) 
    {
        if (iis.Sites[siteName] == null) return false;
        Binding bind =iis.Sites[siteName].Bindings.CreateElement();
        bind.Protocol = "Http";
        bind.BindingInformation = bindInfo;
        iis.Sites[siteName].Bindings.Add(bind);
        iis.CommitChanges();
        return true;
    }
    /// <summary>
    /// 更新绑定信息*:86:www
    /// </summary>
    public bool ChangeSiteBindInfo(string siteName,string bindInfo,int index)
    {
        if (iis.Sites[siteName] == null || iis.Sites[siteName].Bindings[index]==null) return false;
            iis.Sites[siteName].Bindings[index].BindingInformation = bindInfo;
            iis.CommitChanges();
        return true;
    }
    public bool ChangePhysicalPath(string siteName, string path, int index)//修改物理路径,如果不存在,则新增
    {
        if (iis.Sites[siteName] == null) return false;
        iis.Sites[siteName].Applications[0].VirtualDirectories[index].PhysicalPath = path;
        iis.CommitChanges();
        return true;
    }

    public void ClearSiteBindinfo(string siteName)
    {
        iis.Sites[siteName].Bindings.Clear();
        iis.CommitChanges();
    }
    /// <summary>
    /// 获取指定网站的WebConfig,applicationHost.config等都可以这样获取
    /// </summary>
    /// <param name="siteName"></param>
    /// <returns></returns>
    public Configuration GetWebConfig(string siteName)
    {
        return iis.GetWebConfiguration(siteName);
    }
    /// <summary>
    /// 同步IIS与数据库中的信息
    /// </summary>
    public void SyncDB()
    {
        DataTableHelper dtHelper = new DataTableHelper();
        DataTable dt = GetWebSiteList();
        DataTable targetDT = dtHelper.GetTaleStruct("ZL_IDC_SiteList");
        dt.TableName = "ZL_IDC_SiteList";
        dtHelper.UpdateDataToDB(dt, targetDT, "SiteID");
    }
    /// <summary>
    /// 关闭到期站点,根据SiteID
    /// </summary>
    public void StopExpireSite() 
    {
        DataTable dt = siteBll.SelAllExpire();
        foreach (DataRow dr in dt.Rows)
        {
            StopSite(GetNameBySiteID(dr["SiteID"].ToString()));
        }
    }
}
//-----------------------------------------------------------------------------------------------------------------------------------------------------//
[Serializable]
/// <summary>
/// 站点模型
/// </summary>
public class IISWebSite
{
    //最基本的构造,其他省略参数的构造方法基此而成,如批量创建
    public IISWebSite(string siteName, string port, string physicalPath, string domainName, string appPool)
    {
        _siteName = siteName;
        _port = port;
        _physicalPath = physicalPath;
        _domainName = domainName;
        _appPool = appPool;
    }
    public IISWebSite()
    {
        BindInfoList = new List<string>();
        PhysicalPathList = new List<string>();
    }

    private string _siteID;
    /// <summary>
    /// 是否启动
    /// </summary>
    private string _state;
    private string _siteName;
    /// <summary>
    /// index为0的主端口
    /// </summary>
    private string _port;
    /// <summary>
    /// index为0的路径
    /// </summary>
    private string _physicalPath;
    /// <summary>
    /// index为0的域名
    /// </summary>
    private string _domainName;
    private string _appPool;
    /// <summary>
    /// 存放所有绑信息
    /// </summary>
    public List<string> BindInfoList;
    /// <summary>
    /// 路径详细信息
    /// </summary>
    public List<string> PhysicalPathList;
    public string SiteID { get { return _siteID; } set { _siteID = value; } }
    public string State { get { return _state; } set { _state = value; } }
    public string SiteName { get { return _siteName; } set { _siteName = value; } }
    public string Port { get { return _port; } set { _port = value; } }
    /// <summary>
    /// 物质理路径,以\结尾
    /// </summary>
    public string PhysicalPath { get { return _physicalPath.EndsWith(@"\") ? _physicalPath : _physicalPath + @"\"; } set { _physicalPath = value; } }
    public string DomainName { get { return _domainName; } set { _domainName = value; } }
    public string AppPool { get { return _appPool; } set { _appPool = value; } }
    //----用于用户建站
    public string TempName { get; set; }
    //用于下载的Url路径
    public string TempUrl{get;set;}
    public string TempDir { get; set; }
    public string dbName { get; set; }
    /// <summary>
    /// 自检看给定的各项值是否符合基本规范
    /// </summary>
    /// <returns></returns>
    public bool CheckIsValid()
    {
        bool flag = true;
        if (_siteName == "" || _port == "" || _physicalPath == "")
        {
            flag = false;
        }
        return flag;
    }
}

/// <summary>
/// 模拟用户登录
/// </summary>
public class IdentityAnalogue
{
    //模拟指定用户时使用的常量定义
    public const int LOGON32_LOGON_INTERACTIVE = 2;
    public const int LOGON32_PROVIDER_DEFAULT = 0;
    WindowsImpersonationContext impersonationContext;

    //win32api引用
    [DllImport("advapi32.dll")]
    public static extern int LogonUserA(string lpszUserName,
     string lpszDomain,
     string lpszPassword,
     int dwLogonType,
     int dwLogonProvider,
     ref IntPtr phToken);
    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
     public static extern int DuplicateToken(IntPtr hToken,
     int impersonationLevel,
     ref IntPtr hNewToken);
    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool RevertToSelf();
    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern bool CloseHandle(IntPtr handle);
    public IdentityAnalogue()
    {
    }
    /// <summary>
    /// 模拟指定的用户身份
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="domain"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool ImpersonateValidUser(string userName, string domain, string password)
    {
        WindowsIdentity tempWindowsIdentity;
        IntPtr token = IntPtr.Zero;
        IntPtr tokenDuplicate = IntPtr.Zero;
        if (RevertToSelf())
        {
            if (LogonUserA(userName, domain, password, 2, 0, ref token) != 0)
            {
                if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                {
                    tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                    impersonationContext = tempWindowsIdentity.Impersonate();
                    if (impersonationContext != null)
                    {
                        CloseHandle(token);
                        CloseHandle(tokenDuplicate);
                        return true;
                    }
                }
            }
        }
        if (token != IntPtr.Zero)
            CloseHandle(token);
        if (tokenDuplicate != IntPtr.Zero)
            CloseHandle(tokenDuplicate);
        return false;
    }
    /// <summary>
    /// 取消模拟
    /// </summary>
    public void UndoImpersonation()
    {
        impersonationContext.Undo();
    }

    /// <summary>
    /// 检查是否开启，用户名与密码是否存在,能否正常登录,并完成正常登录,是否跳转
    /// </summary>
    /// <returns></returns>
    public bool CheckEnableSA(bool needJump=true)
    {
        string remind = "";
        bool flag = false;
        if (!StationGroup.EnableSA)
        {
            remind = "未开启超级管理员,无法访问该页面";
        }
        else if (string.IsNullOrEmpty(StationGroup.SAName) || string.IsNullOrEmpty(StationGroup.SAPassWord))
        {
            remind = "超级管理员用户名或密码为空,请补齐资料.";
        }
        else if (!ImpersonateValidUser(EncryptHelper.AESDecrypt(StationGroup.SAName), "", EncryptHelper.AESDecrypt(StationGroup.SAPassWord)))
        {
            remind = "超级管理员用户名或密码不正确,请补齐资料.";
        }
        else { flag = true; }
        if (!flag && needJump)
        {
            HttpContext.Current.Response.Redirect(CustomerPageAction.customPath + "Site/SiteConfig.aspx?remind=" + HttpContext.Current.Server.UrlEncode(remind));
        }
        return flag;
    }
    /// <summary>
    /// 权检测当前保存的用户是否有效，并为出错信息赋值
    /// </summary>
    /// <returns></returns>
    public bool CheckSAIsValid(out string remind) 
    {
        bool flag = false;
        try { 
          if (!StationGroup.EnableSA)
        {
            remind = "未开启超级管理员,无法访问该页面";
        }
        else if (string.IsNullOrEmpty(StationGroup.SAName) || string.IsNullOrEmpty(StationGroup.SAPassWord))
        {
            remind = "超级管理员用户名或密码为空,请补齐资料.";
        }
        else if (!ImpersonateValidUser(EncryptHelper.AESDecrypt(StationGroup.SAName), "", EncryptHelper.AESDecrypt(StationGroup.SAPassWord)))
        {
            remind = "超级管理员用户名或密码不正确,请核对资料.";
        }
        else 
        { 
            flag = true;
            remind = "当前帐户有效，如果需要更改，请点击编辑";
        }
        }
        catch (Exception ex) {remind=ex.Message; }
        return flag;
    }
}
