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

public partial class Manage_Shop_RepairDeailt : System.Web.UI.Page
{
    B_Order_Repair repairBll = new B_Order_Repair();
    public int RepairID { get { return DataConverter.CLng(Request.QueryString["id"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Call.SetBreadCrumb(Master, "<li><a href='ProductManage.aspx'>商城管理</a></li><li><a href='OrderList.aspx'>订单管理</a></li><li class='active'><a href='OrderRepairAudit.aspx'>售后详情</a></li>");
            MyBind();
        }
    }
    public void MyBind()
    {
        M_Order_Repair repairMod = repairBll.SelReturnModel(RepairID);
        ServiceType_L.Text = GetServieType(repairMod.ServiceType);
        ProNum_L.Text = repairMod.ProNum.ToString();
        Deailt_T.InnerText = repairMod.Deailt;
        Attach_Hid.Value = repairMod.ProImgs.Trim('|');
        CretType_L.Text = GetCret(repairMod.CretType);
        ReType_L.Text = GetReType(repairMod.ReProType);
        TakeCounty_L.Text = repairMod.TakeProCounty;
        TakeAddress_L.Text = repairMod.TakeProAddress;
        TakeTime_L.Text = repairMod.TakeTime.ToString();
        ReCounty_L.Text = repairMod.ReProCounty;
        ReAddress_L.Text = repairMod.ReProAddress;
        UserName_L.Text = repairMod.UserName;
        Phone_L.Text = repairMod.Phone;
    }

    public string GetServieType(string type)
    {
        switch (type)
        {
            case "drawback":
                return "退货";
            case "exchange":
                return "换货";
            case "repair":
                return "维修";
            default:
                return "未知";
        }
    }
    public string GetReType(int type)
    {
        switch (type)
        {
            case 1:
                return "上门自取";
            case 2:
                return "送货到自提点";
            default:
                return "未知";
        }
    }
    public string GetCret(string crettype)
    {
        string[] types = crettype.Split(',');
        string str = "";
        foreach (string item in types)
        {
            str += item.Equals("1") ? "发票" : "检测报告";
            str += ",";
        }
        return str.Trim(',');
    }

    protected void Audit_Btn_Click(object sender, EventArgs e)
    {
        repairBll.UpdateStatuByIDS(RepairID.ToString(), 1);
        function.WriteSuccessMsg("审核成功!", "OrderRepairAudit.aspx");
    }

    protected void UnAudit_Btn_Click(object sender, EventArgs e)
    {
        repairBll.UpdateStatuByIDS(RepairID.ToString(), 0);
        function.WriteSuccessMsg("取消审核成功!", "OrderRepairAudit.aspx");
    }

    protected void Del_Btn_Click(object sender, EventArgs e)
    {
        repairBll.Del(RepairID);
        function.WriteSuccessMsg("删除成功!", "OrderRepairAudit.aspx");
    }
}