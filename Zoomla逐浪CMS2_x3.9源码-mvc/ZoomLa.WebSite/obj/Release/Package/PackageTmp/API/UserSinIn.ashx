<%@ WebHandler Language="C#" Class="UserSinIn" %>

using System;
using System.Web;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL.User;

public class UserSinIn : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{
    B_User buser = new B_User();
    B_User_Signin sinBll = new B_User_Signin();
    M_User_Signin sinMod = new M_User_Signin();
    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request.Params["action"];
        string source = context.Request.Params["localtion"];
        switch (action)
        {
            case "sinin":
                SinInUser(context, source);
                break;
            case "sinstatu":
                SinStatu(context, source);
                break;
        }
    }
    void SinStatu(HttpContext context,string localtion)
    {
        M_UserInfo mu = buser.GetLogin();
        int days = sinBll.GetSignCount(mu.UserID, DateTime.Now);
        if (!sinBll.IsSignToday(mu.UserID))
        {
            context.Response.Write("none");
        }
        else
        {
            context.Response.Write(days.ToString());
        }
    }
    void SinInUser(HttpContext context,string localtion)
    {

        M_UserInfo mu = buser.GetLogin();
        if (!sinBll.IsSignToday(mu.UserID)&&mu.UserID>0)
        {
            sinMod.CreateTime = DateTime.Now;
            sinMod.UserID = mu.UserID;
            sinMod.Status = 1;
            sinMod.Remind = mu.UserName + "签到";
            sinBll.Insert(sinMod);
            //buser.AddPurse(buser.GetLogin().UserID, ZoomLa.Components.SiteConfig.UserConfig.SigninPurse);
            int days = sinBll.GetSignCount(mu.UserID, DateTime.Now);
            context.Response.Write(days.ToString());
            //Response.Redirect(Request.RawUrl);
        }
        else
        {
            context.Response.Write("-1");
        }
    }
    public bool IsReusable 
    {
        get {
            return false;
        }
    }


}