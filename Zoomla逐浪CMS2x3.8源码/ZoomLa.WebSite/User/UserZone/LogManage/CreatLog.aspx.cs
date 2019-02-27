using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.Components;
using BDUModel;
using BDUBLL;
using ZoomLa.BLL;
using ZoomLa.API;
using ZoomLa.Common;
using ZoomLa.SQLDAL;

public partial class User_UserZone_LogManage_CreatLog : System.Web.UI.Page
{
    #region 业务对象
    LogManageBLL logbll = new LogManageBLL();
    B_User ubll = new B_User();
    private bool IsNew
    {
        get
        {
            if (ViewState["IsNew"] != null)
                return bool.Parse(ViewState["IsNew"].ToString());
            else return true;
        }
        set
        {
            ViewState["IsNew"] = value;
        }
    }

    private Guid LogID
    {
        get
        {
            if (ViewState["LogID"] != null)
                return new Guid(ViewState["LogID"].ToString());
            else return Guid.Empty;
        }
        set
        {
            ViewState["LogID"] = value;
        }
    }

    #endregion

    #region 对象初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var weathers = new[] { new { Name = "晴", ID = 1 },
                                       new { Name = "雨", ID = 2 },
                                       new { Name = "阴", ID = 3 },
                                       new { Name = "雾", ID = 4 },
                                       new { Name = "霾", ID = 5 },
                                       new { Name = "雪", ID = 6 },
                                       new { Name = "冰", ID = 7 } };
            Weather_Rads.DataSource = weathers;
            Weather_Rads.DataBind();
            var moods = new[] { new { Name="高兴",ID=1 },
                                    new { Name="兴奋",ID=2 },
                                    new { Name="开心",ID=3 },
                                    new { Name="平淡",ID=4 },
                                    new { Name="忧郁",ID=5 },
                                    new { Name="悲伤",ID=6 },
                                    new { Name="愤怒",ID=7 } };
            Mood_Rads.DataSource = moods;
            Mood_Rads.DataBind();

            if (!ubll.CheckLogin())
            {
                if (SiteConfig.UserConfig.EnableCheckCodeOfLogin)
                {
                    this.PhValCode.Visible = true;
                }
                else
                {
                    this.PhValCode.Visible = false;
                }
                this.dwindow.Style["display"] = "";
            }
            else
            {
                M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
                GetLogType();
                if (Request.QueryString["LogID"] != null)
                {
                    ViewState["LogID"] = Request.QueryString["LogID"];
                    IsNew = false;
                    UserLogToPage();
                }
                else { CDate_T.Text = DateTime.Now.ToShortDateString(); }
            }
        }
    }
    #endregion

    #region 辅助方法

    //获取分组信息
    private void GetLogType()
    {
        this.dropLogType.Items.Clear();
        List<UserLogType> logTypeList = logbll.GetLogTypeByUserID(ubll.GetLogin().UserID);
        this.dropLogType.Items.Add(new ListItem("默认分组", Guid.Empty.ToString()));
        if (logTypeList.Count != 0)
        {
            foreach (UserLogType logType in logTypeList)
            {
                ListItem li = new ListItem();
                li.Text = logType.LogTypeName;
                li.Value = logType.ID.ToString();
                this.dropLogType.Items.Add(li);
            }
        }
        List<UserLogType> logTypeList2 = logbll.GetLogTypeByUserID(0); //系统默认日志分组      
        if (logTypeList2.Count != 0)
        {
            foreach (UserLogType logType in logTypeList2)
            {
                ListItem li = new ListItem();
                li.Text = logType.LogTypeName;
                li.Value = logType.ID.ToString();
                this.dropLogType.Items.Add(li);
            }
        }
    }


    private void UserLogToPage()
    {
        BDUModel.UserLog userLog = logbll.GetUserLogByID(LogID);
        this.txtTitle.Text = userLog.LogTitle;
        content_t.Text = userLog.LogContext;
        this.dropLogType.SelectedValue = userLog.LogTypeID.ToString();
        CDate_T.Text = userLog.CreatDate.ToShortDateString();
    }

    private void PageToUserLog(BDUModel.UserLog userLog)
    {
        userLog.LogTitle = this.txtTitle.Text.Trim();
        userLog.LogContext = content_t.Text;
        userLog.LogTypeID = new Guid(this.dropLogType.SelectedValue);
        userLog.UserID = ubll.GetLogin().UserID;
    }

    private void AddNewLog(int state)
    {
        BDUModel.UserLog userLog = new BDUModel.UserLog();
        PageToUserLog(userLog);
        userLog.LogState = state;
        logbll.CreatLog(userLog);
    }
    #endregion

    protected void btnPut_Click(object sender, EventArgs e)
    {
        if (txtTitle.Text == "" || txtTitle.Text == null) { this.lblErr.Text = "请填写标题"; }
        else
        {
            if (IsNew) { AddNewLog(1); }
            else
            {
                BDUModel.UserLog userLog = new BDUModel.UserLog();
                PageToUserLog(userLog);
                userLog.LogState = 1;
                userLog.ID = LogID;
                logbll.UpdataLog(userLog);
            }
            function.WriteSuccessMsg("操作成功", "SelfLogManage.aspx");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsNew) { AddNewLog(0); }
        else
        {
            BDUModel.UserLog userLog = new BDUModel.UserLog();
            PageToUserLog(userLog);
            userLog.LogState = 0;
            userLog.ID = LogID;
            logbll.UpdataLog(userLog);
        }
        function.WriteSuccessMsg("操作成功", "SelfLogManage.aspx");
    }

    protected void IbtnEnter_Click(object sender, ImageClickEventArgs e)
    {
        //根据用户名和密码验证会员身份，并取得会员信息
        string AdminName = this.TxtUserName.Text.Trim();
        string AdminPass = this.TxtPassword.Text.Trim();
        string CookieStatus = this.DropExpiration.SelectedValue;
        //M_UserInfo info = bll.GetLoginUser(AdminName, AdminPass);
        M_UserInfo info = new M_UserInfo();
        info = ubll.AuthenticateUser(AdminName, AdminPass);//根据输入框的密码进行登录
        //如果用户Model是空对象则表明登录失败
        // Response.Write("<script>alert('" + AdminName + "') </script>");   
        if (info.IsNull)
        {
            this.LbAlert.InnerText = "用户名或密码错误！";
        }
        else
        {
            if (info.Status != 0)
            {
                this.LbAlert.InnerText = "你的帐户未通过验证或被锁定，请与网站管理员联系";
            }
            ubll.SetLoginState(info, CookieStatus);
            this.dwindow.Style["display"] = "none";

        }
    }
}
