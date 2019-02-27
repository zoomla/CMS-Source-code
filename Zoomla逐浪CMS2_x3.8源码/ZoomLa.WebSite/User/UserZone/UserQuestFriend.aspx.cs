using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data;
using ZoomLa.Common;
public partial class User_Guild_UserQuestFriend : System.Web.UI.Page
{
    private B_User user = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }
    protected void btnSecher_Click(object sender, EventArgs e)
    {
        string query = "";
        if (DataConverter.CLng(sex_dp.Value) > 0)
        {
            query += "Sex=" + sex_dp.Value + "&";
        }
        Response.Redirect("QueryUser.aspx?" + query);
    }
    protected void UserID_Skey_Btn_Click(object sender, EventArgs e)
    {
        Response.Redirect("QueryUser.aspx?UserID=" + DataConverter.CLng(UserID_T.Text));
    }
    protected void UserName_Skey_Btn_Click(object sender, EventArgs e)
    {
        Response.Redirect("QueryUser.aspx?Skey=" + HttpUtility.UrlEncode(UserName_T.Text));
    }
}
