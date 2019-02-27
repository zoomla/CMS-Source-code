using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;

namespace ZoomLaCMS.Manage.Shop._3D
{
    public partial class _3DManage : System.Web.UI.Page
    {
        private B_3DShop shopBll = new B_3DShop();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "ProductManage.aspx'>商城管理</a></li><li><a href='3DManage.aspx'>3D店铺管理</a>[<a href='AddShop.aspx'>添加店铺</a>]</li>");
            }
        }
        private void MyBind()
        {
            DataTable dt = new DataTable();
            dt = shopBll.Sel();
            EGV.DataSource = dt;
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "del2":
                    shopBll.Del(Convert.ToInt32(e.CommandArgument));
                    break;
            }
            MyBind();
        }
    }
}