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

public partial class User_UserZone_School_ControlAuditingMemberList : System.Web.UI.UserControl
{
    private B_Student bs = new B_Student();
    private int roomid;
    public int RoomID
    {
        get { return roomid; }
        set { roomid = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (dt.Rows.Count > 0)
        {
            this.DataList1.DataSource = dt;
            this.DataList1.DataBind();
        }
        else
        {
            this.DataList1.Visible = false;
        }
        DataTable dtcount = new DataTable();
        int c = int.Parse(dtcount.Rows[0]["Scount"].ToString());
        if (c > 0)
            this.Label1.Text = "<a href=\"AuditingMemberList.aspx?RoomID=" + RoomID + "\">" + c.ToString() + "</a>";
        else
            this.Label1.Text = "0";
    }
}
