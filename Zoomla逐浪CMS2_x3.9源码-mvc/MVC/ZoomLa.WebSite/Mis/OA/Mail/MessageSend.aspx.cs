using System;
using System.Data;
using System.Web.UI;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.IO;
using ZoomLa.BLL.Helper;

namespace ZoomLaCMS.MIS.OA.Mail
{
    public partial class MessageSend : System.Web.UI.Page
    {
        private B_User buser = new B_User();
        private M_UserInfo muser = new M_UserInfo();
        private OACommon oacom = new OACommon();
        private B_Message msgBll = new B_Message();
        public string MailRemind(M_UserInfo mu)
        {
            string maxSize = "", usedSize = "", surSize = "";
            float percent = 0;
            DataTable dt = msgBll.SelMyMail(mu.UserID, 4);
            usedSize = GetFileSize(dt).ToString("0.0");
            if (mu.MailSize == -1)
            {
                maxSize = "无限制";
                surSize = "无限制";
                percent = 0;
            }
            else if (mu.MailSize == 0)
            {
                maxSize = OAConfig.MailSize.ToString();
                surSize = CheckMailSize(mu).ToString();
                percent = (float.Parse(usedSize) / float.Parse(maxSize)) * 100;
            }
            else
            {
                maxSize = mu.MailSize.ToString();
                surSize = CheckMailSize(mu).ToString();
                percent = (float.Parse(usedSize) / float.Parse(maxSize)) * 100;
            }
            return string.Format("你有{0}M空间,已用{1}M,尚余{2}M", maxSize, usedSize, surSize);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            buser.CheckIsLogin();//检测是否登录BinarySearch
            M_UserInfo info = buser.GetLogin();
            if (info.IsNull)
            {
                Response.Redirect("Login.aspx");
            }
            if (function.isAjax())
            {
                string GID = Request.Form["GID"];
                string UID = Request.Form["UID"];
                string usernames;
                usernames = GetByGroupID(GID);
            }
            if (!IsPostBack)
            {
                string uid = Request.QueryString["uid"];
                this.User_T.Text = buser.GetUserNameByIDS(uid);
                if (!string.IsNullOrEmpty(base.Request.QueryString["id"]))
                {
                    M_Message messInfo = B_Message.GetMessByID(DataConverter.CLng(base.Request.QueryString["id"]));
                    if (!messInfo.IsNull)
                    {
                        this.User_T.Text = buser.GetUserNameByIDS(messInfo.Sender);
                        this.TxtTitle.Text = "回复:" + messInfo.Title;
                    }
                }
                if (!string.IsNullOrEmpty(base.Request.QueryString["ToID"]))
                {
                    M_UserInfo messInfo = buser.GetUserByUserID(DataConverter.CLng(base.Request.QueryString["ToID"]));
                    if (!messInfo.IsNull)
                    {
                        this.User_T.Text = messInfo.HoneyName;
                    }
                }
                int SentoID = DataConverter.CLng(base.Request.QueryString["userid"]);
                string titlestr = base.Request.QueryString["title"];

                if (buser.IsExit(SentoID))
                {
                    M_UserInfo userinfo = buser.GetUserByUserID(SentoID);
                    this.User_T.Text = userinfo.HoneyName;
                    this.TxtTitle.Text = titlestr;
                }
                if (!string.IsNullOrEmpty(base.Request.QueryString["Drafid"]))
                {
                    M_Message messInfo = B_Message.GetMessByID(DataConverter.CLng(base.Request.QueryString["Drafid"]));
                    this.User_T.Text = buser.GetUserNameByIDS(messInfo.Incept);
                    this.TxtTitle.Text = messInfo.Title;
                    this.EditorContent.Text = messInfo.Content;
                    if (!string.IsNullOrEmpty(messInfo.Attachment))
                    {
                        hasFileData.Value = messInfo.Attachment + ",";
                        string[] af = messInfo.Attachment.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        string h = "";
                        for (int i = 0; i < af.Length; i++)
                        {
                            h += "<span class='disupFile'>";
                            h += GroupPic.GetShowExtension(GroupPic.GetExtName(af[i]));
                            h += "<a target='_blank' href=" + af[i] + ">" + af[i].Split('/')[(af[i].Split('/').Length - 1)] + "</a><a href='javascript:;' title='删除' onclick='delHasFile(\"" + af[i] + "\",this);' ><img src='/App_Themes/AdminDefaultTheme/images/del.png'/></a></span>";
                        }
                        hasFileTD.InnerHtml = h;
                    }
                }
            }
        }
        //发送
        protected void BtnSend_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            if (mu.MailSize != -1)//邮箱容量检测
            {
                float surSize = CheckMailSize(mu);
                float upSize = 0;
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    if (Request.Files[i].ContentLength < 1) continue;
                    upSize += float.Parse((Request.Files[i].ContentLength / 1024f / 1024f).ToString("0.0"));
                }
                if (surSize <= upSize)//如果剩余容量小于该次上传的附件容量
                {
                    function.Script(this, "alert('邮箱空间已满，无法发送邮件：" + MailRemind(mu) + "');");
                    return;
                }
            }
            M_Message messInfo = new M_Message();
            if (!string.IsNullOrEmpty(Request.QueryString["Drafid"]))
                messInfo = msgBll.SelReturnModel(Convert.ToInt32(Request.QueryString["Drafid"]));
            string UserID = buser.GetLogin().UserID.ToString();
            //发送
            messInfo.Incept = buser.GetIdsByUserName(User_T.Text.Trim());
            messInfo.Sender = UserID;
            messInfo.Title = this.TxtTitle.Text;
            messInfo.PostDate = DataConverter.CDate(DateTime.Now);
            messInfo.Content = this.EditorContent.Text;
            messInfo.Savedata = 0;
            messInfo.Receipt = "";
            messInfo.CCUser = buser.SelUserIDByOA(CCUser_T.Text.Trim());
            if (string.IsNullOrEmpty(messInfo.Incept.Replace(",", "")))
            {
                function.WriteErrMsg("收件人不存在,请检测输入是否正确");
            }
            messInfo.Attachment = SaveFile().TrimEnd(',');
            if (!string.IsNullOrEmpty(Request.QueryString["Drafid"]))
            {
                messInfo.Attachment = (hasFileData.Value + "," + SaveFile()).Trim(',');
                B_Message.Update(messInfo);
            }
            else
                msgBll.GetInsert(messInfo);
            Response.Redirect("MessageOutbox.aspx");
        }
        //清除
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.EditorContent.Text = "";
        }
        //存草稿//
        protected void Button1_Click(object sender, EventArgs e)
        {
            B_Message message = new B_Message();
            if (Page.IsValid)
            {
                M_Message messInfo = new M_Message();
                if (!string.IsNullOrEmpty(Request.QueryString["Drafid"]))
                { messInfo = message.SelReturnModel(Convert.ToInt32(Request.QueryString["Drafid"])); }
                User_T.Text = StrHelper.RemoveDupByIDS(User_T.Text);
                messInfo.Incept = buser.SelUserIDByOA(User_T.Text);
                messInfo.Sender = buser.GetLogin().UserID.ToString();
                messInfo.Title = this.TxtTitle.Text;
                messInfo.PostDate = DateTime.Now;
                messInfo.Content = this.EditorContent.Text;
                messInfo.Savedata = 1;
                messInfo.Receipt = "";
                messInfo.Attachment = SaveFile();
                if (!string.IsNullOrEmpty(Request.QueryString["Drafid"]))
                {
                    messInfo.Attachment = (hasFileData.Value + "," + SaveFile()).Trim(',');
                    B_Message.Update(messInfo);
                }
                else
                    message.GetInsert(messInfo);
            }
            Response.Redirect("MessageDraftbox.aspx");
        }
        public string GetByGroupID(string GroupID)
        {
            string[] GIDS = GroupID.Split(',');
            for (int i = 0; i < GIDS.Length; i++)
            {
                DataTable dt = buser.GetUserByGroupI(DataConverter.CLng(GIDS[i]));
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                }
            }
            return "";
        }
        //保存上传的文件
        public string SaveFile()
        {
            string result = "", userName = buser.GetLogin().HoneyName; ;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files[i].ContentLength < 1) continue;//为空则不处理
                result += oacom.SaveFile(Request.Files[i], OACommon.SaveType.Mail, userName) + ",";
            }
            return result;
        }
        //返回剩余容理,以M为单位,为-1不限制,不进此
        public float CheckMailSize(M_UserInfo mu)
        {

            //我的发件+草稿+回收站
            DataTable dt = msgBll.SelMyMail(mu.UserID, 4);
            //float usedSize = GetFileSize(oacom.OADir + @"\Mail\" + mu.UserName + "\\");
            float usedSize = GetFileSize(dt);
            float maxSize = 0;
            if (mu.MailSize == -1)
            {
                maxSize = 1000;
            }
            else if (mu.MailSize == 0)
            {
                maxSize = OAConfig.MailSize;
            }
            else
            {
                maxSize = mu.MailSize;
            }
            float surSize = maxSize - usedSize < 0 ? 0 : maxSize - usedSize;
            return float.Parse(surSize.ToString("0.0"));
        }
        // 返回M
        public float GetFileSize(DataTable dt)
        {
            //if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            //long size = FileSystemObject.getDirectorySize(path);
            //return (size / 1024 / 1024f);
            long size = 0;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    foreach (string vpath in dr["AttachMent"].ToString().Split(','))
                    {
                        size += new FileInfo(Server.MapPath(vpath)).Length;
                    }
                }
                catch { function.WriteErrMsg(dr["AttachMent"].ToString()); }
            }
            return (size / 1024 / 1024f);
        }
    }
}