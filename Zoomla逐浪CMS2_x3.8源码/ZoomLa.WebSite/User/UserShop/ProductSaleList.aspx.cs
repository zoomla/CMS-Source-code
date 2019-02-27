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

public partial class User_UserShop_ProductSaleList : Page
{
    B_OrderList orderbll = new B_OrderList();
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}