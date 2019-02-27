using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Shop;
using ZoomLa.Model;
using ZoomLa.Model.Shop;
using ZoomLa.Common;

public partial class Manage_Shop_OrderRepairAudit : System.Web.UI.Page
{
    B_Order_Repair repailBll = new B_Order_Repair();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='ProductManage.aspx'>"+ Resources.L.商城管理 + "</a></li><li><a href='OrderList.aspx'>"+ Resources.L.订单管理 + "</a></li><li class='active'><a href='OrderRepairAudit.aspx'>"+ Resources.L.返修单审核 + "</a></li>");
            MyBind();
        }
    }
    public void MyBind()
    {
        EGV.DataSource = repailBll.SelAll();
        EGV.DataBind();
    }

    protected void EGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EGV.PageIndex = e.NewPageIndex;
        MyBind();
    }

    public string GetCret()
    {
        string[] types= Eval("CretType").ToString().Split(',');
        string str = "";
        foreach (string item in types)
        {
            str += item.Equals("1") ? Resources.L.发票 : Resources.L.检测报告;
            str += ",";
        }
        return str.Trim(',');
    }
    public string GetStatus()
    {
        int status = DataConverter.CLng(Eval("Status"));
        switch (status)
        {
            case 1:
                return Resources.L.已审核;
            default:
                return Resources.L.未审核;
        }
    }
    public string GetServieType()
    {
        string type = (Eval("ServiceType")).ToString();
        switch (type)
        {
            case "drawback":
                return Resources.L.退货;
            case "exchange":
                return Resources.L.换货;
            case "repair":
                return Resources.L.维修;
            default:
                return Resources.L.未知;
        }
    }
    protected void Audit_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            repailBll.UpdateStatuByIDS(Request.Form["idchk"], 1);
        }
        MyBind();
    }

    protected void Del_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            repailBll.DelByIDS(Request.Form["idchk"]);
        }
        MyBind();
    }

    protected void UnAudit_Btn_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["idchk"]))
        {
            repailBll.UpdateStatuByIDS(Request.Form["idchk"], 0);
        }
        MyBind();
    }
}