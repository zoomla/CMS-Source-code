using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using jmail;
using ZoomLa.Model;
using ZoomLa.BLL;
using System.IO;
using ZoomLa.SQLDAL;
namespace ZoomLaCMS.MIS.Mail
{
    public partial class MailView : System.Web.UI.Page
    {
        // 定义接收邮件对象
        protected POP3Class popClient;

        // 定义邮件信息接口
        protected jmail.Message messageMail;

        // 定义邮件附件集合接口
        protected Attachments attachments;

        // 定义邮件附件接口
        protected jmail.Attachment attachment;

        B_MailSet bll = new B_MailSet();
        M_MailSet model = new M_MailSet();
        DataTable dt = new DataTable();
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            buser.CheckIsLogin("MailView.aspx?ID=" + DataConvert.CLng(Request["ID"]));
            // 实例化邮件接收类POP3Class
            popClient = new POP3Class();
            dt = bll.SelByUid(buser.GetLogin().UserID);
            if (dt != null && dt.Rows.Count > 0)
            {
                // 连接服务器
                popClient.Connect(dt.Rows[0]["UserMail"].ToString(), dt.Rows[0]["Password"].ToString(), dt.Rows[0]["POP3"].ToString(), 110);
                if (popClient != null)
                {
                    if (popClient.Count > 0)
                    {
                        int index = Convert.ToInt32(Request["ID"]);
                        if (popClient != null)
                        {
                            if (popClient.Count > 0)
                            {
                                messageMail = popClient.Messages[index + 1];
                            }
                        }
                        labFrom.Text = messageMail.FromName + "< " + messageMail.From + " >";
                        labSubject.Text = messageMail.Subject;
                        labTime.Text = messageMail.Date.ToString();
                        labUserMail.Text = dt.Rows[0]["UserMail"].ToString();
                        richtbxMailContentReview.Text = messageMail.Body;
                        attachments = messageMail.Attachments;
                        if (attachments.Count > 0)
                        {
                            for (int i = 0; i < attachments.Count; i++)
                            {
                                attachment = attachments[i]; //取得附件 
                                string attname = attachment.Name; //附件名称 

                                string path = Server.MapPath("/UploadFiles/mailFiles/");
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                try
                                {
                                    attachment.SaveToFile(path + attachment.Name);
                                }
                                catch
                                {
                                    jmail.Attachments atts = messageMail.Attachments;
                                    jmail.Attachment at = messageMail.Attachments[i];
                                }
                                //this.lsbmessage.Items.Add("附件: " + attachment.Name +" (" +attachment.Size+")" );
                                DataTable dts = new DataTable("NodeTree");
                                DataColumn myDataColumn;
                                DataRow myDataRow;

                                myDataColumn = new DataColumn();
                                myDataColumn.DataType = System.Type.GetType("System.Int32");
                                myDataColumn.ColumnName = "ID";
                                dts.Columns.Add(myDataColumn);

                                myDataColumn = new DataColumn();
                                myDataColumn.DataType = System.Type.GetType("System.String");
                                myDataColumn.ColumnName = "name";
                                dts.Columns.Add(myDataColumn);

                                myDataRow = dts.NewRow();
                                myDataRow["name"] = attachment.Name;
                                dts.Rows.Add(myDataRow);

                                Repeater1.DataSource = dts;
                                Repeater1.DataBind();
                                //filelist.InnerHtml = "<a href='/UploadFiles/mailFiles/" + attachment.Name + "'>  附件: " + attachment.Name + " (" + attachment.Size + ") </a>";

                                // attachment.SaveToFile(Server.MapPath("/UploadFiles/mailFiles/") + attachment.Name); 
                                //  attachment.SaveToFile("e:\\" + attname); //上传到服务器 
                                // messageMail.AddCustomAttachment("","",true); 
                                //attachment = attachments[i];
                                //string attrfilepath = Server.MapPath("/jjcnblog/") + attachment.Name;
                                //attachment.SaveToFile(attrfilepath);
                                //this.lsbmessage.Items.Add("附件名称: " + attachment.Name);
                                //this.lsbmessage.Items.Add("附件大小: " + attachment.Size + " bytes");

                            }
                        }

                    }
                }
            }
        }

        protected void Del_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["ID"]))
            {
                int index = Convert.ToInt32(Request["ID"]);
                popClient.DeleteSingleMessage(index + 1);
                popClient.Disconnect();
                richtbxMailContentReview.Text = "";
            }
        }
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string fileName = e.CommandArgument.ToString();
            if (e.CommandName == "lkbDrw")
            {
                SafeSC.DownFile("/UploadFiles/mailFiles/" + fileName);
            }
        }

        // 回复邮件
        protected void btnReplyCurrentMail_Click(object sender, EventArgs e)
        {
            //int index = lstViewMailList.SelectedItems[0].Index;
            int index = Convert.ToInt32(Request["ID"]);
            messageMail = popClient.Messages[index + 1];
            // 使写信选项卡成为当前选项卡
            //  tabControlMyMailbox.SelectTab(tabPageWriteLetter);
            //txbSendTo.Text = lstViewMailList.SelectedItems[0].SubItems[1].Text;
            txbSendTo.Value = messageMail.FromName; //lstViewMailList.SelectedItem.Text;
            txbSubject.Value = "Re:" + messageMail.Subject;
            richtbxBody.Value = "";
            richtbxBody.Focus();
        }
    }
}