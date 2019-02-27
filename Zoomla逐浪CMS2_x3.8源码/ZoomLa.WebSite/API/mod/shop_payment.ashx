<%@ WebHandler Language="C#" Class="shop_payment" %>
using System;
using System.Data;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.User;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using Newtonsoft.Json;

public class shop_payment : API_Base, IHttpHandler
{
    B_User_API buapi = new B_User_API();
    private string OpenID { get { return Req("openid") ?? ""; } }
    public void ProcessRequest(HttpContext context)
    {
        M_UserInfo mu = B_User_API.GetLogin(OpenID);
        retMod.retcode = M_APIResult.Failed;
        //retMod.callback = CallBack;//暂不开放JsonP
        try
        {
            switch (Action)
            {
                case "add":
                    break;
                case "edit":
                    break;
                case "del":
                    break;
                case "list":
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