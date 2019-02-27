using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Common;
using ZoomLa.BLL;

public partial class manage_Config_ShopConfig : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        function.AccessRulo();
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
    }
}
