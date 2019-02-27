using System;
using System.Web;
using System.Web.UI;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.Common;
using System.Security.Cryptography;
using GoogleAuthenticator;
using ZoomLa.BLL.Third;
using ZoomLa.Model.Third;
using ZoomLa.PdoApi.SinaWeiBo;
using ZoomLa.Model.Plat;
using ZoomLa.BLL.Plat;

public partial class User_login : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_Third_Info thirdBll = new B_Third_Info();
    public int RegID { get { return DataConverter.CLng(Request.QueryString["RegID"]); } }
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
    //不允许http跳转,不允许带空格,如未指定返回Url,则以后台为准
    public string ReturnUrl
    {
        get
        {
            if (ViewState["ReturnUrl"] == null)
            {
                string url = HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"] ?? "").Split(' ')[0];
                url = string.IsNullOrEmpty(url) ? SiteConfig.SiteOption.LoggedUrl : url;
                ViewState["ReturnUrl"] = url;
            }
            return ViewState["ReturnUrl"] as string;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string result = "", action = Request.Form["action"];
            switch (action)
            {
                case "login":
                    result =AjaxAuth(Request.Form["user"], Request.Form["pwd"], Request.Form["vcode"], Request.Form["zncode"]);
                    break;
                default:
                    break;
            }
            Response.Write(result); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            M_UserInfo mu = buser.GetLogin();
            if (!mu.IsNull)// && mu.Status == 0
            {
                Response.Redirect(ReturnUrl);
            }
            #region 社会化登录
            M_Third_Info SinaInfo = thirdBll.SelModelByName("Sina");
            if (SinaInfo != null && SinaInfo.Enabled)
            {
                appSina.Visible = true;
            }
            M_Third_Info qqInfo = thirdBll.SelModelByName("QQ");
            if (qqInfo != null && qqInfo.Enabled)
            {
                qq_span.Visible = true;
                qq_a.HRef = "https://graph.qq.com/oauth2.0/authorize?client_id=" + qqInfo.ID.Trim() + "&response_type=token&scope=all&redirect_uri=" + qqInfo.CallBackUrl.Trim();
            }
            //M_Third_Info baiduInfo = thirdBll.SelModelByName("Baidu");
            //if (baiduInfo != null && baiduInfo.Enabled)
            //{
            //    appBaidu.Visible = true;
            //}
            M_Third_Info wechatInfo = thirdBll.SelModelByName("wechat");
            if (wechatInfo != null && wechatInfo.Enabled)
            {
                wechat_span.Visible = true;
            }
            plat_li.Visible = qqInfo.Enabled || SinaInfo.Enabled;// || baiduInfo.Enabled 
            #endregion
            EMail_A.HRef = "login.aspx?RegID=1&returnurl=" + ReturnUrl;
            ID_A.HRef = "login.aspx?RegID=2&returnurl=" + ReturnUrl;
            User_A.HRef = "login.aspx?returnurl=" + ReturnUrl;
            switch (RegID)
            {
                case 1:
                    if (SiteConfig.UserConfig.EmailRegis)
                    {
                        TxtUserName.Attributes["placeholder"] = "邮箱名";
                        EMail_A.Visible = false;
                    }
                    else
                    {
                        function.WriteErrMsg("系统未启用Email注册登录功能!", "/User/Login.aspx?RegID=0");
                    }
                    break;
                case 2://用户ID
                    if (SiteConfig.UserConfig.UserIDlgn)
                    {
                        TxtUserName.Attributes["placeholder"] = "用户ID";
                        ID_A.Visible = false;
                    }
                    else
                    {
                        function.WriteErrMsg("系统未启用ID注册登录功能!", "/User/Login.aspx");
                    }
                    break;
                //case 3://手机号登录
                //    {
                //        TxtUserName.Attributes["placeholder"] = "手机号码";
                //    }
                //    break;
                //case 4:
                //    {
                //        TxtUserName.Attributes["placeholder"] = "用户名/手机号/邮箱";
                //    }
                //    break;
                case 0:
                default://用户名登录
                    User_A.Visible = false;
                    break;
            }
        }
        if (SiteConfig.UserConfig.EnableCheckCodeOfLogin||getVcount>=3)
        {
            this.trVcodeRegister.Visible = true;
        }
    }
    //ajax登录
    public string AjaxAuth(string user, string pwd, string vcode, string zncode = "")
    {
        if (SiteConfig.UserConfig.EnableCheckCodeOfLogin || getVcount >= 3)
        {
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], vcode.Trim()))
            {
                return "验证码不正确";
            }
        }
        string errmsg = "";
        M_UserInfo info = LoginByRegID(ref errmsg, user, pwd);
        //如果用户Model是空对象则表明登录失败
        if (info.IsNull)
        {
            getVcount++;
            if (getVcount == 3) return "True";//刷出验证码
            return errmsg;
        }
        else
        {
            getVcount = 0;
            if (info.Status != 0)
            {
                return "你的帐户未通过验证或被锁定，请与网站管理员联系";
            }
            if (!string.IsNullOrEmpty(info.ZnPassword))
            {
                byte[] secretBytes = Base32String.Instance.Decode(info.ZnPassword);
                PasscodeGenerator pcg = new PasscodeGenerator(new HMACSHA1(secretBytes));
                if (!pcg.VerifyTimeoutCode(zncode))
                {
                    return "动态口令错误";
                }
            }
        }
        return "True";
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //根据用户名和密码验证会员身份，并取得会员信息
        string errmsg = "";
        M_UserInfo info = LoginByRegID(ref errmsg, TxtUserName.Text, TxtPassword.Text);
        if (info.UserID < 1)
        {
            getVcount++;
            function.WriteErrMsg(errmsg, "/User/Login.aspx?RegID=" + RegID + "&returnurl=" + ReturnUrl);
        }
        else
        {
            getVcount = 0;
            if (info.Status != 0)
            {
                function.WriteErrMsg("你的帐户未通过验证或被锁定，请与网站管理员联系", "/User/Login.aspx?RegID="+RegID+"&returnurl=" + ReturnUrl);
            }
            if (!string.IsNullOrEmpty(info.ZnPassword))
            {
                byte[] secretBytes = Base32String.Instance.Decode(info.ZnPassword);
                PasscodeGenerator pcg = new PasscodeGenerator(new HMACSHA1(secretBytes));
                if (!pcg.VerifyTimeoutCode(UserCode_T.Text))
                {
                    function.WriteErrMsg("动态口令错误！");
                }
            }
            buser.SetLoginState(info, "Month");
            Response.Redirect(ReturnUrl);
        }
    }
    protected void appSina_Click(object sender, EventArgs e)
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
    public string GetBKImg() 
    {
        if (SiteConfig.SiteOption.SiteManageMode == 1) { return ""; }
        else { return "http://code.z01.com/user_login.jpg"; }
    }
    //根据用户名和密码验证会员身份，并取得会员信息
    private M_UserInfo LoginByRegID(ref string errmsg, string AdminName, string AdminPass)
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