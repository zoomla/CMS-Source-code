using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;

public partial class User_ASCX_DefaultTop : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        M_UserInfo mu = new B_User().GetLogin();
        uName.Text = mu.UserName;
    }
}