using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL.Shop;

namespace ZoomLaCMS.Manage.Shop.OtherOrder
{
    public partial class DomainOrder : System.Web.UI.Page
    {
        private B_OrderList bll = new B_OrderList();
        private B_Node bNode = new B_Node();
        private B_Model bmode = new B_Model();
        private B_Product Pll = new B_Product();
        private B_CartPro Cll = new B_CartPro();
        private B_MoneyManage mm = new B_MoneyManage();

        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            B_Admin badmin = new B_Admin();

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["OrderType"]))
                {
                    MyBind(Convert.ToInt32(Request.QueryString["OrderType"]));
                }
                else
                {
                    MyBind();
                }
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../../Shop/ProductManage.aspx'>商城管理</a></li><li><a href='../../Shop/OrderList.aspx'>订单管理</a></li><li class='active'>域名订单管理</li>");
        }
        public void MyBind(int orderType = 5)
        {
            DataTable dt = new DataTable();
            int getquicksou = 0;
            int soutype = DataConverter.CLng(Request.QueryString["souchtable"]);
            string key = HttpUtility.HtmlEncode(Request.QueryString["souchkey"]);
            string menu = Request.QueryString["menu"];
            this.souchtable.Text = soutype.ToString();
            switch (menu == "souch" && key != "")
            {
                case true:
                    this.souchkey.Text = key;
                    dt = bll.Getsouchinfo(soutype, key, orderType);
                    break;
                case false:
                    if (getquicksou > 0)
                    {
                        dt = bll.GetOrderListtype(getquicksou, orderType);
                    }
                    else
                    {
                        dt = bll.GetOrderListByOrderType(orderType);
                    }
                    break;
                default:
                    dt = bll.GetOrderListByOrderType(orderType);
                    break;
            }
            Order_RPT.DataSource = dt;
            Order_RPT.DataBind();
        }

        #region 内部方法
        public string GetUsers(string userId)
        {
            B_User buser = new B_User();
            M_UserInfo info = buser.GetUserByUserID(DataConverter.CLng(userId));
            if (info != null && info.UserID > 0)
            {
                return info.UserName;
            }
            else
            {
                return "游客";
            }
        }
        public string formatcs(string money)
        {
            string outstr;
            double doumoney, tempmoney;
            doumoney = DataConverter.CDouble(money);
            tempmoney = System.Math.Round(doumoney, 3);
            outstr = tempmoney.ToString();
            if (outstr.IndexOf(".") == -1)
            {
                outstr = outstr + ".00";
            }
            return outstr;
        }
        public string getMoney_sign(string money_code)
        {
            return "";
        }
        public string formatzt(string zt, string selects)
        {
            string outstr;
            int zts, intselect;
            outstr = "";
            zts = DataConverter.CLng(zt);
            intselect = DataConverter.CLng(selects);
            switch (intselect)
            {
                case 0:
                    outstr = OrderHelper.GetOrderStatus(zts);
                    break;
                case 1:
                    outstr = OrderHelper.GetPayStatus(zts);
                    break;
                case 2:
                    outstr = OrderHelper.GetExpStatus(zts);
                    break;
                default:
                    outstr = "";
                    break;
            }
            return outstr;
        }
        public string getorderno(string id)
        {
            string sss = "";
            int sid = DataConverter.CLng(id);
            M_OrderList cdd = bll.GetOrderListByid(sid);

            int aside = cdd.Aside;
            if (aside == 1)
            {
                sss = "<a href=\"../Orderlistinfo.aspx?id=" + cdd.id + "\"><font color=#cccccc>" + cdd.OrderNo + "</font></a>";
            }
            else
            {
                sss = "<a href=\"../Orderlistinfo.aspx?id=" + cdd.id + "\">" + cdd.OrderNo + "</a>";
            }
            return sss;
        }

        public string getshijiage(string id)
        {
            int Sid = DataConverter.CLng(id);
            //string shijiage = "";
            M_OrderList orders = bll.GetOrderListByid(Sid);
            DataTable tb = bll.GetOrderbyOrderNo(orders.OrderNo);
            object s = tb.Compute("sum(ordersamount)", "orderno='" + orders.OrderNo + "'");
            return DataConverter.CDouble(s).ToString("F2");
        }

        public string fapiao(string cc)
        {
            int cc1;
            string dd1;
            dd1 = "";
            cc1 = DataConverter.CLng(cc);
            if (cc1 == 0)
            {
                dd1 = "<font color=red>×</font>";
            }
            else if (cc1 == 1)
            {
                dd1 = "<font color=blue>√</font>";
            }
            return dd1;
        }

        public string Getclickbotton(string id)
        {
            int tempid = DataConverter.CLng(id);

            M_OrderList rs = bll.GetOrderListByid(tempid);
            string tempstr = "";
            double c1 = rs.Ordersamount;//订单金额
            double c2 = rs.Receivablesamount;//收款金额
            int paymenst = rs.Paymentstatus;
            int Settles = rs.Settle;
            if (paymenst == 1 || Settles == 1)
            {
                tempstr = "<input name=\"idchk\"  disabled=\"disabled\" type=\"checkbox\"/>";
            }
            else
            {
                tempstr = "<input name=\"idchk\" type=\"checkbox\" value=\"" + id + "\"/>";
            }
            return tempstr;
        }

        #endregion
        //搜索
        protected void souchok_Click(object sender, EventArgs e)
        {
            string key = HttpUtility.HtmlEncode(souchkey.Text);
            string soutype = Request.Form["souchtable"];
            string orderType = Request.QueryString["OrderType"];
            if (key != "")
            {
                Response.Redirect("TravelOrder_Manager.aspx?menu=souch&souchtable=" + soutype + "&souchkey=" + key + "&OrderType=" + orderType);
            }
        }
        public string GetUser(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "游客";
            }
            else
            {
                return name;
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string CID = Request.Form["idchk"];//订单ID列表
            if (!String.IsNullOrEmpty(CID))
            {
                DataTable Ode = bll.GetOrderbyOrderlist(CID);//获得订单列表

                int odcount = Ode.Rows.Count;

                for (int p = 0; p < odcount; p++)
                {
                    int CartproOrderid = DataConverter.CLng(Ode.Rows[p]["id"]); //订单ID

                    //历遍清单所有商品数量，查找库存///////////////////
                    DataTable Unew = Cll.GetCartProOrderID(CartproOrderid); //获得详细清单列表

                    for (int s = 0; s < Unew.Rows.Count; s++)
                    {
                        int Onum = DataConverter.CLng(Unew.Rows[s]["Pronum"]);
                        int Opid = DataConverter.CLng(Unew.Rows[s]["ProID"]);

                        M_Product pdin = Pll.GetproductByid(Opid);//获得商品信息

                        if (pdin.JisuanFs == 1)
                        {
                            int pstock = pdin.Stock + Onum;//库存结果,返回的商品数量
                            Pll.ProUpStock(Opid, pstock);
                        }
                    }
                }
                bll.DelByIDS(CID);
                Response.Write("<script language=javascript>alert('删除成功!');location.href='TravelOrder_Manager.aspx'</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('删除失败!');location.href='TravelOrder_Manager.aspx'</script>");
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.Form["Item"])) return;
            B_OrderList orderBll = new B_OrderList();
            string[] idArr = Request.Form["Item"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < idArr.Length; i++)
            {
                orderBll.UpOrderinfo("OrderStatus=99", Convert.ToInt32(idArr[i]));
            }
            MyBind();
        }
    }
}