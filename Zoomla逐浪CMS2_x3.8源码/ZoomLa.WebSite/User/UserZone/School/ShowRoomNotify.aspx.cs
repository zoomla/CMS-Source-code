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
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL;

public partial class User_UserZone_School_ShowRoomNotify : System.Web.UI.Page
{
    B_RoomNotify brn = new B_RoomNotify();
    B_User bu = new B_User();
    B_ClassRoom cll = new B_ClassRoom();
    B_Student bs = new B_Student();
    protected string RoomName;
    protected string Ntitle;
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

            if (Request.QueryString["Nid"] != null)
            {
                M_RoomNotify rn = brn.GetSelect(int.Parse(Request.QueryString["Nid"]));
                RoomID = rn.RoomID;
                RoomName = cll.GetSelect(RoomID).RoomName;
                tdTitle.InnerHtml = rn.NotifyTitle;
                Ntitle = rn.NotifyTitle;
                tdContext.InnerHtml = rn.NotifyContext.Replace("\n","<br/>");
                tdTime.InnerHtml = rn.AddTime.ToString ();
            }
        }
    }

}
