using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using System.Data;
using ZoomLa.Components;

namespace ZoomLaCMS.Manage.Shop.OtherOrder
{
    public partial class Travel_Orderinfo : CustomerPageAction
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
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>商城管理</li><li><a href=\"TravelOrder_Manager.aspx\">旅游订单管理</a></li><li>订单详情</li>");
            B_Admin badmin = new B_Admin();

            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "OrderList"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            int id = DataConverter.CLng(Request.QueryString["id"]);
            Label1.Text = "订 单 信 息";
            if (oll.FondOrder(id) == true)
            {
                M_OrderList orderinfo = oll.GetOrderListByid(id, 3);
                string OrderNo = orderinfo.OrderNo.ToString();
                Label1.Text = Label1.Text + "（订单编号：" + OrderNo + "）";

                lblmoney.Text = "￥" + orderinfo.Ordersamount.ToString();
                lblCo.Text = "￥" + orderinfo.Balance_price.ToString();
                lblIns.Text = orderinfo.Freight.ToString();
                if (orderinfo.OrderStatus == 1)
                {
                    Button4.Enabled = false;
                    Button5.Enabled = true;
                }
                else
                {
                    Button4.Enabled = true;
                    Button5.Enabled = false;
                }
                if (orderinfo.Paymentstatus == (int)M_OrderList.PayEnum.HasPayed)
                {
                    Button2.Enabled = false;
                }
                else
                {
                    Button2.Enabled = true;
                }

                if (orderinfo.Settle == 1)
                {
                    Button9.Enabled = false;
                }
                else
                {
                    Button9.Enabled = true;
                }

                if (orderinfo.Suspended == 1)
                {
                    Button13.Enabled = false;
                }
                else
                {
                    Button13.Enabled = true;
                }

                if (orderinfo.OrderStatus == 1)
                {
                    Button4.Enabled = false;
                }
                else
                {
                    Button4.Enabled = true;
                }


                if (orderinfo.Developedvotes == 1)
                {
                    Button11.Enabled = false;
                }
                else
                {
                    Button11.Enabled = true;
                }
                if (orderinfo.Signed == 1)
                {
                    Button7.Enabled = false;
                }
                else
                {
                    Button7.Enabled = true;
                }


                if (orderinfo.Aside == 0 && orderinfo.Suspended == 0)
                {
                    Button14.Enabled = false;
                }
                else
                {
                    Button14.Enabled = true;
                }

                if (orderinfo.Aside == 1)
                {
                    Button2.Enabled = false;
                    Button3.Enabled = false;
                    Button4.Enabled = false;
                    Button5.Enabled = false;
                    Button7.Enabled = false;
                    Button9.Enabled = false;
                    Button11.Enabled = false;
                    Button13.Enabled = false;
                    Button14.Enabled = false;

                    Button6.Enabled = false;

                }
                else
                {
                    Button6.Enabled = true;
                }


                int paymenst = orderinfo.Paymentstatus;
                int Settles = orderinfo.Settle;
                if (Settles == 1)
                {
                    Button2.Enabled = false;
                    Button3.Enabled = false;
                    Button4.Enabled = false;
                    Button5.Enabled = false;
                    Button6.Enabled = false;
                    Button7.Enabled = false;

                    Button11.Enabled = false;
                    Button12.Enabled = false;
                    Button13.Enabled = false;
                    Button14.Enabled = false;
                }
                M_PayPlat ppay = Pay.GetPayPlatByid(orderinfo.Payment);
                if (orderinfo != null && orderinfo.id > 0)
                {
                    this.lblEncName.Text = orderinfo.Rename.ToString();//联系人姓名
                    this.Email.Text = orderinfo.Email.ToString();//电子信箱
                    this.Phone.Text = orderinfo.Phone.ToString();//联系电话
                    this.lblZipCode.Text = orderinfo.ZipCode;
                }
                DataTable cplist = cpl.GetCartProOrderIDW(id);
                if (cplist != null && cplist.Rows.Count > 0)
                {
                    hlNo.Text = cplist.Rows[0]["proname"].ToString();
                    string siteUrl = SiteConfig.SiteInfo.SiteUrl;
                    if (siteUrl.LastIndexOf('\\') <= 0)
                    {
                        siteUrl = siteUrl + "\\";
                    }
                    hlNo.NavigateUrl = siteUrl + "Content.aspx?ID=" + cplist.Rows[0]["Bindpro"].ToString();
                    hlNo.Target = "_black";
                    //lblNo.Text = cplist.Rows[0]["proname"].ToString();
                    lblPrice.Text = cplist.Rows[0]["Shijia"].ToString();
                    lblInfo.Text = cplist.Rows[0]["PerID"].ToString();
                    lblDate.Text = cplist.Rows[0]["Addtime"].ToString();
                    lblStock.Text = cplist.Rows[0]["Pronum"].ToString();
                }

                procart.DataBind();
                DataTable newtable = cplist.DefaultView.ToTable(false, "Shijia", "Pronum");
                double allmoney = 0;
                for (int i = 0; i < newtable.Rows.Count; i++)
                {
                    allmoney = allmoney + DataConverter.CDouble(cplist.Rows[i]["Allmoney"]);
                }

                double jia = System.Math.Round(allmoney, 2);
                string jiage = jia.ToString();

                if (jiage.IndexOf(".") == -1)
                {
                    jiage = jiage + ".00";
                }
                double alljia = DataConverter.CDouble(jiage);
                alljia = System.Math.Round(alljia, 2);

                string tempalljia = alljia.ToString();

                if (tempalljia.IndexOf(".") == -1)
                {
                    tempalljia = tempalljia + ".00";
                }

                Label31.Text = tempalljia.ToString();

                double pp = orderinfo.Receivablesamount;
                pp = System.Math.Round(pp, 2);
                string temppp = pp.ToString();
                if (temppp.IndexOf(".") == -1)
                {
                    temppp = temppp + ".00";
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
        protected void Button4_Click(object sender, EventArgs e)//确认订单
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            string str = "OrderStatus=1";
            oll.UpOrderinfo(str, id);
            Response.Write("<script language=javascript>location.href='Travel_Orderinfo.aspx?id=" + id + "';</script>");
        }
        protected void Button7_Click(object sender, EventArgs e)//客户已签收
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            string str = "Signed=1";
            oll.UpOrderinfo(str, id);
            Response.Write("<script language=javascript>location.href='Travel_Orderinfo.aspx?id=" + id + "';</script>");

        }
        protected string Getprotype(string proid)
        {
            int id = DataConverter.CLng(proid);
            if (pll.GetproductByid(id).Priority == 1 && pll.GetproductByid(id).Propeid > 0)
            {
                return "<font color=green>[绑]</font>";
            }
            else
            {
                return "";
            }
        }
        protected void Button13_Click(object sender, EventArgs e)//暂停处理
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            string str = "Suspended=1";
            oll.UpOrderinfo(str, id);
            Response.Write("<script language=javascript>location.href='Travel_Orderinfo.aspx?id=" + id + "';</script>");


        }
        protected void Button2_Click(object sender, EventArgs e)//已经支付
        {

            int id = DataConverter.CLng(Request.QueryString["id"]);
            string str = "Paymentstatus=1,Receivablesamount=" + Label31.Text;
            oll.UpOrderinfo(str, id);
            Response.Write("<script language=javascript>location.href='Travel_Orderinfo.aspx?id=" + id + "';</script>");

        }
        protected void Button5_Click(object sender, EventArgs e)//取消确认
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            string str = "OrderStatus=0";
            oll.UpOrderinfo(str, id);
            Response.Write("<script language=javascript>location.href='Travel_Orderinfo.aspx?id=" + id + "';</script>");
        }
        protected void Button11_Click(object sender, EventArgs e)//开发票
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            string str = "Developedvotes=1";
            oll.UpOrderinfo(str, id);
            Response.Write("<script language=javascript>location.href='Travel_Orderinfo.aspx?id=" + id + "';</script>");

        }
        protected void Button14_Click(object sender, EventArgs e)//恢复正常
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            string str = "Aside=0,Suspended=0";
            oll.UpOrderinfo(str, id);
            Response.Write("<script language=javascript>location.href='Travel_Orderinfo.aspx?id=" + id + "';</script>");

        }
        protected void Button3_Click(object sender, EventArgs e)//发货
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);

            //历遍清单所有商品数量，查找库存///////////////////
            DataTable Unew = cpl.GetCartProOrderID(id); //获得详细清单列表
            M_OrderList orlist = oll.GetOrderListByid(id, 3);
            for (int s = 0; s < Unew.Rows.Count; s++)
            {
                int Onum = DataConverter.CLng(Unew.Rows[s]["Pronum"]);
                int Opid = DataConverter.CLng(Unew.Rows[s]["ProID"]);

                M_Product pdin = pll.GetproductByid(Opid);//获得商品信息

                if (pdin.JisuanFs == 0)
                {
                    int pstock = pdin.Stock - Onum;//库存结果,返回的商品数量
                    pll.ProUpStock(Opid, pstock);
                }
                M_Stock SData = new M_Stock();
                SData.id = 0;
                SData.proid = Opid;
                SData.stocktype = 1;
                SData.proname = pdin.Proname;
                SData.danju = "CK" + orlist.OrderNo.ToString();
                SData.adduser = orlist.Reuser.ToString();
                SData.addtime = DateTime.Now;
                SData.content = "订单:" + orlist.Reuser.ToString() + "发货";
                SData.Pronum = DataConverter.CLng(Unew.Rows[0]["Pronum"]);
                Sll.AddStock(SData);
            }






            string str = "StateLogistics=1";
            oll.UpOrderinfo(str, id);
            Response.Write("<script language=javascript>location.href='Travel_Orderinfo.aspx?id=" + id + "';</script>");

        }
        protected void Button6_Click(object sender, EventArgs e)//订单作废
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            string str = "Aside=1";
            oll.UpOrderinfo(str, id);
            Response.Write("<script language=javascript>location.href='Travel_Orderinfo.aspx?id=" + id + "';</script>");

        }
        protected void Button9_Click(object sender, EventArgs e)//结清订单
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);
            string str = "Settle=1";
            oll.UpOrderinfo(str, id);
            Response.Write("<script language=javascript>location.href='Travel_Orderinfo.aspx?id=" + id + "';</script>");

        }
        protected void Button12_Click(object sender, EventArgs e)//删除订单
        {
            int id = DataConverter.CLng(Request.QueryString["id"]);

            //历遍清单所有商品数量，查找库存///////////////////
            DataTable Unew = cpl.GetCartProOrderID(id); //获得详细清单列表

            for (int s = 0; s < Unew.Rows.Count; s++)
            {
                int Onum = DataConverter.CLng(Unew.Rows[s]["Pronum"]);
                int Opid = DataConverter.CLng(Unew.Rows[s]["ProID"]);

                M_Product pdin = pll.GetproductByid(Opid);//获得商品信息

                if (pdin.JisuanFs == 1)
                {
                    int pstock = pdin.Stock + Onum;//库存结果,返回的商品数量

                    pll.ProUpStock(Opid, pstock);
                }
            }

            oll.DeleteByID(id);
            Response.Write("<script language=javascript>alert('删除成功!');location.href='OrderList.aspx';</script>");
        }        
        protected void Button1_Click(object sender, EventArgs e)
        {
            int sid = DataConverter.CLng(Request.QueryString["id"]);
            string strOrder = oll.GetOrderListByid(sid, 3).OrderNo.ToString();
            DataTable dt = oll.Getsouchinfo(1, strOrder);
            if (dt != null && dt.Rows.Count > 0)
            {
                M_OrderList order = oll.GetOrderListByid(DataConverter.CLng(dt.Rows[0]["id"] + ""));
                order.Paymentstatus = (int)M_OrderList.PayEnum.HasPayed;
                oll.Update(order);
                Response.Redirect("?id=" + base.Request.QueryString["id"]);
            }
            if (dt != null)
            {
                dt.Dispose();
            }
        }
    }
}