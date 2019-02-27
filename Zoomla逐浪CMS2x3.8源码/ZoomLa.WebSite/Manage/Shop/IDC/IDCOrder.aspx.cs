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
using ZoomLa.SQLDAL;
using System.Text.RegularExpressions;
using System.Text;
using ZoomLa.Safe;


public partial class Manage_Shop_IDC_IDCOrder : System.Web.UI.Page
{
    B_OrderList orderBll = new B_OrderList();
    B_Order_IDC idcBll = new B_Order_IDC();
    B_User buser = new B_User();
    OrderCommon orderCom = new OrderCommon();
    B_Admin badmin = new B_Admin();
    //到期时间存在ZL_CartPro中,后期看如何改进
    //public int OrderType { get { int _ordertype = DataConverter.CLng(Request.QueryString["OrderType"]); if (_ordertype == 0)_ordertype = 7; return _ordertype; } }
    public string SelType
    {
        get
        {
            return ViewState["type"] as string;
        }
        set { ViewState["type"] = value; }
    }
    public string OrderBy
    {
        get
        {
            return ViewState["order"] as string;
        }
        set { ViewState["order"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            int oid = Convert.ToInt32(Request.Form["oid"]);
            string action = Request.Form["action"];
            string value = Request.Form["value"];
            string result = "";
            switch (action)
            {
                case "Save":
                    M_OrderList orderMod = orderBll.SelReturnModel(oid);
                    orderMod.Ordermessage = value;
                    orderBll.UpdateByID(orderMod);
                    result = "1";
                    break;
            }
            Response.Clear();
            Response.Write(result); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            SelType = Request.QueryString["type"];
            Call.HideBread(Master);
            MyBind();
        }
    }
    public void MyBind()
    {
        function.Script(this, "showtab('" + SelType + "');");
        DataTable dt = orderBll.IDC_Sel((int)M_OrderList.OrderEnum.IDC, souchtable.SelectedValue, souchkey.Text, OrderBy, SelType);
        double totalSum = 0;
        foreach (DataRow dr in dt.Rows)
        {
            totalSum += Convert.ToDouble(dr["Ordersamount"]);
        }
        TotalSum_Hid.Value = totalSum.ToString("f2");
        RPT.DataSource = dt;
        RPT.DataBind();
    }
    public string GetUsers(string uid)
    {
        M_UserInfo info = buser.GetUserByUserID(DataConverter.CLng(uid));
        string tname = string.IsNullOrEmpty(info.HoneyName) ? "" : "(" + info.HoneyName + ")";
        return info.UserName + tname;
    }
    public string GetTotalSum()
    {
        return TotalSum_Hid.Value;
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
    public string formatzt(string zt, string selects)
    {
        string outstr = "";
        int zts = DataConverter.CLng(zt), intselect = DataConverter.CLng(selects);
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
    //搜索
    protected void souchok_Click(object sender, EventArgs e)
    {
        string soukey = souchkey.Text.Replace(" ", "");
        if (!string.IsNullOrEmpty(soukey)) { sel_box.Attributes.Add("style", "display:inline;"); }
        MyBind();
    }
    //是否到期
    public string IsExpire(object e, object status)
    {
        if (DataConverter.CLng(status) == 0) { return "<span class='rd_red'>未付款</span>"; }
        DateTime st = DateTime.Now, et = DateTime.Now;
        string result = "<span style='color:orange'>无法判断</span>";
        if (DateTime.TryParse(e.ToString(), out et))
        {
            if (st >= et)
            {
                result = "<span style='color:red'>已到期</span>";
            }
            else
            {
                string day = et.Subtract(st).Days > 10000 ? "无限期" : et.Subtract(st).Days + "天";
                result = string.Format("<span>剩余:{0}</span>", day);
            }
        }
        return result;
    }
    //批量删除
    protected void Del_B_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            orderBll.DelByIDS(Request.Form["idchk"]);
        }
        MyBind();
    }
    //设为成功
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.Form["idchk"])) return;
        B_OrderList orderBll = new B_OrderList();
        string[] idArr = Request.Form["idchk"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < idArr.Length; i++)
        {
            orderBll.UpOrderinfo("OrderStatus=99", Convert.ToInt32(idArr[i]));
        }
        MyBind();
    }
    public string GetShopUrl()
    {
        if (DataConverter.CLng(Eval("ProID")) == 0) { return ""; }
        return orderCom.GetShopUrl(Eval("StoreID"), Eval("ProID"));
    }
    public string GetOP()
    {
        string result = "";
        //已支付才能续费
        if (Eval("PaymentStatus", "").Equals(((int)M_OrderList.PayEnum.HasPayed).ToString()))
        {
            result += "<a href='javascript:;' class='option_style' onclick=\"opentitle('IDC_Ren.aspx?ID=" + Eval("OrderNo") + "','IDC续费');\"><i class='fa  fa-shopping-basket'></i>续费</a>";
        }
        return result;
    }

    protected void AddTime_Order_Click(object sender, EventArgs e)
    {
        string text = AddTime_Order.Text;
        EndTime_Order.Text = "到期时间";
        if (text.Contains("fa-long-arrow-up"))//升序
        {
            AddTime_Order.Text = "生效时间 <i class='fa fa-long-arrow-down'></i>";
            OrderBy = "AddTime DESC";
        }
        else if (text.Contains("fa-long-arrow-down"))
        {
            AddTime_Order.Text = "生效时间";
            OrderBy = "";
        }
        else
        {
            AddTime_Order.Text = "生效时间 <i class='fa fa-long-arrow-up'></i>";
            OrderBy = "AddTime ASC";
        }
        MyBind();
    }

    protected void EndTime_Order_Click(object sender, EventArgs e)
    {
        string text = EndTime_Order.Text;
        AddTime_Order.Text = "生效时间";
        if (text.Contains("fa-long-arrow-up"))//升序
        {
            EndTime_Order.Text = "到期时间 <i class='fa fa-long-arrow-down'></i>";
            OrderBy = "EndTime DESC";
        }
        else if (text.Contains("fa-long-arrow-down"))
        {
            EndTime_Order.Text = "到期时间";
            OrderBy = "";
        }
        else
        {
            EndTime_Order.Text = "到期时间 <i class='fa fa-long-arrow-up'></i>";
            OrderBy = "EndTime ASC";
        }
        MyBind();
    }
    protected void RPT_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "del2":
                orderBll.DelByIDS(id.ToString());
                break;
        }
        MyBind();
    }

    protected void ExportExcel_B_Click(object sender, EventArgs e)
    {
        OfficeHelper helper = new OfficeHelper();
        DataTable dt = orderBll.IDC_Sel((int)M_OrderList.OrderEnum.IDC, souchtable.SelectedValue, souchkey.Text, OrderBy, SelType);
        dt.Columns.Add("UserName");
        dt.Columns.Add("IsExpire");//是否过期
        dt.Columns.Add("OStatus"); //状态
        dt.Columns.Add("PStatus"); //财务
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            dr["UserName"] = GetUsers(dr["UserID"].ToString());
            dr["Domain"] = dr["Domain"].ToString().ToLower().Replace("www.", "");
            dr["STime"] = DataConvert.CDate(dr["STime"]).ToString("yyyy-MM-dd");
            dr["ETime"] = DataConvert.CDate(dr["ETime"]).ToString("yyyy-MM-dd");
            dr["OStatus"] = RemoveHtml(formatzt(dr["OrderStatus"].ToString(), "0"));
            dr["PStatus"] = RemoveHtml(formatzt(dr["Paymentstatus"].ToString(), "1"));
        }
        DataTable newDt = dt.DefaultView.ToTable(false, "ID,OrderNo,Domain,UserName,Proname,STime,ETime,IsExpire,OStatus,PStatus".Split(','));
        string columnames = "ID,订单号,绑定域名,会员,商品名称,生效时间,到期时间,是否到期,状态,财务";
        SafeC.DownStr(helper.ExportExcel(newDt, columnames), DateTime.Now.ToString("yyyyMMdd") + "IDC订单表.xls", Encoding.UTF8);
    }

    public string RemoveHtml(string str)
    {
        return Regex.Replace(Regex.Replace(str, "<[^>]+>", ""), "&[^;]+;", "");
    }
}