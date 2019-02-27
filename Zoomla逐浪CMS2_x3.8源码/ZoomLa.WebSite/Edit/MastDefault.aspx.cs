using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Model;
using ZoomLa.BLL;

public partial class Edit_EditContent : System.Web.UI.Page
{
    B_User b_User = new B_User();
    protected string content = string.Empty;
    protected new string ID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!b_User.CheckLogin())
        {
            Response.Redirect("/User/Login.aspx");
        }
    }
        
}
