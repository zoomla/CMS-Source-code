using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

public partial class Plat_Left_app : System.Web.UI.Page
{
    B_Search searchBll = new B_Search();
    protected void Page_Load(object sender, EventArgs e)
    {
        MyBind();
    }
    private void MyBind()
    {
        DataTable dt = new DataTable();
        //dt = fileBll.SelByVPath(MyVPath);
        dt = searchBll.SelByUserGroup(0);
        RPT.DataSource = dt;
        RPT.DataBind();
    }
}