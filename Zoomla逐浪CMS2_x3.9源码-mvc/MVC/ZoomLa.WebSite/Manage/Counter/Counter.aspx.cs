using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;

namespace ZoomLaCMS.Manage.Counter
{
    public partial class Counter : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li> <li><a href='Counter.aspx'>访问统计</a></li><li><a href='Counter.aspx'>统计导航</a></li>");
        }
    }
}