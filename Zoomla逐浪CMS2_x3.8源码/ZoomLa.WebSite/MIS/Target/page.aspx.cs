using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

public partial class MIS_page : System.Web.UI.Page
{
    B_Mis bll = new B_Mis();
    DataTable dt = new DataTable();
    B_User buser=new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        Bind1();
    }
    protected void Bind1()
    {
      
    }
    protected void Button_Click(object sender, EventArgs e)
    {
        string drType = this.drType.SelectedValue;
        string TxtKey = this.TxtKey.Text;
        dt = bll.selbytitle(buser.GetLogin().UserName, drType, TxtKey);
        Repeater2.DataSource = dt;
        Repeater2.DataBind();
    }
}