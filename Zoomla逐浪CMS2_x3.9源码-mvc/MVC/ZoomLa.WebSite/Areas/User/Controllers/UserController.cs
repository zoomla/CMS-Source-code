using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Plat;
using ZoomLa.PdoApi.SinaWeiBo;

namespace ZoomLaCMS.Areas.User.Controllers
{
    //用户方面相关操作,如登录,退出,修改信息等
    public class UserController : Controller
    {
        B_User buser = new B_User();
        public int getVcount
        {
            get
            {
                if (this.Session["ValidateCount"] == null)
                {
                    this.Session["ValidateCount"] = 0;
                }
                return Convert.ToInt32(this.Session["ValidateCount"]);
            }
            set
            {
                if (value >= 0)
                {
                    this.Session["ValidateCount"] = value;
                }
                else
                {
                    this.Session["ValidateCount"] = 0;
                }
            }
        }
        //自有账号登录(跳转页面)
        public string Login_Ajax(string uname, string upwd, string vcode, int regid)
        {
            string err = "";
            M_APIResult retMod = new M_APIResult(M_APIResult.Failed);
            if (SiteConfig.UserConfig.EnableCheckCodeOfLogin || getVcount >= 3)
            {
                if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], vcode.Trim()))
                {
                    retMod.retmsg = "验证码不正确";
                    return retMod.ToString();
                }
            }
            M_UserInfo mu = LoginByRegID(ref err, uname, upwd, regid);
            if (mu.IsNull) { getVcount++; retMod.retmsg = err; }
            else if (mu.Status != 0) { retMod.retmsg = "你的帐户未通过验证或被锁定，请与网站管理员联系"; }
            else {
                retMod.retcode = M_APIResult.Success;
                buser.SetLoginState(mu, "Month");
            }
            return retMod.ToString();
        }
        //新浪社会化登录(跳转链接)
        public void Login_Sina()
        {
            B_User_Token tokenBll = new B_User_Token();
            M_User_Token tokenMod = tokenBll.SelModelByUid(buser.GetLogin().UserID);
            if (tokenMod != null)//已存有用户信息,则直接登录
            {
                SinaHelper sinaBll = new SinaHelper(tokenMod.SinaToken);
                Response.Redirect(sinaBll.GetAuthUrl());
            }
            else
            {
                SinaHelper sinaBll = new SinaHelper("");
                Response.Redirect(sinaBll.GetAuthUrl());
            }
        }
        public void Logout()
        {
            buser.ClearCookie();
            string url = "/User/Index/Login";
            string ReturnUrl = Request.QueryString["ReturnUrl"];
            if (!string.IsNullOrEmpty(ReturnUrl)) { url += "?ReturnUrl=" + ReturnUrl; }
            Response.Write("<script>setTimeout(function(){location='" + url + "';},500);</script>");
        }
        private M_UserInfo LoginByRegID(ref string errmsg, string AdminName, string AdminPass,int RegID)
        {
            AdminName = AdminName.Replace(" ", "");
            AdminPass = AdminPass.Replace(" ", "");
            M_UserInfo info = new M_UserInfo(true);
            switch (RegID)
            {
                case 1:
                    errmsg = "邮箱名或密码错误";
                    info = buser.AuthenticateEmail(AdminName, AdminPass);
                    break;
                case 2:
                    errmsg = "用户ID或密码错误";
                    info = buser.AuthenticateID(DataConverter.CLng(AdminName), AdminPass);
                    break;
                case 3:
                    errmsg = "手机号码或密码错误";
                    info = buser.AuthenByMobile(AdminName, AdminPass);
                    break;
                case 4://默认不开放
                    errmsg = "用户名或密码错误";
                    info = buser.AuthenByUME(AdminName, AdminPass);
                    break;
                default:
                    errmsg = "用户名或密码错误";
                    info = buser.AuthenticateUser(AdminName, AdminPass);
                    break;
            }
            return info;
        }
    }
}
