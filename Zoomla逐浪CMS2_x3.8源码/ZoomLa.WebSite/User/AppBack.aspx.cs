using System;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.BLL.User.Addon;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Third;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.Model.Third;
using Newtonsoft.Json.Linq;
using ZoomLa.BLL.Helper;
using ZoomLa.Components;
using NetDimension.OpenAuth.Sina;
using ZoomLa.PdoApi.SinaWeiBo;

/*
 * 登录--回调--是否存在--验证Token--(openid)注册|直接登录
 */
public partial class AppBack : System.Web.UI.Page
{
    B_User buser = new B_User();
    B_UserAPP appBll = new B_UserAPP();
    M_Third_Info thirdMod = new M_Third_Info();
    B_Third_Info thirdBll = new B_Third_Info();
    public string targetUrl = "/User/Default.aspx";
    private string ZType
    {
        get
        {
            string _type = Request.QueryString["Type"] ?? "";
            _type = string.IsNullOrEmpty(_type) ? "qq" : _type.ToLower();
            return _type;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (function.isAjax())
        {
            string source = Request.Form["source"];
            string openid = Request.Form["openid"];
            int result = HasExist(openid, source) ? M_APIResult.Success : M_APIResult.Failed;
            Response.Write(result); Response.Flush(); Response.End();
        }
        if (!IsPostBack)
        {
            ForAudit_Hid.Value = "1";//开启自动注册,用于人工验证
            if (ForAudit_Hid.Value == "1") { reg_div.InnerHtml = "正在检测用户信息"; }
            switch (ZType)
            {
                case "qq":
                    QQ();
                    break;
                case "sina":
                    Sina();
                    break;
                case "baidu":
                case "wechat":
                default:
                    function.WriteErrMsg("未该启该平台登录");
                    break;
            }
        }
    }
    protected void Register_Click(object sender, EventArgs e)
    {
        UserName_T.Text = UserName_T.Text.Replace(" ", "");
        UserPwd_T.Text = UserPwd_T.Text.Replace(" ", "");
        ConfirmPwd_T.Text = ConfirmPwd_T.Text.Replace(" ", "");
        if (string.IsNullOrEmpty(UserName_T.Text)) { function.WriteErrMsg("用户名不能为空"); }
        if (string.IsNullOrEmpty(UserPwd_T.Text)) { function.WriteErrMsg("密码不能为空"); }
        if (ConfirmPwd_T.Text != UserPwd_T.Text) { function.WriteErrMsg("密码不一致"); }
        if (buser.IsExistUName(UserName_T.Text)) { function.WriteErrMsg("用户名已存在,请更换用户名注册"); }
        //if (buser.IsExist("ume", UserName_T.Text)) { function.WriteErrMsg("用户名已存在,请更换用户名注册"); }
        M_UserInfo mu = new M_UserInfo();
        mu.UserName = UserName_T.Text;
        mu.UserPwd = StringHelper.MD5(UserPwd_T.Text);
        mu.Email = Email_T.Text;
        mu.Question = function.GetRandomString(10);
        mu.Answer = function.GetRandomString(10);
        Appinfo appMod = new Appinfo();
        appMod.SourcePlat = ZType;
        switch (appMod.SourcePlat)
        {
            case "qq":
                {
                    if (string.IsNullOrEmpty(QQ_OpenID_Hid.Value)) { function.WriteErrMsg("绑定信息错误"); }
                    if (appBll.SelModelByOpenID(QQ_OpenID_Hid.Value, "qq") != null) { function.WriteErrMsg("该OpenID已存在,不可重复绑定"); }
                    appMod.OpenID = QQ_OpenID_Hid.Value;
                    //JObject obj = new QQHelper(QQ_Token_Hid.Value, QQ_OpenID_Hid.Value).GetUserInfo();
                    //mu.HoneyName = obj["data"]["nick"].ToString();||obj["nickname"]
                }
                break;
            case "sina":
                {
                    if (string.IsNullOrEmpty(Sina_OpenID_Hid.Value)) { function.WriteErrMsg("未指定openid"); }
                    if (appBll.SelModelByOpenID(Sina_OpenID_Hid.Value, "sina") != null) { function.WriteErrMsg("该OpenID已存在,不可重复绑定"); }
                    appMod.OpenID = Sina_OpenID_Hid.Value;
                }
                break;
            case "wechat":
            default:
                throw new Exception("暂未开启该[" + ZType + "]平台注册");
        }
        mu.UserID = buser.AddModel(mu);
        if (mu.UserID < 1) function.WriteErrMsg("用户添加失败!!!");
        appMod.UserID = mu.UserID;
        appBll.Insert(appMod);
        buser.SetLoginState(mu);
        Response.Redirect(targetUrl);
    }
    //--------
    protected void QQ()
    {
        //?Type=QQ&#access_token=FB82968B4FC03BCB39FB917375D9BB51&expires_in=7776000
        thirdMod = thirdBll.SelModelByName("QQ");
        PlatConfig.QQKey = thirdMod.ID;//底层是使用QQKey,将其传入
        QQ_Div.Visible = true;
        Script_Lit.Text = "<script src=\"http://qzonestyle.gtimg.cn/qzone/openapi/qc_loader.js\" data-appid=\"" + thirdMod.ID + "\" data-callback=\"true\" charset=\"utf-8\"></script>";
        function.Script(this, "QQBind();");
    }
    protected void Sina()
    {
        //?Type=sina&code=9d9dbbd8596b730993e5db69a4771f68
        //产生一次性的code,依此获取Token与用户信息
        M_Third_Info sinaInfo = thirdBll.SelModelByName("Sina");
        string code = Request.QueryString["code"];
        if (string.IsNullOrEmpty(code)) { function.WriteErrMsg("未传指定参数"); }
        //SinaWeiboClient client = new SinaWeiboClient(sinaInfo.Key, sinaInfo.Secret, sinaInfo.CallBackUrl);
        //string token = client.GetAccessTokenByCode(code);
        SinaHelper sinaBll = new SinaHelper(null);
        string openid = sinaBll.GetUidByCode(code); 
        if (HasExist(openid, "sina"))//用户已存在,直接登录
        {
            Response.Redirect(targetUrl);
        }
        else
        {
            //否则选填信息后登录
            if (ForAudit_Hid.Value == "1") { AutoUser("sina", openid); }
            else
            {
                Sina_OpenID_Hid.Value = openid;
                Sina_div.Attributes["display"] = "";
            }
        }
    }
    protected void Baidu() 
    {
        ////百度处理
        //Xml.Load(Server.MapPath("/config/Suppliers.xml"));
        //XBaidu = Xml.SelectSingleNode("SuppliersList/Baidu");
        //string api_key = XBaidu.Attributes["Key"].Value;//使用前先配置web.config中的api_key
        //string secret_key = XBaidu.Attributes["Secret"].Value;//使用前先配置web.config中的secret_key 密钥
        //OAuthMessage msg = null;
        //if (!string.IsNullOrEmpty(Request.QueryString["code"]))
        //{
        //    try
        //    {
        //        //用获取到的Authorization Code换取Access Token
        //        msg = OAuthClient.GetAccessTokenByAuthorizationCode(api_key, secret_key, Request.QueryString["code"], XBaidu.Attributes["CallBackUrl"].Value);
        //        //   SiteConfig.SiteInfo.SiteUrl + "user/AppBack.aspx"
        //        //存储 Access Token等信息以便调用API时使用
        //        Session["token"] = msg;
        //    }
        //    catch (OAuthException oauthException)
        //    {
        //        function.WriteErrMsg(oauthException.Error_description);
        //    }
        //}
        //// 使用http请求数据；
        //try
        //{
        //    client = new BaiduApiClient(msg.Session_key, msg.Session_secret);
        //}
        //catch (Exception)
        //{

        //    function.WriteErrMsg("该页面不可直接访问");
        //}
        ////获取用户登录基本信息
        //IUsersService userServer = null;
        //msg = Session["token"] as OAuthMessage;
        //if (msg != null)
        //{
        //    userServer = client.UserService;
        //}
        //string result = null;
        //result = userServer.GetLoggedInUser();
        //if (!string.IsNullOrEmpty(result))
        //{
        //    //用户id,用户名称,生日,年龄,实名,婚姻,血,星座
        //    InfoHid.Value = userServer.GetInfo();
        //    Apptype.Text = "百度";
        //    Sitename.Text = SiteConfig.SiteInfo.SiteName;
        //    string[] jsons = InfoHid.Value.TrimStart('{').TrimEnd('}').Replace("\"", "").Split(',');
        //    string[] userinfo = jsons[0].Split(':');
        //    info = buser.SeleAppbool(userinfo[1].ToString());

        //    //如果不为空取到了值，则登录
        //    if (info != null)
        //    {
        //        buser.SetLoginState(info, "Day");
        //        Response.Redirect(targetUrl);
        //    }
        //    //否则页面读取用户名，让其注册
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>GetInfo();</script>");
        //}
    }


    //如果存在,则直接登录
    private bool HasExist(string openid, string source)
    {
        bool result = true;
        Appinfo appMod = appBll.SelModelByOpenID(openid, source);
        if (appMod != null)
        {
            M_UserInfo mu = buser.GetSelect(appMod.UserID);
            if (mu.IsNull) { result = false; }
            else { buser.SetLoginState(mu); }
        }
        else
        {
            result = false;
        }
        return result;
    }
    //用于QQ人工审核
    protected void QQAudit_Btn_Click(object sender, EventArgs e)
    {
        AutoUser("qq", QQ_OpenID_Hid.Value);
    }
    //自动注册新用户并登录
    private void AutoUser(string plat, string openid)
    {
        Appinfo appMod = new Appinfo();
        M_UserInfo mu = new M_UserInfo();
        mu.UserName = plat + function.GetRandomString(6).ToLower();
        mu.UserPwd = StringHelper.MD5("123123aa");
        mu.Email = mu.UserName + "@random.com";
        if (string.IsNullOrEmpty(openid)) { function.WriteErrMsg("openid不存在"); }
        appMod = appBll.SelModelByOpenID(openid,plat);
        if (appMod != null && appMod.UserID > 0)
        {
            mu = buser.SelReturnModel(appMod.UserID);
        }
        else
        {
            appMod = new Appinfo();
            mu.UserID = buser.AddModel(mu);
            appMod.OpenID = openid;
            appMod.SourcePlat = plat;
            appMod.UserID = mu.UserID;
            appBll.Insert(appMod);
        }
        buser.SetLoginState(mu);
        Response.Redirect(targetUrl);
    }
}