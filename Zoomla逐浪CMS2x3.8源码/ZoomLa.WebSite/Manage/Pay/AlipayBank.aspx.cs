using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class Manage_I_Pay_AlipayBank : System.Web.UI.Page
{
    /*
     * 支付宝网银支付的ID为15
     */ 
    private B_PayPlat bll = new B_PayPlat();
    private M_PayPlat info = new M_PayPlat();
    B_Admin badmin = new B_Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        B_ARoleAuth.CheckEx(ZLEnum.Auth.other, "PayManage");

        Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Config/SiteInfo.aspx'>系统设置</a></li><li><a href='PayPlatManage.aspx'>在线支付平台</a></li><li class=\"active\">支付宝单网银</li>");
        if (!IsPostBack)
        {
           info= bll.GetPayPlatByClassID("15");
           if (info!=null)
           {
               this.DDLPayName.Text = info.PayPlatName;
               this.TxtAccountID.Text = info.AccountID;
               this.TxtMD5Key.Text = info.MD5Key;
               this.TxtSellerEmail.Text = info.SellerEmail;
               this.TxtRate.Text = info.Rate.ToString();
               string[] bankArr = info.PayPlatinfo.Split(',');
               for (int i = 0; i < bankList.Items.Count; i++)//如果有数据就先清空,再填充
               {
                   //bankArr.Select((string s) => s == bankList.Items[i].Value);
                   if (!bankArr.Contains(bankList.Items[i].Value))
                       bankList.Items[i].Selected = false;
               }

           }
        }
    }

    protected void EBtnSubmit_Click(object sender, EventArgs e)
    {
        if (this.Page.IsValid)//修改与保存
        {
            info = bll.GetPayPlatByClassID("15");
            if (info!=null)
            {
                info.PayPlatName = this.DDLPayName.Text;
                info.AccountID = this.TxtAccountID.Text;
                info.MD5Key = this.TxtMD5Key.Text;
                info.SellerEmail = this.TxtSellerEmail.Text;
                info.Rate = DataConverter.CFloat(this.TxtRate.Text.Trim());
                info.PayPlatinfo="";
                for (int i = 0; i < bankList.Items.Count; i++)
                {
                    if (bankList.Items[i].Selected)
                        info.PayPlatinfo += bankList.Items[i].Value + ",";
                }
                info.PayPlatinfo = info.PayPlatinfo.TrimEnd(',');
                this.bll.Update(info);
                function.WriteSuccessMsg("修改成功!");
            }
            else
            {
                info = new M_PayPlat();
                info.PayPlatID = 0;
                info.PayClass = 15;//支付ID
                info.PayPlatName = this.DDLPayName.Text;
                info.AccountID = this.TxtAccountID.Text;
                info.MD5Key = this.TxtMD5Key.Text;
                info.SellerEmail = this.TxtSellerEmail.Text;
                info.Rate = DataConverter.CFloat(this.TxtRate.Text.Trim());
                info.IsDefault = false;
                info.IsDisabled = false;
                info.payType = "payOnline";
                info.OrderID = this.bll.GetMaxOrder() + 1;
                for (int i = 0; i < bankList.Items.Count; i++)
                {
                    if (bankList.Items[i].Selected)
                        info.PayPlatinfo += bankList.Items[i].Value+",";
                }

                info.PayPlatinfo = info.PayPlatinfo.TrimEnd(',');
                info.UID = 0;
                this.bll.Add(info);
                function.WriteSuccessMsg("添加成功!");
            }
        }
    }
}