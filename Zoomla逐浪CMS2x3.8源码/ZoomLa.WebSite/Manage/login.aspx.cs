namespace ZoomLa.WebSite.Manage
{
    using System;
    using System.Security.Cryptography;
    using ZoomLa.BLL;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using GoogleAuthenticator;
   
    public partial class login : System.Web.UI.Page
    {
        public string ReturnUrl { get { return Request.QueryString["r"] ?? ""; } }
        B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                string result = "", action = (Request.Form["action"] ?? "");
                switch (action)
                {
                    case "login":
                        try
                        {
                            result = AjaxVaild(Request.Form["user"], Request.Form["pwd"], Request.Form["vcode"], Request.Form["zncode"]);
                        }
                        catch (Exception ex) { result = ex.Message; }
                        //ZLLog.ToCE(ZLEnum.Log.alogin, new M_Log()
                        //{
                        //    UName = Request.Form["user"],
                        //    Action = "管理员登录",
                        //    Message = "管理员登录,AJAX验证:" + result
                        //});
                        break;
                    default:
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End(); return;
            }
            if (!IsPostBack)
            {
                if (badmin.CheckLogin())
                {
                    Response.Redirect(CustomerPageAction.customPath2 + "Default.aspx");
                }
                //显示申请功能
                if (SiteConfig.SiteOption.RegManager == 1)
                {
                    test_Link.Visible = true;
                    test_Link.HRef = CustomerPageAction.customPath2 + "AccountForm.aspx";
                }
                if (string.IsNullOrEmpty(Request.QueryString["hasAccount"]) && SiteConfig.SiteOption.RegManager == 1)
                { Server.Transfer(CustomerPageAction.customPath2 + "AccountForm.aspx"); }
            }
            if (!SiteConfig.SiteOption.EnableSiteManageCode)
            {
                safecode.Visible = false;
            }
            if (!IsPostBack && SiteConfig.SiteOption.EnableSoftKey)
            {
                TxtPassword.Attributes.Add("onfocus", "ShowSoftKeyboard(this)");
            }
            if (getVcount < 3)
            {
                VCode.Visible = false;
                VCode_img.Visible = false;
                CodeLi.Visible = false;
            }
            else
            {
                //IbtnEnter.Attributes.Add("onclick", "return code();");
                VCode.Visible = true;
                VCode_img.Visible = true;
                CodeLi.Visible = true;
            }
            if (!string.IsNullOrEmpty(SiteConfig.SiteOption.AdminKey))
            { ZnCode_Li.Visible = true; }
        }
        /// <summary>
        /// 用户ajax登录验证
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <param name="vcode">vcode:验证码</param>
        /// <param name="zncode">zncode:动态口令</param>
        /// <param name="txtadmincode">txtadmincode:管理码</param>
        /// <returns></returns>
        public string AjaxVaild(string user,string pwd,string vcode,string zncode="",string txtadmincode="")
        {
            user = user.Trim(); pwd = pwd.Trim();
            if (getVcount >= 3)
            {
                if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], vcode))
                {
                    return "验证码不正确";
                }
            }
            if (SiteConfig.SiteOption.EnableSiteManageCode && (txtadmincode.Trim() != SiteConfig.SiteOption.SiteManageCode))
            {
                getVcount = getVcount + 1;
                if (getVcount == 3) return "True";//刷出验证码
                return "管理码不正确";
            }
            M_AdminInfo info = B_Admin.AuthenticateAdmin(user, pwd);
            if (info == null || info.IsNull)
            {
                getVcount = getVcount + 1;
                if (getVcount == 3) { return "True"; } else { return "用户名或密码错误！"; }
            }
            else if (info.IsLock)
            {
                return "你的帐户被锁定，请与超级管理员联系";
            }
            else
            {
                if (!string.IsNullOrEmpty(SiteConfig.SiteOption.AdminKey))
                {
                    byte[] keys = Base32String.Instance.Decode(SiteConfig.SiteOption.AdminKey);
                    PasscodeGenerator pass = new PasscodeGenerator(new HMACSHA1(keys));
                    if (!pass.VerifyTimeoutCode(zncode))
                    {
                        return "动态口令不对！";
                    }
                }
                getVcount = 0;//清空
            }
            return "True";
        }
        protected void IbtnEnter_Click(object sender, EventArgs e)
        {
            M_AdminInfo info = B_Admin.AuthenticateAdmin(TxtUserName.Text, TxtPassword.Text);
            if (info == null || info.IsNull)
            {
                getVcount = getVcount + 1;
                function.WriteErrMsg("用户名或密码错误！", "../Login.aspx");
            }
            else
            {
                badmin.SetLoginState(info);
                if (string.IsNullOrEmpty(ReturnUrl))
                { Response.Redirect(CustomerPageAction.customPath2 + "Default.aspx"); }
                else { Response.Redirect(ReturnUrl); }
            }
        }
        // 验证次数
        public int getVcount
        {
            get
            {
                if (Session["ValidateCount"] == null)
                {
                    Session["ValidateCount"] = 0;
                }
                return Convert.ToInt32(Session["ValidateCount"]);
            }
            set
            {
                if (value >= 0)
                {
                    Session["ValidateCount"] = value;
                }
                else
                {
                    Session["ValidateCount"] = 0;
                }
            }
        }
    }
}