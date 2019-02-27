using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using ZoomLa.BLL.API;

public class API_Base : IRequiresSessionState
{
    public M_APIResult retMod = new M_APIResult();
    public string Action { get { return Req("Action").ToLower(); } }
    public string CallBack { get { return Req("callback"); } }
    public string Req(string name) { return HttpContext.Current.Request[name] ?? ""; }
    public void RepToClient(M_APIResult result)
    {
        result.action = Action;
        HttpResponse rep = HttpContext.Current.Response;
        rep.Clear(); rep.Write(result.ToString()); rep.Flush(); rep.End();
    }
    //public void ProcessRequest(HttpContext context)
    //{
    //    function.WriteErrMsg("接口默认关闭,请联系管理员开启");
    //    M_UserInfo mu = buser.GetLogin();
    //    retMod.retcode = M_APIResult.Failed;
    //    //retMod.callback = CallBack;//暂不开放JsonP
    //    try
    //    {
    //        switch (Action)
    //        {
    //            case "add":
    //                break;
    //            case "edit":
    //                break;
    //            case "del":
    //                break;
    //            case "list":
    //                break;
    //            default:
    //                retMod.retmsg = "[" + Action + "]接口不存在";
    //                break;
    //        }
    //    }
    //    catch (Exception ex) { retMod.retmsg = ex.Message; }
    //    RepToClient(retMod);
    //}
}