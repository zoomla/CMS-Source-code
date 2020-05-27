using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class UserInfoBar : System.Web.UI.UserControl
    {
        B_User buser = new B_User();
        [Browsable(true)]
        public int Width { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            function.Script(this.Page, "InitUserBar(" + buser.CountUserField(buser.GetLogin().UserID) + ")");
        }
    }
}