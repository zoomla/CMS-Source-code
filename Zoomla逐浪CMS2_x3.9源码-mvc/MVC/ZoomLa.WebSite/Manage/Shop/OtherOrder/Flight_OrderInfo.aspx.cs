using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
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
namespace ZoomLaCMS.Manage.Shop.OtherOrder
{
    public partial class Flight_OrderInfo : CustomerPageAction
    {
        private B_Cart bll = new B_Cart();
        private B_Node bNode = new B_Node();
        private B_Model bmode = new B_Model();
        private B_CartPro cpl = new B_CartPro();
        private B_Product pll = new B_Product();
        private B_OrderList oll = new B_OrderList();
        private B_PayPlat Pay = new B_PayPlat();
        protected B_Stock Sll = new B_Stock();

        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>商城管理</li><li><a href=\"Hotel_Order_Manager.aspx\">航班订单管理</a></li><li>订单详情</li>");
            B_Admin badmin = new B_Admin();

            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "OrderList"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            int id = DataConverter.CLng(Request.QueryString["id"]);
            Label1.Text = "订 单 信 息";
            if (oll.FondOrder(id) == true)
            {
                M_OrderList orderinfo = oll.GetOrderListByid(id);
                string OrderNo = orderinfo.OrderNo.ToString();
                Label1.Text = Label1.Text + "（订单编号：" + OrderNo + "）";

                M_PayPlat ppay = Pay.GetPayPlatByid(orderinfo.Payment);
                if (orderinfo != null && orderinfo.id > 0)
                {
                    this.lblEncName.Text = orderinfo.Rename.ToString();//联系人姓名
                    this.Email.Text = orderinfo.Email.ToString();//电子信箱
                    this.Phone.Text = orderinfo.Phone.ToString();//联系电话
                    this.lblZipCode.Text = orderinfo.ZipCode;
                    lblIns.Text = orderinfo.Freight.ToString();
                }
                DataTable cplist = cpl.GetCartProOrderIDW(id);
                if (cplist != null && cplist.Rows.Count > 0)
                {
                    lblNo.Text = cplist.Rows[0]["proname"].ToString();
                    lblPrice.Text = cplist.Rows[0]["Shijia"].ToString();
                    lblInfo.Text = cplist.Rows[0]["Proinfo"].ToString();
                    lblDate.Text = cplist.Rows[0]["Addtime"].ToString();
                }

            }
        }

        #region private function

        public string GetCreType(string type)
        {
            switch (type)
            {
                case "0":
                    return "身份证";
                case "1":
                    return "护照";
                case "2":
                    return "军人证";
                case "3":
                    return "港澳通行证";
                case "4":
                    return "台胞证";
                case "5":
                    return "回乡证";
                case "6":
                    return "国际海员证";
                case "7":
                    return "外国人永久居留证";
                case "8":
                    return "旅行证";
                case "9":
                    return "其他";
            }
            return "";
        }

        public string GetCreID(string CreID, int type)
        {
            string value = "";
            if (!string.IsNullOrEmpty(CreID))
            {
                string[] va = CreID.Split('|');
                if (va != null && va.Length > 0)
                {
                    string[] CreId = va[0].Split(':');
                    if (CreId != null && CreId.Length > 1)
                    {
                        value = CreId[type];
                    }
                }
            }
            return value;

        }
        #endregion

        protected void procart_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int sid = DataConverter.CLng(e.CommandArgument);
                M_CartPro pretempros = cpl.GetCartProByid(sid);
                int proid = pretempros.Orderlistid;
                cpl.DeleteByPreID(sid);
                Response.Redirect("OrderlistinfoDgou.aspx?id=" + proid + "");
            }
        }
    }
}