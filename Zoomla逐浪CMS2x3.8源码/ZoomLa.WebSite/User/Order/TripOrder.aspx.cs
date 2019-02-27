using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Data;
using ZoomLa.BLL.Shop;

public partial class User_PrintServer_TripOrder : System.Web.UI.Page
{
    private B_User buser = new B_User(); 
    private M_UserInfo muser = new M_UserInfo();
    protected B_OrderList orderbll = new B_OrderList();
    protected B_Cart cll = new B_Cart();
    B_CartPro cartproBll = new B_CartPro();
    protected B_Product bll = new B_Product();
    protected B_Product pro = new B_Product();
    B_CartPro bcart = new B_CartPro();
    public int sid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin();
        //this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;

        this.sid = DataConverter.CLng(Request.QueryString["id"]);

        M_UserInfo info = buser.GetLogin();
        string UserName = info.UserName;
        if (info.IsNull)
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["menu"]))
            {
                if (Request.QueryString["menu"] == "ViewOrderlist")
                {
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    Panel3.Visible = true;
                    GetViewOrderList();
                }
                else if (Request.QueryString["menu"].Equals("Aside")) 
                {
                    orderbll.DelByIDS_U(Request.QueryString["id"], buser.GetLogin().UserID, 1);
                    GetOrderinfo();
                }
                else
                {
                    Panel1.Visible = true;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                    GetOrderinfo();
                }
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

    private void GetViewOrderList()
    {
        Label1.Text = "乘客信息";
        Label10.Text = "旅游信息";
        sid = DataConverter.CLng(Request.QueryString["id"]);
        M_OrderList orderlist = orderbll.GetOrderListByid(sid);
        lblIns.Text = orderlist.Freight.ToString();
        
        DataTable orderlistW = bcart.GetCartProOrderIDW(sid);
        if (orderlistW != null && orderlistW.Rows.Count > 0)
        {
            EGV2.DataSource = orderlistW;
            EGV2.DataBind();
            preojiage.Text = orderlist.Ordersamount.ToString(); 
        }
        if (orderlist.Paymentstatus== (int)M_OrderList.PayEnum.HasPayed)
        {
            lb.Text = "成功付款";
            lb.Enabled = false;
        }
        else if (orderlist.OrderStatus == 1)
        {
            lb.Attributes.Add("onclick", "javascript:window.open('../PayOnline/Orderpay.aspx?OrderCode=" +orderlist.OrderNo + "')");
        }
        else
        {
            lb.Text = "待确认";
            lb.Enabled = false;
        }
    }

    private string GetContent(string con)
    {
        return DataConverter.CDouble(con).ToString("0:00");
    }

    protected void GetOrderinfo()
    {
        string UserName = buser.GetLogin().UserName;
        #region 获取页数
        int CPage;
        int temppage;
        if (Request.Form["DropDownList3"] != null)
        {
            temppage = Convert.ToInt32(Request.Form["DropDownList3"]);
        }
        else
        {
            temppage = Convert.ToInt32(Request.QueryString["CurrentPage"]);
        }
        CPage = temppage;
        if (CPage <= 0)
        {
            CPage = 1;
        }
        #endregion
        this.Label1.Text = "我的订单列表";
        DataTable olist = orderbll.GetUserlist(UserName, 0, 3);
        EGV1.DataSource = olist;
        EGV1.DataBind();
    }

    protected string getAction(string id)
    {
        int sid = DataConverter.CLng(Request.QueryString["id"]);//订单ID
        string restr = "";
        if (orderbll.GetOrderListByid(sid).Rename == buser.GetLogin().UserName)
        {
            M_OrderList olist = orderbll.GetOrderListByid(sid, 3);//订单信息
            if (olist.Paymentstatus == (int)M_OrderList.PayEnum.HasPayed && olist.StateLogistics == 1 && olist.OrderStatus == 1)//付款状态-物流状态-订单状态
            {
                restr = "<font color=red>成交OK</font>";
            }
            else if (olist.OrderStatus == 1 || olist.Paymentstatus == (int)M_OrderList.PayEnum.HasPayed)//付款状态-订单状态,只要确认一个就不能删除
            {
                restr = "<font color=green>已确认无权再操作</font>";
            }
            else
            {
                restr = "<a href=?menu=delprecartpro&id=" + id + " onclick=\"return confirm('不可恢复性删除数据,你确定将该数据删除吗？');\">删除</a>";
            }
        }
        return restr;
    }
    public string GetToPay() 
    {
        if (!Eval("Paymentstatus").ToString().Equals(((int)M_OrderList.PayEnum.HasPayed).ToString()))
        {
            return "<a href='/PayOnline/Orderpay.aspx?OrderCode="+Eval("OrderNo")+"' target='_blank' title='前往完成付款'>前往付款</a>";
        }
        return "";
    }
    public string GetOrderStatus()
    {
        string restr = "";
        int Paymentstatus = Convert.ToInt32(Eval("Paymentstatus"));
        int StateLogistics = Convert.ToInt32(Eval("StateLogistics"));
        int OrderStatus = Convert.ToInt32(Eval("OrderStatus"));
        int aside = Convert.ToInt32(Eval("aside"));
        if (Paymentstatus == (int)M_OrderList.PayEnum.HasPayed && StateLogistics == 1 && OrderStatus == 1)//付款状态-物流状态-订单状态
        {
            restr = "<font color=red>成交OK</font>";
        }
        else if (OrderStatus == 1 || Paymentstatus == (int)M_OrderList.PayEnum.HasPayed)//付款状态-订单状态,只要确认一个就不能删除
        {
            restr = "<font color=green>交易中</font>";
        }
        else if (aside == 1)
        {
            restr = "<font color=#CCCCCC>已作废</font>";
        }
        else
        {
            restr = "<a href=?menu=Aside&id=" + Eval("ID") + " onclick=\"return confirm('作废后此订单不可再交易,你确定将该订单删除吗？')\">删除</a>";
        }
        return restr;
    }

    public string Gettypes(string id)
    {
        int sid = DataConverter.CLng(id);
        int type = pro.GetproductByid(sid).Priority;
        if (type == 1) { return "<font color=green>[绑]</font>"; } else { return ""; }
    }
    public string formatcc(string moneys)
    {
        return OrderHelper.GetPriceStr(DataConverter.CDouble(moneys), Eval("AllMoney_Json").ToString());
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
            case 3:
                DataTable dt = orderbll.Getsouchinfo(zt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (OrderHelper.IsHasPayed(dt.Rows[0]))
                    {
                        return "<font color=blue>成功付款</font>";
                    }
                    else if (dt.Rows[0]["OrderStatus"].ToString() == "1")
                    {
                        return "<a href=\"#\" onclick=\"javascript:window.open('../PayOnline/Orderpay.aspx?OrderCode=" + zt + "') \"> <font color=red>等待支付</font></a>";
                    }
                    else
                    {
                        return "待确认";
                    }
                }
                break;
            default:
                outstr = "";
                break;

        }
        return outstr;
    }
    protected string ChkExtra(string str, string id)
    {
        DataTable dt = orderbll.Getsouchinfo(1, str);
        if (dt != null && dt.Rows.Count > 1)
        {
            if (OrderHelper.IsHasPayed(dt.Rows[0]))
            {
                return "<font color=blue>成功付款</font>";
            }
            else
            {
                return "<a href=\"#\" onclick=\"javascript:window.open('Shopfee/PayWindow.aspx?sid=" + id + "','','width=600px;heigth=350px') \"> <font color=red>等待支付</font></a>";
            }
        }
        else
        {
            return "<font color=blue>待确认</font>";
        }
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

    public string getshijiage(string id)
    {
        int Sid = DataConverter.CLng(id);
        //    string shijiage = "";
        M_OrderList orders = orderbll.GetOrderListByid(Sid, 3);//GetOrderbyOrderNo
        DataTable tb = orderbll.GetOrderbyOrderNo(orders.OrderNo);
        object s = tb.Compute("sum(ordersamount)", "orderno='" + orders.OrderNo + "'");
        return DataConverter.CDouble(s).ToString("F2");
    }

    public string getproimg(string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product pinfo = pro.GetproductByid(pid);
        string restring, type;

        restring = "";

        type = pinfo.Thumbnails;


        if (type != "") { type = type.ToLower(); }
        if (type != "" && (type.IndexOf(".gif") > -1 || type.IndexOf(".jpg") > -1 || type.IndexOf(".png") > -1))
        {
            restring = "<img src=../../" + type + " width=60 height=45>";
        }
        else
        {
            restring = "<img src=../../UploadFiles/nopic.gif width=60 height=45>";
        }
        return restring;

    }

    public string getProUnit(string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product pinfo = pro.GetproductByid(pid);
        return pinfo.ProUnit.ToString();
    }

    public string getjiage(string type, string proid)
    {
        int pid = DataConverter.CLng(proid);
        int ptype = DataConverter.CLng(type);
        M_Product pinfo = pro.GetproductByid(pid);
        string jiage;
        jiage = "";
        if (type == "1")
        {
            double jia = System.Math.Round(pinfo.ShiPrice, 3);
            jiage = jia.ToString();

        }
        else if (type == "2")
        {
            double jia = System.Math.Round(pinfo.LinPrice, 3);
            jiage = jia.ToString();
        }

        if (jiage.IndexOf(".") == -1)
        {
            jiage = jiage + ".00";
        }

        return jiage;
    }

    public string formatnewstype(string proid)
    {
        int pid = DataConverter.CLng(proid);
        M_Product pinfo = pro.GetproductByid(pid);
        int type = pinfo.ProClass;
        int newtype;
        string restring;
        restring = "";
        newtype = DataConverter.CLng(type.ToString());

        if (newtype == 2)
        {
            restring = "<font color=red>特价</font>";
        }
        else if (newtype == 1)
        {
            restring = "<font color=blue>正常</font>";
        }
        return restring;
    }

    public string getprojia(string cid)
    {
        int pid = DataConverter.CLng(cid);
        string jiage = "";
        M_CartPro minfo = cartproBll.GetCartProByid(pid);
        double jiag = System.Math.Round(minfo.AllMoney, 2);
        jiage = jiag.ToString();
        if (jiage.IndexOf(".") == -1)
        {
            jiage = jiage + ".00";
        }
        return jiage;
    }

    public string shijia(string jiage)
    {
        double jia;
        jia = DataConverter.CDouble(jiage);
        double jiag = System.Math.Round(jia, 2);
        string jj = jiag.ToString();

        if (jj.IndexOf(".") == -1)
        {
            jj = jj + ".00";
        }
        return jj;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        int sid = DataConverter.CLng(Request.QueryString["id"]);
        string strOrder = orderbll.GetOrderListByid(sid, 3).OrderNo.ToString();
        DataTable dt = orderbll.Getsouchinfo(1, strOrder);
        if (dt != null && dt.Rows.Count > 0)
        {
            M_OrderList order = orderbll.GetOrderListByid(DataConverter.CLng(dt.Rows[0]["id"] + ""));
            order.Paymentstatus = (int)M_OrderList.PayEnum.HasPayed;
            orderbll.Update(order);
        }
    }

    public string getIP()
    {
        string result = String.Empty;
        result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (null == result || result == String.Empty)
        {
            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        if (null == result || result == String.Empty)
        {
            result = HttpContext.Current.Request.UserHostAddress;
        }
        if (null == result || result == String.Empty)
        {
            return "0.0.0.0";
        }
        return result;

    }

    protected void txtPage_TextChanged(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        GetOrderinfo();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOrderinfo();
    }
    protected void Dels_Click(object sender, EventArgs e)
    {
        string ids = Request.Form["idchk"];
        orderbll.DelByIDS_U(ids, buser.GetLogin().UserID, 1);
        Response.Redirect(Request.RawUrl);
    }
}
