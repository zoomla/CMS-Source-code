using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

public partial class manage_Animation_Default : CustomerPageAction
{

    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
    }
}