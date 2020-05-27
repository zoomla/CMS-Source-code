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
namespace ZoomLaCMS.Manage.User.Mail
{
    public partial class MailShow : CustomerPageAction
    {
        B_User bull = new B_User();
        B_MailInfo bmll = new B_MailInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            ZoomLa.Common.function.AccessRulo();
            //B_Admin badmin = new B_Admin();
            //badmin.CheckIsLogin();
            if (Request.QueryString["id"] != null)
            {
                M_MailInfo mm = bmll.GetSelect(int.Parse(Request.QueryString["id"].ToString()));

                tdtitle.InnerHtml = "<strong>" + mm.MailTitle + "</strong>";
                tdtime.InnerHtml = "发送时间：" + mm.MailSendTime;
                tdcontext.InnerHtml = mm.MailContext;
            }
            Call.SetBreadCrumb(Master, "<li>后台管理</li><li>邮件订阅</li><li>已发送邮件</li><li>查看邮件信息</li>");
        }
    }
}