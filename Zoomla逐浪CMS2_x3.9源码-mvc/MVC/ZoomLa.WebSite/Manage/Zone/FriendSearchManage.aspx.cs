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
namespace ZoomLaCMS.Manage.Zone
{
    public partial class FriendSearchManage : CustomerPageAction
    {
        protected B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckIsLogin();
            if (!IsPostBack)
            {

            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li class='active'>搜索好友管理</li>");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}