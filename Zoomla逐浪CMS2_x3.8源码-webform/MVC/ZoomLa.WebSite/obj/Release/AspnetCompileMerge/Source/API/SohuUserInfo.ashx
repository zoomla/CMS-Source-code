<%@ WebHandler Language="C#" Class="SohuUserInfo" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;
using System.Net;

public class SohuUserInfo :System.Web.SessionState.IReadOnlySessionState, IHttpHandler {
    //http://demo.z01.com/test/chatAPI.aspx?callback=queryUserInfoFn&_=1394585483822 
    /*
     * 畅言评论管理,返回JsonP格式数据,查询用户信息API
     */
    B_User buser = new B_User();
    public void ProcessRequest (HttpContext context) {
        string jsonP = "";
        HttpResponse Response = HttpContext.Current.Response;
        if (buser.CheckLogin())
        {
            M_UserInfo mu = buser.GetLogin();
            M_Uinfo userBase = buser.GetUserBaseByuserid(mu.UserID);
            string imgUrl = "http://demo.z01.com" + mu.UserFace.Replace("~", "");
            string nickName = mu.HoneyName;
            string userID = mu.UserID.ToString();
            string sign = "werdfasdfasdf";
            jsonP = "queryUserInfoFn({\"is_login\": 1,\"user\": {\"img_url\": \"" + imgUrl + "\",\"nickname\": \"" + nickName + "\",\"profile_url\": \"" + imgUrl + "\",\"user_id\": \"" + userID + "\",\"sign\":\"" + sign + "\"}})";

        }
        else
        {
            jsonP = "queryUserInfoFn({\"is_login\": 0});";
        }
        Response.Write(jsonP); Response.Flush(); Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}