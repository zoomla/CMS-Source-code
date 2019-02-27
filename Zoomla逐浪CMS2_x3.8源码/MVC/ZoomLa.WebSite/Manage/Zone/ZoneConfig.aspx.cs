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
using ZoomLa.Components;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Zone
{
    public partial class ZoneConfig : CustomerPageAction
    {
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li class='active'>会员空间配置</li>");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
        }
    }
}