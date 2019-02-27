using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace DBDiff.BLL
{
    public class IISHelper
    {
        private ServerManager iis = new ServerManager();
        /// <summary>
        /// 获取网站信息列表
        /// </summary>
        public DataTable GetWebSiteList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SiteID", typeof(string)));
            dt.Columns.Add(new DataColumn("SiteName", typeof(string)));
            dt.Columns.Add(new DataColumn("SiteState", typeof(string)));
            dt.Columns.Add(new DataColumn("SitePort", typeof(string)));
            dt.Columns.Add(new DataColumn("PhysicalPath", typeof(string)));
            dt.Columns.Add(new DataColumn("Domain", typeof(string)));
            dt.Columns.Add("AppPoolName", typeof(string));
            dt.Columns.Add("EndDate", typeof(DateTime));
            //dt.Columns.Add(new DataColumn("CreateTime",typeof(string)));

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
                }
                catch { }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 获取逐浪网站列表,true只返回逐浪网站,false返回全部，并有逐浪版本号
        /// </summary>
        public DataTable GetWebSiteList(bool flag)
        {
            DataTable siteList = GetWebSiteList();
            siteList.Columns.Add("zoomlaVersion", typeof(string));
            for (int i = 0; i < siteList.Rows.Count; i++)
            {
                siteList.Rows[i]["zoomlaVersion"] = IsZoomlaWebSite(siteList.Rows[i]["PhysicalPath"].ToString() + "/Config/AppSettings.config");
            }
            if (flag)
                siteList.DefaultView.RowFilter = "zoomlaVersion not in ('')";//为true则只返回逐浪网站
            return siteList.DefaultView.ToTable();
        }
        public string IsZoomlaWebSite(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
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
}
