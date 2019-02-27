using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using Microsoft.Web.Administration;
using System.IO;
using System.Data;
using ZoomLa.BLL;

public partial class manage_IISManage_CreateSite : CustomerPageAction
{
    IISHelper iisHelper = new IISHelper();
    ServerManager iis = new ServerManager();
    protected B_Admin badmin = new B_Admin();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //此处控件未实例化，不能对控件进行操作
        if (!string.IsNullOrEmpty(Request.QueryString["remote"]))
        {
            this.MasterPageFile = "~/manage/Site/OptionMaster.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
         IdentityAnalogue ia = new IdentityAnalogue();
         ia.CheckEnableSA();
         if (function.isAjax())
         {
             /*
              * 1,Empty
              * 2,Illegal format
              * 3,Duplicate error
              */
             string flag = "<span style='color:green;'>*信息输入正确</span>";
             string data = Request.Form["data"];
             string mark=Request.Form["mark"];

              if (string.IsNullOrEmpty(data))
                     flag = "*选项不能为空";
             else if (mark == "1")//第一行,网站名称
             {
                if (iis.Sites[data] != null)
                 {
                     flag = "*该网站名称已存在";
                 }
             }
             else if (mark == "2")
             {
                 if(!Directory.Exists(data) )
                 {
                     flag = "*目录不存在，或路径不正确()";
                 }
             }
             else if (mark == "3")//Detected Port,Must be a Numeric and unoccupied
             {
                 int t=0;
                 if (!Int32.TryParse(data,out t))
                 {
                     flag = "*端口只能为数字";
                 }
             }
             else if (mark == "4")
             {
                
             }
             Response.Write(flag); Response.Flush(); Response.End();
         }
         Call.HideBread(Master);
    }
    //创建站点
    protected void CSWSBtn_Click(object sender, EventArgs e)
    {
        IISWebSite webSite = new IISWebSite(CSWebName.Text.Trim(), CSPort.Text.Trim(), CSPhysicalPath.Text.Trim(), CSDomain.Text.Trim(), CSAppPool.Text.Trim());
        if (webSite.CheckIsValid())
        {
            if (!iisHelper.CreateSite(webSite)) { Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('创建失败,站点名存在或信息填写错误.');", true); }
            if (string.IsNullOrEmpty(Request.Form["chk"]))//不立即启动站点
                iisHelper.StopSite(webSite.SiteName);
            iisHelper.SyncDB();//创建站点时,同步下数据库
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "disParent('SiteCloudSetup.aspx?SiteName=" + webSite.SiteName + "');", true);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('信息不完整,无法创建');", true);
        }
        if (!string.IsNullOrEmpty(Request.Form["sitePort"]) && !string.IsNullOrEmpty(Request.Form["Domain"]))//批量绑定信息
        {
            string[] sitePort = Request.Form["sitePort"].Split(',');
            string[] domain = Request.Form["Domain"].Split(',');
            for (int i = 0; i < sitePort.Length && i < domain.Length; i++)
            {
                Binding b = iis.Sites[webSite.SiteName].Bindings.CreateElement();
                b.Protocol = "Http";
                b.BindingInformation = "*:" + sitePort[i] + ":" + domain[i];
                iisHelper.AddSiteBindInfo(webSite.SiteName, b);
            }
        }
    }
}