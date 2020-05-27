<%@ WebHandler Language="C#" Class="Notify" %>

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.BLL.API;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using System.Data.SqlClient;

public class Notify : API_Base, IHttpHandler
{
    B_User buser = new B_User();
    public void ProcessRequest(HttpContext context)
    {
        M_UserInfo mu = buser.GetLogin();
        retMod.retcode = M_APIResult.Failed;
        if (mu.IsNull) { retMod.retmsg = "用户未登录"; RepToClient(retMod); }
        //retMod.callback = CallBack;//暂不开放JsonP
        try
        {
            switch (Action)
            {
                case "add"://授权开放
                    break;
                case "list":
                    var list = B_User_Notify.ReadNotify(mu.UserID.ToString());
                    if (list.Count < 1) { retMod.retmsg = "没有通知信息"; }
                    else { retMod.retcode = M_APIResult.Success; retMod.result = JsonConvert.SerializeObject(list); }
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
/// <summary>
/// 返回的通知结果
/// </summary>
public class M_NotifyResult
{
    public int retcode = 1;
    public int num = 0;    //一级重要信息,不二次封装,如消息数量等
    public int num2 = 0;
    public int num3 = 0;
    public string msg = "";//正常等时的提示
    public string errmsg = "";//错误时的提示
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}