using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Sentiment;
namespace ZoomLaCMS.Manage.I.ASCX
{
    public partial class SenTree : System.Web.UI.UserControl
    {
        B_Sen_Task senBll = new B_Sen_Task();
        protected void Page_Load(object sender, EventArgs e)
        {
            RPT.DataSource = senBll.SelTop();
            RPT.DataBind();
        }
    }
}