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

public partial class Manage_Copyright_WorksList : System.Web.UI.Page
{
    private string Skey { get { return Request.QueryString["skey"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        //copyBll.Redirect_URI = Request.Url.ToString();
        if (!IsPostBack)
        {
            C_CopyRight.CheckLogin();
            workslist_ifr.Attributes.Add("src", "http://sale.banquanyin.com/works/get_list/all");
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Plus/ADManage.aspx'>扩展功能</a></li><li><a  href=\"Config.aspx\">版权中心</a></li><li class=\"active\">作品列表[<a href=\"AddWorks.aspx\">添加作品</a>]</li>");
        }
    }
}