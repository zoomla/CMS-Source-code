using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Shop;
using ZoomLa.Common;
using ZoomLa.Common.Addon;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Shop;

public partial class Manage_Shop_IDC_IDCOrderInfo : System.Web.UI.Page
{
    B_OrderList oll = new B_OrderList();
    B_Order_IDC idcBll = new B_Order_IDC();
    B_User buser = new B_User();
    B_Product proBll = new B_Product();
    B_IServer Serverbll = new B_IServer();
    B_MailManage mailBll = new B_MailManage();
    M_OrderList orderinfo = null;
    public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
    public string OrderNO { get { return ViewState["OrderNO"].ToString(); } set { ViewState["OrderNO"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!B_ARoleAuth.Check(ZLEnum.Auth.shop, "OrderList")) { function.WriteErrMsg("没有权限进行此项操作"); }
        if (Mid < 1) { function.WriteErrMsg("未指定订单"); }
        if (!IsPostBack)
        {
            orderinfo = oll.SelReturnModel(Mid);
            OrderNO = orderinfo.OrderNo;
            M_UserInfo mu = buser.SelReturnModel(orderinfo.Userid);
            //----------------------------------------------------
            HeadTitle_L.Text = "订 单 信 息（订单编号：" + orderinfo.OrderNo + "）";
            Invoiceneeds.Text = orderinfo.Invoiceneeds == 1 ? ComRE.Icon_OK : ComRE.Icon_Error;
            Developedvotes.Text = orderinfo.Developedvotes == 1 ? ComRE.Icon_OK : ComRE.Icon_Error;
            OrderAmount_L.Text = orderinfo.Ordersamount.ToString("f2");
            Internalrecords_T.Text = orderinfo.Internalrecords;
            Ordermessage_T.Text = orderinfo.Ordermessage;
            if (orderinfo.Paymentstatus >= (int)M_OrderList.PayEnum.HasPayed)
            {
                Paymentstatus.Text = "<span  style='color:green;'>已经汇款</span>";
            }
            else
            {
                Paymentstatus.Text = "<span style='color:red;'>等待汇款</span>";
            }
            OS_Normal_Btn.Enabled = orderinfo.OrderStatus != 0;
            if (orderinfo.OrderStatus >= (int)M_OrderList.StatusEnum.OrderFinish) { IDC_Complete_Btn.Attributes.Add("disabled", "disabled"); }

            //----IDC特殊逻辑处理
            //IDC_Complete_Btn.Visible = true;
            //CompleteOrder_Btn.Visible = false;
            //if (!CompleteOrder_Btn.Enabled) { IDC_Complete_Btn.Attributes.Add("disabled", "disabled"); }
            //-------------------------
            M_Order_IDC idcMod = idcBll.SelModelByOrderNo(orderinfo.OrderNo);
            M_Product proMod = proBll.GetproductByid(idcMod.ProID);
            IDC_UName_L.Text = "<a href='javascript:;' onclick='showuinfo(" + mu.UserID + ");' title='查看用户'>" + mu.UserName + "</a>";
            IDC_Proname_L.Text = "<a href='../ShowProduct.aspx?id=" + proMod.ID + "'title='查看商品'>" + proMod.Proname + "</a>";
            IDC_Domain_T.Text = idcMod.Domain;
            IDC_STime_T.Text = idcMod.STime.ToString("yyyy-MM-dd");
            IDC_ETime_L.Text = idcMod.ETime.ToString("yyyy-MM-dd");
            IDC_Complete_L.Text = "用户[" + mu.UserName + "]现有" + mu.Purse.ToString("f2") + ",需支付" + orderinfo.Ordersamount.ToString("f2") + ",结余:" + (mu.Purse - orderinfo.Ordersamount).ToString("f2");
            //判断订单所处状态
            {
                if (orderinfo.OrderStatus < (int)M_OrderList.StatusEnum.Normal || orderinfo.Aside == 1)
                {
                    prog_order_div.InnerHtml = OrderHelper.GetOrderStatus(orderinfo.OrderStatus, orderinfo.Aside, orderinfo.StateLogistics);
                }
                else
                {
                    int current = 2;
                    if (orderinfo.OrderStatus >= (int)M_OrderList.StatusEnum.OrderFinish) { current = 5; }
                    else if (orderinfo.Paymentstatus >= (int)M_OrderList.PayEnum.HasPayed)
                    {
                        current++;
                        switch (orderinfo.StateLogistics)
                        {
                            case (int)M_OrderList.ExpEnum.HasSend:
                                current++;
                                break;
                            case (int)M_OrderList.ExpEnum.HasReceived:
                                current += 2;
                                break;
                        }
                    }
                    function.Script(this, "$('#prog_order_div').ZLSteps('订单生成,等待用户支付,等待商户发货,等待用户签收,订单完结'," + current + ")");
                }
            }
            //Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='../ProductManage.aspx'>商城管理</a></li><li><a href='IDCOrder.aspx'>订单管理</a></li><li class='active'><a href='" + Request.RawUrl + "'>订单详情</a></li>");
        }
    }
    //IDC专用,扣减用户余额,支持负数
    protected void IDC_Complete_Submit_Click(object sender, EventArgs e)
    {
        M_OrderList orderMod = oll.SelReturnModel(Mid);
        buser.MinusVMoney(orderMod.Userid, orderMod.Ordersamount, M_UserExpHis.SType.Purse, "IDC订单[" + orderMod.OrderNo + "]");
        OrderHelper.FinalStep(orderMod);
        Response.Redirect(Request.RawUrl);
    }
    protected void IDC_Save_Btn_Click(object sender, EventArgs e)
    {
        orderinfo = oll.SelReturnModel(Mid);
        M_Order_IDC idcMod = idcBll.SelModelByOrderNo(orderinfo.OrderNo);
        idcMod.Domain = IDC_Domain_T.Text;
        idcMod.STime = DataConverter.CDate(IDC_STime_T.Text);
        orderinfo.Internalrecords = Internalrecords_T.Text;
        orderinfo.Ordermessage = Ordermessage_T.Text;
        idcBll.UpdateByID(idcMod);
        oll.UpdateByID(orderinfo);
        function.WriteSuccessMsg("信息修改成功");
    }
    //protected void SaveRemind_Btn_Click(object sender, EventArgs e)
    //{
    //    orderinfo = oll.SelReturnModel(Mid);
    //    orderinfo.Internalrecords = Internalrecords_T.Text;
    //    orderinfo.Ordermessage = Ordermessage_T.Text;
    //    oll.UpdateByID(orderinfo);
    //    function.WriteSuccessMsg("修改成功");
    //}
    protected void OS_Normal_Btn_Click(object sender, EventArgs e)
    {
        string str = "Aside=0,Suspended=0,Settle=0,OrderStatus=" + (int)M_OrderList.StatusEnum.Normal;
        oll.UpOrderinfo(str, Mid);
        Response.Redirect(Request.RawUrl);
    }
    protected void OS_Invoice_Btn_Click(object sender, EventArgs e)
    {
        string str = "Developedvotes=1";
        oll.UpOrderinfo(str, Mid);
        Response.Redirect(Request.RawUrl);
    }
    protected void Pay_Cancel_Btn_Click(object sender, EventArgs e)
    {
        oll.UpOrderinfo("Paymentstatus=" + (int)M_OrderList.PayEnum.NoPay, Mid);
        Response.Redirect(Request.RawUrl);
    }
    //已经支付
    protected void Pay_Has_Btn_Click(object sender, EventArgs e)
    {
        oll.UpOrderinfo("Paymentstatus=" + (int)M_OrderList.PayEnum.HasPayed, Mid);
        Response.Redirect(Request.RawUrl);
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        showData("pre");
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        showData("next");
    }
    private void showData(string str)
    {
        M_OrderList orderMod = oll.SelNext(Mid, str);
        if (str == "next")
        {
            if (orderMod == null)
            {
                btnNext.Text = "已是最后一个订单";
                btnNext.Enabled = false;
                //btnNext2.Enabled = false;
            }
            else { Response.Redirect("Orderlistinfo.aspx?id=" + orderMod.id); }
        }
        else if (str == "pre")
        {
            if (orderMod == null)
            {
                btnPre.Text = "已是第一个订单";
                btnPre.Enabled = false;
                //btnPre2.Enabled = false;
            }
            else { Response.Redirect("Orderlistinfo.aspx?id=" + orderMod.id); }
        }
    }
    public string GetiServerNum()
    {
        int num = Serverbll.getiServerNum("", buser.GetLogin().UserID, "", "", Mid);
        if (num > 0)
        {
            return "<span class='iserver'>" + num + "</span>";
        }
        else
        {
            return "";
        }
    }

    protected void ExportExcel_B_Click(object sender, EventArgs e)
    {
        StringWriter sw = new StringWriter();
        var mu = buser.GetLogin();
        string url = "ExportIDCOrder.aspx?id=" + Mid + "&uname=" + mu.UserName + "&upwd=" + mu.UserPwd;
        Server.Execute(url, sw);
        string name = "DC订单详情-" + Mid;
        string html = sw.ToString();

        name = HttpUtility.UrlPathEncode(name);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel.numberformat:@";
        this.EnableViewState = false;
        Response.Charset = "UTF-8";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//设置输出流为简体中文  
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + name + ".xls");
        Response.Write(html);
        Response.End();
    }

