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

public partial class User_UserZone_School_ShowProblemList : System.Web.UI.Page
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
                DataTable dt = bs.SelByURid(Roomid,1,uinfo.UserID);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["StatusType"].ToString() == "1")
                    {
                        td1.Visible = true;
                    }
                    else
                    {
                        td1.Visible = false;
                    }
                    GetMessage();
                }
              
            }

        }

    }
    //接收ID
    protected  int Roomid
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
    private void GetMessage()
    {
        //DataTable dt = ibll.SelByRid(Roomid);
        //this.EGV .DataSource = dt;
        //this.EGV.DataBind();
    }

    //显示用户昵称
    protected string getusername(string uid)
    {
        return ubll.GetUserByUserID(int.Parse(uid)).UserName;

    }

}
