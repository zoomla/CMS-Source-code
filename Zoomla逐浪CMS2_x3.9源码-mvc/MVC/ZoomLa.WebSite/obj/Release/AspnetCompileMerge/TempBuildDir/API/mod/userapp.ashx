<%@ WebHandler Language="C#" Class="userapp" %>

using System;
using System.Web;
using System.Data;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Helper;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//主用于APP用户整合,用户信息客户端缓存,服务端不缓存
public class userapp : API_Base, IHttpHandler
{
    B_User buser = new B_User();
    B_User_API buapi = new B_User_API();
    B_Safe_Mobile mobileBll = new B_Safe_Mobile();
    RegexHelper regHelp = new RegexHelper();
    private string OpenID { get { return Req("openid"); } }
    public void ProcessRequest(HttpContext context)
    {
        //throw new Exception("接口默认关闭,请联系管理员开启");
        retMod.retcode = M_APIResult.Failed;
        retMod.callback = CallBack;//暂不开放JsonP
        try
        {
            switch (Action)
            {
                #region 手机号注册流程
                case "reg1"://请输入手机号
                    {
                        string mobile = Req("mobile");
                        if (!regHelp.IsMobilPhone(mobile)) { retMod.retmsg = "手机号码格式不正确"; }
                        else if (buser.IsExist("ume", mobile)) { retMod.retmsg = "手机号已被注册"; }
                        else
                        {
                            //发送验证码
                            if (mobileBll.CheckMobile(mobile, IPScaner.GetUserIP()))
                            {
                                string code = function.GetRandomString(6, 2);//验证码
                                SendWebSMS.SendMessage(mobile, "【" + SiteConfig.SiteInfo.SiteName + "】你的注册验证码:" + code);
                                //添加一条发送手机短信记录
                                mobileBll.Insert(new M_Safe_Mobile() { Phone = mobile, IP = IPScaner.GetUserIP() });
                                context.Session.Add("mobile_" + mobile, code);
                                retMod.retcode = M_APIResult.Success;
                                retMod.retmsg = "生成验证码成功";
                            }
                            else
                            {
                                retMod.retmsg = "短信发送次数超过上限!";
                            }
                        }
                    }
                    break;
                case "reg2"://根据手机收到的验证码,开通注册
                    {
                        string mobile = Req("mobile");
                        string code = Req("code");
                        string serverCode = context.Session["mobile_" + mobile] as string;
                        if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(code)) { retMod.retmsg = "手机号或验证码为空"; }
                        else if (string.IsNullOrEmpty(serverCode)) { retMod.retmsg = "服务端验证码不存在"; }
                        else if (!code.Equals(serverCode)) { retMod.retmsg = "验证码不正确"; }
                        else
                        {
                            context.Session.Remove("mobile_" + mobile);
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                case "reg3"://填密码,完成注册(用户补充信息等在个人信息处配置)
                    {
                        string uname = Req("mobile");//手机号
                        string upwd = Req("upwd");
                        if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(upwd)) { retMod.retmsg = "用户名和密码不能为空"; }
                        else if (buser.IsExistUName(uname)) { retMod.retmsg = "用户名已存在"; }
                        else
                        {
                            M_UserInfo mu = new M_UserInfo();
                            M_Uinfo basemu = new M_Uinfo();
                            mu.UserName = uname;
                            mu.UserPwd = StringHelper.MD5(upwd);
                            mu.Email = function.GetRandomString(8) + "@random.com";
                            mu.UserFace = Req("UserFace");
                            mu.HoneyName = Req("HoneyName");
                            mu.UserID = buser.AddModel(mu);
                            basemu.UserId = mu.UserID;
                            basemu.Mobile = Req("mobile");
                            basemu.UserSex = true;
                            buser.AddBase(basemu);
                            mu = buapi.Login(uname, upwd);//自动登录
                            retMod.retcode = M_APIResult.Success;
                            retMod.result = new M_AJAXUser(mu).ToJson();
                        }
                    }
                    break;
                #endregion
                case "edit"://提供昵称,头像,手机号等的修改,返回修改后的用户信息
                    {
                        M_UserInfo mu = B_User_API.GetLogin(OpenID);
                        if (mu.IsNull) { retMod.retmsg = "用户未登录"; }
                        else
                        {
                            JObject jobj = JsonConvert.DeserializeObject<JObject>(Req("uinfo"));
                            if (jobj["UserFace"] != null) { mu.UserFace = jobj["UserFace"].ToString(); }
                            if (jobj["HoneyName"] != null) { mu.HoneyName = jobj["HoneyName"].ToString(); }
                            buser.UpdateByID(mu);
                            retMod.result = new M_AJAXUser(mu).ToJson(); 
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                case "edit_pwd"://修改密码,返回新的opneid
                    {
                        M_UserInfo mu = B_User_API.GetLogin(OpenID);
                        string oldpwd = Req("oldpwd").Trim();
                        string upwd = Req("upwd").Trim();
                        string err = buser.CheckPwdRegular(upwd);
                        if (mu.IsNull) { retMod.retmsg = "用户未登录"; }
                        else if (!StringHelper.MD5(oldpwd).Equals(mu.UserPwd)) { retMod.retmsg = "原密码不正确,取消修改"; }
                        else if (!string.IsNullOrEmpty(err)) { retMod.retmsg = err; }
                        else
                        {
                            mu.UserPwd = StringHelper.MD5(upwd);
                            buser.UpdateByID(mu);
                            //重新生成openid,并将新的openid返回
                            mu.OpenID = buapi.CreateOpenID(mu);
                            retMod.result = mu.OpenID;
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                case "edit_mobile"://新手机需要验证
                    break;
                case "login":
                    {
                        string uname = Req("uname");
                        string upwd = Req("upwd");
                        M_UserInfo mu = buapi.Login(uname, upwd);
                        if (mu.IsNull) { retMod.retmsg = "用户不存在"; }
                        else 
                        {
                            retMod.result = new M_AJAXUser(mu).ToJson();
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                case "getlogin"://根据openid获取用户信息
                    {
                        M_UserInfo mu = B_User_API.GetLogin(OpenID);
                        if (mu == null || mu.IsNull) { retMod.retmsg = "用户未登录"; }
                        else
                        {
                            M_AJAXUser ajaxmu = new M_AJAXUser(mu);
                            retMod.result = ajaxmu.ToJson();
                            retMod.retcode = M_APIResult.Success;
                        }
                    }
                    break;
                case "get"://根据ID获取指定用户的信息
                    {
                        int uid = DataConverter.CLng(Req("uid"));
                        M_UserInfo mu = buser.GetSelect(uid);
                        if (mu.IsNull) { retMod.retmsg = "用户不存在"; }
                        else
                        {
                            retMod.retcode = M_APIResult.Success;
                            M_AJAXUser ajaxmu = new M_AJAXUser(mu);
                            ajaxmu.openid = "";
                            retMod.result = ajaxmu.ToJson();
                        }
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