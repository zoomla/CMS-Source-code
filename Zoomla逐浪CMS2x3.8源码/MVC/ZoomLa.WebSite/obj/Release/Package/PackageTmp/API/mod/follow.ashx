<%@ WebHandler Language="C#" Class="follow" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.User;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using Newtonsoft.Json;

public class follow : API_Base, IHttpHandler
{

    B_User buser = new B_User();
    B_User_Follow fwBll = new B_User_Follow();
    //关注来源
    private string ZSource { get { return Req("source"); } }
    private int TUserID { get { return DataConvert.CLng(Req("tuid")); } }
    public void ProcessRequest(HttpContext context)
    {
        //关注不支持匿名
        M_UserInfo mu = buser.GetLogin();
        string err = "";
        retMod.retcode = M_APIResult.Failed;
        if (mu.IsNull) { retMod.retmsg = "请先登录"; RepToClient(retMod); }
        //retMod.callback = CallBack;//暂不开放JsonP
        try
        {
            switch (Action)
            {
                case "add":
                    {
                        M_User_Follow fwMod = new M_User_Follow();
                        fwMod.UserID = mu.UserID;
                        fwMod.TUserID = TUserID;
                        fwMod.ZSource = ZSource;
                        if (fwBll.Add(fwMod, ref err)) { B_User_Notify.Add("信息提醒", mu.HoneyName + "关注了你", fwMod.TUserID.ToString()); retMod.retcode = M_APIResult.Success; }
                        else { retMod.retmsg = err; }
                    }
                    break;
                case "del":
                    {
                        fwBll.Del(mu.UserID, TUserID);
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "list":
                    {
                        DataTable dt = fwBll.SelByUser(mu.UserID);
                        retMod.retcode = M_APIResult.Success;
                        retMod.result = JsonConvert.SerializeObject(dt);
                    }
                    break;
                case "befllowed"://被哪些用户关注(返回用户名头像等)
                    {
                        DataTable dt = fwBll.SelByTUser(mu.UserID);
                        retMod.retcode = M_APIResult.Success;
                        retMod.result = JsonConvert.SerializeObject(dt);
                    }
                    break;
                case "has"://是否已经关注过了
                    {
                        retMod.retcode = M_APIResult.Success;
                        retMod.result = fwBll.Has(mu.UserID, TUserID).ToString();
                    }
                    break;
                default:
                    {
                        retMod.retmsg = "[" + Action + "]接口不存在";
                    }
                    break;
            }
        }
        catch (Exception ex) { retMod.retmsg = ex.Message; }
        RepToClient(retMod);
    }
    public bool IsReusable { get { return false; } }
}