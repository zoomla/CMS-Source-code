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

public partial class User_UserZone_School_RoomMemberList : System.Web.UI.Page
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
            M_UserInfo mu = bu.GetLogin();
            RoomName = cll.GetSelect(RoomID).RoomName;
            Bind();
        }
    }

    private void Bind()
    {
        DataTable dt = bs.SelByURid(RoomID,4);
        this.EGV.DataSource = dt;
        this.EGV.DataBind();
    }


    public string GetUserName(string id)
    {
        string str=bu.GetUserByUserID(int.Parse(id)).UserName;
        if (string.IsNullOrEmpty(str))
        {
            return "未登记";
        }
        else
        {
            return str;
        }
    }

    public string GetUserType(string type)
    {
        switch (type)
        {
            case "1":
                return "学生";
            case "2":
                return "老师";
            case "3":
                return "家长";
            default :
                return "";
        }
    }
}
