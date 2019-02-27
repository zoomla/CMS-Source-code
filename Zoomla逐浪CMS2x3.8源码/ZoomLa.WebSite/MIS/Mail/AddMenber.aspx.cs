using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Data.SqlClient;

public partial class MIS_Mail_AddMenber : System.Web.UI.Page
{
    B_User buser = new B_User();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin(Request.Url.LocalPath);

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}