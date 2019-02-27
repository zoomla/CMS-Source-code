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
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.API;
using ZoomLa.DZNT;
using System.Collections.Generic;
using ZoomLa.BLL.User;

public partial class User_UserFriend_Default : System.Web.UI.Page
{
    B_User_Friend friBll = new B_User_Friend();
    B_User_FriendApply alyBll = new B_User_FriendApply();
    B_User buser = new B_User();
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
        DataTable dt = friBll.SelMyFriend(mu.UserID, UNameSkey_T.Text);
        EGV.DataSource = dt;
        EGV.DataBind();
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
            case "del2":
                friBll.DelFriend(mu.UserID, tuid);
                break;
        }
        MyBind();
    }
    protected void UNameSkey_Btn_Click(object sender, EventArgs e)
    {
        MyBind();
    }
}
