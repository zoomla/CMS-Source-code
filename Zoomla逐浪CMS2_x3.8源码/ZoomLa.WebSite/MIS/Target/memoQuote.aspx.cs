using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using System.Data.SqlClient;

public partial class MIS_Target_memoQuote : System.Web.UI.Page
{
    B_MisInfo bll = new B_MisInfo();
    M_MisInfo model = new M_MisInfo();
    DataTable dt = new DataTable();
    B_User buser = new B_User();
    //string types = "";
    //int id = 0;
    //string Pro = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       

    }
}