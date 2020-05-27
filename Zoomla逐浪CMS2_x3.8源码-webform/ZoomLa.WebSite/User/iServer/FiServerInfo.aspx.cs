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
using System.Net.Mail;


public partial class manage_iServer_FiServerInfo : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_IServer serverBll = new B_IServer();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "filedown")
        {
            string path = Request.QueryString["filepath"];
            if (path != "")
            {
                string filepath = Server.MapPath(path);
                System.IO.FileInfo file = new System.IO.FileInfo(filepath);
                if (file.Exists)
                {
                    SafeSC.DownFile(path);
                }
                else
                {
                    Response.Write("<script>alert('该文件不存在！');history.go(-1);</script>");
                }
            }
        }
        if (!IsPostBack)
        {
            int QuestionId =DataConverter.CLng(Request.QueryString["QuestionId"]);
            MyBind(QuestionId);
        }        
    }

    public void MyBind(int QuestionId)
    {
        M_UserInfo info = buser.GetLogin();
        M_IServer iserver = new M_IServer();
        iserver = serverBll.SeachById(QuestionId);
        if (iserver != null && iserver.QuestionId > 0)
        {
            lblQuestionId.Text = iserver.QuestionId.ToString();
            lblTitle.Text = BaseClass.Htmlcode(iserver.Title);
            lblState.Text = iserver.State.ToString();
            lblPriority.Text = iserver.Priority.ToString();
            lblRoot.Text = iserver.Root;
            lblType.Text = iserver.Type;
            lblSubTime.Text = iserver.SubTime.ToString();
            lblReadCount.Text = BaseClass.Htmlcode(iserver.ReadCount.ToString());
            lblSubTime_R.Text = iserver.SubTime.ToString();
            lblTitle_R.Text = BaseClass.Htmlcode(iserver.Title.ToString());
            lblName.Text = info.UserName.ToString();
            lblConent.Text = iserver.Content.ToString();
            spDiv.InnerHtml = GetFile(iserver.Path);
            if (iserver.SolveTime == DateTime.MinValue)
                lblSolveTime.Text = "";
            else
                lblSolveTime.Text = iserver.SolveTime.ToString();
            DataTable dt = B_IServerReply.SeachById(QuestionId);
            if (dt != null && dt.Rows.Count > 0)
            {
                //rep.Visible = true;
                resultsRepeater.DataSource = B_IServerReply.SeachById(QuestionId);
                resultsRepeater.DataBind();
            }
            else
            {
                
            }
            if (iserver.State == "已锁定")
            {
                replyDiv.Visible = false;
            }
            else
            {
                replyDiv.Visible = true;
            }
            B_IServerReply brep = new B_IServerReply();
            bool res = brep.GetUpdataState(1, QuestionId);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        B_User buser = new B_User();
        M_UserInfo info = buser.GetLogin();
        M_IServerReply reply = new M_IServerReply();
        reply.UserId = info.UserID;
        reply.Title =BaseClass.Htmleditcode(txtTitle.Text.Trim());
        reply.Content =BaseClass.Htmleditcode(textarea1.Value.ToString());
        reply.Path = Attach_Hid.Value;
        reply.QuestionId = int.Parse(lblQuestionId.Text.ToString());
        reply.ReplyTime = DateTime.Now;
        if (reply.Content == "" || reply.Content == null)
        {
            Response.Write("<script>alert('请输入回复内容!');</script>");
            return;
        }
        else
        {
           
            if (B_IServerReply.Add (reply))
            {
                M_IServer iserver = serverBll.SeachById(reply.QuestionId);
                if (iserver != null && iserver.State == "已解决")
                {
                    iserver.State = "处理中";
                    serverBll.Update(iserver);
                }
                
                SendMess(buser.GetLogin().UserID, reply.QuestionId);

                //SendEmailToUser(info.UserID, reply.QuestionId);
               // SendEmailToAdmin(info.UserID, reply.QuestionId);
                Response.Write("<script>alert('回复成功!');location.href='FiServerInfo.aspx?QuestionId="+reply.QuestionId.ToString()+"';</script>");
            }
            else
            {
                Response.Write("<script>alrty('提交失败-可能是由于系统未开放功能所致')");
                return;
            }
        }
    }
    private void SendEmailToAdmin(int id, int QuestionID)
    {
        string address = SiteConfig.MailConfig.MailServerList;
        string[] s = address.Split(',');
        for (int i = 0; i < s.Length; i++)
        {

            M_UserInfo userinfo = buser.GetUserByUserID(id);
            MailInfo minfo = new MailInfo();
            minfo.IsBodyHtml = true;
            M_IServer iserver = serverBll.SeachById(QuestionID);
            minfo.ToAddress = new MailAddress(s[i]);

            string url = SiteConfig.SiteInfo.SiteUrl;
            if (!string.IsNullOrEmpty(url) && url.Substring(url.Length - 1) == ",")
            {
                url = url.Substring(0, url.Length - 1);
            }
            string EmailContent = "亲爱的管理员：<br/>会员[" + userinfo.UserName + "]回复了问题。<br/><br/>标题：<strong>" + iserver.Title + "</strong><br/>内容：" + textarea1.Value.ToString() + "<br/>点此浏览并处理问题：<a href='" + url + CustomerPageAction.customPath2 + "iServer/BiServerInfo.aspx?QuestionId=" + iserver.QuestionId + "'>" + "" + url + CustomerPageAction.customPath2 + "iServer/BiServerInfo.aspx?QuestionId=" + iserver.QuestionId + "</a><br/><br/>" + SiteConfig.SiteInfo.SiteName + "<br/>" + DateTime.Now.ToLongDateString().ToString();
            minfo.MailBody = EmailContent;
            minfo.Subject = iserver.Title + "_回复信息";
            if (SendMail.Send(minfo) == SendMail.MailState.Ok)
            {
                //发送成功
            }
        }
    }

    private void SendEmailToUser(int id, int QuestionID)
    {
        M_UserInfo userinfo = buser.GetUserByUserID(id);
        MailInfo minfo = new MailInfo();
        minfo.IsBodyHtml = true;

        M_IServer iserver = serverBll.SeachById(QuestionID);

        minfo.ToAddress = new MailAddress(userinfo .Email);
        
        string url = SiteConfig.SiteInfo.SiteUrl;
        if (!string.IsNullOrEmpty(url) && url.Substring(url.Length - 1) == ",")
        {
            url = url.Substring(0, url.Length - 1);
        }

        string EmailContent = "亲爱的" + userinfo.UserName + "：<br/>您回复了问题。<br/><br/>标题：<strong>" +iserver.Title  + "</strong><br/>内容：" + textarea1.Value.ToString() + "<br/>点此查看回复：<a href='" + url + "/user/iServer/FiServerInfo.aspx?QuestionId=" + iserver.QuestionId  + "'>" + url + "/user/iServer/FiServerInfo.aspx?QuestionId=" + iserver.QuestionId + "</a><br/><br/>" + SiteConfig.SiteInfo.SiteName + "<br/>" + DateTime.Now.ToLongDateString().ToString() + "";

        minfo.MailBody = EmailContent;
        minfo.Subject = iserver.Title + "_回复信息";
        if (SendMail.Send(minfo) == SendMail.MailState.Ok)
        {
            //发送成功
        }
    }

    private void SendMess(int userid, int QuestionId)
    {
        B_Admin badmin = new B_Admin();
        B_Message bmess = new B_Message();
        M_UserInfo info = buser.GetUserByUserID(userid);
        if (info != null && info.UserID > 0)
        {
            //站内短信
            M_Message mess = new M_Message();
            mess.Incept = badmin.GetAdminLogin().AdminName;
            string UserName = info.UserName;
            mess.Sender = UserName;
            M_IServer iserver = serverBll.SeachById(QuestionId);
            M_UserInfo userinfo = buser.GetUserByUserID(iserver.UserId);
            string usernames = userinfo != null && userinfo.UserID > 0 ? userinfo.UserName : "匿名用户";
            if (iserver != null && iserver.QuestionId > 0)
            {
                mess.Title = "问题回复";
                mess.PostDate = DataConverter.CDate(DateTime.Now);
                mess.Content = "您好," + UserName + "," + usernames + "于" + iserver.SubTime.ToShortDateString() + "提交的问题 :" + iserver.Title + " 有新回复,请查看!";
            }
            mess.Savedata = 0;
            mess.Receipt = "";
            int i = bmess.GetInsert(mess);
        }
    }


    public string GetUserName(string UserId)
    {
        B_User buser = new B_User();
        return buser.GetUserByUserID(DataConverter.CLng(UserId)).UserName;
    }

    public string getFilePath()
    {
        return Server.MapPath("/" + SiteConfig.SiteOption.UploadDir + "/iServer/");
    }

    /// <summary>
    /// 获取图片路径
    /// </summary>
    /// <param name="path">图片路径</param>
    /// <returns></returns>
    public string GetFile(string path)
    {
        string content="";
        string[] paths = path.Split('|');
        if (paths != null && paths.Length>0)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                string filepath = Server.MapPath(paths[i]);
                System.IO.FileInfo file = new System.IO.FileInfo(filepath);
                if (file.Exists)
                {
                    if (string.IsNullOrEmpty(content))
                    {
                        content = "附件：";
                    }
                    content += "<a href='FiServerInfo.aspx?menu=filedown&filepath=" + paths[i] + "'>" + file.Name + "</a>&nbsp;";
                }
            }
        }
        return content;
    }
    
}
