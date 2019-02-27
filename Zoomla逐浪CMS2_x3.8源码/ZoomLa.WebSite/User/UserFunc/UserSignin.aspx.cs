using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using System.Data;
using ZoomLa.SQLDAL;

public partial class User_UserFunc_UserSignin : System.Web.UI.Page
{
    B_User_Signin sinBll = new B_User_Signin();
    M_User_Signin sinMod = new M_User_Signin();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int uid = buser.GetLogin().UserID;
            if (sinBll.IsSignToday(uid))
            {
                Signin_Btn.Enabled = false;
                Remind_Span.Visible = true;
            }
            function.Script(this, "HasSignDays('" + sinBll.GetHasSignDays(uid) + "');");
        }

    }
        

    protected void Signin_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        if (!sinBll.IsSignToday(mu.UserID))
        {
            sinMod.CreateTime = DateTime.Now;
            sinMod.UserID = mu.UserID;
            sinMod.Status = 1;
            sinMod.Remind = mu.UserName + "签到";
            sinBll.Insert(sinMod);
            Signin_Btn.Enabled = false;
            Remind_Span.Visible = true;
        }
        else
        {
            function.Script(this, "alert('你今天已经签过到了!!');");
        }
    }
}