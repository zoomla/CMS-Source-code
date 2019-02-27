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
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.API;
using ZoomLa.DZNT;
using System.Data.OleDb;
using System.Net.Mail;
using ZoomLa.BLL.Plat;

namespace ZoomLa.WebSite.User
{
    /// <summary>
    /// 支持邮件找回,问题找回,手机找回
    /// </summary>
    public partial class User_GetPassword : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_MailManage mailBll = new B_MailManage();
        //密码找回方式
        private string Method { get { return (Request.QueryString["method"] ?? "").ToLower(); } }
        private int Uid { get { return DataConverter.CLng(Request.QueryString["uid"]); } }
        private string Key { get { return Request.QueryString["key"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Key) && Uid > 0)//通过邮件校验找回
                {
                    M_UserInfo mu = buser.SelReturnModel(Uid);
                    if (mu.IsNull) { function.WriteErrMsg("用户不存在"); }
                    if (string.IsNullOrEmpty(mu.seturl)) { function.WriteErrMsg("用户未发起找回密码"); }
                    if (!mu.seturl.Equals(Key)) { function.WriteErrMsg("key值不正确"); }
                    Final_Div.Visible = true;
                }
                else 
                {
                    switch (Method)
                    {
                        case "answer":
                            Answer_Div.Visible = true;
                            break;
                        default:
                            Email_Div.Visible = true;
                            break;
                    }
                }
            }
        }
        protected void SendMsg_Btn_Click(object sender, EventArgs e)
        {
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], VCode.Text))
            {
                function.WriteErrMsg("验证码不正确", "/User/GetPassword.aspx");
            }
            string UserName = TxtUserName.Text.Trim();
            M_UserInfo mu = buser.GetUserByName(UserName);
            M_Uinfo basemu = buser.GetUserBaseByuserid(mu.UserID);
            if (mu.IsNull) { function.WriteErrMsg("[" + UserName + "]用户不存在"); }
            if (string.IsNullOrEmpty(basemu.Mobile)) { function.WriteErrMsg("用户未设置手机号,无法通过手机号找回"); }
            string code = function.GetRandomString(6, 2);
            string content = "【" + SiteConfig.SiteInfo.SiteName + "】,你正在使用找回密码服务,校验码:" + code;
            SendWebSMS.SendMessage(basemu.Mobile, content);
            //短信信息存入数据库
            M_Message messInfo = new M_Message();
            messInfo.Title = "验证码:找回密码";
            messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToLocalTime().ToString());
            messInfo.Content = content;
            messInfo.Receipt = "";
            messInfo.MsgType = 3;
            messInfo.status = 1;
            messInfo.Incept = mu.UserID.ToString();
            B_Message.Add(messInfo);

            mu.seturl = code;
            buser.UpdateByID(mu);
            HideAll();
            Mobile2_Div.Visible = true;
        }
        protected void SendMail_Btn_Click(object sender, EventArgs e)
        {
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], VCode.Text))
            {
                function.WriteErrMsg("验证码不正确", "/User/GetPassword.aspx");
            }
            string UserName = TxtUserName.Text.Trim();
            M_UserInfo mu = buser.GetUserByName(UserName);
            if (mu.IsNull) { function.WriteErrMsg("[" + UserName + "]用户不存在"); }
            if (string.IsNullOrEmpty(mu.Email) || mu.Email.Contains("@random")) { function.WriteErrMsg("用户未设置邮箱,无法通过邮箱找回"); }
            //生成Email验证链接
            string seturl = function.GetRandomString(12) + "," + DateTime.Now.ToString();
            mu.seturl = seturl;
            buser.UpDateUser(mu);
            //Email发送
            string url = SiteConfig.SiteInfo.SiteUrl + "/User/GetPassWord.aspx?key=" + mu.seturl + "&uid=" + mu.UserID;
            string returnurl = "<a href=\"" + url + "\" target=\"_blank\">" + url + "</a>";
            string content = mailBll.SelByType(B_MailManage.MailType.RetrievePWD);
            content = new OrderCommon().TlpDeal(content, GetPwdEmailDt(mu.UserName, SiteConfig.SiteInfo.SiteName, returnurl));
            MailInfo mailInfo = SendMail.GetMailInfo(mu.Email, SiteConfig.SiteInfo.SiteName, SiteConfig.SiteInfo.SiteName + "_找回密码", content);
            SendMail.Send(mailInfo);
            function.WriteSuccessMsg("密码重设请求提交成功,<a href='" + B_Plat_Common.GetMailSite(mu.Email) + "' target='_blank'>请前往邮箱查收</a>!!", "", 0);
        }
        private DataTable GetPwdEmailDt(string username, string sitename, string returnurl)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserName");
            dt.Columns.Add("SiteName");
            dt.Columns.Add("ReturnUrl");
            dt.Rows.Add(dt.NewRow());
            dt.Rows[0]["UserName"] = username;
            dt.Rows[0]["SiteName"] = sitename;
            dt.Rows[0]["ReturnUrl"] = returnurl;
            return dt;
        }
        protected void SureAnswer_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetUserByName(TxtUserName.Text);
            if (mu.IsNull) { function.WriteErrMsg("[" + TxtUserName.Text + "]用户不存在"); }
            if (string.IsNullOrEmpty(mu.Answer) || string.IsNullOrEmpty(mu.Question)) { function.WriteErrMsg("用户未设置问答内容,无法通过问答找回"); }
            if (mu.Answer.Equals(Answer_T.Text.Trim()))
            {
                HideAll();
                Final_Div.Visible = true;
            }
            else
            {
                function.WriteErrMsg("密码提示答案不正确", "~/User/GetPassword.aspx");
            }
        }
        //验证手机号
        protected void ValidMobile_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetUserByName(TxtUserName.Text);
            if (mu.IsNull) { function.WriteErrMsg("用户不存在"); }
            if (string.IsNullOrEmpty(mu.seturl)) { function.WriteErrMsg("校验码不存在"); }
            if (!CheckCode_T.Text.Equals(mu.seturl)) { function.WriteErrMsg("校验码不正确"); }
            HideAll();
            Final_Div.Visible = true;
        }
        protected void Final_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetUserByName(TxtUserName.Text);
            if (Uid > 0) { mu = buser.SelReturnModel(Uid); }
            if (mu.IsNull) { function.WriteErrMsg("[" + TxtUserName.Text + "]用户不存在"); }
            if (!TxtPassword.Text.Equals(TxtConfirmPassword.Text)) { function.WriteErrMsg("两次输入密码不一致"); }
            mu.UserPwd = StringHelper.MD5(TxtPassword.Text);
            mu.seturl = "";
            buser.UpDateUser(mu);
            function.WriteSuccessMsg("密码修改成功!", "/User/Login.aspx");
        }
        private void HideAll()
        {
            Email_Div.Visible = false;
            Answer_Div.Visible = false;
            Mobile2_Div.Visible = false;
            Final_Div.Visible = false;
        }
    }
}