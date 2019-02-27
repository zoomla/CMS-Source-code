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

public partial class User_UserFriend_FollowList : System.Web.UI.Page
{
    B_User_Follow fwBll = new B_User_Follow();
    B_User buser = new B_User();
    public string ZType { get { return Request.QueryString["type"] ?? "0"; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        DataTable dt = null;
        if (ZType == "1")//关注我的
        {
            dt = fwBll.SelByTUser(mu.UserID, Skey_T.Text.Trim());
            EGV.Columns[5].Visible = false;//操作
            Dels_Btn.Visible = false;
        }
        else
        {
            dt = fwBll.SelByUser(mu.UserID, Skey_T.Text.Trim());
            EGV.Columns[5].Visible = true;
            Dels_Btn.Visible = true;
        }
        EGV.DataSource = dt;
        EGV.DataBind();
        function.Script(this, "CheckType(" + ZType + ");");
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
    protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        int tuid = DataConverter.CLng(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del":
                fwBll.Del(mu.UserID, tuid);
                break;
        }
        MyBind();
    }
    protected void Skey_Btn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
    protected void Dels_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            fwBll.DelByIDS(Request.Form["idchk"]);
        }
        MyBind();
    }
}