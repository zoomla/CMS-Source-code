using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Sns.BLL;
using ZoomLa.Model;
using ZoomLa.Components;

public partial class User_UserZone_SetCenter_SC_ANotification : System.Web.UI.Page
{
    #region 业务实体
    B_User ut = new B_User();
    UserTableBLL utbll = new UserTableBLL();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        ut.CheckIsLogin();
        if (!IsPostBack)
        {
                //初始化用户详细信息表
            if (utbll.GetMoreinfoByUserid(ut.GetLogin().UserID).UserID == 0)
            {
                utbll.AddMoreinfo(ut.GetLogin().UserID);
            }           
            M_UserInfo uinfo = ut.GetLogin(); 

        }
    }
}