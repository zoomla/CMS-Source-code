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
public partial class User_UserZone_School_ControlRoomActive : System.Web.UI.UserControl
{
    B_RoomActive raBll = new B_RoomActive();
    private int roomid;
    public int RoomID
    {
        get { return roomid; }
        set { roomid = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Repeater1.DataSource = raBll.Select_ByValue(" top 1 * ", " RoomID=" + RoomID + " and ActiveEndTime>'" + DateTime.Now.Date  + "' or ActiveEndTime='" + DateTime.Now.Date  + "'", " ActiveStateTime desc");
        this.Repeater1.DataBind();
        if (this.Repeater1.Items.Count <= 0)
        {
            tdr.InnerHtml = "本班暂时没有任何活动";
        }
    }

    protected string GetStr(string str,string id)
    {
        
        if (str.Length > 140)
        {
            return str.Substring(0, 140) + ".....<a href='ShowRoomActive.aspx?Aid=" + id + "'>查看详细</a>";
        }
        else
        {
            return str + "   <a href='ShowRoomActive.aspx?Aid=" + id + "'>查看详细</a>"; ;
        }
    }

    protected string GetDate(string stateTime, string endTime)
    {
        DateTime dt1 = DateTime.Parse(stateTime);
        DateTime dt2 = DateTime.Parse(endTime);
        return dt1.Year + "-" + dt1.Month + "-" + dt1.Day + " ~ " + dt2.Year + "-" + dt2.Month + "-" + dt2.Day;
    }
}
