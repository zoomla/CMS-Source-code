using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;

namespace ZoomLaCMS.MIS.File
{
    public partial class Default : System.Web.UI.Page
    {
        B_MisInfo bll = new B_MisInfo();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}