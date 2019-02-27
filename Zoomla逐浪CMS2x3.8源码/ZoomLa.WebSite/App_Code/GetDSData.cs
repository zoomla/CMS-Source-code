using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using ZoomLa.Model.Content;
using ZoomLa.BLL.Content;
using System.Xml;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using Microsoft.Web.Administration;
using System.IO;
/*
 * 数据源数据获取
 * 不同模板，不同标签，全局的来源，XML中存信息直接数据ID，为空则为本地服务端.
 */
[System.ComponentModel.DataObject]
public class GetDSData
{
    M_DataSource dsModel = new M_DataSource();
    B_DataSource dsBll = new B_DataSource();
    B_Admin badmin = new B_Admin();
	public GetDSData()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 返回数据源数据
    /// </summary>
    public DataTable getAllData() 
    {
        return dsBll.Sel();
    }

    //---------CRM
    private B_CrmAuth crmBll = new B_CrmAuth();
    public DataTable GetAllCrmOption()//返回所有的附加字段 
    {
        string xmlPath = HttpContext.Current.Server.MapPath("~/Config/CRM_Dictionary.xml");
        DataSet ds = new DataSet();
        ds.ReadXml(xmlPath);
        for (int i = 1; i < ds.Tables.Count; i++)
        {
            ds.Tables[0].Merge(ds.Tables[i]);
        }
        if (ds.Tables[0].Columns["isAdd"] == null)
        {
            ds.Tables[0].Columns.Add("isAdd",typeof(string));
        }
        ds.Tables[0].DefaultView.RowFilter = "isAdd=1";
        DataTable resultDT = ds.Tables[0].DefaultView.ToTable();
        resultDT.Columns.Add("ID", typeof(int));//用来作为标识位
        for (int i = 0; i < resultDT.Rows.Count; i++)
        {
            resultDT.Rows[i]["ID"] = i + 1;
        }
        return resultDT;
    }
    public DataTable GetTableList(string node_id,string xmlPath)
    {
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(xmlPath);
        XmlNodeList list = myDoc.SelectNodes("isAdd");
        XmlElement xmle = null;
        DataTable dt = new DataTable();
        dt.Columns.Add("sort", typeof(System.String));
        dt.Columns.Add("default_", typeof(System.Boolean));
        dt.Columns.Add("enable", typeof(System.Boolean));
        dt.Columns.Add("content", typeof(System.String));
        for (int i = 0; i < list.Count; i++)
        {
            xmle = (XmlElement)list[i];
            DataRow dr = dt.NewRow();
            dr["sort"] = xmle.SelectSingleNode("sort").InnerText;
            dr["default_"] = xmle.SelectSingleNode("default_").InnerText;
            dr["enable"] = xmle.SelectSingleNode("enable").InnerText;
            dr["content"] = xmle.SelectSingleNode("content").InnerText;
            if (dr["enable"].ToString() == "True")
            {
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }
    public DataTable GetFPList(string id)
    {
        return null;
        //return pBll.SelectCRMByID(id);
    }
    /// <summary>
    /// 获取到期未跟进的客户
    /// </summary>
    public DataTable GetCrmExpireList(string disType) 
    {
        //0:显示未跟进(默认);1:显示所有未分配跟进人(拥有分配跟进人权限的才看的到)
        M_AdminInfo info = B_Admin.GetAdminByID(badmin.GetAdminLogin().AdminId);//info中有role信息
        DataTable Cll = new DataTable();
        DataTable authDT = crmBll.GetAuthTable(info.RoleList.Split(','));
            //Cll = pBll.SelectCRMExpire();
            if (!crmBll.IsHasAuth(authDT, "AllCustomer", info))
            {
                if (Cll != null && Cll.Rows.Count > 0)
                {
                    Cll.DefaultView.RowFilter = "UserID = " + info.AdminId;
                    Cll = Cll.DefaultView.ToTable();
                    Cll.DefaultView.RowFilter = "1=1";
                }
            }
        return Cll;
    }
    public DataTable GetCrmNoFPManList() 
    {
        B_Client_Basic cb = new B_Client_Basic();
       
        M_AdminInfo info = B_Admin.GetAdminByID(badmin.GetAdminLogin().AdminId);//info中有role信息
        DataTable Cll = new DataTable();
        DataTable authDT = crmBll.GetAuthTable(info.RoleList.Split(','));
        Cll =  cb.SelectCrmNoFPMan();
        //if (!crmBll.IsHasAuth(authDT, "AllCustomer", info))
        //{
        //    if (Cll != null && Cll.Rows.Count > 0)
        //    {
        //        Cll.DefaultView.RowFilter = "UserID = " + info.AdminId;
        //        Cll = Cll.DefaultView.ToTable();
        //        Cll.DefaultView.RowFilter = "1=1";
        //    }
        //}
        return Cll;
    }
    //---------Site
    /// <summary>
    /// 返回所有站点列表
    /// </summary>
    public DataTable GetWSData()
    {
        IISHelper iis = new IISHelper();
        DataTable dt = iis.GetWebSiteList();
        dt.Columns.Add("ZoomlaVersion", typeof(string));
        EnviorHelper enHelper = new EnviorHelper();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["PhysicalPath"] != null)
                dt.Rows[i]["ZoomlaVersion"] = enHelper.IsZoomlaWebSite(dt.Rows[i]["PhysicalPath"].ToString() + "/Config/AppSettings.config");
        }
        return dt;
    }
    //传上来的是True或False
    public DataTable GetWSData(bool f1, bool f2, bool f3, bool f4,bool f5)
    {
        
        IISHelper iis = new IISHelper();
        DataTable dt = iis.GetWebSiteList();
        dt.Columns.Add("ZoomlaVersion", typeof(string));
        EnviorHelper enHelper = new EnviorHelper();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["PhysicalPath"] != null)
                dt.Rows[i]["ZoomlaVersion"] = enHelper.IsZoomlaWebSite(dt.Rows[i]["PhysicalPath"].ToString() + "/Config/AppSettings.config");
        }
        if (!f1)//显示普通网站
        {
            dt.DefaultView.RowFilter = "ZoomlaVersion  not in ('')";
        }
        if (!f2)//显示逐浪网站
        {
            dt=dt.DefaultView.ToTable();
            dt.DefaultView.RowFilter = "ZoomlaVersion  in ('')";
        }
        if (!f3)//运行中
        {
            dt = dt.DefaultView.ToTable();
            dt.DefaultView.RowFilter = "SiteState   not in ('" + ObjectState.Started + "')";
        }
        if (!f4)//已停止
        {
            dt = dt.DefaultView.ToTable();
            dt.DefaultView.RowFilter = "SiteState  not in ('" + ObjectState.Stopped + "')";
        }
        if (f5)//已到期,与上不同,仅显示已到期的站
        {
            dt = dt.DefaultView.ToTable();
            dt.DefaultView.RowFilter = "EndDate < #"+DateTime.Now+"#";
        }
        return dt.DefaultView.ToTable();
    }
   
    /// <summary>
    /// 获取指定站点的信息列表(其下的绑定信息)
    /// </summary>
    public DataTable GetSiteData(string siteName)
    {
        IISHelper iis = new IISHelper();
        return iis.GetSiteData(siteName);
    }
    public DataTable GetAppPool()
    {
        IISHelper iis = new IISHelper();
        return iis.GetAppPoolList();
    }
    //---------Site_DataCenter
    string path = "~/UploadFiles/Log/";
    string logName = "SiteLog.xml";
    DataTableHelper dtHelper = new DataTableHelper();
    public DataTable GetHistoryLog() 
    {
        if (!File.Exists(HttpContext.Current.Server.MapPath(path + logName)))
        {
            return new DataTable();
        }
        string ppath = HttpContext.Current.Server.MapPath(path + logName);
        DataTable logDT = dtHelper.DeserializeDataTable(ppath, true);
        return logDT;
    }
}