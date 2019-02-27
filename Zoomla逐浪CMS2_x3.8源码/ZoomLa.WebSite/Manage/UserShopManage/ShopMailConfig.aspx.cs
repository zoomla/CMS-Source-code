using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;

public partial class manage_UserShopManage_ShopMailConfig : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ZoomLa.Common.function.AccessRulo();
        B_Admin ba = new B_Admin();
        ba.CheckIsLogin();
        if (!IsPostBack)
        {
            RadioButtonList1_SelectedIndexChanged(null, null);
        }
        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='StoreManage.aspx'>店铺管理</a><li class='active'>店铺邮件设置</li>");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //switch (RadioButtonList1.SelectedValue)
        //{
            //case "1":
            //    SiteConfig.BuyConfig.ShopBuildupSuccessMailTitle = txtTitle.Text;
            //    SiteConfig.BuyConfig.ShopBuildupSuccessMailContext = txtContext.Text;
            //    break;
            //case "2":
            //    SiteConfig.BuyConfig.ShopBuildupMailTitle = txtTitle.Text;
            //    SiteConfig.BuyConfig.ShopBuildupMailContext = txtContext.Text;
            //    break;
            //case "3":
            //    SiteConfig.BuyConfig.ShopCompeteSuccessMailTitle = txtTitle.Text;
            //    SiteConfig.BuyConfig.ShopCompeteSuccessMailContext = txtContext.Text;
            //    break;
            //case "4":
            //    SiteConfig.BuyConfig.ShopCompeteMailTitle = txtTitle.Text;
            //    SiteConfig.BuyConfig.ShopCompeteMailContext = txtContext.Text;
            //    break;
        //}
        SiteConfig.Update();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //switch (RadioButtonList1.SelectedValue)
        //{
            //case "1":
            //    txtTitle.Text = SiteConfig.BuyConfig.ShopBuildupSuccessMailTitle;
            //     txtContext.Text= SiteConfig.BuyConfig.ShopBuildupSuccessMailContext;
            //    break;
            //case "2":
            //    txtTitle.Text = SiteConfig.BuyConfig.ShopBuildupMailTitle;
            //    txtContext.Text = SiteConfig.BuyConfig.ShopBuildupMailContext;
            //    break;
            //case "3":
            //    txtTitle.Text = SiteConfig.BuyConfig.ShopCompeteSuccessMailTitle;
            //    txtContext.Text = SiteConfig.BuyConfig.ShopCompeteSuccessMailContext;
            //    break;
            //case "4":
            //    txtTitle.Text = SiteConfig.BuyConfig.ShopCompeteMailTitle;
            //    txtContext.Text = SiteConfig.BuyConfig.ShopCompeteMailContext;
            //    break;
        //}
    }
}
