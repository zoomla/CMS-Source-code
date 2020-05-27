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

namespace ZoomLaCMS.Plat.Mail
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
                LblSender.Text = "<a href='MessageSend.aspx?id=" + Mid + "'>" + buser.GetUserNameByIDS(messInfo.Sender) + "</a>";
                string userNames = buser.GetUserNameByIDS(messInfo.Incept.ToString());
                LblIncept.Text = StringHelper.SubStr(userNames);
                LblSendTime.Text = messInfo.PostDate.ToString("yyyy年MM月dd日 HH:mm");
                LblTitle.Text = messInfo.Title;
                txt_Content.Text = messInfo.Content;
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
