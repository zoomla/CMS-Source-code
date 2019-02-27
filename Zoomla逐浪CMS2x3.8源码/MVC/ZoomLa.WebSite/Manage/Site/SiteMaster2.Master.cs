using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Site
{
    public partial class SiteMaster2 : System.Web.UI.MasterPage
    {
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (buser.CheckLogin())
                {
                    nameL.Text = "会员名:" + buser.GetLogin().UserName;
                    loginSpan.Visible = false;
                    UserInfoSpan.Visible = true;
                }
            }
        }
    }
}