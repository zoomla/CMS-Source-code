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

public partial class User_UserZone_WebUserControlTop : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected int id
    {
        get
        {
            if (HttpContext.Current.Request.Cookies["UserState"]["UserID"] != null)
                return int.Parse(HttpContext.Current.Request.Cookies["UserState"]["UserID"]);
            else
                return 0;

        }
        set
        {
            id = value;
        }
    }
}
