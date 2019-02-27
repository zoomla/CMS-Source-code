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
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;


public partial class LogReadCriticism : Page
{
    #region 业务对象
    LogManageBLL logbll = new LogManageBLL();
    B_User ubll = new B_User();
    int currentUser = 0;
    public  Guid LogID
    {
        get
        {
            if (ViewState["LogID"]!= null)
                return new Guid(ViewState["LogID"].ToString());
            else return Guid.Empty;
        }
        set
        {
            ViewState["LogID"] = value;
        }
    }

    #endregion

    #region 业务对象初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        currentUser = ubll.GetLogin().UserID;
        if (currentUser == 0)
            Page.Response.Redirect(@"~/user/login.aspx");
        if (!IsPostBack)
        {            
            M_UserInfo uinfo = ubll.GetUserByUserID(currentUser);
            if (Page.Request.QueryString["LogID"] != null)
            {
                ViewState["LogID"] = Page.Request.QueryString["LogID"];
                GetLog(null);
                UserLog userlog = logbll.GetUserLogByID(LogID);
                //若不是作者阅读，增加阅读量
                if (userlog.UserID!=currentUser)
                {
                    userlog.ReadCount += 1;
                    logbll.UpdataReadCount(LogID, userlog.ReadCount);
                }
            }
        }
    }
    #endregion 

    #region 辅助方法
    //获取日志
    private void GetLog(BDUModel.PagePagination pagepag)
    {
        BDUModel.UserLog userLog = logbll.GetUserLogByID(LogID);
        if (userLog != null)
        {
            labTitle.Text = userLog.LogTitle;
            labTime.Text = userLog.CreatDate.ToString();
            try
            {
                labLogType.Text = logbll.GetLogTypeByID(userLog.LogTypeID).LogTypeName;
            }
            catch
            {
                labLogType.Text = "默认分类";
            }
            labContent.Text = userLog.LogContext;
            labReadCount.Text = userLog.ReadCount.ToString();
            labCrCount.Text = userLog.CriticismCount.ToString();
            GetCriticism(pagepag);
        }
    }

    private void GetCriticism(BDUModel.PagePagination pagepag)
    {
        List<LogCriticism> list = logbll.GetLogCriticismByLogID(LogID, null);
        gvCriticism.DataSource = list;
        gvCriticism.DataBind();
    }

    protected string GetPic(string str)
    {
        return ubll.GetUserBaseByuserid(int.Parse(str)).UserFace;
    }
    #endregion

    #region 页面功能
    

    //发表评论
    protected void btnOK_Click(object sender, EventArgs e)
    {
        LogCriticism logCriticism = new LogCriticism();
        logCriticism.CriticismConten = this.txtContext.Text.Replace("\n","<br/>");
        logCriticism.LogID = LogID;
        logCriticism.UserID = currentUser;
        logbll.CreatLogCriticism(logCriticism);
        Response.Write("<script>location.href='LogReadCriticism.aspx?LogID=" + ViewState["LogID"] +"'</script>");
    }

    //删除评论
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        GridViewRow gr = lb.Parent.Parent as GridViewRow;
        logbll.DeleteCriticism(new Guid(this.gvCriticism.DataKeys[gr.RowIndex].Value.ToString()));
        GetCriticism(null);
    }

    //删除日志
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        logbll.DeleteLogByID(LogID);
        string script = "<script language='javascript'>window.location.href='SelfLogManage.aspx'</script>";
        function.Script(this,script);
    }
    #endregion
}

