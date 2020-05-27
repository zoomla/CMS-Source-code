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
using ZoomLa.Components;
using System.Net.Mail;

using ZoomLa.BLL.Shop;

namespace ZoomLaCMS.Manage.Shop
{
    public partial class Chickorder : CustomerPageAction
    {
        private B_Cart bll = new B_Cart();
        private B_Model bmode = new B_Model();
        private B_CartPro cpl = new B_CartPro();
        private B_Product pll = new B_Product();
        private B_OrderList oll = new B_OrderList();
        private B_PayPlat Pay = new B_PayPlat();
        protected B_Stock Sll = new B_Stock();
        protected B_InvtoType binvtype = new B_InvtoType();
        protected B_OrderBaseField borderbasefiled = new B_OrderBaseField();
        protected OrderCommon orderCom = new OrderCommon();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();

            if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "OrderList"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                MyBind();
            }
            Label1.Text = SiteConfig.SiteInfo.SiteName + "[客户签收单]";
            if (oll.FondOrder(Mid) == true)
            {
                M_OrderList orderinfo = oll.GetOrderListByid(Mid);
                string OrderNo = orderinfo.OrderNo.ToString();
                this.Reuser.Text = orderinfo.Reuser.ToString();
                int paymenst = orderinfo.Paymentstatus;
                int Settles = orderinfo.Settle;
                this.dingdanID.Text = OrderNo;//订单号
                this.addtime.Text = orderinfo.AddTime.ToString();//订单时间            
                this.Reusers.Text = orderinfo.Reuser.ToString();//订货人
                this.Jiedao.Text = orderinfo.Shengfen + " " + orderinfo.Jiedao;//联系地址             
                this.Deliverytime.Text = orderinfo.Deliverytime.ToString();//送货时间
                switch (orderinfo.Deliverytime)
                {
                    case 1:
                        this.Deliverytime.Text = "对送货时间没有特殊要求";
                        break;
                    case 2:
                        this.Deliverytime.Text = "双休日或者周一至周五的晚上送达";
                        break;
                    case 3:
                        this.Deliverytime.Text = "周一至周五的白天送达";
                        break;
                    default:
                        break;
                }
                this.Phone.Text = orderinfo.Phone.ToString();//联系电话           
                this.Mobile.Text = orderinfo.MobileNum;//手机         
                this.Ordermessage.Text = orderinfo.Ordermessage.ToString();//订货留言
                DataTable cplist = cpl.GetCartProOrderID(Mid);
                DataTable newtable = cplist.DefaultView.ToTable(false, "Shijia", "Pronum");
                double allmoney = 0;
                for (int i = 0; i < newtable.Rows.Count; i++)
                {
                    allmoney = allmoney + DataConverter.CDouble(cplist.Rows[i]["Allmoney"]);
                }
                AllMoney_L.Text = orderinfo.Ordersamount.ToString("f2");
                Fare_L.Text = orderinfo.Freight.ToString("f2");
            }
            else
            {
                function.WriteErrMsg("找不到购物车信息");
            }
            Call.SetBreadCrumb(Master, "<li>商城管理</li><li><a href='UserOrderinfo.aspx'>店铺订单</a></li><li>订单列表</li>");
        }
        public void MyBind()
        {
            DataTable cplist = cpl.GetCartProOrderID(Mid);
            procart.DataSource = cplist;
            procart.DataBind();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        public string GetShopUrl()
        {
            return orderCom.GetShopUrl(0, Convert.ToInt32(Eval("ProID")));
        }
        public void cartinfo_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rpColumnNews = (Repeater)e.Item.FindControl("Repeater1");
                DataRowView rowv = (DataRowView)e.Item.DataItem;
                string proid = rowv["Bindpro"].ToString();
                if (!string.IsNullOrEmpty(proid))
                {
                    DataTable ddinfos = pll.Souchprolist(proid);
                    rpColumnNews.DataSource = ddinfos.DefaultView;
                    rpColumnNews.DataBind();
                }
            }
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
        public string getProUnit(string proid)
        {
            int pid = DataConverter.CLng(proid);
            M_Product pinfo = pll.GetproductByid(pid);
            return pinfo.ProUnit.ToString();
        }
        public string GetLinPrice()
        {
            return OrderHelper.GetPriceStr(Convert.ToDouble(Eval("LinPrice")), Eval("LinPrice_Json").ToString());
        }
        public string GetPrice()
        {
            return OrderHelper.GetPriceStr(Convert.ToDouble(Eval("AllMoney")), Eval("AllMoney_Json").ToString());
        }
        public string qixian(string cid)
        {

            return "";
        }
        public string getjiage(string proclass, string type, string proid)
        {
            int pid = DataConverter.CLng(proid);
            int ptype = DataConverter.CLng(type);
            M_Product pinfo = pll.GetproductByid(pid);
            string jiage;
            jiage = "";
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
            if (proclass == "3")
            {
                jiage = pinfo.PointVal.ToString();
            }
            return jiage;
        }
    }
}