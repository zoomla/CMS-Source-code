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
using ZoomLa.Sns.Model;
using System.Collections.Generic;
using ZoomLa.Sns.BLL;
using ZoomLa.BLL;

namespace ZoomLaCMS.Zone
{
    public partial class GetSearch : System.Web.UI.Page
    {
        UserTableBLL utbll = new UserTableBLL();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}