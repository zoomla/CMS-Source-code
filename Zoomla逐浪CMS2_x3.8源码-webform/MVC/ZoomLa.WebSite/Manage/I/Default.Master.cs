using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

namespace ZoomLaCMS.Manage.I
{
    public partial class Default : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            B_Admin.CheckIsLogged();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}