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

public partial class User_UserZone_School_ControlProblemTop : System.Web.UI.UserControl
{
    //B_Interlocution ibll = new B_Interlocution();
    private int roomid;
    public int RoomID
    {
        get { return roomid; }
        set { roomid = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //this.Repeater1.DataSource = ibll.SelByRid(RoomID);
        //this.Repeater1.DataBind();
    }
}