    protected void DownWord_B_Click(object sender, EventArgs e)
    {
        StringWriter sw = new StringWriter();
        var mu = buser.GetLogin();
        string url = "ExportIDCOrder.aspx?id=" + Mid + "&uname=" + mu.UserName + "&upwd=" + mu.UserPwd;
        string vpath = "/UploadFiles/IDC订单详情-" + Mid + ".doc";
        Server.Execute(url, sw);
        SafeSC.DownFile(OfficeHelper.W_HtmlToWord(sw.ToString(), vpath));
    }

    protected void SendMail_B_Click(object sender, EventArgs e)
    {
        orderinfo = oll.SelReturnModel(Mid);
        M_UserInfo uinfo = buser.SelReturnModel(orderinfo.Userid);
        MailAddress address = new MailAddress(uinfo.Email);
        MailInfo mailInfo = new MailInfo();
        mailInfo.IsBodyHtml = true;
        mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
        mailInfo.ToAddress = address;
        mailInfo.MailBody = new OrderCommon().TlpDeal(mailBll.SelByType(B_MailManage.MailType.IDCOrder), GetEmailDt());
        mailInfo.Subject = "IDC订单邮件提醒";
        if (SendMail.Send(mailInfo) == SendMail.MailState.Ok) { function.Script(this,"alert('邮件发送成功!');"); }
        else { function.Script(this, "alert('邮件发送失败!');"); }
    }
    public DataTable GetEmailDt()
    {
        orderinfo = oll.SelReturnModel(Mid);
        M_Order_IDC idcMod = idcBll.SelModelByOrderNo(orderinfo.OrderNo);
        M_Product proMod = proBll.GetproductByid(idcMod.ProID);
        DataTable dt = new DataTable();
        dt.Columns.Add("UserName");
        dt.Columns.Add("CDate");
        dt.Columns.Add("OrderNo");
        dt.Columns.Add("Proname");
        dt.Columns.Add("Domain");
        dt.Columns.Add("STime");
        dt.Columns.Add("ETime");
        dt.Columns.Add("Ordersamount");
        dt.Columns.Add("PayStatus");
        dt.Columns.Add("Ordermessage");
        DataRow dr = dt.NewRow();
        dr["UserName"] = orderinfo.Rename;
        dr["CDate"] = orderinfo.AddTime.ToString("yyyy年MM月dd日");
        dr["OrderNo"] = orderinfo.OrderNo;
        dr["Proname"] = proMod.Proname;
        dr["Domain"] = idcMod.Domain;
        dr["STime"] = idcMod.STime.ToString("yyyy-MM-dd");
        dr["ETime"] = idcMod.ETime.ToString("yyyy-MM-dd");
        dr["Ordersamount"] = orderinfo.Ordersamount.ToString("f2");
        if (orderinfo.Paymentstatus >= (int)M_OrderList.PayEnum.HasPayed)
        {
            dr["PayStatus"] = "<span  style='color:green;'>已经汇款</span>";
        }
        else
        {
            dr["PayStatus"] = "<span style='color:red;'>等待汇款</span>";
        }
        dr["Ordermessage"] = orderinfo.Ordermessage;
        dt.Rows.Add(dr);
        return dt;

    }
}