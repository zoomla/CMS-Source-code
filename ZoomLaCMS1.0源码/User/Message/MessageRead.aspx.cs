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
namespace ZoomLa.WebSite.User
{
    public partial class MessageRead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["MsgID"] = Request.QueryString["id"].ToString();
            if (!Page.IsPostBack)
            {
                this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
                M_Message messInfo = B_Message.GetMessByID(DataConverter.CLng(ViewState["MsgID"].ToString()));
                this.LblSender.Text = messInfo.Sender.ToString();
                this.LblIncept.Text = messInfo.Incept.ToString();
                this.LblSendTime.Text = messInfo.PostDate.ToString();
                this.LblTitle.Text = messInfo.Title;
                this.LblContent.Text = messInfo.Content;
            }
        }
        //删除
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            B_Message.DelteById(DataConverter.CLng(ViewState["MsgID"].ToString()));
            Response.Redirect("Message.aspx");
        }
        //回复
        protected void BtnReply_Click(object sender, EventArgs e)
        {
            Response.Redirect("MessageSend.aspx?id=" + ViewState["MsgID"].ToString() + "");
        }
        //返回
        protected void BtnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Message.aspx");
        }
    }
}