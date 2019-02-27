using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manage_Shop_profile_StatisticsBriefing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "shop/ProductManage.aspx'>商城管理</a></li><li><a href='StatisticsBriefing.aspx'>推广返利</a></li><li>统计简报</li>");
    }
}