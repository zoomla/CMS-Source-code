using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.WxPayAPI;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_WeiXin_PayWeiXin : CustomerPageAction
{
    B_PayPlat bll = new B_PayPlat();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_Admin.IsSuperManage();
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "WeiXin/Home.aspx'>移动微信</a></li><li class='active'>微信支付</li>");
        }
    }
    public void MyBind()
    {
        M_PayPlat wxpay = B_PayPlat.GetModelForWx();
        if (wxpay!=null)
        {
            AppID_T.Text = wxpay.payType;//appid
            Secret_T.Text = wxpay.PayPlatinfo;//Secret
            MchID_T.Text = wxpay.AccountID;
            Key_T.Text = wxpay.MD5Key;
           // Cert_T.Text = wxpay.SellerEmail;//证书路径
            //CertPWD_T.Text = wxpay.leadtoGroup;//证书密码
        }
    }
    protected void Save_B_Click(object sender, EventArgs e)
    {
        M_PayPlat wxpay = B_PayPlat.GetModelForWx();
        if (wxpay == null) { wxpay = new M_PayPlat(); }
        wxpay.PayPlatName = "微信支付";
        wxpay.payType = AppID_T.Text;
        wxpay.PayPlatinfo = Secret_T.Text;
        wxpay.AccountID = MchID_T.Text;
        wxpay.PayClass = 21;
        wxpay.MD5Key = Key_T.Text;
        if (wxpay.PayPlatID == 0) { bll.insert(wxpay); }
        else { bll.UpdateByID(wxpay); }
        WxPayConfig.UpdateByMod(wxpay);
        function.WriteSuccessMsg("保存成功!");
    }
}