using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;

public partial class manage_AddOn_StructManage : System.Web.UI.Page
{
    B_Structure bll = new B_Structure();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    { 
        function.AccessRulo();
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
    } 
}