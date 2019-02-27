using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class Manage_WeiXin_PayWeiXinAPP : CustomerPageAction
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MyBind();
            Call.SetBreadCrumb(Master, "<li><a href='" + customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "WeiXin/Home.aspx'>移动微信</a></li><li class='active'>微信APP支付</li>");
        }
    }
    private void MyBind() 
    {
        APPID_T.Text = PlatConfig.WXPay_APPID;
        APPSecret_T.Text = PlatConfig.WXPay_APPSecret;
        MCHID_T.Text = PlatConfig.WXPay_MCHID;
        Key_T.Text = PlatConfig.WXPay_Key;
    }
    protected void Save_Btn_Click(object sender, EventArgs e)
    {
        PlatConfig.WXPay_APPID = APPID_T.Text.Trim();
        PlatConfig.WXPay_APPSecret = APPSecret_T.Text.Trim();
        PlatConfig.WXPay_MCHID = MCHID_T.Text.Trim();
        PlatConfig.WXPay_Key = Key_T.Text.Trim();
        PlatConfig.Update();
        function.WriteSuccessMsg("配置更新成功");
    }
}