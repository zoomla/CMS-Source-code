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
using MSXML2;
using System.Net;
using System.Text;

public partial class User_ChangeMP : Page
{
    B_User buser = new B_User();
    B_MailManage mailBll = new B_MailManage();
    RegexHelper regHelp = new RegexHelper();
    private string CheckNum { get { return ViewState["Mail_CheckNum"] as string; } set { ViewState["Mail_CheckNum"] = value; } }
    private string NewCheckNum { get { return ViewState["Mail_NewCheckNum"] as string; } set { ViewState["Mail_NewCheckNum"] = value; } }
    private string NewMobile { get { return ViewState["Mail_NewMobile"] as string; } set { ViewState["Mail_NewMobile"] = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            M_Uinfo basemu = buser.GetUserBaseByuserid(buser.GetLogin().UserID);
            SMobile_T.Text = basemu.Mobile;
            if (string.IsNullOrEmpty(basemu.Mobile))
            {
                step2_div.Visible = true;
            }
            else { step1_div.Visible = true; }
        }
    }
    //发送校验号
    protected void SendEMail_Btn_Click(object sender, EventArgs e)
    {
        M_Uinfo basemu = buser.GetUserBaseByuserid(buser.GetLogin().UserID);
        CheckNum = function.GetRandomString(6, 2).ToLower();
        //CheckNum = "111111";
        if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], VCode.Text))
        {
            ShowAlert("验证码不正确"); return;
        }
        else
        {
            string content = "【" + SiteConfig.SiteInfo.SiteName + "】你正在使用修改手机号服务,校验码:" + CheckNum;
            SendWebSMS.SendMessage(basemu.Mobile, content);
            M_Message messInfo = new M_Message();
            messInfo.Title = "验证码:修改手机号";
            messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToLocalTime().ToString());
            messInfo.Content = content;
            messInfo.Receipt = "";
            messInfo.MsgType = 2;
            messInfo.status = 1;
            messInfo.Incept = basemu.UserId.ToString();
            B_Message.Add(messInfo);
            ShowInfo("校验码已成功发送到你的手机");
        }
    }
    protected void Next_Btn_Click(object sender, EventArgs e)
    {
        CheckNum_T.Text = CheckNum_T.Text.Trim();
        if (string.IsNullOrEmpty(CheckNum)) { ShowAlert("校验码不存在,请重新发送校验码"); return; }
        if (!CheckNum_T.Text.Equals(CheckNum)) { ShowAlert("校验码不匹配"); return; }
        step1_div.Visible = false;
        step2_div.Visible = true;
        ShowInfo("<span style='color:green;'>原手机号校验成功,请输入您的新手机号</span>");
    }
    //-----------------Step2
    protected void SendNewEmail_Btn_Click(object sender, EventArgs e)
    {
        M_Uinfo basemu = buser.GetUserBaseByuserid(buser.GetLogin().UserID);
        NewMobile = NewMobile_T.Text.Trim();
        if (buser.IsExist("ume", NewMobile)) { ShowAlert("该手机号已存在"); return; }
        NewCheckNum = function.GetRandomString(6, 2).ToLower();
        //NewCheckNum = "111111";
        if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["NewVCode_hid"], NewVCode.Text))
        {
            ShowAlert("验证码不正确" + Request.Form["NewVCode_hid"] + ":" + NewVCode.Text); return;
        }
        else
        {
            string content = "【" + SiteConfig.SiteInfo.SiteName + "】校验码:" + NewCheckNum;
            SendWebSMS.SendMessage(basemu.Mobile, content);
            M_Message messInfo = new M_Message();
            messInfo.Title = "验证码:修改手机号";
            messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToLocalTime().ToString());
            messInfo.Content = content;
            messInfo.Receipt = "";
            messInfo.MsgType = 2;
            messInfo.status = 1;
            messInfo.Incept = basemu.UserId.ToString();
            B_Message.Add(messInfo);
            ShowInfo("校验码已成功发送到你的新手机!");
        }
    }
    //使用新的手机号
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(NewCheckNum)) { ShowAlert("校验码不存在,请重新发送校验码"); return; }
        if (!NewCheckNum_T.Text.Equals(NewCheckNum)) { ShowAlert("校验码不匹配"); return; }
        if (buser.IsExist("ume", NewMobile)) { ShowAlert("该手机号已存在"); return; }
        if (!regHelp.IsMobilPhone(NewMobile)) { ShowAlert("手机格式不正确"); return; }
        M_Uinfo basemu = buser.GetUserBaseByuserid(buser.GetLogin().UserID);
        basemu.Mobile = NewMobile;
        buser.UpdateBase(basemu);
        function.WriteSuccessMsg("修改手机号成功", "/User/Info/UserInfo.aspx");
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

