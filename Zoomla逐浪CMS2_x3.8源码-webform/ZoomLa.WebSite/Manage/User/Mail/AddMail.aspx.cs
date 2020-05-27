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
using System.Collections.Generic;
using ZoomLa.Common;
using ZoomLa.BLL;

using ZoomLa.Components;
using System.Net.Mail;
using ZoomLa.Model;
using System.Text;
using System.Threading;

public partial class manage_Qmail_AddMail : CustomerPageAction
{
    B_MailManage mmbll = new B_MailManage();
    M_MailInfo mm = new M_MailInfo();
    B_MailInfo bm = new B_MailInfo();
    B_Subscribe bs = new B_Subscribe();
    B_MailIdiograph mibll = new B_MailIdiograph();

    MailMessage mailx;
    SmtpClient clientx;
    AddRunlist runlist = new AddRunlist();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtSend.Enabled = true;
            if (Request["item"] != null && Request["item"] != "")
            {
                string[] ids = Request["item"].Split(',');
                string emails = "";
                B_User bu = new B_User();
                for (int i = 0; i < ids.Length; i++)
                {
                    if (i == 0)
                    {
                        emails = bu.GetUserByUserID(DataConverter.CLng(ids[i])).Email;
                    }
                    else
                    {
                        string usermail =  bu.GetUserByUserID(DataConverter.CLng(ids[i])).Email;
                        string tempemail = "," + emails + ",";
                        if (tempemail.IndexOf("," + usermail + ",") == -1)
                        {
                            emails += "," + bu.GetUserByUserID(DataConverter.CLng(ids[i])).Email;
                        }
                    }
                }
                if (emails != null && emails!="")
                {
                    txtSend.Text = emails;
                }
            }
            DataTable dt = mibll.GetState("1");
            DataRow dr = dt.NewRow();
            dr["Name"] = "不使用签名";
            dr["Context"] = "";
            dt.Rows.InsertAt(dr, 0);
            ddlMailIdiograph.DataSource = dt;
            ddlMailIdiograph.DataBind();
            if (!(Request["item"] != null && Request["item"] != ""))
            {

                GetUserData(mmbll.GetSend());
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "User/UserManage.aspx'>用户管理</a></li><li>订阅群发</li>");
        }
    }

    protected void BtnSend_Click(object sender, EventArgs e)
    {
        int num = 0;
        int num2 = 0;

        string temp = "<html>\n<title></title>\n<body>\n<TABLE border=0 width='100%' align=center>\n<TBODY>\n<TR>\n<TD valign=middle align=top>\n内容\n<center><font color=red>签名</font>\n</TD></TR></TBODY></TABLE><br><hr width=100% size=1>\n</body>\n</html>";
        //DataSecurity.HtmlDecode(
        mm.MailAddTime = DateTime.Now;
        mm.MailContext = temp.Replace("内容", this.HiddenField1.Value).Replace("签名", this.ddlMailIdiograph.SelectedValue);
        mm.MailTitle = this.TxtSubject.Text;
        mm.MailAddRees = this.txtSend.Text;

        if (rblSendType.SelectedIndex == 0)//立即发送
        {
            string[] userData = this.txtSend.Text.Split(new char[] { ',' });
            if (userData != null)
            {
                if (userData.Length <= 0)
                {
                    function.WriteErrMsg("没有获得会员邮件地址信息");
                }
                MailInfo mailInfo = this.GetMailInfo();
                foreach (string strArray in userData)
                {
                    if (DataValidator.IsEmail(strArray))
                    {
                        mailInfo.ToAddress = new MailAddress(strArray, null);
                        if (SendMail(strArray))
                        //if (SendMail.Send(mailInfo) == MailState.Ok)
                        {
                            num++;
                        }
                        else
                        {
                            num2++;
                        }
                    }
                }
                mm.MailState = true;
                mm.MailSendTime = DateTime.Now;
                bm.GetInsert(mm);
                function.WriteSuccessMsg("发送成功" + num.ToString() + "封邮件，发送失败" + num2.ToString() + "封邮件", "../SendMailList.aspx");
            }
        }
        else//定时发送
        {
            mm.MailState = false;
            mm.MailSendTime = DateTime.Parse(this.txtSendTime.Text + " " + ddlHour.SelectedValue + ":" + ddlMinute.SelectedValue + ":00");
            runlist.AddMail(mm);
            bm.GetInsert(mm);
        }
        function.WriteSuccessMsg("邮件信息已经成功添加！", "../SendMailList.aspx");
    }
    // Message发送邮件,成功true
    public bool SendMail(string ToMail)
    {
        if (!string.IsNullOrEmpty(ToMail))
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ToMail);

            mail.From = new MailAddress(SiteConfig.MailConfig.MailFrom, SiteConfig.SiteInfo.SiteName, Encoding.UTF8);

            mail.Subject = mm.MailTitle;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.Body = mm.MailContext;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            //SMTP
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(SiteConfig.MailConfig.MailServerUserName, SiteConfig.MailConfig.MailServerPassWord);
            client.Host = SiteConfig.MailConfig.MailServer;

            object userState = mail;

            try
            {
                this.mailx = mail;
                this.clientx = client;
                Thread m_thread = new System.Threading.Thread(new System.Threading.ThreadStart(sendmail));
                m_thread.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }
        return false;
    }

    private void sendmail()
    {
        MailMessage mail = this.mailx;
        SmtpClient client = this.clientx;
        clientx.Send(mailx);
    }

    // 创建邮件信息
    private MailInfo GetMailInfo()
    {
        MailInfo info = new MailInfo();
        info.Subject = mm.MailTitle;
        info.MailBody = mm.MailContext;

        info.Priority = MailPriority.Normal;
        info.FromName = SiteConfig.SiteInfo.SiteName;
        info.ReplyTo = new MailAddress(SiteConfig.SiteInfo.WebmasterEmail);

        return info;
    }

    #region 设置收件人
    private void GetUserData(DataTable dt)
    {
        string str = "";
        foreach (DataRow dr in dt.Rows)
        {
            str += dr["Email"].ToString() + ",";
        }
        if (str.EndsWith(","))
        {
            str = str.Substring(0, str.Length - 1);
        }
        txtSend.Text = str;

    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetUserData(mmbll.GetSend());
    }
    #endregion

    //设置定时
    protected void rblSendType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblSendType.SelectedIndex == 0)
        {
            td1.Visible = false;
        }
        else
        {
            td1.Visible = true;
            ddlHour.Items.Clear();
            for (int i = 0; i < 24; i++)
                ddlHour.Items.Add(new ListItem(i.ToString(), i.ToString()));
            ddlMinute.Items.Clear();
            for (int i = 0; i < 60; i++)
                this.ddlMinute.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }
}
