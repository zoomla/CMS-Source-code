using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.Model;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.IO;
using jmail;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

public partial class MIS_Mail_NewMail : System.Web.UI.Page
{
    // 定义邮件发送类
    protected SmtpClient smtpClient;
    protected TcpClient tcpClient;
    protected NetworkStream networkStream;
    protected StreamReader streamReader;
    protected StreamWriter streamWriter;


    // 定义接收邮件对象
    protected POP3Class popClient;

    // 定义邮件信息接口
    protected jmail.Message messageMail;

    // 定义邮件附件集合接口
    protected Attachments attachments;

    // 定义邮件附件接口
    protected jmail.Attachment attachment;
    B_MailSet bll = new B_MailSet();
    B_User buser = new B_User();

    M_MailInfo mm = new M_MailInfo();
    B_MailInfo bm = new B_MailInfo();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        // 初始化界面 
        txbSendTo.Text = "yansha@hx008.com";
        txbSubject.Text = "测试邮件";
        richtbxBody.Text = "这是一封测试邮件，由系统自动发出，无须回复";
        bind();
    }
    // 发送邮件
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ID"]))
        {
            dt = bll.Sel(DataConvert.CLng(Request["ID"]));
        }
        else dt = bll.SelIsDefault(1);
        if (dt != null && dt.Rows.Count > 0)
        {
            //this.Cursor = Cursors.WaitCursor;
            // 实例化一个发送的邮件
            // 相当于与现实生活中先写信，程序中把信（邮件）抽象为邮件类了
            MailMessage mailMessage = new MailMessage();
            // 指明邮件发送的地址，主题，内容等信息
            // 发信人的地址为登录收发器的地址，这个收发器相当于我们平时Web版的邮箱或者是OutLook中配置的邮箱
            mailMessage.From = new MailAddress(dt.Rows[0]["UserMail"].ToString());
            mailMessage.To.Add(txbSendTo.Text);
            mailMessage.Subject = txbSubject.Text;
            mailMessage.SubjectEncoding = Encoding.Default;
            mailMessage.Body = richtbxBody.Text;
            mailMessage.BodyEncoding = Encoding.Default;
            // 设置邮件正文不是Html格式的内容
            mailMessage.IsBodyHtml = false;
            // 设置邮件的优先级为普通优先级
            mailMessage.Priority = MailPriority.Normal;
            ////mailMessage.ReplyTo = new MailAddress(tbxUserMail.Text);
            // 封装发送的附件
            System.Net.Mail.Attachment attachment = null;

            string filpath = "";
            if (add_fields() != "")
                filpath = add_fields();
            string[] files = filpath.Split(new Char[] { '|' });
            int fileCount = 0;
            // if (cmbAttachment.Items.Count > 0)
            //{
            //       for (int i = 0; i < cmbAttachment.Items.Count; i++)
            //    {
            if (files.Length > 0)
            {
                for (fileCount = 0; fileCount < files.Length; fileCount++)
                {
                    //定义访问客户端上传文件的对象 
                    string fileNamePath, extName;
                    //取得上传得文件名
                    fileNamePath = (Server.MapPath("/UploadFiles/") + files[fileCount]).Replace("/", "\\");
                    // throw new Exception(fileNamePath);
                    if (fileNamePath != String.Empty)
                    {
                        //取得文件的扩展名
                        extName = System.IO.Path.GetExtension(fileNamePath);
                        //string fileNamePath = cmbAttachment.Items[i].ToString();
                        // string extName = Path.GetExtension(fileNamePath).ToLower();
                        //if (extName == ".rar" || extName == ".zip")
                        //{
                        attachment = new System.Net.Mail.Attachment(fileNamePath, MediaTypeNames.Application.Zip);
                        //}
                        //else
                        //{
                        //    //fileNamePath = @"E:\Code\Zoomla6x\ZoomLa.WebSite\UploadFiles\mailFiles\Upload\logo.jpg";
                        //    attachment = new System.Net.Mail.Attachment(fileNamePath, MediaTypeNames.Application.Octet);
                        //}

                        // 表示MIMEContent-Disposition标头信息
                        // 对于ContentDisposition具体类的解释大家可以参考MSDN
                        // 这里我就不重复贴出来了，给个地址: http://msdn.microsoft.com/zh-cn/library/System.Net.Mime.ContentDisposition.aspx (着重看备注部分)
                        ContentDisposition cd = attachment.ContentDisposition;
                        cd.CreationDate = File.GetCreationTime(fileNamePath);
                        cd.ModificationDate = File.GetLastWriteTime(fileNamePath);
                        cd.ReadDate = File.GetLastAccessTime(fileNamePath);
                        // 把附件对象加入到邮件附件集合中
                        mailMessage.Attachments.Add(attachment);
                    }
                }
            }
            try
            {
                // 登录成功后实例化邮件发送对象，以便后面完成发送邮件的操作
                // 实例化邮件发送类（SmtpClient）对象
                if (smtpClient == null)
                {
                    smtpClient = new SmtpClient();
                    smtpClient.Host = dt.Rows[0]["Smtp"].ToString();
                    smtpClient.Port = 25;

                    // 不使用默认凭证，即需要认证登录
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(dt.Rows[0]["UserMail"].ToString(), dt.Rows[0]["Password"].ToString());
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                }
                // 发送写好的邮件

                // SmtpClient类用于将邮件发送到SMTP服务器
                // 该类封装了SMTP协议的实现，
                // 通过该类可以简化发送邮件的过程，只需要调用该类的Send方法就可以发送邮件到SMTP服务器了。
                //  throw new Exception(smtpClient.Host.ToString());
                //  throw new Exception(mailMessage.From.ToString() + "|" + txbSendTo.Text + "|" + mailMessage.Subject + "|" + mailMessage.SubjectEncoding + "|" + mailMessage.Body + "|" + mailMessage.BodyEncoding + "|" + mailMessage.IsBodyHtml + "|" + mailMessage.Priority + "|" + mailMessage.Attachments);
                //System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                //for (int i = 0; i < files.Count; i++)
                //{
                //    System.Web.HttpPostedFile filePicture = files[i];
                //    string str = System.IO.Path.GetFullPath(filePicture.FileName);
                //    string FileType = System.IO.Path.GetExtension(filePicture.FileName).ToLower(); // 上传文件类型(扩展名)
                //    string sFileName = Guid.NewGuid().ToString() + FileType;
                //    //throw new Exception(filePicture.FileName);
                //     messageMail.AddAttachment(str, false, "image/jpg");
                //    //  str = SaveFile(filePicture);
                //}

                smtpClient.Send(mailMessage);
                if (saveSend.Checked == true)
                {
                    mm.MailAddTime = DateTime.Now;
                    mm.MailContext = richtbxBody.Text;
                    mm.MailTitle = this.txbSubject.Text;
                    mm.MailAddRees = this.txbSendTo.Text;
                    mm.MailState = true;
                    mm.MailSendTime = DateTime.Now;
                    mm.UserID = buser.GetLogin().UserID;
                    mm.SendMail = dt.Rows[0]["UserMail"].ToString();
                    filpath = "";
                    if (add_fields() != "")
                        filpath = add_fields().Replace("\\", "/");
                    mm.Files = filpath;
                    bm.GetInsert(mm);
                }
                lblMsg.Text = "邮件发送成功！";
                //  MessageBox.Show("邮件发送成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SmtpException smtpError)
            {
                lblMsg.Text = "邮件发送失败！[" + smtpError.StatusCode + "]；["
                    + smtpError.Message + "];\r\n[" + smtpError.StackTrace + "].";
            }
            finally
            {
                mailMessage.Dispose();
            }
        }
        else
        {
            Response.Write("<script>alert('未配置邮箱！！！');location.href='MailSetList.aspx';</script>");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["ID"]))
        {
            dt = bll.Sel(DataConvert.CLng(Request["ID"]));
        }
        else dt = bll.SelIsDefault(1);
        if (dt != null && dt.Rows.Count > 0)
        {
            mm.MailAddTime = DateTime.Now;
            mm.MailContext = richtbxBody.Text;
            mm.MailTitle = this.txbSubject.Text;
            mm.MailAddRees = this.txbSendTo.Text;
            mm.MailState = false;
            mm.MailSendTime = DateTime.Now;
            mm.UserID = buser.GetLogin().UserID;
            mm.SendMail = dt.Rows[0]["UserMail"].ToString();
            string filpath = "";
            if (add_fields() != "")
                filpath = add_fields().Replace("\\", "/");
            mm.Files = filpath;
            bm.GetInsert(mm);
        }
        else
        {
            Response.Write("<script>alert('未配置邮箱！！！');location.href='MailSetList.aspx';</script>");
        }
    }
    #region 邮件发送功能代码
    // 添加附件
    //protected void btnAddFile_Click(object sender, EventArgs e)
    //{ 
    //     //添加字段
    //    string strs1 = ""; 

    //        string[] strArrs;

    //        strs1 = add_fields();

    //        strArrs = strs1.Split('|');

    //        if (strArrs.Length > 1)
    //        {
    //            for (int j = 0; j < strArrs.Length; j++)
    //            {
    //                cmbAttachment.Items.Insert(j, strArrs[j]);
    //              //  messageMail.AddAttachment(strArrs[j], false, "image/jpg");
    //            }
    //        }  
    //}
    protected string SaveFile(System.Web.HttpPostedFile file)
    {
        string FileType;    // 上传文件类型(扩展名)        
        string ConfigType = ConfigurationManager.AppSettings["ImgType"].ToString();
        string ConfigSize = ConfigurationManager.AppSettings["ImgSize"].ToString();
        ConfigSize = (ConfigSize == null ? "0" : ConfigSize);
        ConfigType = (ConfigType == null ? "" : ConfigType);
        // 如果文件长度为0字节   
        if (file.ContentLength == 0) return "0";
        // 如果文件超过最大长度5000000     
        if (file.ContentLength > int.Parse(ConfigSize))
            return "1";        // 获取文件扩展名                  
        FileType = System.IO.Path.GetExtension(file.FileName).ToLower();
        if (ConfigType.IndexOf(FileType) == -1)
            return "2";
        return "";
    }
    //插入 
    protected string add_fields()
    {
        System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
        string path = "";
        string fpath = "";
        int fileCount = 0;
        for (fileCount = 0; fileCount < files.Count; fileCount++)
        {
            //定义访问客户端上传文件的对象
            System.Web.HttpPostedFile postedFile = files[fileCount];
            string fileName,fileExtension;
            //取得上传得文件名
            fileName = System.IO.Path.GetFileName(postedFile.FileName);
            if (fileName != String.Empty)
            {
                //取得文件的扩展名
                fileExtension = System.IO.Path.GetExtension(fileName);
                path = Server.MapPath("\\UploadFiles\\mailFiles\\Upload\\");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                try
                {
                    SafeSC.SaveFile(path + fileName, postedFile);
                }
                catch { }
                if (fileCount == 0)
                {
                    fpath += "mailFiles\\Upload\\" + fileName;
                }
                else
                {
                    fpath += "|" + "mailFiles\\Upload\\" + fileName;
                }
            }
        }
        return fpath;
        //string str1 = "";
        //if (!string.IsNullOrEmpty(hfNumber.Value))
        //{
        //    string str = hfNumber.Value;
        //    str = str.Substring(0, str.Length - 1);
        //    string[] strArr;
        //    strArr = str.Split(',');
        //    foreach (string s in strArr)
        //    { 
        //        if (!string.IsNullOrEmpty(Request["file" + s]))
        //        {
        //            str1 += Request["file" + s] + "|";
        //        }
        //    }
        //}
        //return str1; 
    }

    // 删除附件
    protected void btnDeleteFile_Click(object sender, EventArgs e)
    {
        int index = cmbAttachment.SelectedIndex;
        if (index == -1)
        {
            Response.Write("请选择要删除的附件！");
            //MessageBox.Show("请选择要删除的附件！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        else
        {
            cmbAttachment.Items.RemoveAt(index);
        }
    }

    // 取消发送
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txbSendTo.Text = "yansha@hx008.com";
        txbSubject.Text = "测试邮件";
        richtbxBody.Text = "这是一封测试邮件，由系统自动发送，无须回复。";
        if (cmbAttachment.Items.Count > 0)
        {
            cmbAttachment.Items.Clear();
        }
        // 使收件箱选项卡成为当前选项卡
        //tabControlMyMailbox.SelectTab(tabPageInbox);
    }

    #endregion

    protected void bind()
    { 
      
    }
    protected void Repeater1_ItemCommand(object sender,RepeaterCommandEventArgs e)
    {  
       
    }
}