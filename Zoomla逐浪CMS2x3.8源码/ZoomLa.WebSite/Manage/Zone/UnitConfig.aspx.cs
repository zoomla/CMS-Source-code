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
using ZoomLa.Components;
using ZoomLa.Model;

public partial class manage_Zone_UnitConfig : CustomerPageAction
{
    protected B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        badmin.CheckIsLogin();
        if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "ZoneInfo"))
        {
            function.WriteErrMsg("没有权限进行此项操作");
        }
        if (!this.IsPostBack)
        {
            this.TextBox1.Text = SiteConfig.ShopConfig.Unit;
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li><a href='ZoneManage.aspx'>会员空间管理</a></li><li class='active'>空间信息配置</li>");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        function.WriteSuccessMsg("配置保存成功!");
    }
}
