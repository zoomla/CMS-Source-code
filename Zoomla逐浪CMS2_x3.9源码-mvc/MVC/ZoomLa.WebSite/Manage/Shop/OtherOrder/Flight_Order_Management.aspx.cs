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

namespace ZoomLaCMS.Manage.Shop.OtherOrder
{
    public partial class Flight_Order_Management : CustomerPageAction
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
                Bind();
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../../Shop/ProductManage.aspx'>商城管理</a></li><li><a href='../../Shop/OrderList.aspx'>订单管理</a></li><li class='active'>航班订单管理</li>");
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

        private void Bind()
        {
            int CPage;
            int temppage;

            if (Request.Form["DropDownList1"] != null)
            {
                temppage = DataConverter.CLng(Request.Form["DropDownList1"]);
            }
            else
            {
                temppage = DataConverter.CLng(Request.QueryString["CurrentPage"]);
            }
            CPage = temppage;
            if (CPage <= 0)
            {
                CPage = 1;
            }


            DataTable Cll;
            int getquicksou = 0;
            int getquicksoutemp = DataConverter.CLng(Request.Form["quicksouch"]);
            int getquicksouv2 = DataConverter.CLng(Request.QueryString["quicksouch"]);

            if (getquicksoutemp > 0 && getquicksouv2 == 0)
            {
                getquicksou = getquicksoutemp;
            }
            else if (getquicksoutemp == 0 && getquicksouv2 > 0)
            {
                getquicksou = getquicksouv2;
            }
            else
            {
                getquicksou = 0;
            }


            //int getquicksou = DataConverter.CLng(Request.QueryString["quicksouch"]);
            //int getquicksou = DataConverter.CLng(Request.Form["quicksouch"]);

            int soutype = DataConverter.CLng(Request.QueryString["souchtable"]);
            string key = function.HtmlEncode(Request.QueryString["souchkey"]);
            string menu = Request.QueryString["menu"];
            this.souchtable.Text = soutype.ToString();
            quicksouch.Text = getquicksou.ToString();
            switch (menu == "souch" && key != "")
            {
                case true:
                    this.souchkey.Text = key;
                    Cll = bll.Getsouchinfo(soutype, key, 2);
                    break;
                case false:
                    if (getquicksou > 0)
                    {
                        Cll = bll.GetOrderListtype(getquicksou, 2);
                    }
                    else
                    {
                        Cll = bll.GetOrderListByOrderType(2);
                    }
                    break;
                default:
                    Cll = bll.GetOrderListByOrderType(2);
                    break;
            }
            PagedDataSource cc = new PagedDataSource();
            cc.DataSource = Cll.DefaultView;
            cc.AllowPaging = true;
            cc.PageSize = 10;
            if (!string.IsNullOrEmpty(Request.QueryString["txtPage"]))
            {
                cc.PageSize = DataConverter.CLng(Request.QueryString["txtPage"]);
            }
            if (Request.Form["txtPage"] != null && Request.Form["txtPage"] != "")
            {
                cc.PageSize = Convert.ToInt32(Request.Form["txtPage"]);
            }
            cc.CurrentPageIndex = CPage - 1;
            Orderlisttable.DataSource = cc;
            Orderlisttable.DataBind();

            Allnum.Text = Cll.DefaultView.Count.ToString();
            int thispagenull = cc.PageCount;//总页数
            int CurrentPage = cc.CurrentPageIndex;
            int nextpagenum = CPage - 1;//上一页
            int downpagenum = CPage + 1;//下一页
            int Endpagenum = thispagenull;
            if (thispagenull <= CPage)
            {
                downpagenum = thispagenull;
                Downpage.Enabled = false;
            }
            else
            {
                Downpage.Enabled = true;
            }
            if (nextpagenum <= 0)
            {
                nextpagenum = 0;
                Nextpage.Enabled = false;
            }
            else
            {
                Nextpage.Enabled = true;
            }

            double ddmoney;
            int num;
            ddmoney = 0.00;
            num = 0;
            string tempstrs = "";

            //本次查询
            num = Cll.Rows.Count;

            for (int i = 0; i < num; i++)
            {
                ddmoney = ddmoney + Convert.ToDouble(getshijiage(Cll.Rows[i]["id"].ToString()));
            }

            tempstrs = ddmoney.ToString();

            if (tempstrs.IndexOf(".") == -1)
            {
                tempstrs = ddmoney + ".00";
            }

            thisall.Text = tempstrs.ToString();
            //所有总数：
            try
            {
                //int all = bll.Getmoney();
                //strallnum = all.ToString();
                //if (strallnum.IndexOf(".") == -1)
                //{
                //    strallnum = strallnum + ".00";
                //}
                DataTable allorders = bll.GetOrderListByOrderType(2);
                double tempss = 0;

                for (int i = 0; i < allorders.Rows.Count; i++)
                {
                    tempss = tempss + Convert.ToDouble(getshijiage(allorders.Rows[i]["id"].ToString()));
                }

                tempstrs = tempss.ToString();

                if (tempstrs.IndexOf(".") == -1)
                {
                    tempstrs = tempstrs + ".00";
                }

                allall.Text = tempstrs.ToString();

            }
            catch { }



            Toppage.Text = "<a href=?quicksouch=" + getquicksou + "&txtPage=" + txtPage.Text + "&souchtable=" + soutype + "&souchkey=" + key + " &menu=" + menu + "&Currentpage=0>首页</a>";
            Nextpage.Text = "<a href=?quicksouch=" + getquicksou + "&txtPage=" + txtPage.Text + "&souchtable=" + soutype + "&souchkey=" + key + "&menu=" + menu + "&Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
            Downpage.Text = "<a href=?quicksouch=" + getquicksou + "&txtPage=" + txtPage.Text + "&souchtable=" + soutype + "&souchkey=" + key + "&menu=" + menu + "&Currentpage=" + downpagenum.ToString() + ">下一页</a>";
            Endpage.Text = "<a href=?quicksouch=" + getquicksou + "&txtPage=" + txtPage.Text + "&souchtable=" + soutype + " &souchkey=" + key + "&menu=" + menu + "&Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
            PageSize.Text = thispagenull.ToString();
            Nowpage.Text = CPage.ToString();
            this.txtPage.Text = cc.PageSize.ToString();
            DropDownList1.Items.Clear();
            for (int i = 1; i <= thispagenull; i++)
            {
                DropDownList1.Items.Add(i.ToString());
            }

        }

