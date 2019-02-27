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

namespace ZoomLa.WebSite.User
{
    public partial class MessageRead : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_Message msgBll = new B_Message();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin();
                M_Message messInfo = B_Message.GetMessByID(Mid);
                if (messInfo == null) { function.WriteErrMsg("邮件不存在"); }
                if (!msgBll.AllowRead(messInfo.MsgID, mu.UserID.ToString())) { function.WriteErrMsg("无权限阅读该邮件!!!"); }
                Sender_L.Text = buser.GetUserNameByIDS(messInfo.Sender.ToString());
                string userNames = buser.GetUserNameByIDS(messInfo.Incept.ToString());
                Incept_L.Text = StringHelper.SubStr(userNames);
                PostDate_L.Text = messInfo.PostDate.ToString("yyyy年MM月dd日 HH:mm");
                Title_L.Text = messInfo.Title;
                Content_T.Text = messInfo.Content;
                Attach_Hid.Value = messInfo.Attachment;
                if (!string.IsNullOrEmpty(Request.QueryString["read"]))
                {
                    string ccuser = "," + mu.UserID + ",";
                    if (messInfo.CCUser.IndexOf(ccuser) > -1)
                    {
                        CC_Btn.Visible = false;
                    }
                    if (messInfo.ReadUser.IndexOf("," + mu.UserID + ",") == -1)
                    {
                        messInfo.ReadUser += mu.UserID.ToString();
                        messInfo.status = 1;
                        B_Message.Update(messInfo);
                    }
                }
            }
        }
    }
}