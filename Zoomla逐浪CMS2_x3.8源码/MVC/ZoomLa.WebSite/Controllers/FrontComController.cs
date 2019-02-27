using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.Model.User;
using ZoomLa.Common;
using ZoomLa.SQLDAL;
using ZoomLa.Model;

namespace ZoomLaCMS.Controllers
{
    public class FrontComController : Controller
    {
        B_User buser = new B_User();
        public int LoginCount
        {
            get
            {
                if (Session["ValidateCount"] == null)
                {
                    Session["ValidateCount"] = 0;
                }
                return Convert.ToInt32(Session["ValidateCount"]);
            }
            set
            {
                Session["ValidateCount"] = value;
            }
        }
        public void ViewHistory()
        {
            if (Request["InfoId"] != null && Request["type"] != null)
            {
                B_ViewHistory vhBll = new B_ViewHistory();
                M_ViewHistory vhMod = new M_ViewHistory();
                vhMod.InfoId = Convert.ToInt32(Request["InfoId"]);
                vhMod.type = Request["type"];
                vhMod.UserID = buser.GetLogin().UserID;
                vhMod.addtime = DateTime.Now;
                vhBll.Add(vhMod);
            }
        }
        public void Promo()
        {
            string PStr = DataConvert.CStr(Request.QueryString["p"]);
            string PUName = DataConvert.CStr(Request.QueryString["pu"]);
            int PUserID = DataConvert.CLng(EncryptHelper.DesDecrypt(PStr));
            M_UserInfo mu = new M_UserInfo();
            if (PUserID < 1 && string.IsNullOrEmpty(PUName)) { function.WriteErrMsg("无用户信息");return; }
            if (PUserID > 0)
            {
                mu = buser.SelReturnModel(PUserID);
            }
            else if (!string.IsNullOrEmpty(PUName))
            {
                mu = buser.GetUserByName(PUName);
            }
            //if (mu.UserID < 1) { function.WriteErrMsg("推广用户不存在");return; }
            //if (mu.UserID > 1) { Response.Cookies["UserState2"]["ParentUserID"] = mu.UserID.ToString(); }
            Response.Redirect("/User/Register?ParentUserID=" + mu.UserID);return;
        }
        //简洁用户登录
        public ActionResult Login()
        {
            M_UserInfo mu = buser.GetLogin();
            string uname = Request.Form["TxtUserName"];
            string upwd = Request.Form["TxtPassword"];
            ViewBag.err = "";
            if (string.IsNullOrEmpty(uname + upwd))
            {

            }
            else
            {
                if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(upwd))
                {
                    ViewBag.err = "用户名与密码不能为空";
                }
                else
                {
                    mu = buser.GetUserByName(uname, StringHelper.MD5(upwd));
                    if (mu.IsNull) { ViewBag.err = "用户名或密码错误"; }
                    else { buser.SetLoginState(mu); }
                }
            }
            return View(mu);
        }
        //AJAX用户登录页
        public ActionResult Login_Ajax()
        {
            M_UserInfo mu = buser.GetLogin();
            ViewBag.LoginCount = LoginCount;
            return View(mu);
        }
    }
}
