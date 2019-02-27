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
using ZoomLa.Components;
using ZoomLa.Common;

public partial class User_UserZone_School_AddRoomActive : System.Web.UI.Page
{
    B_User bu = new B_User();
    B_ClassRoom cll = new B_ClassRoom();
    B_RoomActive bra = new B_RoomActive();
    B_RoomActiveJoin braj = new B_RoomActiveJoin();
    protected string RoomName;
    protected int RoomID
    {
        get
        {
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
            }
            if (Request.QueryString["AID"] != null)
            {
                M_RoomActive ra = bra.GetSelect(int.Parse(Request.QueryString["AID"]));
                txtTitle.Text = ra.ActiveTitle;
                txtStateDate.Text = ra.ActiveStateTime.Year+"-"+ra.ActiveStateTime.Month +"-"+ra.ActiveStateTime.Day;
                txtEndDate.Text = ra.ActiveEndTime.Year + "-" + ra.ActiveEndTime.Month + "-" + ra.ActiveEndTime.Day;
                txtContext.Text = ra.ActiveContext.Replace("<br/>","\n");
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        M_RoomActive ra = new M_RoomActive();
        M_UserInfo mui=bu.GetLogin();
        ra.ActiveAddTime = DateTime.Now;
        ra.ActiveTitle = txtTitle.Text;
        ra.ActiveStateTime = DataConverter.CDate(txtStateDate.Text);
        ra.ActiveEndTime = DataConverter.CDate(txtEndDate.Text);
        ra.ActiveContext = txtContext.Text.Replace("\n","<br/>");
        ra.ActiveUserID = mui.UserID;
        ra.RoomID = RoomID;
        if (Request.QueryString["AID"] != null)
        {
            ra.AID = DataConverter.CLng(Request.QueryString["AID"]);
            bra.GetUpdate(ra);
        }
        else
        {
            bra.GetInsert(ra);
            
            M_RoomActiveJoin mraj = new M_RoomActiveJoin();
            mraj.UserName = mui.UserName;
            mraj.UserID = mui.UserID;
            DataTable dt=bra.Select_ByValue(" Max(AID) ","","");
            if(dt.Rows.Count >0)
            {
                mraj.ActiveID = int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                mraj.ActiveID = 0;
            }
            mraj.AddTime = DateTime.Now;
            braj.GetInsert(mraj);
        }
        Response.Write("<script>location.href='RoomActiveList.aspx?RoomID="+RoomID+"'</script>");
    }
}
