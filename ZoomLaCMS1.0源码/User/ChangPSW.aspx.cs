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

namespace ZoomLa.WebSite.User
{
    public partial class ChangPSW : System.Web.UI.Page
    {
        private B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            buser.CheckIsLogin();
            this.LblSiteName.Text = SiteConfig.SiteInfo.SiteName;
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                string UserName = HttpContext.Current.Request.Cookies["UserState"]["LoginName"];
                M_UserInfo info = buser.GetUserByName(UserName);
                if (info.UserPwd != StringHelper.MD5(this.TxtOldPassword.Text.Trim()))
                {
                    function.WriteErrMsg("输入的原密码与登录密码不相同！");
                }
                info.UserPwd = StringHelper.MD5(this.TxtPassword.Text.Trim());
                buser.UpDateUser(info);
                function.WriteSuccessMsg("修改密码成功！");
            }
        }
        protected void BtnCancle_Click(object sender, EventArgs e)
        {
            this.TxtOldPassword.Text = "";
            this.TxtPassword.Text = "";
            this.TxtPassword2.Text = "";
        }
    }

}