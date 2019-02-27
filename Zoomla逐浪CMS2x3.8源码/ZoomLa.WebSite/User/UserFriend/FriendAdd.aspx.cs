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

public partial class User_UserFriend_FriendAdd : System.Web.UI.Page
{
    #region 业务对象
    B_User ubll = new B_User();
    #endregion

    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            
            ViewState["fid"] = Request.QueryString["fid"];
            GetInit();
        }
    }
    #endregion

    #region 页面方法
    private int uid
    {
        get
        {
            if (HttpContext.Current.Request.Cookies["UserState"]["UserID"] != null)
                return int.Parse(HttpContext.Current.Request.Cookies["UserState"]["UserID"].ToString());
            else
                return 0;
        }
        set
        {
            uid = value;
        }
    }

    private void GetInit()
    {
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
       
        
    }
    #endregion
}
