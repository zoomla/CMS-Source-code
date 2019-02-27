using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class App_Default : CustomerPageAction
{
    B_App appBll = new B_App();
    B_User buser = new B_User();
    //private DataTable FileDT { get { return ViewState["FileDT"] as DataTable; } set { ViewState["FileDT"] = value; } }
    //public string SiteID { get { return Request.QueryString["SiteID"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(SiteID) || SiteID.Length != 12 || SiteID.Contains(".")) { function.WriteErrMsg("站点标识错误"); }
        //Appdir += SiteID + "/";
        B_User.CheckIsLogged(Request.RawUrl);
        M_UserInfo mu = buser.GetLogin();
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        DataTable dt = appBll.SelBySite(mu.UserID.ToString());//改为
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        //FileDT = null;
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "down":
                M_UserInfo mu = buser.GetLogin();
                M_App appMod = appBll.Select(Convert.ToInt32(e.CommandArgument));
                if (!mu.UserID.ToString().Equals(appMod.UserID)) { function.WriteErrMsg("你无权下载该APK"); }
                string fpath = appMod.APPDir + "\\" + appMod.AppName + ".apk";
                if (!File.Exists(fpath)) { function.WriteErrMsg("文件不存在"); }
                SafeSC.DownFile(function.PToV(fpath));
                break;
        }
        MyBind();
    }
    public string GetCodeImg()
    {
        string url = Eval("ImageUrl").ToString();
        if (string.IsNullOrEmpty(url))
            return "<a href='CL.aspx?appid="+Eval("ID")+"'>生成二维码</a>";
        else
            return "<img src='/Common/Common.ashx?url=" + url + "' class='codeimg'  />";
    }
    public string GetTypeStr()
    {
        string html = "";
        switch (DataConverter.CLng(Eval("MyStatus")))
        {
            case 0:
                html = "<span>等待生成</span>";
                break;
            default:
                html = "<span style='color:green;'>已生成</span>";
                break;
        }
        return html;
    }
    public string UrlDeal() 
    {
        return StrHelper.UrlDeal(Eval("FUrl").ToString());
    }
}