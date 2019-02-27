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

public partial class User_UserZone_School_ControlNotifyTop : System.Web.UI.UserControl
{
    private B_RoomNotify brn = new B_RoomNotify();
    private int roomid;
    public int RoomID
    {
        get { return roomid; }
        set { roomid = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = brn.SelByValue(RoomID);
        if (dt.Rows.Count > 0)
        {
            tdTitle.InnerHtml = "["+dt.Rows[0]["NotifyTitle"].ToString()+"]";
            string context = "";
            if (dt.Rows[0]["NotifyContext"].ToString().Length > 140)
            {
                context = dt.Rows[0]["NotifyContext"].ToString().Substring(0, 140) + "....    <a href=\"ShowRoomNotify.aspx?Nid=" + dt.Rows[0]["ID"].ToString() + "\">查看全文</a>";
                
            }
            else
            {
                context = dt.Rows[0]["NotifyContext"].ToString() + "     <a href=\"ShowRoomNotify.aspx?Nid=" + dt.Rows[0]["ID"].ToString() + "\">查看全文</a>";
            }
            tdContext.InnerHtml = context;
            tdTime.InnerHtml = dt.Rows[0]["AddTime"].ToString();
        }
        else
        {
            tdContext.InnerHtml = "本班暂时没有黑板报！";
        }

    }
}
