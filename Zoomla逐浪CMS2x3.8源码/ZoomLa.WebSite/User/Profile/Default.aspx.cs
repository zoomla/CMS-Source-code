using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Net.Mail;
using ZoomLa.Components;

using System.Xml;

public partial class User_Profile_Default : System.Web.UI.Page
{
    //B_Honor bhonor = new B_Honor();
    B_User buser = new B_User();
    B_Redindulgence bredindulgence = new B_Redindulgence();
    B_RedEnvelope bredEnve = new B_RedEnvelope();
    B_Accountinfo bacc = new B_Accountinfo();
    B_GiftCard_shop bgcshop = new B_GiftCard_shop();
    B_GiftCard_User bgcuser = new B_GiftCard_User();
    B_PayPlat bpay = new B_PayPlat();

    protected void Page_Load(object sender, EventArgs e)
    {
        int UserID = buser.GetLogin().UserID;
        if (!IsPostBack)
        {
            //double HonorMoney_w = bhonor.GetHonorMoneyByState(UserID, 0);
            //double HonorMoney_y = bhonor.GetHonorMoneyByState(UserID, 1);
            //this.lblFmoney.Text = buser.GetLogin().RebatesBalance.ToString("0.00") + "元";
            //this.lblOPro.Text = HonorMoney_y.ToString("0.00") + "元";
            //this.lblSMoney.Text = HonorMoney_w.ToString("0.00") + "元";
            //this.Label1.Text = bredindulgence.GetRedCount(UserID, 0).ToString() + "封";
            //DataTable reds = bredEnve.GetSelectByUserId(UserID);
            //if (reds != null && reds.Rows.Count > 0)
            //{
            //    this.Label2.Text = reds.Rows.Count + "次";
            //}
            //else
            //{
            //    this.Label2.Text = "暂未申请";
            //}
            //BindSmellMoney();
            //MyBind();
            //BindRedEnve();
            //ReadXml();
        }
    }

    private void BindRedEnve()
    {
        //List<M_Honor> honors = bhonor.GetSelectByUserid(buser.GetLogin().UserID);
        //if (honors != null && honors.Count > 0)
        //{
        //    UpdatePanel1.Visible = true;
        //    lbltip.Visible = false;
        //}
        //else
        //{
        //    lbltip.Visible = true;
        //    UpdatePanel1.Visible = false;
        //}
      
    }

