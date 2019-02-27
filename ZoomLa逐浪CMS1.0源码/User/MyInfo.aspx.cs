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
using ZoomLa.Components;
namespace ZoomLa.WebSite.User
{
    public partial class MyInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User buser = new B_User();
            buser.CheckIsLogin();
            this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
            string UserName = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
            M_UserInfo info = buser.GetUserByName(UserName);
            if (info.IsNull)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                this.LblUserName.Text = info.UserName;
                this.LblEmail.Text = info.Email;
                this.LblGroupName.Text = info.GroupID.ToString();
                this.LblUnreadMsg.Text = B_Message.UserMessCount(UserName).ToString();
                this.LblRegTime.Text = info.RegTime.ToString();
                this.LblLoginTimes.Text = info.LoginTimes.ToString();
                this.LblLastLoginTime.Text = info.LastLoginTimes.ToString();
                this.LblLastLoginIP.Text = info.LastLoginIP;
                this.LblLastLockTime.Text = info.LastLockTime.ToString();
                this.LblChgPswTime.Text = info.LastPwdChangeTime.ToString();
            }
        }
    }
}