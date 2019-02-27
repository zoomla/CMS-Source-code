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
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Net.Mail;

using System.Text;
using System.Collections.Specialized;

public partial class user_iServer_AddQuestion : System.Web.UI.Page
{
    B_User buser = new B_User();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["OrderID"] != null && Request.QueryString["OrderID"] != "")
            {
                this.DropDownList2.SelectedValue = "订单";
                string OrderID = Request.QueryString["OrderID"].ToString();
                this.OrderID.Value = OrderID;
            }
            if (Request["title"] != null && Request["title"] != "")
            {
                this.TextBox1.Text = Request["title"];
                this.textarea1.Value = Request["con"];
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        B_User buser = new B_User();
        M_UserInfo info = buser.GetLogin();
        M_IServer iserver = new M_IServer();
        iserver.UserId = info.UserID;
        iserver.Title = BaseClass.Htmleditcode(this.TextBox1.Text.ToString());
        iserver.Content = BaseClass.Htmleditcode(this.textarea1.Value.ToString());
        iserver.Priority = this.DropDownList1.SelectedValue.ToString();
        iserver.Type = this.DropDownList2.SelectedValue.ToString();
        iserver.SubTime = DateTime.Now;
        iserver.Root = "网页表单";
        iserver.State = "未解决";
        iserver.RequestTime = DataConverter.CDate(Request.QueryString["mydate_t"]); 
        if (Request.Form["OrderID"] != null)
        {
            iserver.OrderType = DataConverter.CLng(Request.Form["OrderID"]);
        }
        else
        {
            iserver.OrderType = 0;
        }
        iserver.Path = Attach_Hid.ToString();
        if (iserver.Title == "" || iserver.Content == "" || iserver.Title == null || iserver.Content == null)
        {
            function.WriteErrMsg("请输入标题");
        }
        else
        {
            int QuestionID = new B_IServer().AddQuestion(iserver);
            if (QuestionID > 0)
            {
                if (Request.QueryString["OrderID"] != null)
                {
                    function.WriteSuccessMsg("提交成功", "FiServer.aspx?OrderID=" + Request.QueryString["OrderID"]);
                }
                else
                {
                    function.WriteSuccessMsg("提交成功", "FiServer.aspx");
                }
            }
            else
            {
                function.WriteErrMsg("提交失败-可能是由于系统未开放功能所致");
            }
        }
    }

    private void SendEmailToAdmin(int id, int QuestionID)
    {
        string address = SiteConfig.MailConfig.MailServerList;
        string[] s = address.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < s.Length; i++)
        {
            M_UserInfo userinfo = buser.GetUserByUserID(id);
            MailInfo minfo = new MailInfo();
            minfo.IsBodyHtml = true;
            minfo.Subject = SiteConfig.SiteInfo.SiteName + "有问必答-" + this.TextBox1.Text;

            minfo.ToAddress = new MailAddress(s[i]);

            string url = SiteConfig.SiteInfo.SiteUrl;
            if (!string.IsNullOrEmpty(url) && url.Substring(url.Length - 1) == ",")
            {
                url = url.Substring(0, url.Length - 1);
            }
            string EmailContent = "亲爱的管理员：<br/>会员" + userinfo.UserName + "提交了问题。<br/><br/>标题：<strong>" + this.TextBox1.Text + "</strong><br/>内容：" + textarea1.Value.ToString() + "<br/>点此浏览并处理问题：<a href='" + url + CustomerPageAction.customPath2 + "iServer/BiServerInfo.aspx?QuestionId=" + QuestionID + "'>" + "" + url + CustomerPageAction.customPath2 + "iServer/BiServerInfo.aspx?QuestionId=" + QuestionID + "</a><br/><br/>" + SiteConfig.SiteInfo.SiteName + "<br/>" + DateTime.Now.ToLongDateString().ToString();
            minfo.MailBody = EmailContent;
            if (SendMail.Send(minfo) == SendMail.MailState.Ok)
            {
                //发送成功
            }
        }
    }

    private void SendEmailToUser(int id, string usEmail, int QuestionID)
    {
        M_UserInfo userinfo = buser.GetUserByUserID(id);
        MailInfo minfo = new MailInfo();
        minfo.IsBodyHtml = true;
        minfo.Subject = SiteConfig.SiteInfo.SiteName + "有问必答-" + this.TextBox1.Text;
        minfo.ToAddress = new MailAddress(usEmail);

        string url = SiteConfig.SiteInfo.SiteUrl;
        if (!string.IsNullOrEmpty(url) && url.Substring(url.Length - 1) == ",")
        {
            url = url.Substring(0, url.Length - 1);
        }

        string EmailContent = "亲爱的" + userinfo.UserName + "：<br/>您提交了问题。<br/><br/>标题：<strong>" + this.TextBox1.Text + "</strong><br/>内容：" + textarea1.Value.ToString() + "<br/>点此查看回复：<a href='" + url + "/user/iServer/FiServerInfo.aspx?QuestionId=" + QuestionID + "'>" + url + "/user/iServer/FiServerInfo.aspx?QuestionId=" + QuestionID + "</a><br/><br/>" + SiteConfig.SiteInfo.SiteName + "<br/>" + DateTime.Now.ToLongDateString().ToString();

        minfo.MailBody = EmailContent;
        if (SendMail.Send(minfo) == SendMail.MailState.Ok)
        {
            //发送成功
        }
    }

    public string getFilePath()
    {
        string strPath = "";
        string filepath = Server.MapPath("/" + SiteConfig.SiteOption.UploadDir + "/iServer/");
        string[] allfile = SiteConfig.SiteOption.UploadFileExts.Split('|');
        //判断上传控件是否上传文件
        //if (FileUpload1.HasFile)
        //{
        //    //判断上传文件的扩展名
        //    fileExtension = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower();
        //    strPath = "/" + SiteConfig.SiteOption.UploadDir + "/iServer/" + FileUpload1.FileName;
        //    for (int i = 0; i < allfile.Length; i++)
        //    {
        //        if (fileExtension == "." + allfile[i])
        //        {
        //            fileOk = true;
        //        }
        //    }
        //    int ss = 512000;
        //    if (FileUpload1.PostedFile.ContentLength > ss)
        //    {
        //        lblMessage.Text = "你的文件过大,单个附件小于 500KB";
        //    }
        //}
        //if (FileUpload2.HasFile)
        //{
        //    //判断上传文件的扩展名
        //    fileExtension = System.IO.Path.GetExtension(FileUpload2.FileName).ToLower();
        //    strPath += ",/" + SiteConfig.SiteOption.UploadDir + "/iServer/" + FileUpload2.FileName;
        //    for (int i = 0; i < allfile.Length; i++)
        //    {
        //        if (fileExtension == "." + allfile[i])
        //        {
        //            fileOk = true;
        //        }
        //    }
        //    int ss = 512000;
        //    if (FileUpload2.PostedFile.ContentLength > ss)
        //    {
        //        lblMessage.Text = "你的文件过大,单个附件小于 500KB";
        //    }
        //}
        //if (FileUpload3.HasFile)
        //{
        //    //判断上传文件的扩展名
        //    fileExtension = System.IO.Path.GetExtension(FileUpload3.FileName).ToLower();
        //    strPath += ",/" + SiteConfig.SiteOption.UploadDir + "/iServer/" + FileUpload3.FileName;
        //    for (int i = 0; i < allfile.Length; i++)
        //    {
        //        if (fileExtension == "." + allfile[i])
        //        {
        //            fileOk = true;
        //        }
        //    }
        //    int ss = 512000;
        //    if (FileUpload3.PostedFile.ContentLength > ss)
        //    {
        //        lblMessage.Text = "你的文件过大,单个附件小于 500KB";
        //    }
        //}
        //如果上传文件名为允许的扩展名,则将文件保存到文件中
        //if (fileOk)
        //{
        //    try
        //    {
        //        FileSystemObject.CreateFileFolder(filepath, HttpContext.Current);
        //        FileUpload1.PostedFile.SaveAs(filepath + FileUpload1.FileName);
        //        FileUpload2.PostedFile.SaveAs(filepath + FileUpload2.FileName);
        //        FileUpload3.PostedFile.SaveAs(filepath + FileUpload3.FileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "文件不符合,只能上传 " + SiteConfig.SiteOption.UploadFileExts + "类型的附件" + ex.Message;
        //    }
        //}
        //else if (fileExtension != "")
        //{
        //    lblMessage.Text = "该类型文件不能上传或者该文件不可用";
        //}
        return strPath;
    }

    /// <summary>
    /// 创建邮件信息
    /// </summary>
    private void GetMailInfo()
    {
        MailInfo info = new MailInfo();
        info.Subject = "邮件发送测试";
        info.MailBody = "邮件发送测试";
        info.Priority = MailPriority.High;
        info.FromName = "www";
        info.ReplyTo = new MailAddress("web@z01.com");
        info.ToAddress = new MailAddress("binbin_go123@163.com");
        SendMail.Send(info);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("FiServer.aspx");
    }
}