using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using System.Data;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data.SqlClient;
public partial class manage_Shop_PromotionBalance : CustomerPageAction
{
    B_ArticlePromotion bap = new B_ArticlePromotion();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='ProductManage.aspx'>商城管理</a></li><li class='active'>商品推广结算</li>");
        }
    }
    protected void MyBind()
    {
        DataTable dt = bap.SelWithCart();
        EGV.DataSource = dt;
        EGV.DataBind();
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}