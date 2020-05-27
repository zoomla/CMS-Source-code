<%@ WebHandler Language="C#" Class="api_qq_im" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using ZoomLa.BLL.API;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.PdoApi.TencentIM;

public class api_qq_im :API_Base,IHttpHandler {

    C_QQIM_Bll imBll = new C_QQIM_Bll();
    B_User buser = new B_User();
    string OpenID { get { return Req("OpenID"); } }
    public void ProcessRequest(HttpContext context)
    {
        //M_UserInfo mu = buser.GetLogin();
        M_UserInfo mu = B_User_API.GetLogin(OpenID);
        retMod.retcode = M_APIResult.Failed;
        retMod.callback = CallBack;
        if (mu == null || mu.IsNull) { retMod.retmsg = "[" + OpenID + "]用户未登录"; RepToClient(retMod); }
        try
        {
            switch (Action)
            {
                case "login"://使用独立模式,获取用户的签名与IM身份
                    M_QQIM_User umod = imBll.GetSignByUser(mu);
                    retMod.result = "{\"sign\":\"" + umod.Sign + "\",\"identity\":\"" + umod.IM_Identity + "\"}";
                    retMod.retcode = M_APIResult.Success;
                    break;
                case "getuser"://获取指定用户的用户ID,昵称,头像信息(如无IM信息的话,则新建)
                    M_UserInfo tmu = buser.SelReturnModel(Convert.ToInt32(Req("uid")));
                    if (tmu == null||tmu.IsNull) { retMod.retmsg = "指定用户不存在"; }
                    else
                    {
                        M_QQIM_User tumod = imBll.SelModelByUid(tmu.UserID);
                        if (tumod == null) { imBll.GetSignByUser(tmu); tumod = imBll.SelModelByUid(tmu.UserID); }
                        retMod.retcode = M_APIResult.Success;
                        tmu.UserFace = string.IsNullOrEmpty(tmu.UserFace) ? "/Images/userface/noface.png" : tmu.UserFace;
                        retMod.result = "{\"uid\":\"" + mu.UserID + "\",\"imid\":\"" + tumod.IM_Identity + "\",\"userface\":\"" + tmu.UserFace + "\",\"honeyname\":\"" + B_User.GetUserName(tmu.HoneyName, tmu.UserName) + "\"}";
                    }
                    break;
                default:
                    retMod.retmsg = "[" + Action + "]接口不存在";
                    break;
            }
        }
        catch (Exception ex) { retMod.retmsg = ex.Message; }
        RepToClient(retMod);
    }
    public bool IsReusable { get { return false; } }
}