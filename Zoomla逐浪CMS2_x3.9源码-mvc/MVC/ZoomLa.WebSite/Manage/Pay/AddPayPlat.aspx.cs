using System;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Pay
{
    public partial class AddPayPlat : CustomerPageAction
    {
        private B_PayPlat bll = new B_PayPlat();
        private int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_ARoleAuth.CheckEx(ZLEnum.Auth.other, "PayManage");
            if (!IsPostBack)
            {
                DDLPayPlat.Items.Add(new ListItem("线下支付", "99"));
                DDLPayPlat.Items.Add(new ListItem("支付宝[即时到帐]", "12"));
                DDLPayPlat.Items.Add(new ListItem("支付宝实物双工", "1"));
                DDLPayPlat.Items.Add(new ListItem("支付宝货到付款", "100"));
                DDLPayPlat.Items.Add(new ListItem("微信支付", "21"));
                DDLPayPlat.Items.Add(new ListItem("快钱支付", "2"));
                DDLPayPlat.Items.Add(new ListItem("网银在线", "3"));
                DDLPayPlat.Items.Add(new ListItem("中国银联", "9"));
                DDLPayPlat.Items.Add(new ListItem("汇付天下", "13"));
                DDLPayPlat.Items.Add(new ListItem("易宝支付", "5"));
                DDLPayPlat.Items.Add(new ListItem("重庆摩宝", "16"));
                DDLPayPlat.Items.Add(new ListItem("财付通", "4"));
                DDLPayPlat.Items.Add(new ListItem("Bfopay宝付", "22"));
                DDLPayPlat.Items.Add(new ListItem("江西工行", "23"));
                DDLPayPlat.Items.Add(new ListItem("江西建行", "26"));
                DDLPayPlat.Items.Add(new ListItem("双乾支付", "24"));
                DDLPayPlat.Items.Add(new ListItem("贝付通", "25"));
                DDLPayPlat.Items.Add(new ListItem("汇潮支付", "27"));
                //15为支付宝网银不显示
                //商户ID=私钥,证书等放根目录
                DDLPayPlat.SelectedValue = "12";
                if (Mid > 0)
                {
                    M_PayPlat info = bll.GetPayPlatByid(Mid);
                    if (info.PayClass == (int)M_PayPlat.Plat.WXPay)//微信支付跳转
                    { Response.Redirect("../WeiXin/PayWeiXin.aspx"); return; }
                    DDLPayName.Text = info.PayPlatName;
                    DDLPayPlat.SelectedValue = info.PayClass.ToString();
                    TxtAccountID.Text = info.AccountID;
                    TxtMD5Key.Text = info.MD5Key;
                    TxtSellerEmail.Text = info.SellerEmail;
                    //TxtRate.Text = info.Rate.ToString();
                    txtRemark.Text = info.PayPlatinfo;
                    PrivateKey_T.Text = info.PrivateKey;
                    PublicKey_T.Text = info.PublicKey;
                    Other_T.Text = info.Other;
                    IsDisabled.Checked = !info.IsDisabled;
                    LblTitle.Text = "修改支付平台";
                }
                else
                {
                    LblTitle.Text = "添加支付平台";
                }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li><a href='PayPlatManage.aspx'>在线支付平台</a></li><li class='active'>" + LblTitle.Text + "</li>");
            }
        }
        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            M_PayPlat info = new M_PayPlat();
            if (Mid > 0)
            {
                info = bll.GetPayPlatByid(Mid);
            }
            info.PayClass = DataConverter.CLng(this.DDLPayPlat.SelectedValue);//支付ID
            info.PayPlatName = DDLPayName.Text;
            info.AccountID = TxtAccountID.Text;
            info.MD5Key = TxtMD5Key.Text;
            info.SellerEmail = TxtSellerEmail.Text;
            info.PayPlatinfo = txtRemark.Text;
            info.PublicKey = PublicKey_T.Text.Trim();
            info.PrivateKey = PrivateKey_T.Text.Trim();
            info.Other = Other_T.Text;
            info.IsDisabled = !IsDisabled.Checked;
            if (Mid > 0)
            {
                bll.Update(info);
            }
            else
            {
                info.IsDefault = false;
                info.IsDisabled = false;
                info.OrderID = bll.GetMaxOrder() + 1;
                info.UID = 0;
                bll.Add(info);
            }
            function.WriteSuccessMsg("操作成功", "PayPlatManage.aspx");
        }
        protected void DDLPayPlat_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtMD5Key.Visible = true;
            trMD5Key.Visible = true;
            RequiredFieldValidator2.Enabled = false;
            DDLPayName.Text = DDLPayPlat.SelectedIndex > 0 ? DDLPayPlat.SelectedItem.Text : "";
        }
    }
}