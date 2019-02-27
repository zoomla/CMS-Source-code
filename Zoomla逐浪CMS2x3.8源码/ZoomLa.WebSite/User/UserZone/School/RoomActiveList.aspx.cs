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
using ZoomLa.Components;
using ZoomLa.Model;

public partial class User_UserZone_School_RoomActiveList : System.Web.UI.Page
{
    B_User bu = new B_User();
    B_ClassRoom cll = new B_ClassRoom();
    B_RoomActive ra = new B_RoomActive();
    B_RoomActiveJoin raj = new B_RoomActiveJoin();
    protected string RoomName;
    protected int RoomID
    {
        get {
            if (ViewState["Roomid"] != null)
                return int.Parse(ViewState["Roomid"].ToString());
            else
                return 0;
        }
        set { ViewState["Roomid"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        bu.CheckIsLogin();
        if (!IsPostBack)
        {
            M_UserInfo mu = bu.GetLogin();
            if (Request.QueryString["RoomID"] != null)
            {
                RoomID = int.Parse(Request.QueryString["RoomID"].ToString());
                //查询班级详细信息
                M_ClassRoom cr = cll.GetSelect(RoomID);
                RoomName = cr.RoomName;
                Bind();
            }
        }

    }

    private void Bind()
    {
        DataTable dt = ra.Sel(RoomID);
        this.EGV.DataSource = dt;
        this.EGV.DataBind();
        if (dt.Rows.Count <= 0)
            tdn.InnerHtml = "<br/>本班暂时没有任何活动！";
    }

    protected string GetType(string id)
    {
        int userid=bu.GetLogin().UserID;
        if (ra.GetSelect(int.Parse(id)).ActiveUserID == userid)
        {
            return "<font color=\"red\">发起</font>";
        }
        else
        {
            if (raj.SelByUid(userid,Convert.ToInt32(id)).Rows.Count > 0)
            {
                return "<font color=\"#009933\">参与</font>";
            }
            else
            {
                return "未参与";
            }
        }
    }

    protected string GetName(string id)
    {
        return  bu.GetUserByUserID(int.Parse(id)).UserName;
    }

    protected string GetStr(string stateTime, string endTime)
    {
        DateTime dt1 = DateTime.Parse(stateTime);
        DateTime dt2 = DateTime.Parse(endTime);
        
        if (dt1.Date <= DateTime.Now.Date )
        {
            if (dt2.Date  >= DateTime.Now.Date )
            {
                return "<font color=\"#009933\">进行中</font>";
            }
            else
            {
                return "<font color=\"red\">已结束</font>";
            }
        }
        else
        {
            return "未开始";
        }
    }

    protected string GetDate(string stateTime, string endTime)
    {
        DateTime dt1 = DateTime.Parse(stateTime);
        DateTime dt2 = DateTime.Parse(endTime);
        return dt1.Year + "-" + dt1.Month + "-" + dt1.Day + " ~ " + dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
    }
}
