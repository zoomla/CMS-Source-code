using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class WxTree : System.Web.UI.UserControl
    {
        B_WX_APPID wxBll = new B_WX_APPID();
        protected void Page_Load(object sender, EventArgs e)
        {
            RPT.DataSource = wxBll.Sel();
            RPT.DataBind();
        }
    }
}