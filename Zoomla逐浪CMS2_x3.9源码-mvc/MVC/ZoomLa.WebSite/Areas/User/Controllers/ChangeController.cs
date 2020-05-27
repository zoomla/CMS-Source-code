using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class ChangeController : Controller
    {
        //
        // GET: /User/Change/

        B_User buser = new B_User();
        RegexHelper regHelp = new RegexHelper();
        M_Uinfo basemu = new M_Uinfo();
        M_UserInfo mu = new M_UserInfo();
        M_APIResult retMod = new M_APIResult(M_APIResult.Failed);
        private string CheckNum { get { return Session["Mail_CheckNum"] as string; } set { Session["Mail_CheckNum"] = value; } }
        private string NewCheckNum { get { return Session["Mail_NewCheckNum"] as string; } set { Session["Mail_NewCheckNum"] = value; } }
        private int Step { get { return DataConverter.CLng(Session["Mail_Step"]); } set { ViewBag.step = Session["Mail_Step"] = value; } }
        //private string NewMobile { get { return ViewBag["Mail_NewMobile"] as string; } set { ViewBag["Mail_NewMobile"] = value; } }
        public ActionResult Mobile()
        {
            mu = buser.GetLogin();
            basemu = buser.GetUserBaseByuserid(mu.UserID);
            ViewBag.mu = mu;
            ViewBag.basemu = basemu;
            if (string.IsNullOrEmpty(basemu.Mobile))//刷新step
            {
                Step = 2;
            }
            else { Step = 1; }
            return View();
        }
        public ActionResult Mobile_1(string CheckNum_T)
        {
            mu = buser.GetLogin();
            basemu = buser.GetUserBaseByuserid(mu.UserID);
            ViewBag.mu = mu;
            ViewBag.basemu = basemu;
            if (string.IsNullOrEmpty(CheckNum)) { ShowAlert("校验码不存在,请重新发送校验码"); }
            else if (!CheckNum_T.Equals(CheckNum)) { ShowAlert("校验码不匹配"); }
            else
            {
                ShowInfo("<span style='color:green;'>原手机号校验成功,请输入您的新手机号</span>");
                Step = 2;
            }
            return View("Mobile");
        }
        public void Mobile_2()
        {
            string mobile = Request.Form["NewMobile_T"];
            string key = Request.Form["NewVCode_hid"];
            string vcode = Request.Form["NewVCode"];
            string checknum = Request.Form["NewCheckNum_T"];
            mu = buser.GetLogin();
            basemu = buser.GetUserBaseByuserid(mu.UserID);
            if (string.IsNullOrEmpty(NewCheckNum)) { ShowAlert("校验码不存在,请重新发送校验码"); }
            else if (!checknum.Equals(NewCheckNum)) { ShowAlert("校验码不匹配"); }
            else if (buser.IsExist("ume", mobile)) { ShowAlert("该手机号已存在"); }
            else if (!regHelp.IsMobilPhone(mobile)) { ShowAlert("手机格式不正确"); }
            else
            {
                basemu.Mobile = mobile;
                buser.UpdateBase(basemu);
            }
            function.WriteSuccessMsg("修改手机号成功", "/User/Info/UserInfo");
        }
        public ActionResult Answer() { return View(); }
        public void Answer_Submit(string quest, string answer, string newanswer)
        {
            mu = buser.GetLogin();
            if (string.IsNullOrEmpty(newanswer) || string.IsNullOrEmpty(answer)) { function.WriteErrMsg("安全问题与答案不能为空"); }
            else if (!mu.Question.Equals(quest)) { function.WriteErrMsg("安全问题不正确"); }
            else if (!mu.Answer.Equals(answer)) { function.WriteErrMsg("问题答案不正确"); }
            else
            {
                mu.Answer = newanswer;
                buser.UpdateByID(mu);
                function.WriteSuccessMsg("修改安全问题成功", "/User/Info/");
            }
        }
        public ActionResult Email() { return View(); }
        //---------------------------------------------------
        private void ShowAlert(string msg)
        {
            ViewBag.info = msg;
        }
        private void ShowInfo(string msg)
        {
            ViewBag.info = msg;
        }
        //发送手机验证码(步骤1或步骤2的)
        public string SendValidCode(string key, string vcode)
        {
            B_Safe_Mobile mbBll = new B_Safe_Mobile();
            basemu = buser.GetUserBaseByuserid(buser.GetLogin().UserID);
            CheckNum = ""; NewCheckNum = "";
            switch (Step)
            {
                case 2:
                    NewCheckNum = function.GetRandomString(6, 2).ToLower();
                    break;
                default:
                    CheckNum = function.GetRandomString(6, 2).ToLower();
                    break;
            }
            if (!ZoomlaSecurityCenter.VCodeCheck(key, vcode))
            {
                retMod.retmsg = "验证码不正确";
            }
            else
            {
                if (mbBll.CheckMobile(basemu.Mobile))
                {
                    string content = "【" + SiteConfig.SiteInfo.SiteName + "】你正在使用修改手机号服务,校验码:" + CheckNum + NewCheckNum;
                    SendWebSMS.SendMessage(basemu.Mobile, content);
                    M_Message messInfo = new M_Message();
                    messInfo.Sender = basemu.UserId.ToString();
                    messInfo.Title = "验证码:修改手机号[" + basemu.Mobile + "]";
                    messInfo.Content = content;
                    messInfo.Receipt = "";
                    messInfo.MsgType = 2;
                    messInfo.status = 1;
                    messInfo.Incept = basemu.UserId.ToString();
                    B_Message.Add(messInfo);
                    retMod.retcode = M_APIResult.Success;
                    retMod.retmsg = "校验码已成功发送到你的手机";
                }
                else { retMod.retmsg = "禁止向该号码发送短信,请联系管理员"; }
            }
            return retMod.ToString();
        }

    }
}
