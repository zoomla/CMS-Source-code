using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
public partial class App_APPTlp_MyTlpList : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_App_AppTlp tlpBll = new B_App_AppTlp();
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin(Request.RawUrl);
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        EGV.DataSource = tlpBll.SelByUid(buser.GetLogin().UserID);
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void Dels_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            tlpBll.DelByIDS(Request.Form["idchk"]);
            MyBind();
        }
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
       
        switch (e.CommandName)
        {
            case "capp":
                M_App appMod = new M_App();
                appMod.APKMode = 2;
                appMod.AppName = "temp";
                appMod.MyStatus = 0;
                appMod.UserID = mu.UserID.ToString();
                break;
            case "down":
                {
                    int id=Convert.ToInt32(e.CommandArgument);
                    M_APP_APPTlp tlpMod = tlpBll.SelReturnModel(id);
                    if (tlpMod.UserID != mu.UserID) { function.WriteErrMsg("你没有下载该模板的权限"); }
                    SafeSC.DownFile(tlpMod.TlpUrl);
                }
                break;
        }
    }
}