using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Room;
using ZoomLa.Model;
using ZoomLa.Model.Exam;
using ZoomLa.Common;

public partial class User_Class_ConfigList : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_EDU_School schBll = new B_EDU_School();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        RPT.DataSource = schBll.SelByUidAndClass(mu.UserID);
        RPT.DataBind();
    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.Equals("Del"))
        {
            schBll.Del(Convert.ToInt32(e.CommandArgument));
            MyBind();
        }
    }
}