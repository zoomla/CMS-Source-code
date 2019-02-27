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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;


public partial class User_UserShop_WebUserControlTop : System.Web.UI.UserControl
{
    private B_Pub bp = new B_Pub();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (bp.SelectNode(3).Rows.Count == 0)
        {
            HyperLink1.Visible = false;
        }
    }
}
