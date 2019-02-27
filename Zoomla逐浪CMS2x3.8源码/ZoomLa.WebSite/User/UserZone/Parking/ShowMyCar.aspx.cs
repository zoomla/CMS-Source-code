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
using ZoomLa.Sns;
using System.Collections.Generic;
using ZoomLa.BLL;

public partial class User_UserZone_Parking_ShowMyCar :Page
{
    Parking_BLL pbll = new Parking_BLL();
    B_User buser = new B_User();
    int currentUser = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = buser.GetLogin().UserID;
        if (currentUser == 0)
            Page.Response.Redirect(@"~/user/login.aspx");
        Bind();
    }
    private void Bind()
    {
        List<ZL_Sns_MyCar> list = pbll.GetMyCarList(currentUser);
        EGV.DataSource = list;
        EGV.DataBind();
    }

    public string GetStr(string userid)
    {
        if (userid != null && int.Parse(userid) > 0)
        {
            return "你的车现在停放在";
        }
        else
            return "你的车现在正停放在马路上，<br/>快点找个车位吧！";
    }
    
}
