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
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.API;
using ZoomLa.DZNT;
using System.Xml;
using ZoomLa.BLL.Shop;

public partial class User_PrintServer_FeightOrder : System.Web.UI.Page
{
    #region shuxing
    private B_User buser = new B_User();
    protected B_OrderList oll = new B_OrderList();
    protected B_CartPro pll = new B_CartPro();
    protected B_Product bll = new B_Product();
    protected B_Product pro = new B_Product(); 
    public int sid = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin();
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
        this.TableTitle_L.Text = "乘客信息";
        Label10.Text = "航班信息";
        sid = DataConverter.CLng(Request.QueryString["id"]);
        M_OrderList orderlist = oll.GetOrderListByid(sid);
        lblIns.Text = orderlist.Freight.ToString();
        DataTable orderlistW = pll.GetCartProOrderIDW(sid);
        if (orderlistW != null && orderlistW.Rows.Count > 0)
        {
            EGV2.DataSource = orderlistW;
            EGV2.DataBind();
            preojiage.Text = orderlist.Ordersamount.ToString();
        }
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

        this.TableTitle_L.Text = "我的订单列表";

        DataTable olist = oll.GetUserlist(UserName, 0, 2);
        DataTable olist1 = oll.GetUserlist(UserName, 0, 2);
        if (olist != null && olist.Rows.Count > 0)
        {
            olist1.Rows.Clear();
            foreach (DataRow dr in olist.Rows)
            {
                if (OrderHelper.IsHasPayed(dr))
                {
                    olist1.Rows.Add(dr.ItemArray);
                }
            }
        }
        EGV1.DataSource = olist1;
        EGV1.DataBind();
   


    }

    public string getbotton(string id)
    {
        string restr = "";
        int sid = DataConverter.CLng(id);
        if (oll.GetOrderListByid(sid).Rename == buser.GetLogin().UserName)
        {
            M_OrderList olist = oll.GetOrderListByid(sid, 2);
            if (olist.Paymentstatus == (int)M_OrderList.PayEnum.HasPayed)//付款状态-物流状态-订单状态
            {
                restr = "<font color=red>交易OK</font>";
            }
            else if (olist.OrderStatus == 1 || olist.Paymentstatus == (int)M_OrderList.PayEnum.HasPayed)//付款状态-订单状态,只要确认一个就不能删除
            {
                restr = "<font color=green>交易OK</font>";
            }
            else if (olist.Aside == 1)
            {
                restr = "<font color=#CCCCCC>已作废</font>";
            }
            else
            {
                restr = "<a href=?menu=Aside&id=" + sid + " onclick=\"return confirm('作废后此订单不可再交易,你确定将该订单作废吗？')\">作废</a>";
            }
        }
        return restr;
    }

    public string formatcc(string moneys)
    {
        return OrderHelper.GetPriceStr(DataConverter.CDouble(moneys), Eval("AllMoney_Json").ToString());
    }

   
    public string getshijiage(string id)
    {
        int Sid = DataConverter.CLng(id);
        //    string shijiage = "";
        M_OrderList orders = oll.GetOrderListByid(Sid, 2);//GetOrderbyOrderNo
        DataTable tb = oll.GetOrderbyOrderNo(orders.OrderNo, 2);
        object s = tb.Compute("sum(ordersamount)", "orderno='" + orders.OrderNo + "'");
        return DataConverter.CDouble(s).ToString("F2");
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
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            oll.DelByIDS_U(Request.Form["idchk"], buser.GetLogin().UserID, 1);
            GetOrderinfo();
        }
    }
}

