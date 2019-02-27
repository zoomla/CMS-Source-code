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
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Text;
using BDUBLL;
using BDUModel;
using ZoomLa.Sns;

public partial class manage_Zone_ZoneEdit : CustomerPageAction
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        B_Admin badmin = new B_Admin();
        badmin.CheckIsLogin();
        blogTableBLL btbll = new blogTableBLL();
        B_User ubll = new B_User();
        int id = int.Parse(base.Request.QueryString["id"]);
        blogTable bt = btbll.GetUserBlog(id);
        this.namelaber.Text = bt.BlogName;
        //this.Label1.Text = ubll.GetUserByUserID(bt.UserID).UserName;
        this.Label2.Text = bt.BlogName;
        this.Label3.Text = bt.BlogContent;
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li class='active'>查看申请信息</li>");
    }
}
