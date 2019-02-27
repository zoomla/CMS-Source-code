using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    //问卷调查
    public class SurveyController : ZLCtrl
    {
        B_Survey surBll = new B_Survey();
        public void Index()
        {
            Response.Redirect("SurveyAll"); return;
        }
        public ActionResult SurveyAll()
        {
            PageSetting setting = surBll.SelPage(CPage, PSize, mu.UserID, Request["skey"]);
            return View(setting);
        }
        public PartialViewResult Survey_Data()
        {
            PageSetting setting = surBll.SelPage(CPage, PSize, mu.UserID, Request["skey"]);
            return PartialView("SurveyAll_List", setting);
        }
        public void Survey_Add() { }//目标模块相关方法,必须要实现,
        public int Survey_Del(string ids) { return Success; } 
    }
}
