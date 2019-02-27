namespace Zoomla.Website.manage.Shop
{
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
    using ZoomLa.Components;

    public partial class DeliverList : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "Shop/ProductManage.aspx'>商城管理</a></li><li><a href='BankRollList.aspx'>明细记录</a></li><li class='active'>发退货明细</li>");
        }
    }
}