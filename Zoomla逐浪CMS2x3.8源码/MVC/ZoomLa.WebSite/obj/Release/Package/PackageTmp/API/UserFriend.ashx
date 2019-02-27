<%@ WebHandler Language="C#" Class="UserFriend" %>

using System;
using System.Web;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.User;
using ZoomLa.SQLDAL;
using Newtonsoft.Json;
using System.Data;

public class UserFriend : System.Web.SessionState.IReadOnlySessionState, IHttpHandler {
    B_User_FriendApply alyBll = new B_User_FriendApply();
    B_User_Friend friBll = new B_User_Friend();
    B_User buser = new B_User();
    string Action { get { return (HttpContext.Current.Request["Action"] ?? "").ToLower(); } }
    
    public void ProcessRequest (HttpContext context) {
        M_UserInfo mu = buser.GetLogin();
        M_APIResult retMod =new M_APIResult();
        int tuid = DataConvert.CLng(Req("uid"));
        switch (Action)
        {
            case "add"://添加好友申请
                {
                    retMod.retcode = M_APIResult.Failed;
                    string remind = Req("remind");
                    //先检测是否已是好友
                    if (mu.UserID == tuid) { retMod.retmsg = "你不能将自己添加为好友"; }
                    else if (mu.UserID < 1 || tuid < 1) { retMod.retmsg = "好友的用户ID不正确"; }
                    else if (friBll.IsFriend(mu.UserID, tuid)) { retMod.retmsg = "你们已经是好友了"; }
                    else if (alyBll.IsExist(mu.UserID, tuid)) { retMod.retmsg = "你已经发起过申请了"; }
                    else//发起申请
                    {
                        alyBll.Insert(new M_User_FriendApply() { UserID = mu.UserID, TUserID = tuid, Remind = remind });
                        retMod.retcode = M_APIResult.Success;
                        retMod.retmsg = "发起申请成功";
                    }
                }
                break;
            case "del"://删除好友
                {
                    friBll.DelFriend(mu.UserID, tuid);
                    retMod.retcode = M_APIResult.Success;
                }
                break;
            case "list":
                {
                    DataTable dt = friBll.SelMyFriend(mu.UserID);
                    retMod.result = JsonConvert.SerializeObject(dt); 
                }
                break;
            default:
                {
                    retMod.retcode = M_APIResult.Failed;
                    retMod.retmsg = "[" + Action + "]接口不存在"; 
                }
                break;
        }
        RepToClient(retMod);
    }
    private string Req(string name) { return HttpContext.Current.Request[name] ?? ""; }
    private void RepToClient(M_APIResult result) {
        HttpResponse rep = HttpContext.Current.Response;
        rep.Clear();rep.Write(result.ToString()); rep.Flush(); rep.End();
    }
    public bool IsReusable { get { return false; } }

}