
using System;
using System.IO;
using System.Web;
using System.Xml;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Runtime.Serialization.Json;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using Microsoft.Web.Administration;
using URLRewriter;
using ZoomLa.Components;
using ZoomLa.Model;
/*
 * Does't allow duplicate name
 * Do not assign Application Pool will use the Default Pool
 * Note that index needs to be -1 before use
 * VD is VirtualDirectory
 */
public partial class IISMain : CustomerPageAction
{
    protected B_Create b = new B_Create();
    protected B_Content bContent = new B_Content();
    protected int m_CreateCount;
    protected string siteName;

    protected ServerManager iis = new ServerManager();
    protected IISHelper iisHelper = new IISHelper();
    protected B_Admin badmin = new B_Admin();
    public string serviceUrl = "API/SiteGroupFunc.asmx"; 
    protected void Page_PreInit(object sender, EventArgs e)
    {
        ////此处控件未实例化，不能对控件进行操作
        //if (!string.IsNullOrEmpty(Request.QueryString["remote"]))
        //{
        //        this.MasterPageFile = "~/manage/Site/OptionMaster.master";
        //}
    }
    private void ProcAjax() 
    {
        string action = Request.Form["action"];
        if (string.IsNullOrEmpty(action))
        {
            Response.Write(-1);
            Response.Flush(); Response.End();
        }
        else if (action.Equals("del"))
        {
            iisHelper.DeleteSite(siteName);
        }
        else if (action.Equals("stop"))
        {
            iisHelper.StopSite(siteName);
        }
        else if (action.Equals("start"))
        {
            iisHelper.StartSite(siteName);
        }
        else if (action.Equals("restart"))
        {
            iisHelper.RestartSite(siteName);
        }
        else if (action.Equals("del2"))//Del Binding information
        {
            string[] temp = siteName.Split(':');
            iisHelper.RemoveBinding(temp[0], Convert.ToInt32(temp[1]) - 1, temp[2]);
        }
        else if (action.Equals("del3"))//Del VD
        {
            string[] temp = siteName.Split(':');
            iisHelper.RemoveVD(temp[0], Convert.ToInt32(temp[1]) - 1);
        }
        else if (action.Equals("getDomain"))
        {
            string s = iisHelper.GetDomainsBySite(siteName);
            Response.Write(s);
        }
        else if (action.ToLower().Equals("serverinfo"))
        {
            //获取Token并回发前台,Iframe跳转到目标页
            string value = Request.Form["value"];
            DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "Select * From ZL_IDC_Server Where ID = " + value);
            DataRow dr = dt.Rows[0];
            object obj = ServicesHelper.InvokeWebSer(dr["SiteUrl"].ToString() + ServicesHelper.siteGroupService,
           "SiteGroup",
           "SiteGroupFunc",
           "GetToken",
            new object[] { dr["SiteUser"].ToString(), dr["SitePasswd"].ToString() });
            string url = dr["SiteUrl"] + "" + dr["CustomPath"] + "Site/Default.aspx?Token=" + obj.ToString() + "&remote=true";
            Response.Write(url);
            Response.Flush();
        }
        Response.End();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["remote"]))
        {
            badmin.CheckIsLogin();
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["Token"]))//直接进入,带用户名与密码
        {
            if (Application["SiteToken"] == null || !Application["SiteToken"].ToString().Equals(Request.QueryString["Token"]))
                function.WriteErrMsg("Token不正确");
            string a = Application["SiteUser"] as string;
            string p = Application["SitePasswd"] as string;
            M_AdminInfo info = B_Admin.AuthenticateAdmin(a, p);
            badmin.SetLoginState(info);
            Application.Clear();
            Response.Redirect("Default.aspx?remote=true");
            Response.End();
        }
        IdentityAnalogue ia = new IdentityAnalogue();
        ia.CheckEnableSA();
        siteName=Server.UrlDecode(Request.Params["siteName"]);
        //ia.UndoImpersonation();//恢复为当前用户，当然关闭页面效果相同
        if (function.isAjax())
        {
            ProcAjax();
        }
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='Default.aspx'>站群管理</a></li>");
            int totalSite = iis.Sites.Count;//Default WebSite不算
            int startedSite = iisHelper.GetSiteCountByState(0);
            int stoppedSite = iisHelper.GetSiteCountByState(1);

            int zoomlaSite = iisHelper.GetWebSiteList(true).Rows.Count;
            int zoomlaStarted = iisHelper.GetSiteCountByState(0,true);
            int zoomlaStopped = iisHelper.GetSiteCountByState(1, true);

            titleSpan.InnerText = string.Format(titleSpan.InnerText, totalSite, startedSite, stoppedSite, zoomlaSite, zoomlaStarted, zoomlaStopped);
            //远程加载该页面
            if (string.IsNullOrEmpty(Request.QueryString["remote"]))
            {
                DataTable dt= new DataTable();
                dt= SqlHelper.ExecuteTable(CommandType.Text, "Select * From ZL_IDC_Server");
                serverList.DataSource=dt;
                serverList.DataTextField = "SiteName";
                serverList.DataValueField = "ID";
                serverList.DataBind();
                serverList.Items.Insert(0, new ListItem("本地", "0"));
                serverList.Items.Insert( dt.Rows.Count+1, new ListItem("添加服务器集群", "add"));
            }
            else
            {
                serverDiv.Visible = false;
            }
        }
    }
    //--------EGV1
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Edit2":
                EGV.EditIndex = Convert.ToInt32(e.CommandArgument as string);
                break;
            case "Save":
                string[] s = e.CommandArgument.ToString().Split(':');
                Update(DataConvert.CLng(s[0]), s[1]);
                EGV.EditIndex = -1;
                break;
            case "Cancel":
                EGV.EditIndex = -1;
                break;
            default: break;
        }
    }
    protected void Update(int rowNum, string id)//Update WebSite with index=0,Name,Port,PPath,Domain
    {
        IISWebSite site = new IISWebSite();
        GridViewRow gr = EGV.Rows[rowNum];

        site.SiteName = ((TextBox)gr.FindControl("EditSiteName")).Text.Trim();
        site.Port = ((TextBox)gr.FindControl("EditPort")).Text.Trim();
        //site.PhysicalPath = ((TextBox)gr.FindControl("EditPhySicalPath")).Text.Trim();
        //site.DomainName = ((TextBox)gr.FindControl("EditDomain")).Text.Trim();
        IISHelper iisM = new IISHelper();
        DataTable dt = iisM.GetWebSiteList();
        dt.DefaultView.RowFilter = "SiteID=" + id;
        DataRow dr = dt.DefaultView.ToTable().Rows[0];
        //-----有更改才更新,先更新域名
        if (!(dr["SiteName"] as string).Equals(site.SiteName))
            iisHelper.ChangeSiteName(dr["SiteName"] as string, site.SiteName);
        if (!(dr["SitePort"] as string).Equals(site.Port))
            iisHelper.ChangeSitePort(site.SiteName, site.Port);
        //if (!(dr["PhysicalPath"] as string).Equals(site.PhysicalPath))
        //    iisHelper.ChangeSitePath(site.SiteName, site.PhysicalPath);
        //if (!(dr["Domain"] as string).Equals(site.DomainName))
        //    iisHelper.ChangeSiteDomain(site.SiteName, site.DomainName);

        B_Site_SiteList siteBll = new B_Site_SiteList();
        M_Site_SiteList siteModel = new M_Site_SiteList();
        siteModel= siteBll.SelBySiteID(id);
        siteModel.EndDate = DataConvert.CDate(((TextBox)gr.FindControl("EditEndDate")).Text.Trim());
        siteBll.UpdateModel(siteModel);
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改完成');location=location;", true);
    }
    protected void batStart_Click(object sender, EventArgs e)//Batch start
    {
        string errorMessage = "";
        if (!string.IsNullOrEmpty(Request.Form["chk"]))
        {
            string[] temp = Request.Form["chk"].Split(',');
            foreach (string s in temp)
            {
                try
                {
                    iisHelper.StartSite(s);
                }
                catch (Exception ex) { errorMessage = s + "启动失败;原因:" + ex.Message; }
            }
            if (!string.IsNullOrEmpty(errorMessage)) { this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + errorMessage + "');location=location;", true); }
            else { this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "location=location;", true); }
        }

    }
    protected void batStop_Click(object sender, EventArgs e)//Batch Stop
    {
        //该功能取消所以注释
        //string errorMessage = "";
        //if (!string.IsNullOrEmpty(Request.Form["chk"]))
        //{
        //    string[] temp = Request.Form["chk"].Split(',');
        //    foreach (string s in temp)
        //    {
        //        try
        //        {
        //            iisHelper.StopSite(s);
        //        }
        //        catch (Exception ex) { errorMessage = s + "停止失败;原因:" + ex.Message; }
        //    }
        //    if (!string.IsNullOrEmpty(errorMessage)) { this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + errorMessage + "');location=location;", true); }
        //    else { this.Page.ClientScript.RegisterStartupScript(this.GetType(), "", "location=location;", true); }
        //}
    }
    //--------EGV2
    //protected void EGV2_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    switch (e.CommandName)
    //    {
    //        case "Edit2":
    //            EGV2.EditIndex = Convert.ToInt32(e.CommandArgument as string);
    //            break;
    //        case "Save":
    //            string[] s = e.CommandArgument.ToString().Split(':');
    //            UpdateBinds(DataConvert.CLng(s[0]), s[1], (DataConvert.CLng(s[2]) - 1));
    //            break;
    //        case "Cancel":
    //            EGV2.EditIndex = 0;
    //            break;
    //        default: break;
    //    }
    //}
    //protected void UpdateBinds(int rowNum, string siteName, int id)//Update Binding information such as Port and domain 
    //{
    //    GridViewRow gr = EGV2.Rows[rowNum];
    //    string port = ((TextBox)gr.FindControl("EditPort")).Text.Trim();
    //    string domain = ((TextBox)gr.FindControl("EditDomain")).Text.Trim();
    //    Site site = iis.Sites[siteName];

    //    iisHelper.ChangeSitePort(siteName, port, id);
    //    iisHelper.ChangeSiteDomain(siteName, domain, id);
    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改完成');location=location;", true);
    //}
    //protected void BindBtn1_Click(object sender, EventArgs e)//Binding new infromation to the site 
    //{
    //    Binding b = iis.Sites[BindSiteName.Value].Bindings.CreateElement();
    //    b.Protocol = BindProtocol.Text.Trim();
    //    b.BindingInformation = BindIP.Text.Trim() + ":" + BindPort.Text.Trim() + ":" + BindDomain.Text.Trim();
    //    iisHelper.AddBinding(BindSiteName.Value, b);
    //}
    //protected void batDel2_Click(object sender, EventArgs e)//BindInfo Batch Del
    //{
    //    string[] index = Request.Form["chk2"].Split(',');
    //    for (int i = 0; i < index.Length; i++)
    //    {
    //        iisHelper.RemoveBinding(siteName, (DataConvert.CLng(index[i]) - 1));
    //    }
    //    DataBind();
    //}
    //--------EGV3
    //protected void EGV3_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    switch (e.CommandName)
    //    {
    //        case "Edit2":
    //            EGV3.EditIndex = Convert.ToInt32(e.CommandArgument as string);
    //            break;
    //        case "Save":
    //            //throw (new Exception(e.CommandArgument.ToString()));
    //            string[] s = e.CommandArgument.ToString().Split(':');
    //            UpdatePath(DataConvert.CLng(s[0]), s[1], (DataConvert.CLng(s[2]) - 1));
    //            EGV3.EditIndex = -1;
    //            break;
    //        case "Cancel":
    //            EGV3.EditIndex = -1;
    //            break;
    //        default: break;
    //    }
    //}
    //protected void UpdatePath(int rowNum, string siteName, int index)//Update VD's physicalPath
    //{
    //    GridViewRow gr = EGV3.Rows[rowNum];
    //    //string vpath = ((TextBox)gr.FindControl("Path")).Text.Trim();
    //    string spath = ((TextBox)gr.FindControl("EditPhySicalPath")).Text.Trim();
    //    iisHelper.ChangePhysicalPath(siteName, spath, index);
    //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('修改完成');location=location;", true);
    //}
    //protected void AddPathBtn_Click(object sender, EventArgs e)//Add VirtualDirectory
    //{
    //    VirtualDirectory v = iis.Sites[siteName].Applications[0].VirtualDirectories.CreateElement();
    //    v.Path = VPath.Text.Trim();
    //    v.PhysicalPath = PPath.Text.Trim();
    //    if (string.IsNullOrEmpty(v.Path) || string.IsNullOrEmpty(v.PhysicalPath) || (v.Path + v.PhysicalPath).Contains("例:"))//not allowed to add
    //    {
    //        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('信息不完整或包含非法字符');", true);//AJAX或UpdatePanel中无用
    //        throw (new Exception("信息不完整或包含非法字符"));
    //    }
    //    else if (!Directory.Exists(v.PhysicalPath))
    //    {
    //        throw (new Exception("目录不存在或无法访问"));
    //    }
    //    else
    //    {
    //        iisHelper.AddVD(BindSiteName.Value, v);
    //        EGV3.DataBind();//UpdatePanel中使用其重新获取数据。
    //    }
    //}
    //protected void batDel3_Click(object sender, EventArgs e)//VD Batch Del
    //{
    //    string[] index = Request.Form["chk3"].Split(',');
    //    int[] temp = new int[index.Length];
    //    for (int i = 0; i < index.Length; i++)
    //    {
    //        temp[i] = DataConvert.CLng(index[i]);
    //    }
    //    Sort(ref temp);
    //    for (int i = 0; i < temp.Length; i++)
    //    {
    //        iisHelper.RemoveVD(siteName,(temp[i]-1));
    //    }
    //    EGV3.DataBind();
    //}
 

    // From large to small
    protected void Sort(ref int[] list)
    {
        int i, j, temp;
        bool done = false; j = 1;
        while ((j < list.Length) && (!done))
        {
            done = true;
            for (i = 0; i < list.Length - j; i++)
            {
                if (list[i] < list[i + 1])//> is the oppsite(from small to large)
                {
                    done = false;
                    temp = list[i];
                    list[i] = list[i + 1];
                    list[i + 1] = temp;
                }
            }
            j++;
        }
    }

    //protected void EGV4_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState == (DataControlRowState.Alternate | DataControlRowState.Edit))
    //        {
    //            DropDownList list =  (DropDownList)e.Row.FindControl("EditNetVersion");
    //            DropDownList list2 = (DropDownList)e.Row.FindControl("EditMode");
    //            list.SelectedValue = ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Row.FindControl("NetVersion")).Value;
    //            list2.SelectedValue = ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Row.FindControl("Mode")).Value;
    //            list.DataBind(); list2.DataBind();

                
    //        }
    //    }
    //}

    protected void EGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow) //判断是否数据行;
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            if( drv["siteName"]!=null)
            e.Row.Attributes.Add("ondblclick", "location='SiteDetail.aspx?siteName="+Server.UrlEncode(drv["siteName"].ToString())+"';");
        }
    }

    public string GetSiteDetailUrl(string siteName) 
    {
        string url = "SiteDetail.aspx?siteName="+Server.UrlEncode(siteName);
        if (!string.IsNullOrEmpty(Request.QueryString["remote"]))
        {
            url += "&remote=true";
        }
        return url;
    }
    public string GetFileUrl(string siteName)
    {
        //"SiteFileManage.aspx?siteName=<%# Server.UrlEncode(Eval("SiteName")as string) %>&index=0"
        string url = "SiteFileManage.aspx?siteName=" + Server.UrlEncode(siteName) + "&index=0";
        if (!string.IsNullOrEmpty(Request.QueryString["remote"]))
        {
            url += "&remote=true";
        }
        return url;
    }
    public string GetFranUrl(string siteName)
    {
        string url = "SiteFranManage.aspx?siteName=" + Server.UrlEncode(siteName);
        if (!string.IsNullOrEmpty(Request.QueryString["remote"]))
        {
            url += "&remote=true";
        }
        return url;
    }
    public string GetDate(string date) 
    {
        DateTime time = new DateTime();
        if (string.IsNullOrEmpty(date))
            date= "<span>点击设置<span>";
        else if (DateTime.TryParse(date, out time))
        {
            if (time < DateTime.Now)
                date = "<span style='color:red'>" + date + "</span>";
            else
                date = "<span style='color:green'>" + date + "</span>";
        }
        return date;
    }
    public void CheckDateAuth() 
    {
        //if (DateTime.Now > DataConvert.CDate("2014-01-05"))
        //{
        //    function.WriteErrMsg("授权到期");
        //}
    }
    public string ShowOPBtn()
    {
        string ophtml = "";
        if (Eval("SiteState", "").Equals("Started"))
        {
            ophtml = "<a href=\"javascript:postToCS('stop','" + Eval("SiteName") + "')\"  title=\"停止\" class=\"option_style\"><i class=\"fa fa-pause\"></i>停止</a>";
        }
        else
        {
            // <a href="javascript:postToCS('start','<%#Eval("SiteName") %>')" title="启动" class="option_style"><i class="fa fa-play"></i>启动</a>
            ophtml = "<a href=\"javascript:postToCS('start','" + Eval("SiteName") + "')\"  title=\"启动\" class=\"option_style\"><i class=\"fa fa-play\"></i>启动</a>";
        }
        return ophtml;
    }
}


