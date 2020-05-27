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
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using System.IO;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.MIS.OA.Mail
{
    public partial class MessageRead : System.Web.UI.Page
    {
        private B_User buser = new B_User();
        private M_UserInfo muser = new M_UserInfo();
        private B_Message bll = new B_Message();
        protected void Page_Load(object sender, EventArgs e)
        {
            M_UserInfo info = buser.GetLogin();
            if (info.IsNull)
            {
                Response.Redirect("Login.aspx");
            }
            //if (!B_UserPurview.SelReturnByIDs(buser.GetLogin().UserRole, "OAEmailRead"))
            //{
            //    function.WriteErrMsg("对不起，您所在角色组没有使用权限");
            //}
            ViewState["MsgID"] = Request.QueryString["id"].ToString();
            if (!Page.IsPostBack)
            {
                M_Message messInfo = B_Message.GetMessByID(DataConverter.CLng(ViewState["MsgID"].ToString()));
                if (!bll.AllowRead(messInfo.MsgID, info.UserID.ToString()))//如无读该条信息的权限
                {
                    function.WriteErrMsg("无权限阅读该条信息!!!");
                }
                this.LblSender.Text = buser.GetUserNameByIDS(messInfo.Sender.ToString());
                string userNames = buser.GetUserNameByIDS(messInfo.Incept.ToString());
                this.LblIncept.Text = userNames.Length > 100 ? userNames.Substring(0, 100) + "..." : userNames;
                this.LblSendTime.Text = messInfo.PostDate.ToString("yyyy年MM月dd日 HH:mm");
                this.LblTitle.Text = messInfo.Title;
                this.txt_Content.Text = messInfo.Content;
                if (!string.IsNullOrEmpty(messInfo.Attachment))
                    GetFile(messInfo.Attachment);
                if (!string.IsNullOrEmpty(Request.QueryString["read"]))
                {
                    string ccuser = "," + info.UserID + ",";
                    if (messInfo.CCUser.IndexOf(ccuser) > -1)
                    {
                        BtnReply.Visible = false;
                        LBCopy.Visible = true;
                    }
                    if (messInfo.ReadUser.IndexOf("," + info.UserID + ",") == -1)
                    {
                        messInfo.ReadUser += info.UserID.ToString();
                        messInfo.status = 1;
                        B_Message.Update(messInfo);
                    }
                }
            }
        }
        //删除
        //protected void BtnDelete_Click(object sender, EventArgs e)
        //{
        //    M_Message meinfo = B_Message.GetMessByID(DataConverter.CLng(ViewState["MsgID"].ToString()));
        //    if (meinfo.Sender == buser.GetLogin().UserName)
        //    {
        //        meinfo.IsDelSendbox = 1;
        //    }
        //    else {
        //        meinfo.IsDelInbox = 1;
        //    }
        //    B_Message.Update(meinfo);
        //    Response.Redirect("Message.aspx");
        //}
        //回复
        protected void BtnReply_Click(object sender, EventArgs e)
        {
            Response.Redirect("MessageSend.aspx?id=" + ViewState["MsgID"].ToString() + "");
        }
        public void GetFile(string Attachment)
        {
            if (!string.IsNullOrEmpty(Attachment))
            {
                string[] af = Attachment.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string h = "";
                for (int i = 0; i < af.Length; i++)
                {
                    h += "<span class='disupFile'>";
                    h += GroupPic.GetShowExtension(GroupPic.GetExtName(af[i]));
                    string filename = Path.GetExtension(af[i]).ToLower().Replace(".", "");
                    //if (filename == "docx" || filename == "doc" || filename == "xls" || filename == "xlsx")
                    //{
                    //    h += af[i].Split('/')[(af[i].Split('/').Length - 1)] + "</a><a class='filea' href='/Common/ShowFlash.aspx?path=" + Server.UrlEncode(af[i]) + "&type=1' target='_blank'>(预览)</a><a href='" + af[i] + "'>(下载)</span>";
                    //}
                    //else
                    h += "<a href='" + af[i] + "' title='点击下载'>" + af[i].Split('/')[(af[i].Split('/').Length - 1)] + "</a></span>";
                }
                publicAttachTD.InnerHtml = h;
            }
        }
    }
}