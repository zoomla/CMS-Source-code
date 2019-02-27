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

public partial class User_UserZone_School_AuditingMemberList : System.Web.UI.Page
{
    B_ClassRoom cll = new B_ClassRoom();
    B_School sll = new B_School();
    B_User bu = new B_User();
    B_Student bs = new B_Student();
    public string RoomName = "";
    public int RoomID
    {
        get
        {
            if (ViewState["roomid"] != null)
                return int.Parse(ViewState["roomid"].ToString());
            else
                return 0;
        }
        set { ViewState["roomid"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        bu.CheckIsLogin();
        if (!IsPostBack)
        {
            RoomID = int.Parse(Request.QueryString["Roomid"].ToString());
            M_UserInfo mu = bu.GetLogin();
            M_ClassRoom cr = cll.GetSelect(RoomID);
            if (cr.CreateUser == mu.UserID)
            {
                this.EGV.DataSource = bs.SelByURid( RoomID ,5);
            }
            else
            {
                this.EGV.DataSource = bs.SelByURid(RoomID,6);
            }
            this.EGV.DataBind();
        }
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        M_UserInfo mu = bu.GetLogin();
        M_Student ms = bs.GetSelect(int.Parse(EGV.DataKeys[e.NewSelectedIndex].Value.ToString()));
        ms.Addtime = DateTime.Now;
        ms.Auditing = 1;
        ms.AuditingUserID = mu.UserID;
        ms.AuditingUserName = mu.UserName;
        bs.GetUpdate(ms);
        Response.Write("<script>location.href='AuditingMemberList.aspx?Roomid=" + RoomID + "'</script>");
    }

    protected string GetType(string type)
    {
        switch (type)
        {
            case "1":
                return "学生";
            case "2":
                return "老师";
            case "3":
                return "家长";
            default:
                return "";
        }
    }
}