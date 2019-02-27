using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using BDUModel;
using BDUBLL;
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.Common;

public partial class SelfLogManage : Page
{
    #region 业务对象
    LogManageBLL logbll = new LogManageBLL();
    private Dictionary<string, string> Order
    {
        get
        {
            Dictionary<string, string> ht = new Dictionary<string, string>();
            ht.Add("CreatDate", "1");
            return ht;
        }
    }
    protected B_User useBll = new B_User();
    private Guid LogTypeID
    {
        get
        {
            if (ViewState["LogTypeID"] != null)
                return new Guid(ViewState["LogTypeID"].ToString());
            else return new Guid("ff503f4b-7972-4cd2-8fc8-08bdcfff7115");
        }
        set
        {
            ViewState["LogTypeID"] = value;
        }
    }

    private DateTime CrateDate
    {
        get
        {
            if (ViewState["CrateDate"] != null)
                return DateTime.Parse(ViewState["CrateDate"].ToString());
            else return new DateTime();
        }
        set
        {
            ViewState["CrateDate"] = value;
        }
    }

    public object DateTable { get; private set; }

    #endregion

    #region 对象初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {           
            B_User ubll = new B_User();
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
                M_UserInfo uinfo = ubll.GetUserByUserID(useBll.GetLogin().UserID);
                GetUserLog(null);
                GetLogTypeCount();
                GetDataLogCount();
            }
        }
    }
    #endregion

    #region 辅助方法
    //我的日志
    private void GetUserLog(PagePagination pagepag)
    {
        List<UserLog> list = logbll.GetSelfUserLogByUserID(useBll.GetLogin().UserID, 1,LogTypeID,CrateDate, null);
        this.gvLog.DataSource = list;
        this.gvLog.DataBind();      
    }

    //获取类型日志数
    private void GetLogTypeCount()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID", Type.GetType("System.Guid")));
        dt.Columns.Add(new DataColumn("LogTypeName", Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("LogCount", Type.GetType("System.Int32")));
        List<UserLogType> list = new List<UserLogType>();
        list = logbll.GetLogTypeByUserID(useBll.GetLogin().UserID);
        foreach (UserLogType userLogType in list)
        {
            DataRow dr = dt.NewRow();
            dr["ID"] = userLogType.ID;
            dr["LogTypeName"] = userLogType.LogTypeName;
            dr["LogCount"] = logbll.GetUserLogByLogTypeID(userLogType.ID, useBll.GetLogin().UserID).Count;
            dt.Rows.Add(dr);
        }
        DataRow dr1 = dt.NewRow();
        dr1["ID"] = Guid.Empty;
        dr1["LogTypeName"] = "默认分类";
        dr1["LogCount"] = logbll.GetUserLogByLogTypeID(Guid.Empty, useBll.GetLogin().UserID).Count;
        dt.Rows.Add(dr1);
        gvLogType.DataSource = dt;
        gvLogType.DataBind();
    }

    //时间统计
    private void GetDataLogCount()
    {
        
        List<UserLog> list = logbll.GetUserLogByData(useBll.GetLogin().UserID);
        gvdateLog.DataSource = list;
        gvdateLog.DataBind();
    }

    protected void gvLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbType = e.Row.FindControl("Label3") as Label;
            if (lbType != null)
            {
                if (lbType.Text != "")
                {
                    if (lbType.Text == Guid.Empty.ToString())
                        lbType.Text = "默认分类";
                    else
                    {
                        UserLogType logType = logbll.GetLogTypeByID(new Guid(lbType.Text));
                        if (logType != null)
                        {
                            lbType.Text = logType.LogTypeName;
                        }
                        else lbType.Text = "默认分类";
                    }
                }
                else lbType.Text = "默认分类";
            }
        }
    }
    #endregion

    #region 页面功能
    //类别按钮
    protected void lbtnLogTypeList_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        GridViewRow gr = lb.Parent.Parent as GridViewRow;
        LogTypeID = new Guid(this.gvLogType.DataKeys[gr.RowIndex].Value.ToString());
        CrateDate = new DateTime();
        GetUserLog(null);
    }

    //时间统计
    protected void lbtnLogDate_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        LogTypeID = new Guid("ff503f4b-7972-4cd2-8fc8-08bdcfff7115");
        CrateDate = DateTime.Parse(lb.Text);
        GetUserLog(null);
    }

    //删除日志
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        LinkButton lb = sender as LinkButton;
        GridViewRow gr = lb.Parent.Parent as GridViewRow;
        logbll.DeleteLogByID(new Guid(this.gvLog.DataKeys[gr.RowIndex].Value.ToString()));
        GetUserLog(null);
    }
    #endregion

    protected void gvLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView theGrid = sender as GridView;  
        int newPageIndex = 0;
        if (e.NewPageIndex<0)
        { 
            TextBox txtNewPageIndex = null;
      
            GridViewRow pagerRow = theGrid.BottomPagerRow;     
            if (null != pagerRow)
            {
                txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;  
            }

            if (null != txtNewPageIndex)
            {
                newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; 
            }
        }
        else
        {  
            newPageIndex = e.NewPageIndex;

        }
        //gvw.PageIndex = e.NewPageIndex;
       // gvCoursewareList.PageIndex = e.NewPageIndex;
        newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
        newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;     
        theGrid.PageIndex = newPageIndex;
        GetUserLog(null);       
    }
    protected void IbtnEnter_Click(object sender, ImageClickEventArgs e)
    {
        if (SiteConfig.UserConfig.EnableCheckCodeOfLogin)
        {
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], TxtValidateCode.Text.Trim()))
            {
                this.LbAlert.InnerText = "验证码不正确！";
            }
        }
        B_User ubll = new B_User();
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
            GetUserLog(null);
            GetLogTypeCount();
            GetDataLogCount();
        }
    }

    protected void Search_B_Click(object sender, EventArgs e)
    {
        //0:按日期，1:按关键字
        //int type = DataConvert.CLng(SearchType_Dlist.SelectedValue);
        //int uid = useBll.GetLogin().UserID;
        //DataTable dt = logbll.Search(uid,type, Keys_T.Text.Replace(" ", ""));
        //gvLog.DataSource = dt;
        //gvLog.DataBind();
    }
}

