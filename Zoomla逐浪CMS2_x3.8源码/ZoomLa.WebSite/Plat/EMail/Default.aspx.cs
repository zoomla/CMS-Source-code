using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Plat_EMail_Default : System.Web.UI.Page
{
    B_Plat_Mail mailBll = new B_Plat_Mail();
    B_Message msgBll = new B_Message();
    B_User buser = new B_User();
    M_APIResult retMod = new M_APIResult();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind()
    {
        M_UserInfo mu = buser.GetLogin();
        {
            DataTable dt = mailBll.Sel(10, mu.UserID, 0, 1,"","");
            Rece_RPT.DataSource = dt;
            Rece_RPT.DataBind();
            Rece_Empty.Visible = dt.Rows.Count < 1;
        }
        {
            DataTable dt = mailBll.Sel(10, mu.UserID, 1, 1, "", "");
            Send_RPT.DataSource = dt;
            Send_RPT.DataBind();
            Send_Empty.Visible = dt.Rows.Count < 1;
        }
        {
            DataTable dt = msgBll.SelMyMail(mu.UserID);
            SiteMail_RPT.DataSource = dt;
            SiteMail_RPT.DataBind();
            SiteMail_Empty.Visible = dt.Rows.Count < 1;
        }
    }
    public string GetUName() 
    {
        return buser.GetUserNameByIDS(Eval("Sender",""));
    }
}