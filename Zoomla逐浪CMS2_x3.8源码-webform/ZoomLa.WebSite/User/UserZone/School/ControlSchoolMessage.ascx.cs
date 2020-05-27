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

public partial class User_UserZone_School_ControlSchoolMessage : System.Web.UI.UserControl
{
    private int roomid;
    public int Roomid
    {
        get
        {
            //if (roomid == 0)
            //    return 0;
            //else
                return roomid;
        }
        set 
        {
            roomid = value; 
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        B_User ubll = new B_User();
        B_RoomMessage mebll = new B_RoomMessage();
        ubll.CheckIsLogin();
        M_RoomMessage me = new M_RoomMessage();
        if (Roomid == 0 && Request.QueryString["Roomid"] != null)
            Roomid = int.Parse(Request.QueryString["Roomid"]);

        me.InceptID = Roomid;
        me.SendID = ubll.GetLogin().UserID;
        me.Mcontent = this.txtMsg.Text.Replace("\n","<br/>");
        me.RestoreID = 0;
        
        mebll.InsertMessage(me);
        this.txtMsg.Text = "";
        Response.Redirect("ShowSchoolMessage.aspx?Roomid=" + Roomid);
    }
}
