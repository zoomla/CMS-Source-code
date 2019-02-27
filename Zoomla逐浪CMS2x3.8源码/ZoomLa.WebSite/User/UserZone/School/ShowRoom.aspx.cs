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
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class User_UserZone_School_ShowRoom : System.Web.UI.Page
{
    B_ClassRoom cll = new B_ClassRoom();
    B_School sll = new B_School();
    B_User bu = new B_User();
    B_Student bs=new B_Student ();
    public string RoomName="";
    public string schoolid ;
    protected int RoomID
    {
        get
        {
            if (ViewState["RoomID"] == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(ViewState["RoomID"].ToString());
            }
        }
        set{
            ViewState["RoomID"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        bu.CheckIsLogin();
        if (!IsPostBack)
        {
            RoomID = int.Parse(Request.QueryString["Roomid"]);
            M_UserInfo mu=bu.GetLogin();
            this.ControlSchoolMessage1.Roomid = RoomID;
            this.ControlRoomInfo1.Roomid = RoomID;
            this.ControlNotifyTop1.RoomID = RoomID;
            this.ControlProblemTop1.RoomID = RoomID;
            this.ControlRoomActive1.RoomID = RoomID;
            this.ControlAuditingMemberList1.RoomID = RoomID;
            M_ClassRoom mcr=cll.GetSelect(RoomID);
            schoolid=mcr.SchoolID.ToString();
            RoomName = mcr.RoomName; 
            //查询用户在班级里的信息
            DataTable dt = bs.SelByURid(RoomID,1, mu.UserID);
            if (dt.Rows.Count > 0)
            {
                switch (dt.Rows[0]["StatusType"].ToString())
                {
                    case "1":
                        trStudent.Visible = true;
                        trTh.Visible = false;
                        break;
                    case "2":
                        trTh.Visible = true;
                        trStudent.Visible = false;
                        break;
                    default:
                        trStudent.Visible = false;
                        trTh.Visible = false;
                        break;
                }
            }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowSchoolMessage.aspx?Roomid=" + RoomID.ToString());
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowProblemList.aspx?Roomid=" + RoomID.ToString());
    }
}
