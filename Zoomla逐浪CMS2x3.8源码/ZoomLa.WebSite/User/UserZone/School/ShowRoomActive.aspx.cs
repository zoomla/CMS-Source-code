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

public partial class User_UserZone_School_ShowRoomActive : System.Web.UI.Page
{
    B_RoomActive bra = new B_RoomActive();
    B_User bu = new B_User();
    B_ClassRoom cll = new B_ClassRoom();
    B_Student bs = new B_Student();
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

    protected int AID
    {
        get
        {
            if (ViewState["aid"] != null)
                return int.Parse(ViewState["aid"].ToString());
            else
                return 0; 
        }
        set { ViewState["aid"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        bu.CheckIsLogin();
        if (!IsPostBack)
        {
            M_UserInfo mu = bu.GetLogin();

            if (Request.QueryString["AID"] != null)
            {
                AID = int.Parse(Request.QueryString["AID"]);
                M_RoomActive ra = bra.GetSelect(AID);

                RoomID = ra.RoomID ;
                //查询班级详细信息
                M_ClassRoom cr = cll.GetSelect(RoomID);
                RoomName = cr.RoomName;
                M_RoomActive mra= bra.GetSelect(AID);
                tdTitle.InnerHtml = mra.ActiveTitle;
                tdContext.InnerHtml = mra.ActiveContext;
                tdDate.InnerHtml = mra.ActiveStateTime.Year + "-" + mra.ActiveStateTime.Month + "-" + mra.ActiveStateTime.Day + " ~ " + mra.ActiveEndTime.Year + "-" + mra.ActiveEndTime.Month + "-" + mra.ActiveEndTime.Day;
                if (mra.ActiveUserID == mu.UserID || cr.CreateUser == mu.UserID)
                {
                    Eitd.Visible = true;
                }
                else
                {
                    Eitd.Visible = false;
                }
                if (raj.SelByUid(mu.UserID,AID).Rows.Count > 0)
                {
                    Button1.Visible = false;
                }
                else
                {
                    Button1.Visible = true;
                }


            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        M_RoomActive ra = bra.GetSelect(AID);
        if (ra.ActiveStateTime.Date > DateTime.Now.Date)
        {
            bra.GetDelete(AID);
            Response.Write("<script>location.href='RoomActiveList.aspx?RoomID=" + RoomID + "'</script>");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = bu.GetLogin();
        M_RoomActiveJoin mraj = new M_RoomActiveJoin();
        mraj.ActiveID = AID;
        mraj.AddTime = DateTime.Now;
        mraj.UserID = mu.UserID;
        mraj.UserName = mu.UserName;
        raj.GetInsert(mraj);
        Response.Write("<script>location.href='ShowRoomActive.aspx?AID=" + AID + "'</script>");
    }
}
