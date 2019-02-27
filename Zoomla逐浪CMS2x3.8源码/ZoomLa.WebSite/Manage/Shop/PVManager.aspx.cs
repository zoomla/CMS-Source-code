using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.BLL.User;
using System.Data;
using ZoomLa.Model.User;
using ZoomLa.BLL.Shop;

public partial class Manage_Shop_PVManager : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_User_Consume consumeBll = new B_User_Consume();
    B_OrderList orderBll = new B_OrderList();
    OrderCommon ordercom = new OrderCommon();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(this.Master, "<li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='OrderList.aspx'>订单管理</a></li><li class='active'>提成管理</li>");
            MyBind();
        }
    }
    private void MyBind()
    {
        EGV.DataSource = orderBll.SelPVOrder(1);
        EGV.DataBind();
    }

    public string GetOrderStatu()
    {
        return OrderHelper.GetOrderStatus(DataConverter.CLng(Eval("OrderStatus")));
    }
    public string GetPayStatu()
    {
        int status = DataConverter.CLng(Eval("Paymentstatus"));
        switch (status)
        {
            case 0:
               return "<font color=red>等待汇款</font>";
            case 1:
                return "<font color=blue>已经汇款</font>";
            default:
                return "<font color=red>等待汇款</font>";
        }
    }
    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        EGV.DataBind();
    }
    //确定分成
    protected void Unit_Btn_Click(object sender, EventArgs e)
    {
        DataTable userDT = buser.Sel();
        DataTable dt = orderBll.SelPVOrder(1);
        foreach (DataRow dr in dt.Rows)
        {
            M_User_Consume consumeMod = new M_User_Consume();
            consumeMod.MType = 2;
            consumeMod.AMount = Convert.ToDouble(dr["PVs"]);
            consumeMod.UserID = Convert.ToInt32(dr["UserID"]);
            consumeMod.PUserID = GetPUserID(userDT, consumeMod.UserID);
            consumeMod.Detail=dr["OrderNo"].ToString()+"增加PV";
            consumeBll.Insert(consumeMod);
            orderBll.UpdateByField("OrderStatus", "100", Convert.ToInt32(dr["ID"]));
        }
        function.WriteErrMsg("订单分成成功");
    }
    public int GetPUserID(DataTable userDT, int uid)
    {
        int puserid = 0;
        DataRow[] drs = userDT.Select("UserID=" + uid);
        if (drs.Length > 0)
        {
            puserid = DataConverter.CLng(drs[0]["ParentUserID"]);
        }
        return puserid;
    }
}