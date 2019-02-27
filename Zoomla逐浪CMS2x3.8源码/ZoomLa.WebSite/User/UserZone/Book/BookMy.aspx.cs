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
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL;

public partial class BookMy : Page
{
    B_User ubll = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        int currentUser = ubll.GetLogin().UserID;
        if (!IsPostBack)
        {
            
            M_UserInfo uinfo = ubll.GetUserByUserID(currentUser);
        }
        
        this.WebUserControlMy1.CurrentPage = Convert.ToInt32(Request.QueryString["CurrentPage"].ToString());
        this.WebUserControlMy1.PageName = "BookMy";
        this.WebUserControlMy1.UserID = currentUser;
        this.WebUserControlMy1.stype = Convert.ToInt32(Request.QueryString["type"].ToString());
    }
}

