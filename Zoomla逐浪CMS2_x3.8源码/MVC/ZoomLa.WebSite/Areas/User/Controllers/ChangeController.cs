using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class ChangeController : Ctrl_Guest
    {
        RegexHelper regHelp = new RegexHelper();
        M_Uinfo basemu = new M_Uinfo();
        M_APIResult retMod = new M_APIResult(M_APIResult.Failed);
        #region 修改Email|手机
        private string CheckNum { get { return Session["Mail_CheckNum"] as string; } set { Session["Mail_CheckNum"] = value; } }
        private string NewCheckNum { get { return Session["Mail_NewCheckNum"] as string; } set { Session["Mail_NewCheckNum"] = value; } }
        private int Step { get { return DataConverter.CLng(Session["Mail_Step"]); } set { ViewBag.step = Session["Mail_Step"] = value; } }
        //private string NewMobile { get { return ViewBag["Mail_NewMobile"] as string; } set { ViewBag["Mail_NewMobile"] = value; } }
        public ActionResult Mobile()
        {
            if (!B_User.CheckIsLogged(Request.RawUrl)) { return null; }
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
            if (!B_User.CheckIsLogged(Request.RawUrl)) { return null; }
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
        public ActionResult Mobile_2()
        {
            if (!B_User.CheckIsLogged(Request.RawUrl)) { return null; }
            string mobile = Request.Form["NewMobile_T"];
            string key = Request.Form["NewVCode_hid"];
            string vcode = Request.Form["NewVCode"];
            string checknum = Request.Form["NewCheckNum_T"];
            basemu = buser.GetUserBaseByuserid(mu.UserID);
            ViewBag.newmoblie = mobile;
            Step = 2;
            if (string.IsNullOrEmpty(NewCheckNum)) { ShowAlert("校验码不存在,请重新发送校验码"); return View("Mobile"); }
            else if (!checknum.Equals(NewCheckNum)) { ShowAlert("校验码不匹配"); return View("Mobile"); }
            else if (buser.IsExist("ume", mobile)) { ShowAlert("该手机号已存在"); return View("Mobile"); }
            else if (!RegexHelper.IsMobilPhone(mobile)) { ShowAlert("手机格式不正确"); return View("Mobile"); }
            else
            {
                basemu.Mobile = mobile;
                buser.UpdateBase(basemu);
            }
            function.WriteSuccessMsg("修改手机号成功", "/User/Info/UserInfo"); return null;
        }
        public ActionResult Answer() { if (!B_User.CheckIsLogged(Request.RawUrl)) { return null; } return View(); }
        public void Answer_Submit(string quest, string answer, string newanswer)
        {
            if (!B_User.CheckIsLogged(Request.RawUrl)) { return; }
            if (string.IsNullOrEmpty(newanswer) || string.IsNullOrEmpty(answer)) { function.WriteErrMsg("安全问题与答案不能为空"); return; }
            else if (!mu.Question.Equals(quest)) { function.WriteErrMsg("安全问题不正确"); return; }
            else if (!mu.Answer.Equals(answer)) { function.WriteErrMsg("问题答案不正确"); return; }
            else
            {
                mu.Answer = newanswer;
                buser.UpdateByID(mu);
                function.WriteSuccessMsg("修改安全问题成功", "/User/Info/"); return;
            }
        }
        #region 修改邮箱
        public ActionResult Email()
        {
            if (!B_User.CheckIsLogged(Request.RawUrl)) { return null; }
            ViewBag.email = mu.Email;
            if (mu.Email.Contains("@random")) //随机生成的则可直接改
            {
                Step = 2;
            }
            else { Step = 1; }
            return View();
        }
        public ActionResult Email_1()
        {
            string checknum = Request.Form["checknum"];
            if (string.IsNullOrEmpty(CheckNum)) { ShowMsg("验证码不存在,请重新发送验证码", "danger"); }
            else if (!checknum.Equals(CheckNum)) { ShowMsg("验证码不匹配", "danger"); }
            else
            {
                Step = 2;
                ShowMsg("请填入您的新邮箱,并完成验证", "info");
            }
            ViewBag.email = mu.Email;
            return View("Email");
        }
        public ActionResult Email_2()
        {
            string newchknum = Request.Form["newchecknum"];
            string newEmail = Request.Form["newemail"];
            ViewBag.newemail = newEmail;
            Step = 2;
            ViewBag.email = mu.Email;
            if (string.IsNullOrEmpty(NewCheckNum)) { ShowMsg("验证码不存在,请重新发送验证码", "danger"); return View("Email"); }
            if (!newchknum.Equals(NewCheckNum)) { ShowMsg("验证码不匹配", "danger"); return View("Email"); }
            if (buser.IsExistMail(newEmail)) { ShowMsg("该邮箱已存在", "danger"); return View("Email"); }
            if (!RegexHelper.IsEmail(newEmail)) { ShowMsg("邮箱格式不正确", "danger"); return View("Email"); }
            mu.Email = newEmail;
            buser.UpdateByID(mu);
            function.WriteSuccessMsg("修改邮箱成功", "/User/Info/UserInfo"); return null;
        }
        public ActionResult Email_SendEmail()
        {
            CheckNum = function.GetRandomString(8).ToLower();
            string mailcontent = "您好，您正在<a href='" + SiteConfig.SiteInfo.SiteUrl + "'>" + SiteConfig.SiteInfo.SiteName + "</a>网站修改邮箱，您本次的验证码为：" + CheckNum;
            MailInfo mailInfo = SendMail.GetMailInfo(mu.Email, SiteConfig.SiteInfo.SiteName, "修改邮箱[" + SiteConfig.SiteInfo.SiteName + "]", mailcontent);
            SendMail.Send(mailInfo);
            ShowMsg("注册验证码已成功发送到你的注册邮箱,<a href='" + B_Plat_Common.GetMailSite(mu.Email) + "' target='_blank'>请前往邮箱查收并验证</a>!", "info");
            ViewBag.email = mu.Email;
            return View("Email");
        }
        public ActionResult Email_SendNewEmail()
        {
            string newEmail = Request["newemail"];
            ViewBag.newemail = newEmail;
            Step = 2;
            if (buser.IsExistMail(newEmail)) { ShowMsg("该邮箱已存在", "danger"); return View("Email"); }
            NewCheckNum = function.GetRandomString(8).ToLower();
            string mailcontent = "您好，您正在<a href='" + SiteConfig.SiteInfo.SiteUrl + "'>" + SiteConfig.SiteInfo.SiteName + "</a>网站修改邮箱，您本次的验证码为：" + NewCheckNum;
            MailInfo mailInfo = SendMail.GetMailInfo(newEmail, SiteConfig.SiteInfo.SiteName, "修改邮箱[" + SiteConfig.SiteInfo.SiteName + "]", mailcontent);
            SendMail.Send(mailInfo);
            ShowMsg("注册验证码已成功发送到你的注册邮箱,<a href='" + B_Plat_Common.GetMailSite(newEmail) + "' target='_blank'>请前往邮箱查收并验证</a>!", "info");
            return View("Email");
        }
        public void ShowMsg(string msg, string type)
        {
            ViewBag.msg = msg;
            ViewBag.msgtype = type;
        }
        #endregion

        private void ShowAlert(string msg)
        {
            ViewBag.info = msg;
        }
        private void ShowInfo(string msg)
        {
            ViewBag.info = msg;
        }
        //发送手机验证码(步骤1或步骤2的)
        public string SendValidCode(string key, string vcode, string mobile)
        {
            if (!B_User.CheckIsLogged(Request.RawUrl)) { return null; }
            B_Safe_Mobile mbBll = new B_Safe_Mobile();
            basemu = buser.GetUserBaseByuserid(buser.GetLogin().UserID);
            CheckNum = ""; NewCheckNum = "";
            switch (Step)
            {
                case 2:
                    NewCheckNum = function.GetRandomString(6, 2).ToLower();
                    basemu.Mobile = mobile;
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
        #endregion
        #region 问答|邮件|短信 找回密码
        public string GetPwdStep { get { return DataConverter.CStr(Session["GetPwdStep"]); } set { ViewBag.step = Session["GetPwdStep"] = value; } }
        public string GetPwdUName { get { return DataConverter.CStr(Session["GetPwdUName"]); } set { Session["GetPwdUName"] = value; } }
        //1,能过问答,邮件|手机短信,验证用户身份
        //2,验证通过显示Final_Div,让其填入新密码
        //3,新密码生效,自动进入用户中心
        public ActionResult GetPassword()
        {
            //密码找回方式
            string Method = (Request.QueryString["method"] ?? "").ToLower();
            int Uid = DataConverter.CLng(Request.QueryString["uid"]);
            string Key = (Request.QueryString["key"] ?? "");
            switch (Method)
            {
                case "answer":
                    GetPwdStep = "answer";
                    break;
                default:
                    GetPwdStep = "email";
                    break;
            }
            if (!string.IsNullOrEmpty(Key) && Uid > 0)//通过邮件校验找回
            {
                M_UserInfo mu = buser.SelReturnModel(Uid);
                if (mu.IsNull) { function.WriteErrMsg("用户不存在"); return Content(""); }
                if (string.IsNullOrEmpty(mu.seturl)) { function.WriteErrMsg("用户未发起找回密码"); return Content(""); }
                if (!mu.seturl.Equals(Key)) { function.WriteErrMsg("key值不正确"); return Content(""); }
                GetPwdStep = "final";
            }
            return View();
        }
        public ActionResult GetPassword_Answer()
        {
            string answer = Request.Form["Answer_T"];
            M_UserInfo mu = GetUserByName(Request.Form["TxtUserName"]);
            if (string.IsNullOrEmpty(mu.Answer) || string.IsNullOrEmpty(mu.Question)) { function.WriteErrMsg("用户未设置问答内容,无法通过问答找回"); return Content(""); }
            if (mu.Answer.Equals(answer))
            {
                GetPwdStep = "final";
            }
            else
            {
                function.WriteErrMsg("密码提示答案不正确"); return Content("");
            }
            return View("GetPassWord");
        }
        public ActionResult GetPassWord_Mobile()
        {
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], Request.Form["VCode"]))
            {
                function.WriteErrMsg("验证码不正确", "/User/Change/GetPassword"); return Content("");
            }
            M_UserInfo mu = GetUserByName(Request.Form["TxtUserName"]);
            M_Uinfo basemu = buser.GetUserBaseByuserid(mu.UserID);
            if (string.IsNullOrEmpty(basemu.Mobile)) { function.WriteErrMsg("用户未设置手机号,无法通过手机号找回"); return Content(""); }
            string code = function.GetRandomString(6, 2);
            string content = "【" + SiteConfig.SiteInfo.SiteName + "】,你正在使用找回密码服务,校验码:" + code;
            SendWebSMS.SendMessage(basemu.Mobile, content);
            //短信信息存入数据库
            M_Message messInfo = new M_Message();
            messInfo.Title = "验证码:找回密码";
            messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToLocalTime().ToString());
            messInfo.Content = content;
            messInfo.Receipt = "";
            messInfo.MsgType = 3;
            messInfo.status = 1;
            messInfo.Incept = mu.UserID.ToString();
            B_Message.Add(messInfo);

            mu.seturl = code;
            buser.UpdateByID(mu);
            GetPwdStep = "mobile_code";
            return View("GetPassWord");
        }
        public void GetPassWord_Email()
        {
            B_MailManage mailBll = new B_MailManage();
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], Request.Form["VCode"]))
            {
                function.WriteErrMsg("验证码不正确", "/User/GetPassword"); return;
            }
            M_UserInfo mu = GetUserByName(Request.Form["TxtUserName"]);
            if (string.IsNullOrEmpty(mu.Email) || mu.Email.Contains("@random")) { function.WriteErrMsg("用户未设置邮箱,无法通过邮箱找回"); return; }
            //生成Email验证链接
            string seturl = function.GetRandomString(12) + "," + DateTime.Now.ToString();
            mu.seturl = seturl;
            buser.UpDateUser(mu);
            //Email发送
            string url = SiteConfig.SiteInfo.SiteUrl + "/User/GetPassWord?key=" + mu.seturl + "&uid=" + mu.UserID;
            string returnurl = "<a href=\"" + url + "\" target=\"_blank\">" + url + "</a>";
            string content = mailBll.SelByType(B_MailManage.MailType.RetrievePWD);
            content = new OrderCommon().TlpDeal(content, GetPwdEmailDt(mu.UserName, SiteConfig.SiteInfo.SiteName, returnurl));
            MailInfo mailInfo = SendMail.GetMailInfo(mu.Email, SiteConfig.SiteInfo.SiteName, SiteConfig.SiteInfo.SiteName + "_找回密码", content);
            SendMail.Send(mailInfo);
            //不需要更新步骤,其从邮箱进入地址栏后再更新
            function.WriteSuccessMsg("密码重设请求提交成功,<a href='" + B_Plat_Common.GetMailSite(mu.Email) + "' target='_blank'>请前往邮箱查收</a>!!", "", 0); return;
        }
        public void GetPassWord_Final()
        {
            if (!GetPwdStep.Equals("final")) { function.WriteErrMsg("你无权访问该页面"); return; }
            string newpwd = Request.Form["TxtPassword"];
            string cnewpwd = Request.Form["TxtConfirmPassword"];
            int Uid = DataConverter.CLng(Request.QueryString["uid"]);
            M_UserInfo mu = buser.GetUserByName(GetPwdUName);
            if (Uid > 0) { mu = buser.SelReturnModel(Uid); }
            if (mu.IsNull) { function.WriteErrMsg("[" + Request["TxtUserName"] + "]用户不存在"); return; }
            if (!newpwd.Equals(cnewpwd)) { function.WriteErrMsg("两次输入密码不一致"); return; }
            mu.UserPwd = StringHelper.MD5(cnewpwd);
            mu.seturl = "";
            buser.UpDateUser(mu);
            function.WriteSuccessMsg("密码修改成功!", "/User/"); return;
        }
        private M_UserInfo GetUserByName(string uname)
        {
            GetPwdUName = (uname ?? "").Trim();
            if (string.IsNullOrEmpty(GetPwdUName)) { function.WriteErrMsg("用户名不能为空"); return null; }
            M_UserInfo mu = buser.GetUserByName(GetPwdUName);
            if (mu.IsNull) { function.WriteErrMsg("[" + GetPwdUName + "]用户不存在"); return null; }
            return mu;
        }
        private DataTable GetPwdEmailDt(string username, string sitename, string returnurl)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserName");
            dt.Columns.Add("SiteName");
            dt.Columns.Add("ReturnUrl");
            dt.Rows.Add(dt.NewRow());
            dt.Rows[0]["UserName"] = username;
            dt.Rows[0]["SiteName"] = sitename;
            dt.Rows[0]["ReturnUrl"] = returnurl;
            return dt;
        }
        #endregion
        //-----------修改密码
        public ActionResult Pwd() { if (!B_User.CheckIsLogged(Request.RawUrl)) { return null; } return View(); }
        public void Pwd_Edit()
        {
            B_User.CheckIsLogged(Request.RawUrl);
            M_UserInfo mu = buser.GetLogin(false);
            string oldPwd = StringHelper.MD5(Request.Form["TxtOldPassword"]);
            string newPwd = Request.Form["TxtPassword"];
            string cnewPwd = Request.Form["TxtPassword2"];
            if (!mu.UserPwd.Equals(oldPwd)) { function.WriteErrMsg("原密码错误,请重新输入"); return; }
            if (StrHelper.StrNullCheck(newPwd, cnewPwd)) { function.WriteErrMsg("新密码与确认密码不能为空"); return; }
            if (!newPwd.Equals(cnewPwd)) { function.WriteErrMsg("新密码与确认密码不匹配"); return; }
            if (newPwd.Length < 6) { function.WriteErrMsg("密码最少需要6位"); return; }
            mu.UserPwd = StringHelper.MD5(newPwd);
            buser.UpdateByID(mu);
            buser.ClearCookie();
            function.WriteSuccessMsg("修改密码成功,请重新登录", "/User/"); return;
        }
    }
}
