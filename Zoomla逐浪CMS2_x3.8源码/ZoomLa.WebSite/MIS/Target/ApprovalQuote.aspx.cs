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

public partial class MIS_Target_ApprovalQuote : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_MisApproval Bma = new B_MisApproval();
    DataTable dt = new DataTable();
    M_MisApproval Map = new M_MisApproval();
    //string types = "";
    //string Pro = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
    }
}