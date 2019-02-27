using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BDUModel;
using BDUBLL;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;


public partial class DraughtLog : Page 
{
    #region 业务对象
    private Dictionary<string, string> Order
    {
        get
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("CreatDate", "0");
            return ht;
        }
    }
    LogManageBLL logbll = new LogManageBLL();
    #endregion
    B_User buser = new B_User();
    int currentUser = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = buser.GetLogin().UserID;
        if (currentUser == 0)
            Page.Response.Redirect(@"~/user/login.aspx");
        B_User ubll = new B_User();
        M_UserInfo uinfo = ubll.GetUserByUserID(currentUser);
    }
}

