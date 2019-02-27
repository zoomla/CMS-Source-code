using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.AppCode.Common;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Controllers
{
    public class GuestController : Ctrl_Base
    {
        B_GuestBookCate cateBll = new B_GuestBookCate();
        B_User buser = new B_User();
        B_GuestBook guestBll = new B_GuestBook();
        public ActionResult Default()
        {
            int CateID = DataConverter.CLng(Request["cateid"]);
            string Skey = Request["skey"];
            M_GuestBookCate cateMod = cateBll.SelReturnModel(CateID);
            if (cateMod == null) { cateMod = new M_GuestBookCate() { CateID = 0, CateName = "留言信息", NeedLog = 0 }; }
            ViewBag.cateMod = cateMod;
            ViewBag.cateDt = cateBll.SelByGuest();
            ViewBag.needlog = cateMod.NeedLog;
            ViewBag.mu = buser.GetLogin();
            PageSetting setting = cateBll.SelPage(CPage, PSize, -100, cateMod.CateID, 0, Skey, buser.GetLogin().UserID, true);
            return View(setting);
        }
        public PartialViewResult Cate_Data()
        {
            int CateID = DataConverter.CLng(Request["cateid"]);
            return PartialView("Default_List", cateBll.SelPage(CPage, PSize, -100, CateID, 0, Request["skey"], buser.GetLogin().UserID, true));
        }
        [ValidateInput(false)]
        public void Add()
        {
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], Request.Form["VCode"]))
            {
                function.WriteErrMsg("验证码不正确", Request.RawUrl); return;
            }
            int CateID = DataConverter.CLng(Request["Cate"]);
            M_GuestBook info = new M_GuestBook();
            M_GuestBookCate cateMod = cateBll.SelReturnModel(CateID);
            //不允许匿名登录,必须登录才能发表留言
            if (cateMod.NeedLog == 1)
            {
                if (buser.CheckLogin())
                {
                    info.UserID = DataConverter.CLng(buser.GetLogin().UserID);
                }
                else
                {
                    B_User.CheckIsLogged(Request.RawUrl); return;
                }
            }
            else if (buser.CheckLogin())
            {
                info.UserID = buser.GetLogin().UserID;
            }
            info.CateID = CateID;
            //是否开启审核
            info.Status = cateMod.Status == 1 ? 0 : 1;
            info.ParentID = 0;
            info.Title = Server.HtmlEncode(Request.Form["Title"]);
            info.TContent = Request.Form["Content"];
            info.IP = EnviorHelper.GetUserIP();
            guestBll.AddTips(info);
            if (cateMod.Status == 1)
            {
                if (cateMod.IsShowUnaudit == 1)
                {
                    function.WriteSuccessMsg("您的留言已提交，请等待系统审核", "/Guest/Default?CateID=" + CateID); return;
                }
                else
                {
                    function.WriteSuccessMsg("您的留言已提交，通过系统审核后会出现在开放列表中", "/Guest/Default?CateID=" + CateID); return;
                }
            }
            else
            {
                function.WriteSuccessMsg("留言成功", "/Guest/Default?CateID=" + CateID); return;
            }
        }
        public ActionResult GuestShow()
        {
            int GID = DataConverter.CLng(Request["GID"]);
            if (GID < 1) { function.WriteErrMsg("没有传入留言ID"); return null; }
            M_GuestBook info = guestBll.GetQuest(GID);
            if (info.IsNull || info.ParentID > 0 || info.Status == -1) { function.WriteErrMsg("留言信息不存在!"); return null; }
            if (info.Status == 0 && info.UserID != buser.GetLogin().UserID) { function.WriteErrMsg("该留言未通过审核,无法查看详情"); return null; }
            M_GuestBookCate cateMod = cateBll.SelReturnModel(info.CateID);
            M_UserInfo mu = buser.GetLogin();
            ViewBag.mu = mu;
            ViewBag.cateMod = cateMod;
            ViewBag.GTitle = info.Title;
            ViewBag.cateDt = cateBll.SelByGuest();
            PageSetting setting = B_GuestBook.GetTipsList_SPage(CPage, 20, GID);
            return View(setting);
        }
        public PartialViewResult GuestShow_Data()
        {
            int GID = DataConverter.CLng(Request["GID"]);
            PageSetting setting = B_GuestBook.GetTipsList_SPage(CPage, 20, GID);
            return PartialView(setting);
        }
        [ValidateInput(false)]
        public void AddReply()
        {
            int GID = DataConverter.CLng(Request["GID"]);
            if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], Request.Form["VCode"]))
            {
                function.WriteErrMsg("验证码不正确", Request.RawUrl); return;
            }
            M_GuestBook pinfo = guestBll.GetQuest(GID);
            M_GuestBook info = new M_GuestBook();
            M_UserInfo mu = buser.GetLogin();
            info.UserID = mu.UserID;
            info.ParentID = GID;
            info.Title = "[会员回复]";
            info.CateID = pinfo.CateID;
            info.TContent = BaseClass.CheckInjection(Request.Form["Content"]);
            info.Status = SiteConfig.SiteOption.OpenAudit > 0 ? 0 : 1;
            guestBll.AddTips(info);
            function.WriteSuccessMsg("回复成功", "GuestShow?Gid=" + GID); return;
        }
    }
}
