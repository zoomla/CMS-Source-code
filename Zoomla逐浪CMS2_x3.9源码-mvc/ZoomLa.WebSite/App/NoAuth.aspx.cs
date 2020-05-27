using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;


namespace ZoomLaCMS.App
{
    public partial class NoAuth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                auth_sp.InnerHtml = SafeSC.ReadFileStr("/APP/Other/auth.html");
            }
        }
    }
}