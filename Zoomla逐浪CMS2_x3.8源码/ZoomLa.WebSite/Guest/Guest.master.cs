using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Guest_Guest : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        M_UserInfo mu = new B_TempUser().GetLogin();
        if (!string.IsNullOrEmpty(mu.UserName) && mu.UserID > 0)
        {
            userlog_div.Visible = true;
            uname_span.InnerText = mu.HoneyName;
            mypost_a.HRef = "/PostSearch?uid="+mu.UserID;
        }
        else
        {
            login_div.Visible = true;
        }
    }
}
