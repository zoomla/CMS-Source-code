using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.User.Addon;
using ZoomLa.Model;
using ZoomLa.Model.User;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZoomLa.Common;
using System.Xml;
using ZoomLa.BLL.Third;
using ZoomLa.Model.Third;

public partial class User_WxLogin : System.Web.UI.Page
{
    B_UserAPP AppBll = new B_UserAPP();
    B_User buser = new B_User();
    B_Third_Info thirdBll = new B_Third_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        //string appid = "wx64dcf57278caa037";
        //string redirect_uri = "http://www.zljdsc.com/test/WxLogin.aspx";
        //string secret = "71c21ab790c34c05e1290a26ff025c70";
        M_Third_Info wxInfo = thirdBll.SelModelByName("wechat");
        string state = "";
        if (string.IsNullOrEmpty(Request.QueryString["code"]))//微信登录操作
        {
            state = function.GetRandomString(8).ToLower();
            Session.Add("wx_state", state);
            string url = "https://open.weixin.qq.com/connect/qrconnect?appid=" + wxInfo.ID + "&redirect_uri=" + HttpUtility.UrlEncode(wxInfo.CallBackUrl) + "&response_type=code&scope=snsapi_login&state=" + state + "#wechat_redirect";
            Response.Redirect(url);
        }
        else//登录返回操作
        {
            //检测state参数是否匹配
            if (Session["wx_state"] == null || !Session["wx_state"].Equals(Request.QueryString["state"])) { function.WriteErrMsg("参数错误!"); }
            Session["wx_state"] = null;
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + wxInfo.ID + "&secret=" + wxInfo.Secret + "&code=" + Request.QueryString["code"] + "&grant_type=authorization_code";
            JObject wxinfo = GetWxInfo(url);
            Appinfo infomod = AppBll.SelModelByOpenID(wxinfo["openid"].ToString(), "wechat");
            if (infomod == null)//新用户注册
            {
                infomod = GetNewUser(wxinfo);
            }
            M_UserInfo mu = buser.SelReturnModel(infomod.UserID);
            buser.SetLoginState(mu);
            Response.Redirect("/User");
        }
    }
    //获得微信返回信息(OpenID等)
    private JObject GetWxInfo(string url)
    {
        WebClient client = new WebClient();
        string result = client.DownloadString(url);
        return JsonConvert.DeserializeObject<JObject>(result);
    }
    //创建新用户
    private Appinfo GetNewUser(JObject wxinfo)
    {
        Appinfo infomod = new Appinfo();
        M_UserInfo mu = new M_UserInfo();
        mu.UserName = "wx_" + function.GetRandomString(8).ToLower();
        mu.UserPwd = StringHelper.MD5(function.GetRandomString(8));
        mu.Email = mu.UserName + "@random.com";
        mu.UserID = buser.Add(mu);

        infomod.UserID = mu.UserID;
        infomod.OpenID = wxinfo["openid"].ToString();
        infomod.SourcePlat = "wechat";
        infomod.Token = wxinfo["access_token"].ToString();
        AppBll.Insert(infomod);
        return infomod;
    }
}