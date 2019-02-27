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
using ZoomLa.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace User
{
    public partial class MessageRead : CustomerPageAction
    {
        B_Message msgBll = new B_Message();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["ID"]); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!B_ARoleAuth.Check(ZLEnum.Auth.user, "MessManage"))
                {
                    function.WriteErrMsg("没有权限进行此项操作");
                }
                M_Message messInfo = B_Message.GetMessByID(DataConverter.CLng(Request.QueryString["id"]));
                LblSender.Text = messInfo.Sender.ToString();
                LblIncept.Text = messInfo.Incept.ToString();
                LblSendTime.Text = messInfo.PostDate.ToString();
                LblTitle.Text = messInfo.Title;
                txt_Content.Text = messInfo.Content;
                Attach_Hid.Value = messInfo.Attachment;
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li>查看短消息</li>");
            }
        }
        //删除
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            msgBll.DelByAdmin(Mid.ToString());
            Response.Redirect("Message.aspx");
        }
        //回复
        protected void BtnReply_Click(object sender, EventArgs e)
        {
            Response.Redirect("MessageSend.aspx?id=" + Request.QueryString["id"] + "");
        }
        //返回
        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Message.aspx");
        }
    }
}