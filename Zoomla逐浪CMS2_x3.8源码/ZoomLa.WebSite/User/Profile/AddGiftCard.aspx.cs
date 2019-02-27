using System;
using System.Collections.Generic;
using System.Web;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;


public partial class User_Profile_AddGiftCard : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_GiftCard_shop bgcshop = new B_GiftCard_shop();
    B_GiftCard_User bgcuser = new B_GiftCard_User();

    protected void Page_Load(object sender, EventArgs e)
    {
        buser.CheckIsLogin();
        if (!IsPostBack)
        {
            string cardid = Request.Form["CardId"];
            int cardType = DataConverter.CLng(Request.Form["CardType"]);
            string returnUrl = Request.Form["returnUrl"];
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/User/Profile/ExChangeRecord.aspx" : returnUrl;

            M_UserInfo info = buser.GetLogin();
            M_GiftCard_shop mshop = bgcshop.GetSelect(DataConverter.CLng(cardid));
            if (mshop!=null&&mshop.Points > 0 && mshop.rebateVal > 0 && info.GroupID == 1)
            {
                Response.Write("<script>alert('您为普通会员，不可兑换非免费礼品卡');location.href='" + returnUrl + "'</script>");
            }
            else
            {
                if (cardType == 1)  //积分
                {
                    if (info.UserExp < mshop.Points)
                    {
                        Response.Write("<script>alert('您的可用积分为" + info.UserExp + ",不够兑换!');location.href='" + returnUrl + "'</script>");
                        return;
                    }
                }
                else  //返利
                {
                    if (info.RebatesBalance < mshop.rebateVal)
                    {
                        Response.Write("<script>alert('您的可用返利为" + info.RebatesBalance + ",不够兑换!');location.href='" + returnUrl + "'</script>");
                        return;
                    }
                }

                M_GiftCard_User carduser = new M_GiftCard_User();
                carduser.ShopCardId = DataConverter.CLng(cardid);
                carduser.CardType = cardType;
                carduser.UserId = info.UserID;
                string lan = "qwertyuiopasdfghjklzxcvbnm";
                carduser.CardNO = DataSecurity.MakeRandomString(lan, 6) + DateTime.Now.Minute + DateTime.Now.Second;
                carduser.password = DataSecurity.MakeRandomString(lan, 6);
                carduser.CardPass = carduser.password;
                carduser.confirmData = DataConverter.CDate("9999/1/1 0:00:00");
                carduser.confirmState = 0;
                carduser.OrderData = DateTime.Now;
                carduser.State = 0;
                int result = bgcuser.GetInsert(carduser);

                if (result > 0)
                {
                    //if (count > 0)
                    //{
                    //    MailInfo mailInfo = new MailInfo();
                    //    mailInfo.IsBodyHtml = true;
                    //    mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
                    //    MailAddress address = new MailAddress(info.Email);
                    //    mailInfo.ToAddress = address;

                    //    string EmailContent = @" 您好,{$userName}！<br />您已成功申请{$cardinfo}礼品卡一张,<br />
                    //卡号:{$cardno}<br />密码:{$password}<br/>有效期:{$prodata}<br/>请在有效期内使用 ";

                    //    mailInfo.MailBody = EmailContent.Replace("{$userName}", info.UserName).Replace("{$cardinfo}", mshop.Cardinfo).Replace
                    //        ("{$cardno}", carduser.CardNO).Replace("{$password}", carduser.CardPass).Replace("{$prodata}", mshop.Period.ToShortTimeString());
                    //    mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "_礼品卡兑换";
                    //    if (SendMail.Send(mailInfo) == SendMail.MailState.Ok)
                    //    {
                    //        Response.Write("<script>alert('兑换成功,卡号及密码已发送到您的邮箱中,请查收!');location.href='" + returnUrl + "'</script>");
                    //    }
                    //    else
                    //    {

                    //        Response.Write("<script>alert('邮件发送失败,请于管理员联系!');location.href='" + returnUrl + "'</script>");
                    //    }
                    //    if (cardType == 1)  //积分
                    //    {
                    //        //info.UserExp = info.UserExp - mshop.Points;
                    //        //bool res = buser.UpdateUserExp((int)info.UserExp, info.UserID, info.GroupID);
                    //    }
                    //    else
                    //    {
                    //        //info.RebatesBalance = info.RebatesBalance - mshop.rebateVal;
                    //        //bool res = buser.UpdateRebatesBalance(info.UserID, info.RebatesBalance);
                    //    }
                    //}
                    //else
                    //{
                    //    Response.Write("<script>alert('兑换申请已提交，您无购物返利记录,请等待审核!');location.href='" + returnUrl + "'</script>");
                    //}
                }
                else
                {
                    Response.Write("<script>alert('兑换失败!');location.href='" + returnUrl + "'</script>");
                }
            }
        }
    }
}
