namespace ZoomLaCMS.Plugins.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Site;
    using ZoomLa.Common;
    using ZoomLa.Components;
    using ZoomLa.Model;
    using ZoomLa.SQLDAL;

    public partial class ViewHave : System.Web.UI.Page
    {
        //订单
        protected B_OrderList OCl = new B_OrderList();
        protected M_OrderList Odata = new M_OrderList();
        protected OrderCommon orderCom = new OrderCommon();
        //购物车
        protected B_CartPro bcart = new B_CartPro();
        protected B_IDC_DomainOrder orderBll = new B_IDC_DomainOrder();
        protected B_User buser = new B_User();

        protected B_Product proBll = new B_Product();
        protected B_Site_SiteList siteBll = new B_Site_SiteList();
        protected M_Product proModel = new M_Product();
        public int Filter { get { return DataConvert.CLng(Request.QueryString["filter"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("/User/Order/OrderList");
        }
    }
}