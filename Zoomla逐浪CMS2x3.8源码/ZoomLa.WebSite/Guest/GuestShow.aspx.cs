using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Collections.Generic;
using ZoomLa.Components;

public partial class BBS_GuestShow : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Admin badmin = new B_Admin();
    M_GuestBook model = new M_GuestBook();
    B_GuestBook bll = new B_GuestBook();
    B_GuestBookCate cateBll = new B_GuestBookCate();
    public string Skey { get { return ViewState["Skey"] as string; } set { ViewState["Skey"] = value; } }
    public int Gid { get { return DataConverter.CLng(Request.QueryString["GID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            checkNeedLog();
            if (buser.CheckLogin())
            {
                Label1.Text = buser.GetLogin().UserName;
            }
            if (Gid <= 0) { function.WriteErrMsg("留言ID不正确！"); }
            M_GuestBook info = bll.GetQuest(Gid);
            if (info.IsNull) { function.WriteErrMsg("留言信息不存在!"); }
            if (info.ParentID > 0) { function.WriteErrMsg("信息不存在"); }
            M_GuestBookCate cateMod = cateBll.SelReturnModel(info.CateID);
            DataTable cateDT = cateBll.Cate_Sel(0);
            Cate_RPT.DataSource = cateDT;
            Cate_RPT.DataBind();
            GTitle_L.Text = info.Title;
            MyBind();
            if (buser.GetLogin() != null)
            {
                this.Label1.Text = buser.GetLogin().UserName;
            }
        }
    }

    private void MyBind()
    {
        int CPage = string.IsNullOrEmpty(Request.QueryString["p"]) ? 1 : DataConverter.CLng(Request.QueryString["p"]);
        if (CPage <= 0) { CPage = 1; }
        int PageSize = 20; 
        DataTable dt = B_GuestBook.GetTipsList(Gid, PageSize, CPage); 
        int Total = B_GuestBook.GetTipsTotal(Gid);
        DataRow[] drs = dt.Select("Status=0");
        foreach (DataRow dr in drs)
        {
            dr["TContent"] = "<p style='color:red'>等待管理员审核!</p>";
        }
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    public string GetUserName(string UserID)
    {
        //判断是否是管理员
        if(buser.SeachByID(DataConverter.CLng(UserID)).UserName=="admin")
        {
            return buser.SeachByID(DataConverter.CLng(UserID)).UserName+"(管理员)";
        }
        else
        {
            return buser.SeachByID(DataConverter.CLng(UserID)).UserName;
        }
    }
    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        B_User.CheckIsLogged(Request.RawUrl);
        if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], VCode.Text))
        {
            function.WriteErrMsg("验证码不正确", Request.RawUrl);
        }
        M_GuestBook pinfo = bll.GetQuest(Gid);
        M_GuestBook info = new M_GuestBook();
        M_UserInfo mu = buser.GetLogin();
        info.ParentID = Gid;
        info.Title = "[会员回复]";
        info.CateID = pinfo.CateID;
        info.TContent = BaseClass.CheckInjection(TxtContents.Value);
        info.Status = SiteConfig.SiteOption.OpenAudit > 0 ? 0 : 1;
        if (bll.AddTips(info))
        {
            MyBind();
            TxtContents.InnerText = "";
            VCode.Text = "";
        }
        Response.Redirect(Request.RawUrl);
    } 
    protected string getstyle()
    {
        if (buser.CheckLogin())
        {
            return "display:inherit";
        }
        else return "display:none";

    }
    protected string getstyles()
    {
        if (buser.CheckLogin())
        {
            return "display:none";
        }
        else return "display:inherit";
    }

    //如果禁止匿名处理
    protected void checkNeedLog()
    {

        int Cateid = bll.GetQuest(DataConverter.CLng(Request.QueryString["GID"])).CateID;
        int NeedLog = cateBll.SelReturnModel(Cateid).NeedLog;
        if (NeedLog == 1 && !buser.CheckLogin())//禁止匿名登录
        {
            banAnony.Visible = true;
            replyDiv.Visible = false;
        }
    }

}
