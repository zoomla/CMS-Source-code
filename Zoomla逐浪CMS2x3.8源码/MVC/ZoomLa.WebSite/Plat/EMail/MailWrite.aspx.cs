using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Data;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;

namespace ZoomLaCMS.Plat.EMail
{
    public partial class MailWrite : System.Web.UI.Page
    {
        B_Plat_Mail mailBll = new B_Plat_Mail();
        B_Plat_MailConfig configBll = new B_Plat_MailConfig();
        B_User buser = new B_User();
        public int Reply { get { return DataConverter.CLng(Request.QueryString["reply"]); } }
        public string REmail { get { return Request.QueryString["remail"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Reply > 0)
                {
                    M_Plat_Mail model = mailBll.SelReturnModel(Reply);
                    Receiver_T.Text = model.Sender;
                    TxtTitle.Text = "回复：" + model.Title;
                }
                else if (!string.IsNullOrEmpty(REmail))
                {
                    Receiver_T.Text = REmail;
                }
                MyBind();
            }
        }
        void MyBind()
        {
            B_User buser = new B_User();
            Emails_D.DataSource = configBll.SelByUid(buser.GetLogin().UserID);
            Emails_D.DataBind();
            if (Emails_D.Items.Count == 0)
            {
                Emails_D.Items.Insert(0, new ListItem("未绑定邮箱！", "-1"));
            }
        }
        protected void Send_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            M_Plat_MailConfig configMod = configBll.SelByMail(mu.UserID, Emails_D.SelectedItem.Text);
            MailAddress Address = new MailAddress(Receiver_T.Text);
            MailInfo model = new MailInfo();
            model.Subject = TxtTitle.Text;
            model.ToAddress = Address;
            model.MailBody = EditorContent.Text;
            model.IsBodyHtml = true;
            string ppaths = "";
            foreach (string vpath in Attach_Hid.Value.Split('|'))
            {
                if (string.IsNullOrEmpty(vpath)) continue;
                ppaths += function.VToP(vpath) + "|";
            }
            ppaths = ppaths.TrimEnd('|');
            SendMail.SendEmail(configMod.Acount, configMod.Passwd, configMod.SMTP, model, ppaths.Split('|'));
            SaveMail(Attach_Hid.Value);
            function.WriteSuccessMsg("发送成功!", "Default.aspx");
        }
        void SaveMail(string vpaths)
        {
            M_Plat_Mail mailMod = new M_Plat_Mail();
            mailMod.MailType = 1;
            mailMod.MailDate = DateTime.Now;
            mailMod.Title = TxtTitle.Text;
            mailMod.Sender = Emails_D.SelectedItem.Text;
            mailMod.Receiver = Receiver_T.Text;
            mailMod.UserID = buser.GetLogin().UserID;
            mailMod.Attach = vpaths;
            mailMod.CDate = DateTime.Now;
            mailMod.Content = EditorContent.Text;
            mailMod.Status = 1;
            mailBll.Insert(mailMod);
        }
    }
}