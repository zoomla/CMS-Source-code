using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using ZoomLa.BLL;
using ZoomLa.BLL.User.Addon;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.User;

namespace ZoomLaCMS.BU
{
    public partial class wxuser : System.Web.UI.Page
    {
        B_UserAPP appBll = new B_UserAPP();
        B_User buser = new B_User();
        B_WX_User wxuserBll = new B_WX_User();

        public string RUrl { get { return Request.QueryString["state"] ?? "/User/"; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WxAPI wxapi = WxAPI.Code_Get();
                string code = Request.QueryString["code"] ?? "";
                if (string.IsNullOrEmpty(code)) { return; }
                string result = APIHelper.GetWebResult("https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + wxapi.AppId.APPID + "&secret=" + wxapi.AppId.Secret + "&code=" + code + "&grant_type=authorization_code");
                JObject obj = (JObject)JsonConvert.DeserializeObject(result);
                string openid = obj["openid"].ToString();
                Appinfo umod = appBll.SelModelByOpenID(openid, "wechat");
                M_UserInfo mu = new M_UserInfo();
                if (umod == null)
                {
                    umod = new Appinfo();
                    M_WX_User wuMod = wxuserBll.SelForOpenid(wxapi.AppId.ID, openid);
                    if (wuMod == null)
                    {
                        wuMod = wxapi.GetWxUserModel(openid);
                        wxuserBll.Insert(wuMod);
                    }
                    mu.UserName = "wx" + DateTime.Now.ToString("yyyyMMddHHmmss") + function.GetRandomString(2).ToLower();
                    mu.UserPwd = StringHelper.MD5(function.GetRandomString(6));
                    mu.Email = function.GetRandomString(10) + "@random.com";
                    mu.HoneyName = wuMod.Name;
                    mu.TrueName = wuMod.Name;
                    mu.UserID = buser.Add(mu);

                    umod.UserID = mu.UserID;
                    umod.SourcePlat = "wechat";
                    umod.OpenID = wuMod.OpenID;
                    appBll.Insert(umod);
                    M_Uinfo mubase = new M_Uinfo();
                    mubase.UserId = mu.UserID;
                    buser.AddBase(mubase);
                }
                else
                {
                    mu = buser.SelReturnModel(umod.UserID);
                }
                //设置为登录状态
                buser.SetLoginState(mu);
                Response.Redirect(RUrl);
            }
        }
    }
}