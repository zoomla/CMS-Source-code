using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Data;

public partial class User_UserShop_UserOrderlist : System.Web.UI.Page
{
    B_User buser = new B_User();
    protected B_OrderList orderbll = new B_OrderList();
    protected B_Product pro = new B_Product();
    protected B_CartPro cartproBll = new B_CartPro();
    protected B_Cart cartBll = new B_Cart();
    protected B_MoneyManage mm = new B_MoneyManage();

    protected void Page_Load(object sender, EventArgs e)
    {
        GetOrderinfo();
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


    /// <summary>
    /// 查询用户店铺ID
    /// </summary>
    /// <returns>店铺ID</returns>
    private int sel()
    {
        B_ModelField mll = new B_ModelField();
        DataTable list = mll.SelectTableName("ZL_Store_Flower", "UserId=" + buser.GetLogin().UserID);
        if (list != null && list.Rows.Count > 0)
        {
            return DataConverter.CLng(list.Rows[0]["ID"]);
        }
        else
        {
            return 0;
        }
    }

    protected void GetOrderinfo()
    {
        string UserName = buser.GetLogin().UserName;
        #region 获取页数
        int CPage;
        int temppage;

        if (Request.Form["DropDownList1"] != null)
        {
            temppage = Convert.ToInt32(Request.Form["DropDownList1"]);
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

        DataTable uolist=null;
        int soutype = DataConverter.CLng(Request.QueryString["souchtable"]);
        string key = function.HtmlEncode(Request.QueryString["souchkey"]);
        string menu = Request.QueryString["menu"];
        this.souchtable.Text = soutype.ToString();
        quicksouch.Text = getquicksou.ToString();
        switch (menu == "souch" && key != "")
        {
            case true:
                this.souchkey.Text = key;
                uolist = orderbll.Getsouchinfo(soutype, key, sel());
                break;
            case false:
                if (getquicksou > 0)
                {
                    uolist = orderbll.GetOrderListtype(getquicksou, sel());
                }
                else
                {
                   // uolist = orderbll.GetSelectBySid(sel());
                }
                break;
            default:
                //uolist = orderbll.GetSelectBySid(sel());
                break;
        }
        PagedDataSource cc = new PagedDataSource();
        if (uolist != null)
        {
            cc.DataSource = uolist.DefaultView;
            cc.AllowPaging = true;
            if (Request.QueryString["txtPage"] != null && Request.QueryString["txtPage"] != "")
            {
                cc.PageSize = DataConverter.CLng(Request.QueryString["txtPage"]);
            }
            if (Request.Form["txtPage"] != null && Request.Form["txtPage"] != "")
            {
                cc.PageSize = DataConverter.CLng(Request.Form["txtPage"]);
            }

            if (uolist != null && cc.PageSize >= uolist.Rows.Count)
            {
                cc.CurrentPageIndex = 0;
                CPage = 1;
            }
            cc.CurrentPageIndex = CPage - 1;
            Orderlisttable.DataSource = cc;
            Orderlisttable.DataBind();

            #region 设置翻页
            Allnum.Text = uolist.DefaultView.Count.ToString();
        }
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

        Toppage.Text = "<a href=?menu=Orderinfo&txtPage=" + cc.PageSize + "&Currentpage=0>首页</a>";
        Nextpage.Text = "<a href=?menu=Orderinfo&txtPage=" + cc.PageSize + "&Currentpage=" + nextpagenum.ToString() + ">上一页</a>";
        Downpage.Text = "<a href=?menu=Orderinfo&&txtPage=" + cc.PageSize + "&Currentpage=" + downpagenum.ToString() + ">下一页</a>";
        Endpage.Text = "<a href=?menu=Orderinfo&&txtPage=" + cc.PageSize + "&Currentpage=" + Endpagenum.ToString() + ">尾页</a>";
        PageSize.Text = thispagenull.ToString();
        Nowpage.Text = CPage.ToString();
        txtPage.Text = cc.PageSize.ToString();
        DropDownList1.Items.Clear();
        for (int i = 1; i <= thispagenull; i++)
        {
            this.DropDownList1.Items.Add(i.ToString());
        }
        for (int j = 0; j < DropDownList1.Items.Count; j++)
        {
            if (DropDownList1.Items[j].Value == Nowpage.Text)
            {
                DropDownList1.SelectedValue = Nowpage.Text;
                break;
            }
        }

        double ddmoney;
        int num;
        ddmoney = 0.00;
        num = 0;
        string tempstrs = "";

        //本次查询
        if (uolist != null)
        {
            num = uolist.Rows.Count;
        }
        for (int i = 0; i < num; i++)
        {
            ddmoney = ddmoney + Convert.ToDouble(getshijiage(uolist.Rows[i]["id"].ToString()));
        }

        tempstrs = ddmoney.ToString();

        if (tempstrs.IndexOf(".") == -1)
        {
            tempstrs = ddmoney + ".00";
        }

        thisall.Text = tempstrs.ToString();
       
        #endregion
    }

   
    /// <summary>
    /// 订单编号显示
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string getorderno(string id)
    {
        string sss = "";
        int sid = DataConverter.CLng(id);
        M_OrderList cdd = orderbll.GetOrderListByid(sid);

        int aside = cdd.Aside;
        if (aside == 1)
        {
            sss = "<a href=\"UserOrderinfo.aspx?id=" + cdd.id + "\"><font color=#cccccc>" + cdd.OrderNo + "</font></a>";
        }
        else
        {
            sss = "<a href=\"UserOrderinfo.aspx?id=" + cdd.id + "\">" + cdd.OrderNo + "</a>";
        }
        return sss;
    }

    /// <summary>
    /// 客户名显示
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 用户名显示
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
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

    public string formatcs(string type, string money)
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
  

    /// <summary>
    /// 实际金额
    /// </summary>
    /// <param name="id">订单编号</param>
    /// <returns></returns>
    public string getshijiage(string id)
    {
        int Sid = DataConverter.CLng(id);
        //    string shijiage = "";
        M_OrderList orders = orderbll.GetOrderListByid(Sid);//GetOrderbyOrderNo
        DataTable tb = orderbll.GetOrderbyOrderNo(orders.OrderNo);
        object s = tb.Compute("sum(ordersamount)", "orderno='" + orders.OrderNo + "'");
        s = DataConverter.CDouble(s).ToString("0.00");
        return s.ToString();
    }


    /// <summary>
    /// 是否需要发票、是否已开发票
    /// </summary>
    /// <param name="cc">发票</param>
    /// <returns></returns>
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

    /// <summary>
    /// 订单、付款、物流状态
    /// </summary>
    /// <param name="zt"></param>
    /// <param name="selects"></param>
    /// <returns></returns>
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
                switch (zts)
                {
                    case 0:
                        outstr = "<font color=red>等待确认</font>";
                        break;
                    case 1:
                        outstr = "<font color=blue>已经确认</font>";
                        break;
                    default:
                        outstr = "<font color=red>等待确认</font>";
                        break;
                }
                break;
            case 1:
                switch (zts)
                {
                    case 0:
                        outstr = "<font color=red>等待汇款</font>";
                        break;
                    case 1:
                        outstr = "<font color=blue>已经汇款</font>";
                        break;
                    default:
                        outstr = "<font color=red>等待汇款</font>";
                        break;
                }
                break;
            case 2:
                switch (zts)
                {
                    case 0:
                        outstr = "<font color=red>配送中</font>";
                        break;
                    case 1:
                        outstr = "<font color=blue>已发货</font>";
                        break;
                    default:
                        outstr = "<font color=red>配送中</font>";
                        break;
                };
                break;
            default:
                outstr = "";
                break;

        }
        return outstr;
    }


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOrderinfo();
    }

    protected void souchok_Click(object sender, EventArgs e)
    {
        string key = function.HtmlEncode(Request.Form["souchkey"]);
        string soutype = Request.Form["souchtable"];
        if (key != "")
        {
            Response.Redirect("UserOrderlist.aspx?menu=souch&souchtable=" + soutype + "&souchkey=" + key + "");
        }
    }
    protected void txtPage_TextChanged1(object sender, EventArgs e)
    {
        ViewState["page"] = "1";
        GetOrderinfo();
    }
}