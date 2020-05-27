using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL.Message;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class GuestController : ZLCtrl
    {
        B_BaikeEdit editBll = new B_BaikeEdit();
        public void Index()
        {
            Response.Redirect("MyAnswer"); return;
        }
        public void Default() { Response.Redirect("MyAnswer"); return; }
        public ActionResult BaikeContribution()
        {
            PageSetting setting = editBll.SelPage(CPage, PSize, mu.UserID, DataConvert.CLng(Request["status"]));
            return View(setting);
        }
        public PartialViewResult Baike_Data()
        {
            PageSetting setting = editBll.SelPage(CPage, PSize, mu.UserID, DataConvert.CLng(Request["status"]));
            return PartialView("BaikeContribution_List", setting);
        }
        public ActionResult AskComment()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult BaikeDraft()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult BaikeFavorite()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult MyAnswer()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult MyApproval()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult MyAsk()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
    }
}
