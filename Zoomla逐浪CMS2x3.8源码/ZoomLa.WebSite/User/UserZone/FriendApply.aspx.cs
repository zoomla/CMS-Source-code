using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;

public partial class User_UserZone_FriendApply : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_User_Friend friBll = new B_User_Friend();
    B_User_FriendApply alyBll = new B_User_FriendApply();
    public string Action { get { return Request.QueryString["action"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        switch (Action)
        {
            case "send":
                Send_EGV.DataSource = alyBll.SelMySendApply(mu.UserID);
                Send_EGV.DataBind();
                Send_EGV.Visible = true;
                break;
            default:
                Rece_EGV.DataSource = alyBll.SelMyReceApply(mu.UserID);
                Rece_EGV.DataBind();
                Rece_EGV.Visible = true;
                break;
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Rece_EGV.PageIndex = e.NewPageIndex;
        Send_EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = DataConvert.CLng(e.CommandArgument);
        M_User_FriendApply alyMod = alyBll.SelReturnModel(id);
        switch (e.CommandName)
        {
            case "reject":
                alyMod.ZStatus = (int)ZLEnum.ConStatus.Reject;
                alyBll.UpdateByID(alyMod);
                break;
            case "agree":
                alyMod.ZStatus = (int)ZLEnum.ConStatus.Audited;
                alyBll.UpdateByID(alyMod);
                friBll.AddFriendByApply(alyMod);
                break;
        }
        MyBind();
    }
    public string GetStatus() 
    {
        switch (DataConvert.CLng(Eval("ZStatus")))
        {
            //case (int)ZLEnum.ConStatus.UnAudit:
            //    return "未确认";
            case (int)ZLEnum.ConStatus.Reject:
                return "<span class='r_red'>已经拒绝</span>";
            case (int)ZLEnum.ConStatus.Audited:
                return "<span class='r_green'>已经同意</span>";
            default:
                return "暂未确认";
        }
    }
}