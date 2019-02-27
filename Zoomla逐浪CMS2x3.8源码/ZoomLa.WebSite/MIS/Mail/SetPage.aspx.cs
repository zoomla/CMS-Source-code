using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

public partial class MIS_Mail_SetPage : System.Web.UI.Page
{
    string callback = "";
    DataTable dt = new DataTable();
    B_MailSet bll = new B_MailSet();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["type"]))
        {
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("ID", Request["ID"]) };
            dt = bll.Sel(DataConvert.CLng(Request["ID"]));
            callback = Request.QueryString["callback"];
            if (!string.IsNullOrEmpty(Request["type"]) && Request["type"] == "IsDef")
            {
                Response.Write(callback + "(0)");
            }
        }
    }
}