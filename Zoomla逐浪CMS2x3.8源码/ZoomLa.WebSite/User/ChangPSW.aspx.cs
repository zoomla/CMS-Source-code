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
using ZoomLa.API;
using ZoomLa.DZNT;
using ZoomLa.SQLDAL;
using GoogleAuthenticator;
using System.Security.Cryptography;

namespace ZoomLa.WebSite.User
{
    public partial class ChangPSW : Page
    {
        B_User buser = new B_User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string action = Request.Form["action"];
                string result = "";
                if (action.Equals("getkey"))
                {
                    M_UserInfo usermod = buser.GetLogin(false);
                    string keys = function.GetRandomString(16, 3).ToUpper();
                    if (Session[usermod.UserID + "_key"] == null)
                        Session.Add(usermod.UserID + "_key", keys);
                    else
                        Session[usermod.UserID + "_key"] = keys;
                    result = "{\"sitename\":\"" + Request.Url.Host + usermod.UserName + "\",\"key\":\"" + keys + "\"}";

                }
                Response.Write(result); Response.Flush(); Response.End();
            }
            else if (!IsPostBack)
            {
                M_UserInfo mu = buser.GetLogin(false);
                if (string.IsNullOrEmpty(mu.ZnPassword))
                {
                    Enable_Btn.Visible = true;
                    Enabled_Span.Visible = false;
                }
            }
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            M_UserInfo mu = buser.GetLogin();
            string oldPwd = StringHelper.MD5(TxtOldPassword.Text.Trim());
            if (!mu.UserPwd.Equals(oldPwd)) { function.WriteErrMsg("原密码错误,请重新输入"); }
            if (!TxtPassword.Text.Equals(TxtPassword2.Text)) { function.WriteErrMsg("新密码与确认密码不匹配"); }
            mu.UserPwd = StringHelper.MD5(TxtPassword.Text.Trim());
            buser.UpdateByID(mu);
            buser.ClearCookie();
            function.WriteSuccessMsg("修改密码成功,请重新登录", "/User/Login.aspx");
        }
        //--------------智能码
        protected void SetUserKey_B_Click(object sender, EventArgs e)
        {
            M_UserInfo usermod = buser.GetLogin(false);
            usermod.ZnPassword = Session[usermod.UserID + "_key"].ToString();
            buser.UpDateUser(usermod);
            Response.Redirect(Request.RawUrl);
        }
        void DelKey(M_UserInfo mu)
        {
            mu.ZnPassword = "";
            buser.UpDateUser(mu);
            function.WriteSuccessMsg("已成功解除口令绑定！");
        }
        protected void ChangePWD_B_Click(object sender, EventArgs e)
        {
            M_UserInfo info = buser.AuthenticateUser(UserName_L.Text, PassWord_T.Text);
            if (info.IsNull) function.WriteErrMsg("用户密码错误!");
            byte[] secretBytes = Base32String.Instance.Decode(info.ZnPassword);
            PasscodeGenerator pcg = new PasscodeGenerator(new HMACSHA1(secretBytes));
            if (!pcg.VerifyTimeoutCode(Code_T.Text)) function.WriteErrMsg("动态口令错误!");
            if (ChangeType_Hid.Value.Equals("1"))
                DelKey(info);
            else
                function.Script(this, "SetKey(1)");
        }
    }

}