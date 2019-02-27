<%@ WebHandler Language="C#" Class="admin" %>

using System;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.Model;

public class admin :API_Base,IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        retMod.retcode = M_APIResult.Failed;
        //retMod.callback = CallBack;//暂不开放JsonP
        try
        {
            switch (Action)
            {
                case "regcheck"://不同有同名用户,同名管理员存在
                    string adminName = Req("name");
                    if (string.IsNullOrEmpty(adminName)) { retMod.retmsg = "名称不能为空"; }
                    else if (B_Admin.IsExist(adminName)) { retMod.retmsg = "名称已存在,请更换名称"; }
                    else if (new B_User().IsExistUName(adminName)) { retMod.retmsg = "用户已存在,请更换用户名"; }
                    else { retMod.retcode = M_APIResult.Success; }
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