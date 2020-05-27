using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Net.Mail;
using System.IO;
using System.Data;

namespace ZoomLaCMS.Manage.iServer
{
    public partial class BiServerInfo : CustomerPageAction
    {
        B_User buser = new B_User();
        B_IServer serverBll = new B_IServer();
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin b_Admin = new B_Admin();
            b_Admin.CheckIsLogin();
            if (string.IsNullOrEmpty(Request.QueryString["QuestionId"])) { function.WriteErrMsg("不存在"); }
            int QuestionId = int.Parse(Request.QueryString["QuestionId"]);
            if (!string.IsNullOrEmpty(Request.QueryString["menu"]) && Request.QueryString["menu"] == "filedown")
            {
                string path = Request.QueryString["filepath"];
                SafeSC.DownFile(path);
                //if (path != "")
                //{
                //    string filepath = Server.MapPath(path);
                //    System.IO.FileInfo file = new System.IO.FileInfo(filepath);
                //    if (file.Exists)
                //    {
                //        Response.Clear();
                //        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
                //        Response.AddHeader("Content-Length", file.Length.ToString());
                //        Response.ContentType = "application/octet-stream";
                //        Response.Filter.Close();
                //        Response.WriteFile(file.FullName);
                //        Response.End();
                //    }
                //    else
                //    {
                //        Response.Write("<script>alert('该文件不存在！')</script>");
                //    }
                //}
            }
            if (!IsPostBack)
            {
                MyBind(QuestionId);
                M_IServer iserver = serverBll.SeachById(QuestionId);
                iserver.ReadCount = iserver.ReadCount + 1;
                serverBll.Update(iserver);
            }
            Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "I/Main.aspx'>工作台</a></li><li><a href='BiServer.aspx'>有问必答</a></li><li class='active'>问题详情</li>");
        }

        /// <summary>
        /// 绑定方法
        /// </summary>
        /// <param name="QuestionId"></param>
        public void MyBind(int QuestionId)
        {
            B_User buser = new B_User();
            M_IServer iserver = new M_IServer();
            iserver = serverBll.SeachById(QuestionId);
            if (iserver == null) { function.WriteErrMsg("不存在"); }
            lblQuestionId.Text = iserver.QuestionId.ToString();
            lblUserName.InnerHtml = " <a onclick=\"opentitle('../User/Userinfo.aspx?id=" + iserver.UserId + "','查看会员')\" href='###' title='查看会员'>" + GetUserName(iserver.UserId.ToString()) + "</a>";
            hfusername.Value = GetUserName(iserver.UserId.ToString());
            DropDownList1.Text = iserver.State.ToString();
            DropDownList2.Text = iserver.Priority.ToString();
            DropDownList3.Text = iserver.Root.ToString();
            DropDownList4.Text = iserver.Type.ToString();
            lblSubTime.Text = iserver.SubTime.ToString();
            lblReadCount.Text = BaseClass.Htmlcode(iserver.ReadCount.ToString());
            if (iserver.SolveTime == DateTime.MinValue)
                lblSolveTime.Text = "";
            else
                lblSolveTime.Text = iserver.SolveTime.ToString();
            if (!string.IsNullOrEmpty(iserver.Path))
            {
                Quest_Attch_Hid.Value = iserver.Path.Trim('|');
                Attch_Tr.Visible = true;
            }
            lblSubTime_R.Text = iserver.SubTime.ToString();
            lblTitle_R.Text = BaseClass.Htmlcode(iserver.Title);
            lblName.Text = GetUserName(iserver.UserId.ToString());
            lblConent.Text = iserver.Content.ToString();
            lblSubTime_V.Text = iserver.SubTime.ToString();
            lblUserName_V.Text = GetUserName(iserver.UserId.ToString());
            UserEmail.Value = buser.GetUserByUserID(iserver.UserId).Email;
            resultsRepeater.DataSource = B_IServerReply.SeachById(QuestionId);
            resultsRepeater.DataBind();
        }


        /// <summary>
        /// 根据用户Id获得Name
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GetUserName(string UserId)
        {
            B_User buser = new B_User();
            return buser.GetUserByUserID(DataConverter.CLng(UserId)).UserName;
        }

        /// <summary>
        /// 确认回复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            M_UserInfo info = buser.GetLogin();
            M_IServerReply reply = new M_IServerReply();
            reply.UserId = info.UserID;
            reply.Title = txtTitle.Value.Trim();
            reply.Content = textarea1.Value;
            reply.Path = Attach_Hid.Value;
            reply.QuestionId = DataConverter.CLng(lblQuestionId.Text.ToString());
            reply.ReplyTime = DateTime.Now;
            if (reply.Content == "" || reply.Content == null)
            {
                function.WriteErrMsg("请输入回复内容!");
                return;
            }
            else
            {
                if (B_IServerReply.Add(reply) && UpdateIServer())
                {
                    SendMess(info.UserID, reply.QuestionId);
                    if (IsEmail.Checked == true)
                        //SendEmailToUser(info.UserID, reply.QuestionId);
                        //SendEmailToAdmin(info.UserID, reply.QuestionId);
                        function.WriteSuccessMsg("回复成功!", "BiServer.aspx");

                }
                else
                {
                    function.WriteErrMsg("回复失败");
                    return;
                }
            }
        }

        private void SendEmailToAdmin(int userid, int QuestionId)
        {
            string address = SiteConfig.MailConfig.MailServerList;
            string[] s = address.Split(',');
            for (int i = 0; i < s.Length; i++)
            {

                MailInfo mailInfo = new MailInfo();
                mailInfo.IsBodyHtml = true;
                M_UserInfo info = buser.GetUserByUserID(userid);
                mailInfo.FromName = info.UserName;
                M_IServer iserver = serverBll.SeachById(QuestionId);

                string url = SiteConfig.SiteInfo.SiteUrl;
                if (!string.IsNullOrEmpty(url) && url.Substring(url.Length - 1) == ",")
                {
                    url = url.Substring(0, url.Length - 1);
                }
                string admEmail = SiteConfig.SiteInfo.WebmasterEmail;
                mailInfo.ToAddress = new MailAddress(s[i]);

                string EmailContent = "亲爱的管理员：<br/>会员[" + info.UserName + "]的问题已经处理！<br/>标题：<strong>" + iserver.Title + "</strong><br/>内容：" + textarea1.Value.ToString() + "<br/><br/>点此查看回复：<br/><a href='" + url + "BiServerInfo.aspx?QuestionId=" + iserver.QuestionId + "'>" + url + "BiServerInfo.aspx?QuestionId=" + iserver.QuestionId + "</a><br/><br/>" + SiteConfig.SiteInfo.SiteName + "<br/>" + DateTime.Now.ToLongDateString().ToString();
                mailInfo.MailBody = EmailContent;
                mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "有问必答_回复信息";
                if (SendMail.Send(mailInfo) == SendMail.MailState.Ok)
                {
                    //成功
                }
            }
        }

        private void SendMess(int userid, int QuestionId)
        {

        }

        private void SendEmailToUser(int userid, int QuestionId)
        {

            if (!UserEmail.Value.Trim().Equals(""))
            {
                // throw new Exception(userid +"*********"+QuestionId);
                MailInfo mailInfo = new MailInfo();
                mailInfo.IsBodyHtml = true;
                M_UserInfo info = buser.GetUserByUserID(userid);
                //mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
                mailInfo.FromName = info.UserName;

                M_IServer iserver = serverBll.SeachById(QuestionId);
                MailAddress address = new MailAddress(UserEmail.Value);
                mailInfo.ToAddress = address;

                string url = SiteConfig.SiteInfo.SiteUrl;
                if (!string.IsNullOrEmpty(url) && url.Substring(url.Length - 1) == ",")
                {
                    url = url.Substring(0, url.Length - 1);
                }
                string EmailContent = "亲爱的[" + info.UserName + "]：<br/>您于" + iserver.SubTime.ToShortDateString() + "提交的问题已经处理！<br/>标题：<strong>" + iserver.Title + "</strong><br/>内容：" + textarea1.Value.ToString() + "<br/><br/>点此查看回复：<br/><a href='" + url + "/user/iServer/FiServerInfo.aspx?QuestionId=" + iserver.QuestionId + "'>" + url + "/user/iServer/FiServerInfo.aspx?QuestionId=" + iserver.QuestionId + "</a><br/><br/>" + SiteConfig.SiteInfo.SiteName + "<br/>" + DateTime.Now.ToLongDateString().ToString();

                mailInfo.MailBody = EmailContent;
                mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "有问必答_回复信息";
                if (SendMail.Send(mailInfo) == ZoomLa.Components.SendMail.MailState.Ok)
                {
                    //发送成功
                }
            }

        }

        public bool UpdateIServer()
        {
            int id = int.Parse(lblQuestionId.Text.ToString());
            M_IServer iserver = serverBll.SeachById(id);
            B_User buser = new B_User();
            iserver.UserId = buser.GetUserByName(hfusername.Value.Trim()).UserID;
            iserver.State = DropDownList1.SelectedValue.ToString();
            iserver.Priority = DropDownList2.SelectedValue.ToString();
            iserver.Root = DropDownList3.SelectedValue.ToString();
            iserver.Type = DropDownList4.SelectedValue.ToString();
            iserver.ReadCount = int.Parse(lblReadCount.Text.ToString());
            iserver.SubTime = DataConverter.CDate(lblSubTime.Text.ToString());
            if (DropDownList1.SelectedValue.ToString().Equals("已解决"))
            {
                iserver.SolveTime = DateTime.Now;
            }
            else
            {
                if (lblSolveTime.Text.ToString() == "")
                    iserver.SolveTime = DateTime.MinValue;
                else
                    iserver.SolveTime = DataConverter.CDate(lblSolveTime.Text.ToString());
            }

            return serverBll.Update(iserver);
        }



        public string getFilePath()
        {

            string strPath = "";
            string filepath = "/" + SiteConfig.SiteOption.UploadDir + "/iServer/";
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
            ////如果上传文件名为允许的扩展名,则将文件保存到文件中
            //if (fileOk)
            //{
            //    try
            //    {
            //        FileSystemObject.CreateFileFolder(Server.MapPath(filepath), HttpContext.Current);
            //        SafeSC.SaveFile(filepath,FileUpload1);
            //        SafeSC.SaveFile(filepath, FileUpload2);
            //        SafeSC.SaveFile(filepath, FileUpload3);
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (UpdateIServer())
                function.WriteSuccessMsg("更新成功!", "BiServerInfo.aspx?QuestionId=" + lblQuestionId.Text.ToString());
            else
                function.WriteErrMsg("更新失败");
        }


        protected void EditReply_B_Click(object sender, EventArgs e)
        {
            B_IServerReply.UpdateByID(Convert.ToInt32(ReplyId_Hid.Value), EditReply_T.Value);
            MyBind(Convert.ToInt32(Request.QueryString["QuestionId"]));
        }

        protected void DownFile_Link_Click(object sender, EventArgs e)
        {

        }

        protected void resultsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is DataRowView)
            {
                DataRowView dr = e.Item.DataItem as DataRowView;
                if (!string.IsNullOrEmpty(dr["Path"].ToString())) { e.Item.FindControl("reply_attch").Visible = true; }
            }
        }
    }
}