using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.PdoApi.CopyRight;
namespace ZoomLaCMS.Manage.Copyright
{
    public partial class Authorized : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                C_CopyRight.CheckLogin();
                auth_ifr.Attributes.Add("src", "http://sale.banquanyin.com/authorization/");
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Plus/ADManage.aspx'>扩展功能</a></li><li><a  href=\"Config.aspx\">版权中心</a></li><li class=\"active\">我的授权</li>");
            }
        }
    }
}