        public string formatcs(string money)
        {
            string outstr;
            double doumoney, tempmoney;
            doumoney = DataConverter.CDouble(money);
            tempmoney = System.Math.Round(doumoney, 2);
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
                sss = "<a href=\"Flight_OrderInfo.aspx?id=" + cdd.id + "\"><font color=#cccccc>" + cdd.OrderNo + "</font></a>";
            }
            else
            {
                sss = "<a href=\"Flight_OrderInfo.aspx?id=" + cdd.id + "\">" + cdd.OrderNo + "</a>";
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
                tempstr = "<input name=\"Item\"  disabled=\"disabled\" type=\"checkbox\"/>";
            }
            else
            {
                tempstr = "<input name=\"Item\" type=\"checkbox\" value=\"" + id + "\"/>";
            }
            return tempstr;
        }

        #endregion
        protected void txtPage_TextChanged(object sender, EventArgs e)
        {
            ViewState["page"] = "1";
            Bind();
        }

        //搜索
        protected void souchok_Click(object sender, EventArgs e)
        {
            string key = function.HtmlEncode(Request.Form["souchkey"]);
            string soutype = Request.Form["souchtable"];
            if (key != "")
            {
                Response.Redirect("Hotel_Order_Manager.aspx?menu=souch&souchtable=" + soutype + "&souchkey=" + key + "");
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bind();
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            string CID = Request.Form["Item"];//订单ID列表
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
                bll.Delorderlist(CID);
                Response.Write("<script language=javascript>alert('删除成功!');location.href='Flight_Order_Management.aspx'</script>");
            }
            else
            {
                Response.Write("<script language=javascript>alert('删除失败!');location.href='Flight_Order_Management.aspx'</script>");
            }
        }
    }
}