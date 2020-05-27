using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;

namespace ZoomLaCMS
{
    public partial class login_Ajax : System.Web.UI.Page
    {
        public int LoginCount
        {
            get
            {
                if (HttpContext.Current.Session["ValidateCount"] == null)
                {
                    HttpContext.Current.Session["ValidateCount"] = 0;
                }
                return Convert.ToInt32(HttpContext.Current.Session["ValidateCount"]);
            }
            set
            {
                HttpContext.Current.Session["ValidateCount"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginCount >= 3)
            {
                function.Script(this, "EnableCode();");
            }
        }
    }
}