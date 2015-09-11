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
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
namespace ZoomLa.WebSite.User
{
    public partial class MessageSend : System.Web.UI.Page
    {
        protected B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            buser.CheckIsLogin();
            this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(base.Request.QueryString["id"]))
                {
                    M_Message messInfo = B_Message.GetMessByID(DataConverter.CLng(base.Request.QueryString["id"]));
                    if (!messInfo.IsNull)
                    {
                        this.TxtInceptUser.Text = messInfo.Sender;                        
                        this.TxtTitle.Text = "回复:" + messInfo.Title;
                    }
                }
            }
        }

        //发送
        protected void BtnSend_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                M_Message messInfo = new M_Message();
                messInfo.Incept = this.TxtInceptUser.Text;
                string UserName = HttpContext.Current.Request.Cookies["UserState"]["UserName"];
                messInfo.Sender = UserName;
                messInfo.Title = this.TxtTitle.Text;
                messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToShortDateString());
                messInfo.Content = this.EditorContent.Text;
                B_Message.Add(messInfo);
                Response.Redirect("Message.aspx");
            }
        }
        //清除
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            this.EditorContent.Text = "";
        }
    }
}