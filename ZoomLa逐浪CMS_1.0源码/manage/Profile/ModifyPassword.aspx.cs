namespace ZoomLa.WebSite
{
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
    using ZoomLa.Web;
    using System.Text;
    using ZoomLa.Common;
    

    public partial class ModifyPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            badmin.CheckMulitLogin();
            M_AdminInfo info = B_Admin.GetAdminByAdminName(HttpContext.Current.Request.Cookies["ManageState"]["LoginName"]);
            if (!info.EnableModifyPassword)
            {
                function.WriteErrMsg("您的帐户不允许修改密码！");
            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (this.IsValid)
            {
                M_AdminInfo info = B_Admin.GetAdminByAdminName(HttpContext.Current.Request.Cookies["ManageState"]["LoginName"]);
                if (info.AdminPassword != StringHelper.MD5(this.TxtOldPassword.Text.Trim()))
                {
                    function.WriteErrMsg("输入的原密码与登录密码不相同！");
                }
                info.AdminPassword = StringHelper.MD5(this.TxtPassword.Text.Trim());
                B_Admin.Update(info);
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