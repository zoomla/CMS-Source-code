<%@ WebHandler Language="C#" Class="signin" %>

using System;
using System.Web;
using ZoomLa.BLL.API;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

//订单相关操作
public class signin : API_Base, IHttpHandler
{
    B_User buser = new B_User();
    B_Plat_Sign signBll = new B_Plat_Sign();
    public void ProcessRequest(HttpContext context)
    {
        M_UserInfo mu = buser.GetLogin();
        retMod.retcode = M_APIResult.Failed;
        try
        {
            switch (Action)
            {
                case "signinit":
                    {
                        retMod.result = signBll.GetSignType(mu);
                        retMod.addon = signBll.GetToDaySign(mu);
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "signin":
                    {
                        retMod.result = signBll.SignIn(mu).ToString();
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "signout":
                    {
                        retMod.result = signBll.SignOut(mu).ToString();
                        retMod.retcode = M_APIResult.Success;
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
