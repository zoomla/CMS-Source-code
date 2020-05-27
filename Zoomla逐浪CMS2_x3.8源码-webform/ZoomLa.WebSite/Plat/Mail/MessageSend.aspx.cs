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
using ZoomLa.API;
using ZoomLa.DZNT;
using System.Net;
using System.IO;
using System.Text;
using System.Data.SqlClient;
namespace ZoomLa.WebSite.User
{
    public partial class MessageSend : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Message msgBll = new B_Message();
        //编辑草稿
        public int Drafid { get { return DataConverter.CLng(Request.QueryString["DraftID"]); } }
        //对某封邮件回复
        public int ReplyID { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        //------------------------------------------
        //直接发送给用户
        public int ToID { get { return DataConverter.CLng(Request.QueryString["ToID"]); } }
        //直接发送给用户By用户名
        public string UName { get { return Request.QueryString["name"] ?? ""; } }
        public string MTitle { get { return Request.QueryString["Title"] ?? ""; } }
        public string Content { get { return Request.QueryString["Content"] ?? ""; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ReplyID > 0)
                {
                    M_Message messInfo = msgBll.SelReturnModel(ReplyID);
                    if (!messInfo.IsNull)
                    {
                        TxtInceptUser.Text = buser.GetUserNameByIDS(messInfo.Sender);
                        TxtTitle.Text = "回复:" + messInfo.Title;
                    }
                }
                else if (Drafid > 0)
                {
                    M_Message messInfo = msgBll.SelReturnModel(Drafid);
                    TxtInceptUser.Text = buser.GetUserNameByIDS(messInfo.Incept);
                    TxtTitle.Text = messInfo.Title;
                    EditorContent.Text = messInfo.Content;
                }
                else
                {
                    if (ToID > 0)
                    {
                        M_UserInfo messInfo = buser.GetUserByUserID(ToID);
                        TxtInceptUser.Text = messInfo.UserName + ",";
                    }
                    TxtInceptUser.Text = UName;
                    TxtTitle.Text = MTitle;
                    EditorContent.Text = Content;
                }
            }
        }
        protected void Send_Btn_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            M_Message messInfo = new M_Message();
            messInfo.Incept = buser.GetIdsByUserName(TxtInceptUser.Text.Trim());
            if (string.IsNullOrEmpty(messInfo.Incept.Trim(','))) { function.WriteErrMsg("收件人不存在!"); }
            messInfo.Sender = mu.UserID.ToString();
            messInfo.Title = Server.HtmlEncode(TxtTitle.Text);
            messInfo.Content = EditorContent.Text;
            messInfo.Savedata = 0;
            messInfo.Receipt = "";
            messInfo.Attachment = Attach_Hid.Value;
            msgBll.GetInsert(messInfo);
            function.WriteSuccessMsg("发送成功", "MessageOutbox.aspx");
        }
        protected void Draft_Btn_Click(object sender, EventArgs e)
        {
            M_Message messInfo = new M_Message();
            messInfo.Incept = TxtInceptUser.Text;
            messInfo.Sender = buser.GetLogin().UserID.ToString();
            messInfo.Title = Server.HtmlEncode(TxtTitle.Text);
            messInfo.Content = EditorContent.Text;
            messInfo.Savedata = 1;
            messInfo.Receipt = "";
            msgBll.GetInsert(messInfo);
            Response.Redirect("MessageDraftbox.aspx");
        }
    }
}