    //判断帐户信息是否已填写
    private bool ValiAccount(M_Accountinfo acc)
    {
        if (string.IsNullOrEmpty(acc.BankOfDeposit))
        {
            return false;
        }
        if (string.IsNullOrEmpty(acc.NameOfDeposit))
        {
            return false;
        }
        if (string.IsNullOrEmpty(acc.Account))
        {
            return false;
        }
        if (string.IsNullOrEmpty(acc.CardID))
        {
            return false;
        }
        if (string.IsNullOrEmpty(acc.Name))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 小额兑换判断
    /// </summary>
    private void BindSmellMoney()
    {
        M_Accountinfo acc = bacc.GetSelectByuserId(buser.GetLogin().UserID);
        if (acc != null && acc.id > 0)
        {
            if (acc.Lock == 1)
            {
                if (ValiAccount(acc))
                {
                    if (buser.GetLogin().RebatesBalance > 10)
                    {
                        //List<M_Honor> dt = bhonor.GetSelectByUserid(buser.GetLogin().UserID);
                        //if (dt == null || dt.Count <= 0)
                        //{
                        //    lblTips.Visible = false;
                        //    UpdatePanel2.Visible = true;
                        //    BindddGife();
                        //    BindGifeCard();
                        //}
                        //else
                        //{
                        //    lblTips.Visible = true;
                        //    UpdatePanel2.Visible = false;
                        //}
                    }
                    else
                    {
                        lblTips.Visible = true;
                        UpdatePanel2.Visible = false;
                    }
                }
                else
                {
                    lblTips.Text = "请填写收款信息";

                    lblTips.Visible = true;
                    UpdatePanel2.Visible = false;
                }
            }
            else
            {
                lblTips.Text = "请绑定您的真实姓名";
                lblTips.Visible = true;
                UpdatePanel2.Visible = false;
            }
        }
        else
        {
            lblTips.Text = "请填写收款信息";
            lblTips.Visible = true;
            UpdatePanel2.Visible = false;
        }
    }

    private void BindddGife()
    {
        ddGife.Items.Clear();
        for (int i = 1; i <= 10; i++)
        {
            ddGife.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    public void MyBind()
    {
        ReadXml();
        M_Accountinfo acc = bacc.GetSelectByuserId(buser.GetLogin().UserID);
        if (acc != null && acc.id > 0)
        {
            if (acc.Lock == 1)
            {
                if (ValiAccount(acc))
                {
                    double cashMon = DataConverter.CDouble(ViewState["cash"]);
                    if (cashMon <= 0) { cashMon = 50; }
                    int num = DataConverter.CLng(((buser.GetLogin().RebatesBalance - (buser.GetLogin().RebatesBalance % cashMon)) / cashMon).ToString());
                    ListItem item = new ListItem();
                    if (buser.GetLogin().RebatesBalance > cashMon)
                    {
                        for (int i = 1; i <= num; i++)
                        {
                            ddlAmount.Items.Add(new ListItem(i * cashMon + "元", (i * cashMon).ToString()));
                        }
                        ddlAmount.Enabled = true;
                        hfAccount.Visible = false;
                        Button1.Disabled = false;
                        //Button1.Enabled = true;
                    }
                    else
                    {
                        ddlAmount.Items.Add(new ListItem("不符合兑换条件", "0"));
                        hfAccount.Visible = false;
                    }
                }
                else
                {
                    ddlAmount.Items.Add(new ListItem("请填写您的收款信息", "0"));
                    hfAccount.NavigateUrl = "Accountinfo.aspx";
                    hfAccount.Visible = true;
                }
            }
            else
            {
                ddlAmount.Items.Add(new ListItem("请绑定您的真实姓名", "0"));
                hfAccount.NavigateUrl = "Accountinfo.aspx";
                hfAccount.Visible = true;
            }
        }
        else
        {
            ddlAmount.Items.Add(new ListItem("请填写您的收款信息", "0"));
            hfAccount.NavigateUrl = "Accountinfo.aspx";
            hfAccount.Visible = true;
        }
    }
    private float GetPayInfo(M_Accountinfo acc)
    {
        if (acc != null && acc.id > 0)
        {
            M_PayPlat mpayplat = bpay.GetPayPlatByid(acc.PayId);
            if (mpayplat != null && mpayplat.PayPlatID > 0)
            {
                return mpayplat.Rate;
            }
        }
        return 0.0f;
    }

    //申请红包
    protected void btnOrder_Click(object sender, EventArgs e)
    {
        ReadXml();
        M_Accountinfo acc = bacc.GetSelectByuserId(buser.GetLogin().UserID);
        float payRate = GetPayInfo(acc);
        string lastOrder = bredEnve.GetSelelastTimeByUserid(buser.GetLogin().UserID);

        if (!string.IsNullOrEmpty(lastOrder) && DataConverter.CDate(lastOrder).Month == DateTime.Now.Month)
        {
            function.WriteErrMsg("对不起,本月您已申请红包");
            return;
        }
        if (!string.IsNullOrEmpty(txtData.Text.Trim()))
        {
            //M_Honor mhonor = bhonor.GetSelectByOrderData(DataConverter.CDate(txtData.Text));
            //M_Honor mhonor = null;
            //if (mhonor != null && mhonor.Id > 0)
            //{
            //    M_RedEnvelope mredEnv = bredEnve.GetSelectByredId(mhonor.Id);
            //    if (mredEnv != null && mredEnv.id > 0)
            //    {
            //        function.WriteErrMsg("此次兑现已申请红包,请重新输入时间!");
            //    }
            //    else
            //    {
            //        if (mhonor.PayData.AddDays(7) < DateTime.Now)
            //        {
            //            function.WriteErrMsg("该兑现支付时间已超过一星期，无法申请红包!");
            //        }
            //        else
            //        {
            //            Dictionary<int, string> val = (Dictionary<int, string>)ViewState["val"];
            //            if (val != null && val.Count > 0)
            //            {
            //                double provalue = 0;
            //                foreach (int item in val.Keys)
            //                {
            //                    double proval = DataConverter.CDouble(val[item].Split('|')[0]);
            //                    double money = DataConverter.CDouble(val[item].Split('|')[1]);
            //                    if (item == -1 && mhonor.HonorMoney >= proval)
            //                    {
            //                        double moneys = DataConverter.CLng(mhonor.HonorMoney) / DataConverter.CLng(proval) * money;
            //                        mredEnv.DeducFee = moneys * (payRate / 100);
            //                        mredEnv.RedEnvelope = (moneys - mredEnv.DeducFee).ToString();
            //                        break;
            //                    }
            //                    else
            //                    {
            //                        if (mhonor.HonorMoney >= proval && provalue <= proval)
            //                        {
            //                            provalue = proval;
            //                            mredEnv.DeducFee = money * (payRate / 100);
            //                            mredEnv.RedEnvelope = (money - mredEnv.DeducFee).ToString();
            //                        }
            //                    }
            //                }
            //            }
            //            if (DataConverter.CDouble(mredEnv.RedEnvelope) <= 0)
            //            {
            //                function.WriteErrMsg("您不符合申领红包的条件,请查看规则!");
            //                return;
            //            }
            //            mredEnv.PayDate = DateTime.Now;
            //            mredEnv.OrderData = DateTime.Now;
            //            mredEnv.Userid = buser.GetLogin().UserID;
            //            mredEnv.RebateId = mhonor.Id;
            //            int id = bredEnve.GetInsert(mredEnv);
            //            if (id > 0)
            //            {
            //                function.WriteSuccessMsg("已成功发送申请,请等待审核", "RedEnvelopeRecard.aspx");
            //            }
            //            else
            //            {
            //                function.WriteErrMsg("发送失败");
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    function.WriteErrMsg("该时间没有已支付兑现记录,请重新输入!");
            //}
        }
        else
        {
            function.WriteErrMsg("请输入返现申请时间");
        }
    }

    //礼品
    protected void ddGife_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGifeCard();
    }

    private void BindGifeCard()
    {
        DataTable dt = bgcshop.GetSelectByRevateVal(DataConverter.CDouble(ddGife.SelectedValue));
        if (dt != null && dt.Rows.Count > 0)
        {
            ddGifeCard.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                ListItem li = new ListItem();
                li.Value = dr["id"].ToString();
                li.Text = dr["Cardinfo"].ToString();
                ddGifeCard.Items.Add(li);
            }
        }
    }

    //兑换
    protected void btnExCh_Click(object sender, EventArgs e)
    {
        M_Accountinfo acc = bacc.GetSelectByuserId(buser.GetLogin().UserID);
        float payRate = GetPayInfo(acc);
        if (ddtype.Value == "1")  //现金
        {
            int UserID = buser.GetLogin().UserID;
            //M_Honor mhonor = new M_Honor();
            //mhonor.Userid = UserID;
            //mhonor.OrderData = DateTime.Now;
            //mhonor.State = 0;
            //mhonor.Fee = 5 * (payRate / 100);
            //mhonor.HonorMoney = 5 - 5 * (payRate / 100);
            //mhonor.PayData = DataConverter.CDate("9999/1/1 0:00:00");
            //int result = bhonor.GetInsert(mhonor);
            //if (result > 0)
            //{
            //    //buser.UpdateRebatesBalance(UserID, buser.GetLogin().RebatesBalance - 5);
            //   // function.WriteSuccessMsg("小额兑现成功", "Default.aspx");
            // }
        }
        else  //礼品
        {
            M_GiftCard_User carduser = new M_GiftCard_User();
            M_UserInfo info = buser.GetLogin();
            M_GiftCard_shop mshop = bgcshop.GetSelect(DataConverter.CLng(ddGifeCard.SelectedValue));
            carduser.ShopCardId = mshop.id;
            carduser.CardType = 2;
            carduser.UserId = info.UserID;
            string lan = "qwertyuiopasdfghjklzxcvbnm";
            carduser.CardNO = DataSecurity.MakeRandomString(lan, 6) + DateTime.Now.Minute + DateTime.Now.Second;
            carduser.password = DataSecurity.MakeRandomString(lan, 6);
            carduser.CardPass = carduser.password;
            carduser.confirmData = DateTime.Now;
            carduser.confirmState = 1;
            carduser.OrderData = DateTime.Now;
            carduser.State = 0;
            int result = bgcuser.GetInsert(carduser);
            if (result > 0)
            {
                //M_Honor mhonors = new M_Honor();
                //mhonors.HonorMoney = DataConverter.CDouble(ddlAmount.SelectedValue);
                //mhonors.Userid = info.UserID;
                //mhonors.OrderData = DateTime.Now;
                //mhonors.State = 1;
                //mhonors.HonorMoney = 0;
                //mhonors.PayData = DateTime.Now;
                //mhonors.Payinfo = "此为小额兑现为礼品时添加的信息";
                //bhonor.GetInsert(mhonors);

                //MailInfo mailInfo = new MailInfo();
                //mailInfo.IsBodyHtml = true;
                //mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
                //MailAddress address = new MailAddress(info.Email);
                //mailInfo.ToAddress = address;

                //string EmailContent = @" 您好,{$userName}！<br />您已成功申请{$cardinfo}礼品卡一张,<br />
                //    卡号:{$cardno}<br />密码:{$password}<br/>有效期:{$prodata}<br/>请在有效期内使用 ";

                //mailInfo.MailBody = EmailContent.Replace("{$userName}", info.UserName).Replace("{$cardinfo}", mshop.Cardinfo).Replace
                //    ("{$cardno}", carduser.CardNO).Replace("{$password}", carduser.CardPass).Replace("{$prodata}", mshop.Period.ToShortTimeString());
                //mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "_礼品卡兑换";
                //if (SendMail.Send(mailInfo) == SendMail.MailState.Ok)
                //{
                //    info.RebatesBalance = info.RebatesBalance - mshop.rebateVal;
                //    //bool res = buser.UpdateRebatesBalance(info.UserID, info.RebatesBalance);
                //    //function.WriteSuccessMsg("兑换成功,卡号及密码已发送到您的邮箱中,请查收!", "Default.aspx");
                //}
                //else
                //{
                //    function.WriteErrMsg("邮件发送失败,请于管理员联系!");
                //}
                BindSmellMoney();
            }
        }
    }

    /// <summary>
    /// 读取xml内容
    /// </summary>
    /// <returns></returns>
    private void ReadXml()
    {
        Dictionary<int, string> val = new Dictionary<int, string>();
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("../../Config/ProjectConfige.xml"));
        XmlNodeList list = doc.SelectNodes("confige");

        foreach (XmlNode node in list)
        {
            XmlNodeList RedEnvs = node.SelectNodes("data");
            for (int i = 0; i < RedEnvs.Count; i++)
            {
                if (RedEnvs[i].Attributes["type"].Value == "redEnv")
                {
                    XmlNodeList RedEnv = RedEnvs[i].SelectNodes("profect");
                    if (RedEnv != null && RedEnv.Count > 0)
                    {
                        for (int j = 0; j < RedEnv.Count; j++)
                        {
                            int id = DataConverter.CLng(RedEnv[j].Attributes["id"].Value);
                            string proVal = RedEnv[j].Attributes["proVal"].Value;
                            string money = RedEnv[j].Attributes["money"].Value;
                            val.Add(id, proVal + "|" + money);
                        }
                    }
                    XmlNode profects = RedEnvs[i].SelectSingleNode("profects");
                    string proVals = "";
                    string moneys = "";
                    if (profects != null)
                    {
                        proVals = profects.Attributes["proVal"].Value;
                        moneys = profects.Attributes["money"].Value;
                        val.Add(-1, proVals + "|" + moneys);
                    }
                    ViewState["val"] = val;
                    divGu.InnerHtml = RedEnvs[i].SelectSingleNode("regurl").InnerText;
                    divImp.InnerHtml = RedEnvs[i].SelectSingleNode("important").InnerText;
                    break;
                }
                if (RedEnvs[i].Attributes["type"].Value == "smallCash")
                {
                    divSmell.InnerHtml = RedEnvs[i].SelectSingleNode("important").InnerText;
                }
                if (RedEnvs[i].Attributes["type"].Value == "honor")
                {
                    divHonor.InnerHtml = RedEnvs[i].SelectSingleNode("important").InnerText;
                    ViewState["cash"] = RedEnvs[i].SelectSingleNode("cash").InnerText;
                }
            }
        }
    }
}
