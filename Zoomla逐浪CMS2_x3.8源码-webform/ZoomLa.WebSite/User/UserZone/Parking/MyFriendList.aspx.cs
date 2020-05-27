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
using ZoomLa.Model;
using System.Collections.Generic;
using ZoomLa.BLL;

public partial class User_UserZone_Parking_MyFriendList : Page 
{
    B_User buser = new B_User();
    int currentUser = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = buser.GetLogin().UserID;
        if (currentUser == 0)
            Page.Response.Redirect(@"~/user/login.aspx");
        if(!IsPostBack )
        SetGroup();
    }

    //绑定用户组信息
    private void SetGroup()
    {
      
    }

    //绑定好友
    private void GetUserFriend(int gid)
    {
        


    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(typeof(string), "TabJs", "<script language='javascript'>window.returnVal='" + RadioButtonList1.SelectedValue + "';window.parent.hidePopWin(true);</script>");
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetUserFriend(int.Parse (DropDownList1.SelectedValue));
    }
}
