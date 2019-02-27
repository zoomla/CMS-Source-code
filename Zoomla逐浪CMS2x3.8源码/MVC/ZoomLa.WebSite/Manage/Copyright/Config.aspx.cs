using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.PdoApi.CopyRight;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Copyright
{
    public partial class Config : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                C_CopyRight.CheckLogin();
                Prompt_Div.Visible = (DeviceHelper.GetBrower() == DeviceHelper.Brower.IE);
                config_ifr.Attributes.Add("src", "http://open.banquanyin.com/login");
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Plus/ADManage.aspx'>扩展功能</a></li><li><a  href=\"Register.aspx\">版权中心</a></li><li class=\"active\">版权配置 [<i class='fa  fa-check-circle-o'></i>配置信息验证成功，请登陆管理版权收入][<a href='" + CustomerPageAction.customPath2 + "/Config/PlatInfoList.aspx'>重新配置</a>]</li>");
            }
        }
    }
}