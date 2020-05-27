<%@ WebHandler Language="C#" Class="shop_address" %>
using System;
using System.Data;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.User;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using Newtonsoft.Json;

public class shop_address : API_Base, IHttpHandler
{
    B_User_API buapi = new B_User_API();
    B_UserRecei receBll = new B_UserRecei();
    private string OpenID { get { return Req("openid") ?? ""; } }
    public void ProcessRequest(HttpContext context)
    {

        M_UserInfo mu = B_User_API.GetLogin(OpenID);
        retMod.retcode = M_APIResult.Failed;
        retMod.callback = CallBack;
        if (mu.IsNull) { retMod.retmsg = "用户未登录"; RepToClient(retMod); }
        try
        {
            switch (Action)
            {
                case "add":
                    {
                        string address_json = Req("model");
                        M_UserRecei receiMod = JsonConvert.DeserializeObject<M_UserRecei>(address_json);
                        receiMod.UserID = mu.UserID;
                        retMod.result = receBll.GetInsert(receiMod).ToString();
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "edit":
                    {
                        string address_json = Req("model");
                        M_UserRecei receiMod = JsonConvert.DeserializeObject<M_UserRecei>(address_json);
                        receiMod.UserID = mu.UserID;
                        if (receBll.GetUpdate(receiMod))
                        {
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                case "del":
                    {
                        int aid = DataConvert.CLng(Req("id"));
                        receBll.U_DelByID(aid, mu.UserID);
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "list":
                    {
                        DataTable dt = receBll.Select_userID(mu.UserID, -1);
                        retMod.result = JsonConvert.SerializeObject(dt);
                        retMod.retcode = M_APIResult.Success;
                    }
                    break;
                case "get":
                    {
                        int id = DataConvert.CLng(Req("id"));
                        M_UserRecei receiMod = receBll.GetSelect(id);
                        if (receiMod.ID < 1) { retMod.retmsg = "该收货地址不存在!"; }
                        else if (receiMod.UserID != mu.UserID) { retMod.retmsg = "你无权访问该信息!"; }
                        else
                        {
                            retMod.result = JsonConvert.SerializeObject(receiMod);
                            retMod.retcode = M_APIResult.Success;
                        }
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