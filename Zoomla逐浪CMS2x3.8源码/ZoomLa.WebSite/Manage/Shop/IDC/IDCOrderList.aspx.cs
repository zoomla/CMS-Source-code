using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.SQLDAL;

public partial class Manage_Shop_IDC_IDCOrderList : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_OrderList orderBll = new B_OrderList();
    public int UserID { get { return DataConvert.CLng(Request.QueryString["userid"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
        }
    }
    public void MyBind()
    {
        EGV.DataSource = orderBll.IDC_Sel((int)M_OrderList.OrderEnum.IDC,"userid",UserID.ToString(),"","");
        EGV.DataBind();
    }
    public string GetUsers(string uid)
    {
        M_UserInfo info = buser.GetUserByUserID(DataConverter.CLng(uid));
        string tname = string.IsNullOrEmpty(info.HoneyName) ? "" : "(" + info.HoneyName + ")";
        return info.UserName + tname;
    }
    //是否到期
    public string IsExpire(object e)
    {
        if (DataConverter.CLng(Eval("Paymentstatus")) == 0) { return "<span style='color:red'>未付款</span>"; }
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

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }
}