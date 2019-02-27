using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZoomLaCMS.Manage.Copyright
{
    public partial class Royalty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                royalty_ifr.Attributes.Add("src", "http://sale.banquanyin.com/account/income");
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Plus/ADManage.aspx'>扩展功能</a></li><li><a  href=\"Register.aspx\">版权中心</a></li><li class=\"active\">版权收益</li>");
            }
        }
    }
}