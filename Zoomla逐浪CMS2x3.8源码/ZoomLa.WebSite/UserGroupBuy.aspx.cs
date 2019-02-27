using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Components;
using ZoomLa.Common;
using ZoomLa.Model;

public partial class UserGroupBuy : System.Web.UI.Page
{
    private B_PayPlat bll = new B_PayPlat();
    private B_User buser = new B_User();
    private B_Payment pay = new B_Payment();
    private B_GroupBuyList bgl = new B_GroupBuyList();
    private B_Product bproduct = new B_Product();
    private B_ZL_GroupBuy Groupbuy = new B_ZL_GroupBuy();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPay();
            int GID = DataConverter.CLng(Request.QueryString["id"]);
            M_GroupBuyList mgl = bgl.GetSelect(GID);
            if (mgl != null && mgl.id > 0)
            {
                M_Product mpro = bproduct.GetproductByid(mgl.ProID);
                if (mpro != null && mpro.ID > 0)
                {
                    if (!string.IsNullOrEmpty(mpro.ColonelTime))
                    {
                        string[] colone = mpro.ColonelTime.Split('|');
                        if (colone != null && colone.Length > 1)
                        {
                            //团购时间已结束
                            if (DataConverter.CDate(colone[1]) <= DateTime.Now)
                            {
                                txtNum.Text = mgl.Snum.ToString();
                                txtShopname.Text = mpro.Proname;
                                double currentPri = DataConverter.CDouble(Groupbuy.GetCurrentPrice(mpro.ID, mpro.Sold));
                                if (currentPri <= 0)
                                {
                                    currentPri = mpro.LinPrice;
                                }
                                TxtvMoney.Text = (currentPri - mgl.Deposit).ToString();
                                hfMoney.Value = (currentPri - mgl.Deposit).ToString();
                                hfGID.Value = mgl.id.ToString();
                                Diverror.Visible = false;
                                Divpay.Visible = true;
                            }
                            else
                            {
                                Diverror.Visible = true;
                                Divpay.Visible = false;
                                DivHtml.InnerHtml = "团购时间未结束";
                            }
                        }
                        else
                        {
                            Diverror.Visible = true;
                            Divpay.Visible = false;
                            DivHtml.InnerHtml = "该商品没有设置团购时间";
                        }
                    }
                    else
                    {
                        Diverror.Visible = true;
                        Divpay.Visible = false;
                        DivHtml.InnerHtml = "该商品没有设置团购时间";
                    }
                }
                else
                {
                    Diverror.Visible = true;
                    Divpay.Visible = false;
                    DivHtml.InnerHtml = "不存在该商品";
                }
            }
            else
            {
                Diverror.Visible = true;
                Divpay.Visible = false;
                DivHtml.InnerHtml = "不存在本团订";  
            }
        }
    }

    private void BindPay()
    {
        this.DDLPayPlat.DataSource = this.bll.GetPayPlatAll();
        this.DDLPayPlat.DataTextField = "PayPlatName";
        this.DDLPayPlat.DataValueField = "PayPlatID";
        this.DDLPayPlat.DataBind();
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        decimal vmoney = DataConverter.CDecimal(this.TxtvMoney.Text);
        int platid = DataConverter.CLng(Request.Form["DDLPayPlat"]);
        M_UserInfo uinfo = buser.GetLogin();
        int UserID = uinfo.UserID;
        M_PayPlat plat = bll.GetPayPlatByid(platid);
        string v_amount = "";              //实际支付金额
        v_amount = decimal.Round(vmoney + vmoney * DataConverter.CDecimal(plat.Rate) / 100, 2).ToString();
        vmoney = decimal.Round(vmoney, 2);
        if (vmoney < 0.01M)
        {
            function.WriteErrMsg("<li>每次划款金额不能低于0.01元</li>");
        }
        B_Payment pay = new B_Payment();
       
    }
}