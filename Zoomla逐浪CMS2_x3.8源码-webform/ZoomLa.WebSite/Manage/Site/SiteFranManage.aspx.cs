using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_Site_SiteFranManage : System.Web.UI.Page
{
    B_Admin badmin = new B_Admin();
    IISHelper iisHelper = new IISHelper();
    ServerManager iis = new ServerManager();
    B_Site_SiteList siteBll = new B_Site_SiteList();
    B_Site_Log logBll = new B_Site_Log();
    M_Site_SiteList siteModel = new M_Site_SiteList();
    public string siteName;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["remote"]))
        {
            this.MasterPageFile = "~/manage/Site/OptionMaster.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        badmin.CheckIsLogin();
        siteName = Request.QueryString["siteName"];
        if (string.IsNullOrEmpty(siteName))
            function.WriteErrMsg("未选择站点");
        IdentityAnalogue ia = new IdentityAnalogue();
        ia.CheckEnableSA();
        if (!IsPostBack)
        {
            TextBind();
            DataBind();
        }
        Call.HideBread(Master);
    }
    private void DataBind(string key="")
    {
        EGV.DataSource = logBll.SelAll();
        EGV.DataBind();
    }
    private void TextBind() 
    {
        siteModel = siteBll.SelBySiteID(iis.Sites[siteName].Id.ToString());
        sName.Text = siteName;
        StartDate.Text = siteModel.CreateDate.ToString("yyyy年M月dd日");
        EndDate.Text = siteModel.EndDate.ToString("yyyy年M月dd日");
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Del2":
                int id = DataConvert.CLng(e.CommandArgument as string);
                break;
            default: break;
        }
        DataBind();
    }
    protected void editBtn_Click(object sender, EventArgs e)
    {
        DateTime start=DataConvert.CDate(StartDate.Text.Trim());
        DateTime end=DataConvert.CDate(EndDate.Text.Trim());
        if (start > end || start == end)
        {
            function.WriteErrMsg("日期格式错误");
        }
        else
        {
            siteModel = siteBll.SelBySiteID(iis.Sites[siteName].Id.ToString());
            siteModel.CreateDate = start;
            siteModel.EndDate = end;
            siteBll.UpdateModel(siteModel);

            string remind ="管理员将日期更改为" + start.ToString("yyyy年M月dd日") + "--" + end.ToString("yyyy年M月dd日");
            SqlParameter[] sp = new SqlParameter[] {
            new SqlParameter("SiteID",siteModel.SiteID),
            new SqlParameter("Type",1),
            new SqlParameter("Remind",remind),
            new SqlParameter("CreateDate",DateTime.Now)
            };
            SqlHelper.ExecuteScalar(CommandType.Text, "Insert Into ZL_IDC_Log ([siteID],[Type],[Remind],[CreateDate]) Values(@SiteID,@Type,@Remind,@CreateDate)", sp);
            TextBind();
            DataBind();
        }
    }
    public string GetType(string type) 
    {
        return "管理员更改";
    }
    public string GetSiteName(string siteID)
    {
       return iisHelper.GetNameBySiteID(siteID);
    }
}