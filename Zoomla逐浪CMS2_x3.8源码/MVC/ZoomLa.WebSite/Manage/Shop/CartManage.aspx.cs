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
using ZoomLa.BLL.Shop;
namespace ZoomLaCMS.Manage.Shop
{
    public partial class CartManage : CustomerPageAction
    {
        B_Cart bll = new B_Cart();
        B_CartPro bpro = new B_CartPro();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "CartManage"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                MyBind();
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='OrderList.aspx'>订单管理</a></li><li class='active'>购物车记录</li>");
            }
        }
        private void MyBind()
        {
            EGV.DataSource = bll.Sel();
            EGV.DataBind();
        }
        protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void EGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "del2":
                    bll.Del(Convert.ToInt32(e.CommandArgument));
                    break;
            }
            MyBind();
        }
        public string formatcc(string moneys)
        {
            return OrderHelper.GetPriceStr(DataConverter.CDouble(moneys), Eval("AllMoney_Json").ToString());
        }

        protected void BatDel_Click(object sender, EventArgs e)
        {
            string ids = Request.Form["idchk"];
            if (!string.IsNullOrEmpty(ids))
            {
                bll.DelByids(ids);
                function.WriteSuccessMsg("删除成功");
            }
            MyBind();
        }
    }
}