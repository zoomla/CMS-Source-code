using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZoomLaCMS.Manage.Common
{
    public partial class ShowIfr : System.Web.UI.Page
    {
        //public string IfrUrl { get { return Request.QueryString["url"]; } }
        public string url = "http://sale.banquanyin.com/register";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
    }
}