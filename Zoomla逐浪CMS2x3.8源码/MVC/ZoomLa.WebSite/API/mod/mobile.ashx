<%@ WebHandler Language="C#" Class="mobile" %>

using System;
using System.Web;
using System.Web.SessionState;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Helper;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
//手机注册与发送验证码等操作
public class mobile : API_Base, IHttpHandler
{
    B_User buser = new B_User();
    B_Safe_Mobile mobileBll = new B_Safe_Mobile();
    public void ProcessRequest(HttpContext context)
    {
        M_UserInfo mu = buser.GetLogin();
        retMod.retcode = M_APIResult.Failed;
        string mobile = Req("mobile").Trim();
        //retMod.callback = CallBack;//暂不开放JsonP
        try
        {
            switch (Action.ToLower())
            {
                case "mobilereg"://手机注册(用于单页面注册)
                    if (string.IsNullOrEmpty(mobile)) { retMod.retmsg = "手机号码不能为空"; }
                    else if (buser.IsExist("ume", mobile)) { retMod.retmsg = "该手机号已被注册"; }
                    else if (!RegexHelper.IsMobilPhone(mobile)) { retMod.retmsg = "手机号码格式不正确"; }
                    else
                    {
                        string err = "";
                        if (SendCode(mobile,out err))
                        {
                            retMod.retcode = M_APIResult.Success;
                        }
                        else { retMod.retmsg = err; }
                    }
                    break;
                case "sendvailmsg"://手机发送验证(用于流程注册)
                    {
                        if (!ZoomlaSecurityCenter.VCodeCheck(context.Request["hcode"], context.Request["code"])) { context.Response.Write("验证码不正确"); return; }
                        //检测手机短信发送次数
                        if (mobileBll.CheckMobile(mobile, context.Request.UserHostAddress))
                        {
                            string vaildnum = function.GetRandomString(6, 2);//验证码
                            string ret = SendWebSMS.SendMessage(mobile, vaildnum);
                            //添加一条发送手机短信记录
                            mobileBll.Insert(new M_Safe_Mobile() { Phone = mobile, IP = IPScaner.GetUserIP(), VCode = vaildnum, Source = "register" });
                            retMod.retcode = M_APIResult.Success;
                        }
                        else
                        {
                            retMod.retmsg = "短信发送次数超过上限!";
                        }
                    }
                    break;
                case "setmobile_1"://验证手机,通过后修改值
                    {
                        if (mu.IsNull) { retMod.retmsg = "用户未登录"; }
                        else if (string.IsNullOrEmpty(mobile)) { retMod.retmsg = "手机信息为空"; }
                        else if (!RegexHelper.IsMobilPhone(mobile)) { retMod.retmsg = "手机格式不正确"; }
                        else
                        {
                            string err = "";
                            if (SendCode(mobile, out err)) { retMod.retcode = M_APIResult.Success; }
                            else { retMod.retmsg = err; }
                        }
                    }
                    break;
                case "setmobile_2":
                    {
                        string code = Req("code");
                        string serverCode = context.Cache["mobile_code_" + mobile] as string;
                        if (mu.IsNull) { retMod.retmsg = "用户未登录"; }
                        else if (string.IsNullOrEmpty(mobile)) { retMod.retmsg = "手机号不能为空"; }
                        else if (!RegexHelper.IsMobilPhone(mobile)) { retMod.retmsg = "手机格式不正确"; }
                        else if (string.IsNullOrEmpty(code)) { retMod.retmsg = "校验码为空"; }
                        else if (serverCode == null || string.IsNullOrEmpty(serverCode)) { retMod.retmsg = "无对应的校验码信息"; }
                        else if (!serverCode.Equals(code, StringComparison.CurrentCultureIgnoreCase)) { retMod.retmsg = "校验码不匹配"; }
                        else
                        {
                            M_Uinfo basemu = buser.GetUserBaseByuserid(mu.UserID);
                            basemu.Mobile = mobile;
                            buser.UpdateBase(basemu);
                            retMod.retcode = M_APIResult.Success;
                            context.Cache["mobile_code_" + mobile] = "";
                        }
                    }
                    break;
                //case "valid"://提交手机号与验证码,根据这两个服务端校验(移changemp代码)
                //    {
                //        string code = Req("code");
                //        string serverCode=context.Cache["mobile_code_" + mobile]as string;
                //        if (string.IsNullOrEmpty(code)) { retMod.retmsg = "校验码为空"; }
                //        else if (serverCode == null || string.IsNullOrEmpty(serverCode)) { retMod.retmsg = "无对应的校验码信息"; }
                //        else if (!serverCode.Equals(code, StringComparison.CurrentCultureIgnoreCase)) { retMod.retmsg = "校验码不匹配"; }
                //        else 
                //        {
                //            context.Cache["mobile_code_" + mobile] = null;
                //            retMod.retcode = M_APIResult.Success;
                //        }
                //    }
                //    break;
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
    //如err不为空,则发送失败
    private bool SendCode(string mobile, out string err)
    {
        //
        err = "";
        //发送验证码
        if (mobileBll.CheckMobile(mobile, IPScaner.GetUserIP()))
        {
            string code = function.GetRandomString(6, 2);//验证码
            SendWebSMS.SendMessage(mobile, "【" + SiteConfig.SiteInfo.SiteName + "】你的注册验证码:" + code);
            //添加一条发送手机短信记录
            mobileBll.Insert(new M_Safe_Mobile() { Phone = mobile, IP = IPScaner.GetUserIP() });
            HttpContext.Current.Cache["mobile_code_" + mobile] = code;
            return true;
        }
        else
        {
            err = "短信发送次数超过上限!"; 
            return false;
        } 
    }
}