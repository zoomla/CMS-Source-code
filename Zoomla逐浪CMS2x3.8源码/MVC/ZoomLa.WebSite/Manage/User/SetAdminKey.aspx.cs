using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Security.Cryptography;
using GoogleAuthenticator;

namespace ZoomLaCMS.Manage.User
{
    public partial class SetAdminKey : System.Web.UI.Page
    {
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Call.SetBreadCrumb(this.Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='UserManage.aspx'>用户管理</a></li><li> <a href='AdminManage.aspx'>管理员管理</a></li><li><a href='" + Request.RawUrl + "' >动态口令加密</a></li>");
                if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "DevCenter")) { function.WriteErrMsg("你没有对该模块的访问权限"); }
                if (!badmin.CheckSPwd(Session["Spwd"] as string))
                    SPwd.Visible = true;
                else
                {
                    if (string.IsNullOrEmpty(SiteConfig.SiteOption.AdminKey))
                        SetImgKey();
                    else
                        Mybind();
                }
            }

        }
        private void Mybind()
        {
            EditDiv.Visible = true;

            Keys_L.Text = SiteConfig.SiteOption.AdminKey;
        }
        private void SetImgKey()
        {
            string keys = function.GetRandomString(16, 3).ToUpper();
            string imgurl = string.Format("otpauth://totp/{0}?secret={1}", StringHelper.ChineseToPY(Call.SiteName) + "Manage", keys);
            if (ViewState["Admin_Key"] == null)
                ViewState.Add("Admin_Key", keys);
            else
                ViewState["Admin_key"] = keys;
            Key_Img.ImageUrl = "/Common/Common.ashx?url=" + imgurl;
            maindiv.Visible = true;
        }
        protected void EnableKey_B_Click(object sender, EventArgs e)
        {
            UpdateConfigKey(ViewState["Admin_Key"].ToString());
            Response.Redirect(Request.RawUrl);
        }
        private void UpdateConfigKey(string key)
        {
            SiteConfig.SiteOption.AdminKey = key;
            SiteConfig.Update();
        }
        void ClearAdminKey()
        {
            SiteConfig.SiteOption.AdminKey = "";
            SiteConfig.Update();
            function.WriteSuccessMsg("已取消动态口令设置");
        }
        protected void Check_B_Click(object sender, EventArgs e)
        {
            byte[] secretBytes = Base32String.Instance.Decode(SiteConfig.SiteOption.AdminKey);
            PasscodeGenerator pcg = new PasscodeGenerator(new HMACSHA1(secretBytes));
            if (!pcg.VerifyTimeoutCode(Code_T.Text)) function.WriteErrMsg("动态口令不正确!");
            if (Type_Hid.Value.Equals("1")) ClearAdminKey();
            Code_Img.Visible = true;
            string imgurl = string.Format("otpauth://totp/{0}?secret={1}", StringHelper.ChineseToPY(Call.SiteName) + "Manage", SiteConfig.SiteOption.AdminKey);
            Code_Img.ImageUrl = "/Common/Common.ashx?url=" + imgurl;
            Keys_L.Visible = true;
            Keys_L.Text = SiteConfig.SiteOption.AdminKey;
        }
    }
}