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
using ZoomLa.Common;
using ZoomLa.Components;
using System.IO;
using ZoomLa.Model;

namespace ZoomLaCMS.Manage.Config
{
    public partial class MailConfig : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "SiteConfig"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            if (!IsPostBack)
            {
                //MailName_T.Text = SiteConfig.MailConfig.MailFrom;
                MailName_T.Text = SiteConfig.MailConfig.MailServerUserName;
                MailPwd_T.Attributes.Add("value", SiteConfig.MailConfig.MailServerPassWord);
                //
                SMTP_T.Text = SiteConfig.MailConfig.MailServer;
                MailPort_T.Text = SiteConfig.MailConfig.Port.ToString();
                TextBox4.Text = SiteConfig.MailConfig.MailServerList;

                //if (SiteConfig.MailConfig.AuthenticationType.ToString().Equals("None")) { RadioButton1.Checked = true; }
                //if (SiteConfig.MailConfig.AuthenticationType.ToString().Equals("Basic")) { RadioButton2.Checked = true; }
                //if (SiteConfig.MailConfig.AuthenticationType.ToString().Equals("Ntlm")) { RadioButton3.Checked = true; }
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='SiteInfo.aspx'>系统设置</a></li><li><a href='SiteInfo.aspx'>网站配置</a></li><li class=\"active\">邮件参数配置</li>" + Call.GetHelp(5));
            }
        }
        protected void Save_Btn_Click(object sender, EventArgs e)
        {
            SiteConfig.MailConfig.MailFrom = MailName_T.Text;
            SiteConfig.MailConfig.MailServer = SMTP_T.Text;
            SiteConfig.MailConfig.Port = Convert.ToInt32(MailPort_T.Text);
            //if (RadioButton1.Checked) { SiteConfig.MailConfig.AuthenticationType = SendMail.AuthenticationType.None; }
            //if (RadioButton2.Checked) { SiteConfig.MailConfig.AuthenticationType = SendMail.AuthenticationType.Basic; }
            //if (RadioButton3.Checked) { SiteConfig.MailConfig.AuthenticationType = SendMail.AuthenticationType.Ntlm; }

            SiteConfig.MailConfig.MailServerUserName = MailName_T.Text;
            SiteConfig.MailConfig.MailServerPassWord = MailPwd_T.Text;
            SiteConfig.MailConfig.MailServerList = TextBox4.Text;
            try
            {
                SiteConfig.Update();
                function.WriteSuccessMsg("邮件参数配置保存成功！", "MailConfig.aspx");
            }
            catch (FileNotFoundException)
            {
                function.WriteErrMsg("文件未找到！", "MailConfig.aspx");
            }
            catch (UnauthorizedAccessException)
            {
                function.WriteErrMsg("检查您的服务器是否给配置文件或文件夹配置了写入权限。", "MailConfig.aspx");
            }
        }
    }
}