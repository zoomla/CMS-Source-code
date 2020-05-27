using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.SQLDAL;

public partial class Guest_Baike_Diag_SelClass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    private void MyBind() 
    {
        DataTable dt = SqlHelper.ExecuteTable(CommandType.Text, "SELECT *,(SELECT Count(GradeID) FROM ZL_Grade WHERE ParentID=A.GradeID) AS ChildCount FROM ZL_Grade A WHERE Cate=3");
        dt.DefaultView.RowFilter = "ParentID=0";
        RPT1.DataSource = dt.DefaultView.ToTable();
        RPT1.DataBind();
        dt.DefaultView.RowFilter = "ParentID>0";
        Cate_Hid.Value = JsonConvert.SerializeObject(dt.DefaultView.ToTable());
    }
}