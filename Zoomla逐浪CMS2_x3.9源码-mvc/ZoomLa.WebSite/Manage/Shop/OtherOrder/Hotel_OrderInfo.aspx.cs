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

namespace ZoomLaCMS.Manage.Shop.OtherOrder
{
    public partial class Hotel_OrderInfo : CustomerPageAction
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
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>商城管理</li><li><a href=\"Hotel_Order_Manager.aspx\">酒店订单管理</a></li><li>订单详情</li>");
            B_Admin badmin = new B_Admin();
            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "OrderList"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            int id = DataConverter.CLng(Request.QueryString["id"]);
            if (oll.FondOrder(id) == true)
            {
                M_OrderList orderinfo = oll.GetOrderListByid(id);
                if (orderinfo != null && orderinfo.id > 0)
                {
                    string OrderNo = "";


                    int paymenst = 0;
                    int Settles = 0;


                    M_PayPlat ppay = Pay.GetPayPlatByid(orderinfo.Payment);

                    if (orderinfo != null && orderinfo.id > 0)
                    {
                        this.lblContact.Text = orderinfo.Rename.ToString();//联系人姓名
                        this.Email.Text = orderinfo.Email.ToString();//电子信箱
                        this.lblMobile.Text = orderinfo.Phone.ToString();//手机号码
                        this.lblZipCode.Text = orderinfo.ZipCode;
                        OrderNo = orderinfo.OrderNo.ToString();
                        paymenst = orderinfo.Paymentstatus;
                        Settles = orderinfo.Settle;

                    }

                    DataTable cplist = cpl.GetCartProOrderIDW(id);
                    DataTable newtable = null;
                    if (cplist != null && cplist.Rows.Count > 0)
                    {
                        procart.DataSource = cplist;
                        procart.DataBind();
                        lblPasser.Text = cplist.Rows[0]["Proinfo"].ToString();
                        newtable = cplist.DefaultView.ToTable(false, "Shijia", "Pronum");
                    }

                    double allmoney = 0;
                    if (newtable != null)
                    {
                        for (int i = 0; i < newtable.Rows.Count; i++)
                        {
                            allmoney = allmoney + DataConverter.CDouble(cplist.Rows[i]["Allmoney"]);
                        }
                    }
                    double jia = System.Math.Round(allmoney, 2);
                    string jiage = jia.ToString();

                    if (jiage.IndexOf(".") == -1)
                    {
                        jiage = jiage + ".00";
                    }

                    Label2.Text = jiage.ToString();
                    double alljia = DataConverter.CDouble(jiage);
                    alljia = System.Math.Round(alljia, 2);

                    string tempalljia = alljia.ToString();

                    if (tempalljia.IndexOf(".") == -1)
                    {
                        tempalljia = tempalljia + ".00";
                    }
                    double pp = orderinfo.Receivablesamount;
                    pp = System.Math.Round(pp, 2);
                    string temppp = pp.ToString();
                    if (temppp.IndexOf(".") == -1)
                    {
                        temppp = temppp + ".00";
                    }
                    DataTable dt = oll.Getsouchinfo(14, orderinfo.id + "");
                }
            }
            else
            {
                function.WriteErrMsg("找不到购物车信息");
            }
        }
        public string getproimg(string proid)
        {
            int pid = DataConverter.CLng(proid);
            M_Product pinfo = pll.GetproductByid(pid);
            string restring, type;
            restring = "";
            if (pinfo != null && pinfo.ID > 0)
            {
                type = pinfo.Thumbnails;

                if (!string.IsNullOrEmpty(type)) { type = type.ToLower(); }

                if (!string.IsNullOrEmpty(type) && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
                {
                    restring = "<img src=../../" + type + " width=60 height=45>";
                }
                else
                {
                    restring = "<img src=../../UploadFiles/nopic.gif width=60 height=45>";
                }
            }
            return restring;

        }
        public string getjiage(string type, string proid)
        {
            int pid = DataConverter.CLng(proid);
            int ptype = DataConverter.CLng(type);
            M_Product pinfo = pll.GetproductByid(pid);
            string jiage;
            jiage = "";
            if (pinfo != null && pinfo.ID > 0)
            {
                if (type == "1")
                {
                    double jia = System.Math.Round(pinfo.ShiPrice, 2);
                    jiage = jia.ToString();

                }
                else if (type == "2")
                {
                    double jia = System.Math.Round(pinfo.LinPrice, 2);
                    jiage = jia.ToString();
                }

                if (jiage.IndexOf(".") == -1)
                {
                    jiage = jiage + ".00";
                }
            }

            return jiage;
        }

        protected void procart_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                int sid = DataConverter.CLng(e.CommandArgument);
                M_CartPro pretempros = cpl.GetCartProByid(sid);
                if (pretempros != null && pretempros.ID > 0)
                {
                    int proid = pretempros.Orderlistid;
                    cpl.DeleteByPreID(sid);
                    Response.Redirect("OrderlistinfoDgou.aspx?id=" + proid + "");
                }
            }
        }
    }
}