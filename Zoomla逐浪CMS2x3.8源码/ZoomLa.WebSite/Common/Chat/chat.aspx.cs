using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.Model.Chat;

public partial class test_chat : System.Web.UI.Page
{
    B_Admin badmin = new B_Admin();
    B_User buser = new B_User();
    B_User_Friend friendBll = new B_User_Friend();
    B_ChatMsg chatBll = new B_ChatMsg();
    B_ServiceSeat SeatBll = new B_ServiceSeat();
    B_Temp tempBll = new B_Temp();
    public string NoFace = "onerror='this.src=\'/Images/userface/noface.png\';'";
    //传了该值则显示欢迎语
    public int CodeID { get { return DataConverter.CLng(Request.QueryString["CodeID"]); } }
    /// <summary>
    /// visitor:登录行为,如未登录,默认游客(不弹窗)
    /// admin:后台管理员,默认同步其绑定用户
    /// </summary>
    public string Login { get { return (Request.QueryString["Login"] ?? "").ToLower(); } }
    private DataTable UserDT
    {
        get
        {
            if (ViewState["UserDT"] == null)
            {
                ViewState["UserDT"] = buser.SelAll();
            }
            return (DataTable)ViewState["UserDT"];
        }
        set { ViewState["UserDT"] = value; }
    }
    //需要对话的人的Uid
    private int UserID { get { return DataConverter.CLng(Request.QueryString["uid"]); } }
    //从临时存储中获取
    private string UserFlag { get { return Request.QueryString["UserFlag"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            customs_ul.Visible = true;
            MyBind();
        }
    }
    public void MyBind()
    {
        //后期将其分离
        M_OnlineUser user = chatBll.GetLogin();
        if (user != null) { UserName_L.Text = user.UserName; UserID_Hid.Value = user.UserID; }
        if (!string.IsNullOrEmpty(Login))
        {
            function.Script(this, "VisitorToLogin('用户" + DateTime.Now.ToString("yyyyMMddHHmmss") + "','" + Request["idflag"] + "');");
        }
        else if (user == null)
        {
            function.Script(this, "ShowModal();");
        }

        if (!string.IsNullOrEmpty(UserFlag))
        {
            M_OnlineUser mu = chatBll.GetModelByUid(UserFlag);
            //function.WriteErrMsg(mu.UserID+":"+mu.UserName);
            function.Script(this, "ChangeTalker('" + mu.UserID + "','" + mu.UserName + "','" + mu.UserFace + "');");
        }
        else if (UserID > 0)
        {
            M_UserInfo mu = buser.GetSelect(UserID);
            function.Script(this, "ChangeTalker('" + mu.UserID + "','" + mu.UserName + "','" + mu.UserFace + "');");
        }
        if (buser.CheckLogin())
        {
            DataTable dt = friendBll.SelMyFriend(Convert.ToInt32(user.UserID));
            Friend_RPT.DataSource = dt;
            Friend_RPT.DataBind();
            if (dt == null) { Friend_Num.Text = "0"; }
            else {
                Friend_Num.Text = dt.Rows.Count.ToString();
                function.Script(this, "InitUserList(" + JsonHelper.JsonSerialDataTable(dt) + ");");
            }

        }
        //用于显示欢迎信息
        if (CodeID > 0)
        {
            M_Temp tempMod = tempBll.SelReturnModel(CodeID);
            if (tempMod != null && tempMod.UseType == 12)
            {
                Wel_Hid.Value = tempMod.Str2;
                function.Script(this, "ShowWel('" + tempMod.Str1 + "');");
            }
        }
        //-----------------------------------
        //if (badmin.CheckLogin())
        //{
        //    visitor_ul.Visible = true;
        //    List<M_OnlineUser> list = chatBll.GetOnlineVisitor();
        //    Y_RPT.DataSource = list;
        //    Y_RPT.DataBind();
        //    Unum_L.Text = list.Count.ToString();
        //    M_AdminInfo adminmod=badmin.GetAdminLogin();
        //    DataTable dt = SeatBll.SelBySUid(adminmod.AdminId);
        //    function.Script(this, "SetMyInfo('" + adminmod.AdminId + "','" + adminmod.AdminName + "','" + dt.Rows[0]["S_ImgUrl"] + "');BeginInit();");
        //}
        //else
        {
            //DataTable dt = SeatBll.Select_All();
            DataTable dt = UserDT;
            dt.DefaultView.RowFilter = "Status=0";
            dt = dt.DefaultView.ToTable();
            Customs_RPT.DataSource = dt;
            Customs_RPT.DataBind();
        }
        if (user != null)
        {
            function.Script(this, "SetMyInfo('" + user.UserID + "','" + user.UserName + "','" + user.UserFace + "');BeginInit();");
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        UserDT = null;
    }
}