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

        DataTable olist = orderbll.GetUserlist(UserName, 0, 1);
        PagedDataSource cc = new PagedDataSource();
        if (olist != null && olist.Rows.Count > 0)
        {
            cc.DataSource = olist.DefaultView;
            cc.AllowPaging = true;
            if (Request.QueryString["txtPage"] != null && Request.QueryString["txtPage"] != "")
            {
                cc.PageSize = DataConverter.CLng(Request.QueryString["txtPage"]);
            }
            if (Request.Form["txtPage"] != null && Request.Form["txtPage"] != "")
            {
                cc.PageSize = DataConverter.CLng(Request.Form["txtPage"]);
            }
            if (olist != null && cc.PageSize >= olist.Rows.Count)
            {
                cc.CurrentPageIndex = 0;
                CPage = 1;
            }
            cc.CurrentPageIndex = CPage - 1;
            Repeater1.DataSource = cc;
            Repeater1.DataBind();
            #region 设置翻页
            int thispagenull = cc.PageCount;//总页数
            int CurrentPage = cc.CurrentPageIndex;
            int nextpagenum = CPage - 1;//上一页
            int downpagenum = CPage + 1;//下一页
            int Endpagenum = thispagenull;
            if (thispagenull <= CPage)
            {
                downpagenum = thispagenull;
            }
            if (nextpagenum <= 0)
            {
                nextpagenum = 0;
            }
            Label2.Text = olist.DefaultView.Count.ToString();
            int thispagenull3 = cc.PageCount;//总页数
            int CurrentPage3 = cc.CurrentPageIndex;
            int nextpagenum3 = CPage - 1;//上一页
            int downpagenum3 = CPage + 1;//下一页
            int Endpagenum3 = thispagenull;
            if (thispagenull <= CPage)
            {
                downpagenum = thispagenull;
            }
            if (nextpagenum <= 0)
            {
                nextpagenum = 0;
            }

            Label3.Text = "<a href=?menu=Orderinfo&Currentpage=0" + "&txtPage=" + cc.PageSize + ">首页</a>";
            if (Label4.Enabled)
            {
                this.Label4.Text = "<a href=?menu=Orderinfo&Currentpage=" + nextpagenum.ToString() + "&txtPage=" + cc.PageSize + ">上一页</a>";
            }
            else
            {
                this.Label4.Text = "上一页";
            }
            if (Label5.Enabled)
            {
                this.Label5.Text = "<a href=?menu=Orderinfo&Currentpage=" + downpagenum.ToString() + "&txtPage=" + cc.PageSize + ">下一页</a>";
            }
            else
            {
                this.Label5.Text = "下一页";
            }
            Label6.Text = "<a href=?menu=Orderinfo&Currentpage=" + Endpagenum.ToString() + "&txtPage=" + cc.PageSize + ">尾页</a>";
            Label7.Text = thispagenull.ToString();
            Label8.Text = CPage.ToString();
            this.txtPage.Text = cc.PageSize.ToString();
            DropDownList3.Items.Clear();
            for (int i = 1; i <= thispagenull; i++)
            {
                this.DropDownList3.Items.Add(i.ToString());
            }
            for (int j = 0; j < DropDownList3.Items.Count; j++)
            {
                if (DropDownList3.Items[j].Value == Label8.Text)
                {
                    DropDownList3.SelectedValue = Label8.Text;
                    break;
                }
            }
        }
            #endregion

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
}