using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.API;
using ZoomLa.DZNT;
using ZoomLa.SQLDAL;
using System.Net.Mail;
using ZoomLa.BLL.Plat;

public partial class User_ChangeEmail :Page
{
    
    B_User buser = new B_User();
    B_MailManage mailBll=new B_MailManage();
    RegexHelper regHelp = new RegexHelper();
    private string CheckNum { get { return ViewState["Mail_CheckNum"] as string; } set { ViewState["Mail_CheckNum"] = value; } }
    private string NewCheckNum { get { return ViewState["Mail_NewCheckNum"] as string; } set { ViewState["Mail_NewCheckNum"] = value; } }
    private string NewEmail { get { return ViewState["Mail_NewMail"] as string; } set { ViewState["Mail_NewMail"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin();
            SEmail_T.Text = mu.Email;
            if (mu.Email.Contains("@random")) //随机生成的则可直接改
            {
                step2_div.Visible = true;
            }
            else { step1_div.Visible = true; }
        }
    }
    //发送验证邮件
    protected void SendEMail_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        CheckNum = function.GetRandomString(8).ToLower();
        //-------------------------------------
        //string regurl = SiteConfig.SiteInfo.SiteUrl.TrimEnd('/') + "/User/ChangeEmail.aspx?CheckNum=" + code;
        string mailcontent = "您好，您正在<a href='" + SiteConfig.SiteInfo.SiteUrl + "'>" + SiteConfig.SiteInfo.SiteName + "</a>网站修改邮箱，您本次的验证码为：" + CheckNum;
        MailInfo mailInfo = SendMail.GetMailInfo(mu.Email,SiteConfig.SiteInfo.SiteName, "修改邮箱[" + SiteConfig.SiteInfo.SiteName + "]", mailcontent);
        SendMail.Send(mailInfo);
        ShowInfo("注册验证码已成功发送到你的注册邮箱,<a href='" + B_Plat_Common.GetMailSite(mu.Email) + "' target='_blank'>请前往邮箱查收并验证</a>!");
    }
    protected void Next_Btn_Click(object sender, EventArgs e)
    {
        CheckNum_T.Text = CheckNum_T.Text.Trim();
        if (string.IsNullOrEmpty(CheckNum)) { ShowAlert("验证码不存在,请重新发送验证码"); return; }
        if (!CheckNum_T.Text.Equals(CheckNum)) { ShowAlert("验证码不匹配"); return; }
        step1_div.Visible = false;
        step2_div.Visible = true;
        ShowInfo("请填入您的新邮箱,并完成验证");
    }
    //-----------------Step2
    protected void SendNewEmail_Btn_Click(object sender, EventArgs e)
    {
        NewEmail = NewEmail_T.Text.Trim();
        if (buser.IsExistMail(NewEmail)) { ShowAlert("该邮箱已存在"); NewEmail = ""; return; }
        NewCheckNum = function.GetRandomString(8).ToLower();
        string mailcontent = "您好，您正在<a href='" + SiteConfig.SiteInfo.SiteUrl + "'>" + SiteConfig.SiteInfo.SiteName + "</a>网站修改邮箱，您本次的验证码为：" + NewCheckNum;
        MailInfo mailInfo = SendMail.GetMailInfo(NewEmail, SiteConfig.SiteInfo.SiteName, "修改邮箱[" + SiteConfig.SiteInfo.SiteName + "]", mailcontent);
        SendMail.Send(mailInfo);
        ShowInfo("注册验证码已成功发送到你的注册邮箱,<a href='" + B_Plat_Common.GetMailSite(NewEmail) + "' target='_blank'>请前往邮箱查收并验证</a>!");
    }
    //确定新邮箱
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(NewCheckNum)) { ShowAlert("验证码不存在,请重新发送验证码"); return; }
        if (!NewCheckNum_T.Text.Equals(NewCheckNum)) { ShowAlert("验证码不匹配"); return; }
        if (buser.IsExistMail(NewEmail)) { ShowAlert("该邮箱已存在"); return; }
        if (!regHelp.IsEmail(NewEmail)) { ShowAlert("邮箱格式不正确"); return; }
        M_UserInfo mu = buser.GetLogin();
        mu.Email = NewEmail;
        buser.UpdateByID(mu);
        function.WriteSuccessMsg("修改邮箱成功", "/User/Info/UserInfo.aspx");
    }
    //-----------------Tools
    private void ShowAlert(string msg) 
    {
        Remind_Div.Visible = true;
        Remind_Div.Attributes["class"] = "alert alert-danger";
        Remind_Div.InnerHtml = msg;
    }
    private void ShowInfo(string msg)
    {
        Remind_Div.Visible = true;
        Remind_Div.Attributes["class"] = "alert alert-info";
        Remind_Div.InnerHtml = msg;
    }
}