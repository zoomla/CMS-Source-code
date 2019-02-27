using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop.Order;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Shop.Order;
using ZoomLa.SQLDAL;
/*
 1,打印机--添加  230mm*127mm打印格式,并取消页眉页角+边距设为无
 2,加载对应快递单图片,并设置好打印坐标(加载的图片带左右边)
 */
namespace ZoomLaCMS.Manage.Shop.Addon
{
    public partial class ExpPrint : System.Web.UI.Page
    {
        B_OrderList orderBll = new B_OrderList();
        B_Order_ExpSender senderBll = new B_Order_ExpSender();
        private int Mid { get { return DataConvert.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_Admin.CheckIsLogged(Request.RawUrl);
                M_OrderList orderMod = orderBll.SelReturnModel(Mid);
                if (orderMod == null) { function.WriteErrMsg("订单不存在"); }
                Order_Hid.Value = JsonConvert.SerializeObject(orderMod);
                SendMan_DP.DataSource = senderBll.Sel();
                SendMan_DP.DataBind();
                MyBind();
            }
        }
        private void MyBind()
        {
            string html = SafeSC.ReadFileStr("/manage/shop/addon/exps/" + ExpTlp_DP.SelectedValue + ".html");
            maindiv.InnerHtml = html;
            M_Order_ExpSender senderMod = senderBll.SelReturnModel(DataConvert.CLng(SendMan_DP.SelectedValue));
            if (senderMod != null)
            {
                SendMan_Hid.Value = JsonConvert.SerializeObject(senderMod);
            }
           
        }
        protected void SendMan_DP_TextChanged(object sender, EventArgs e)
        {
            MyBind();
        }
        protected void ExpTlp_DP_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyBind();
        }
    }
}