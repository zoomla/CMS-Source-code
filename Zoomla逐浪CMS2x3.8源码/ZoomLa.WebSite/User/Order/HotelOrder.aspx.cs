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
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.API;
using ZoomLa.DZNT;
using System.Xml;
using ZoomLa.BLL.Shop;

public partial class User_PrintServer_HotelOrder : System.Web.UI.Page
{
    #region shuxing
    private B_User buser = new B_User();
    private M_UserInfo muser = new M_UserInfo();
    protected B_OrderList orderbll = new B_OrderList();
    protected B_Cart cll = new B_Cart();
    protected B_CartPro pll = new B_CartPro();
    B_CartPro cartproBll = new B_CartPro();
    protected B_Product bll = new B_Product();
    protected B_Product pro = new B_Product();
    public int sid = 0;
    #endregion
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
                    GetViewOrderList();
                }
                else
                {
                    Panel1.Visible = true;
                    Panel2.Visible = false;
                    GetOrderinfo();
                }
            }
        }
    }

    private void GetViewOrderList()
    {
        sid = DataConverter.CLng(Request.QueryString["id"]);
        DataTable orderlistW = pll.GetCartProOrderIDW(sid);
        if (orderlistW != null && orderlistW.Rows.Count > 0)
        {
            Repeater2.DataSource = orderlistW;
            Repeater2.DataBind();
            preojiage.Text = orderlistW.Rows[0]["AllMoney"].ToString();
        }
    }

    protected void GetOrderinfo()
    {
        string UserName = buser.GetLogin().UserName;
        DataTable olist = orderbll.GetUserlist(UserName, 0, 1);
        EGVH.DataSource = olist;
        EGVH.DataBind();

    }
    public string formatcc(string moneys)
    {
        return OrderHelper.GetPriceStr(DataConverter.CDouble(moneys), Eval("AllMoney_Json").ToString());
    }


    public string getshijiage(string id)
    {
        int Sid = DataConverter.CLng(id);
        //    string shijiage = "";
        M_OrderList orders = orderbll.GetOrderListByid(Sid, 1);//GetOrderbyOrderNo
        DataTable tb = orderbll.GetOrderbyOrderNo(orders.OrderNo, 1);
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

    protected void EGVH_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGVH.PageIndex = e.NewPageIndex;
        GetOrderinfo();
    }
}