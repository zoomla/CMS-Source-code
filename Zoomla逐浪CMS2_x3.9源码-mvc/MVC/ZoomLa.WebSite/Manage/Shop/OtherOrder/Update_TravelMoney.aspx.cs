using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
namespace ZoomLaCMS.Manage.Shop.OtherOrder
{
    public partial class Update_TravelMoney : CustomerPageAction
    {
        private B_CartPro cpl = new B_CartPro();
        private B_OrderList oll = new B_OrderList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "OrderList"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                int id = DataConverter.CLng(Request.QueryString["id"]);
                ViewState["id"] = id;
                if (oll.FondOrder(id) == true)
                {
                    M_OrderList orderinfo = oll.GetOrderListByid(id, 3);
                    if (orderinfo != null && orderinfo.id > 0)
                    {
                        DataTable cplist = cpl.GetCartProOrderIDW(id);
                        if (cplist != null && cplist.Rows.Count > 0)
                        {
                            hlNo.Text = cplist.Rows[0]["proname"].ToString();
                            lblPrice.Text = cplist.Rows[0]["Shijia"].ToString();
                            lblInfo.Text = cplist.Rows[0]["PerID"].ToString();
                            lblStock.Text = cplist.Rows[0]["Pronum"].ToString();
                        }
                        txtPrice.Text = getshijiage(id);
                    }
                }
            }
        }

        public string getshijiage(int Sid)
        {
            M_OrderList orders = oll.GetOrderListByid(Sid);
            DataTable tb = oll.GetOrderbyOrderNo(orders.OrderNo);
            object s = tb.Compute("sum(ordersamount)", "orderno='" + orders.OrderNo + "'");
            return DataConverter.CDouble(s).ToString("F2");
        }

        //修改价格
        protected void update_Click(object sender, EventArgs e)
        {
            int id = DataConverter.CLng(ViewState["id"]);
            M_OrderList orderinfo = oll.GetOrderListByid(id, 3);
            if (orderinfo != null && orderinfo.id > 0)
            {
                orderinfo.Balance_price = orderinfo.Ordersamount - DataConverter.CDouble(txtPrice.Text);
                orderinfo.Ordersamount = DataConverter.CDouble(txtPrice.Text);
                bool result = oll.Update(orderinfo);
                if (result)
                {
                    function.WriteSuccessMsg("修改成功!");
                }
                else
                {
                    function.WriteErrMsg("修改失败!");
                }
            }
        }
    }
}