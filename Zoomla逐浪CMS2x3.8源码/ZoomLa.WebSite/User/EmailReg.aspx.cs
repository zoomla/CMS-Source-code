using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public partial class User_EmailReg : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_MailManage mailBll = new B_MailManage();
    private string CheckNum { get { return Request.QueryString["CheckNum"] ?? ""; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //function.WriteErrMsg("邮箱注册功能默认关闭");
            if (string.IsNullOrEmpty(CheckNum))
            {
                step1_div.Visible = true;
            }
            else 
            {
                if (Cache[CheckNum] == null) { function.WriteErrMsg("验证码不存在或不正确"); }
                Email_L.Text = Cache[CheckNum].ToString();
                step2_div.Visible = true;
            }

        }
    }
    protected void Sure_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = new M_UserInfo();
        M_Uinfo basemu = new M_Uinfo();
        mu.UserName = UName_T.Text.Trim();
        mu.UserPwd = StringHelper.MD5(Passwd_T.Text.Replace(" ", ""));
        mu.Email = Cache[CheckNum].ToString();
        if (buser.IsExistUName(mu.UserName)) { function.WriteErrMsg("[" + mu.UserName + "]已存在,取消注册"); }
        if (buser.IsExistMail(mu.Email)) { function.WriteErrMsg("[" + mu.Email + "]已存在,取消注册"); }
        if (!string.IsNullOrEmpty(Mobile_T.Text)) 
        {
            if (buser.IsExist("mobile", Mobile_T.Text)) { function.WriteErrMsg("[" + Mobile_T.Text + "]已存在,取消注册"); }
        }
        basemu.Mobile = Mobile_T.Text;
        mu.UserID = buser.Add(mu);
        basemu.UserId = mu.UserID;
        buser.AddBase(basemu);
        buser.SetLoginState(mu);
        function.WriteSuccessMsg("注册成功", "/User/Default.aspx");
    }
    protected void Next_Btn_Click(object sender, EventArgs e)
    {
        Email_T.Text = Email_T.Text.Trim();
        string code = function.GetRandomString(8).ToLower();
        Cache[code] = Email_T.Text;
        //-------------------------------------
        string regurl = SiteConfig.SiteInfo.SiteUrl.TrimEnd('/') + "/User/EmailReg.aspx?CheckNum=" + code;
        MailAddress address = new MailAddress(Email_T.Text);
        MailInfo mailInfo = new MailInfo();
        mailInfo.Subject = "欢迎注册[" + SiteConfig.SiteInfo.SiteName + "]";
        mailInfo.IsBodyHtml = true;
        mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
        mailInfo.ToAddress = address;

        string mailcontent = mailBll.SelByType(B_MailManage.MailType.NewUserReg);
        mailInfo.MailBody = new OrderCommon().TlpDeal(mailcontent, GetRegEmailDt("用户", code, regurl));
        //MailConfig cfg = SiteConfig.MailConfig;
        //function.WriteErrMsg(SendMail.SendEmail(cfg.MailServerUserName, cfg.MailServerPassWord, cfg.MailServer, mailInfo,"".Split(',')).ToString());
        SendMail.Send(mailInfo);
        function.WriteSuccessMsg("注册验证码已成功发送到你的注册邮箱,<a href='" + B_Plat_Common.GetMailSite(Email_T.Text) + "' target='_blank'>请前往邮箱查收并验证</a>!", "", 0);
    }
    //获取邮件内容模板标签格式
    private DataTable GetRegEmailDt(string username, string checknum, string checkurl)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CheckNum");
        dt.Columns.Add("CheckUrl");
        dt.Columns.Add("UserName");
        dt.Rows.Add(dt.NewRow());
        dt.Rows[0]["CheckNum"] = checknum;
        dt.Rows[0]["CheckUrl"] = checkurl;
        dt.Rows[0]["UserName"] = username;
        return dt;
    }
}