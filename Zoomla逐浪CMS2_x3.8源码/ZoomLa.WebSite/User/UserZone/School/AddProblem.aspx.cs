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

public partial class User_UserZone_School_AddProblem : System.Web.UI.Page
{
     #region 业务对象
    B_User ubll = new B_User();
    //UserTableBLL utbll = new UserTableBLL();
    //B_Interlocution ibll = new B_Interlocution();
    B_Result brbll = new B_Result();
    B_Student bs = new B_Student();
    B_ClassRoom cll = new B_ClassRoom();
    protected string RoomName = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ubll.CheckIsLogin();
        if (!IsPostBack)
        {
            M_UserInfo uinfo = ubll.GetLogin();
            //初始化接收ID
            if (Request.QueryString["Roomid"] != null)
            {
                Roomid = int.Parse(Request.QueryString["Roomid"].ToString());
                RoomName = cll.GetSelect(Roomid).RoomName;
            }
        }

    }
    //接收ID
    public int Roomid
    {
        get
        {
            if (ViewState["Roomid"] != null)
                return int.Parse(ViewState["Roomid"].ToString());
            else return 0;
        }
        set
        {
            ViewState["Roomid"] = value;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //M_Interlocution mi = new M_Interlocution();
        //mi.RoomID = Roomid;
        //mi.UserID = ubll.GetLogin().UserID ;
        //mi.ProblemTitle = txtTitle.Text;
        //mi.ProblemContext = txtContext.Text.Replace("\n","<br/>");
        //mi.AddTime = DateTime.Now;
        //int i=ibll.GetInsert(mi);
        //Response.Redirect("ShowProblem.aspx?Pid=" + i.ToString());
    }
}
