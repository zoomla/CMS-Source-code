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
using ZoomLa.Components;
using System.Net.Mail;

using System.Collections.Generic;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.ZLEnum;
namespace ZoomLa.WebSite.Manage.User
{
    public partial class MailList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                B_Admin badmin = new B_Admin();
                badmin.CheckMulitLogin();
                if (!badmin.ChkPermissions("EmailManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                this.TxtSenderName.Text = SiteConfig.SiteInfo.Webmaster;
                this.TxtSenderEmail.Text = SiteConfig.SiteInfo.WebmasterEmail;
            }
        }

        protected void BtnSend_Click(object sender, EventArgs e)
        {
            int num = 0;
            int num2 = 0;
            IList<string[]> userData = this.GetUserData();
            if (userData != null)
            {
                if (userData.Count <= 0)
                {
                    function.WriteErrMsg("没有获得会员邮件地址信息");
                }
                MailInfo mailInfo = this.GetMailInfo();
                foreach (string[] strArray in userData)
                {
                    if (DataValidator.IsEmail(strArray[1]))
                    {
                        mailInfo.ToAddress = new MailAddress(strArray[1], strArray[0]);
                        if (SendMail.Send(mailInfo) == MailState.Ok)
                        {
                            num++;
                        }
                        else
                        {
                            num2++;
                        }
                    }
                }
                function.WriteSuccessMsg("发送成功" + num.ToString() + "封邮件，发送失败" + num2.ToString() + "封邮件");
            }
        }
        /// <summary>
        /// 创建邮件信息
        /// </summary>
        /// <returns></returns>
        private MailInfo GetMailInfo()
        {
            MailInfo info = new MailInfo();
            info.Subject = this.TxtSubject.Text;
            info.MailBody = this.TxtContent.Text;
            string selectedValue = this.RadlPriority.SelectedValue;
            if (selectedValue != null)
            {
                if (!(selectedValue == "0"))
                {
                    if (selectedValue == "1")
                    {
                        info.Priority = MailPriority.Low;
                    }
                    else if (selectedValue == "2")
                    {
                        info.Priority = MailPriority.High;
                    }
                }
                else
                {
                    info.Priority = MailPriority.Normal;
                }
            }
            if (!string.IsNullOrEmpty(this.TxtSenderName.Text))
            {
                info.FromName = this.TxtSenderName.Text;
            }
            if (!string.IsNullOrEmpty(this.TxtSenderEmail.Text))
            {
                info.ReplyTo = new MailAddress(this.TxtSenderEmail.Text);
            }
            return info;
        }
        private IList<string[]> GetUserData()
        {
            B_User bll = new B_User();
            int num;
            string text = string.Empty;
            if (this.RadUserType0.Checked)
            {
                num = 0;
            }
            else if (this.RadUserType2.Checked)
            {
                num = 2;
                text = this.TxtUserName.Text;
                if (string.IsNullOrEmpty(text))
                {
                    function.WriteErrMsg("没有输入指定会员名");
                    this.TxtUserName.Focus();
                    return null;
                }
            }
            else
            {
                num = 3;
                if (string.IsNullOrEmpty(this.TxtEmails.Text))
                {
                    function.WriteErrMsg("没有指定收件人邮件地址");
                    this.TxtEmails.Focus();
                    return null;
                }
                IList<string[]> list = new List<string[]>();
                foreach (string str2 in this.TxtEmails.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    list.Add(new string[] { "", str2 });
                }
                return list;
            }
            return bll.GetUserNameAndEmailList(num, text);
        }
    }
}