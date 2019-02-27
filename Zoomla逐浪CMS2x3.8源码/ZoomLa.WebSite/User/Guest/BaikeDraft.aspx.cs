using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class User_Guest_BaikeDraft : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Baike bkBll = new B_Baike();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    protected void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        DataTable dt = bkBll.U_SelMy(mu.UserID, "", 0);
        RPT.DataSource = dt;
        RPT.DataBind();
    }
}