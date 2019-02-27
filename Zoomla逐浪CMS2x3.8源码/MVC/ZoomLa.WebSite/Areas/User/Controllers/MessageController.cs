using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class MessageController : ZLCtrl
    {
        //
        // GET: /User/Message/
        B_Message msgBll = new B_Message();
        public void Index()
        {
            Response.Redirect("Message"); return;
        }
        public ActionResult Message()
        {
            PageSetting setting = msgBll.SelPage(CPage, PSize, DataConverter.CLng(Request["ztype"]), Request["skey"]);
            return View(setting);
        }
        public PartialViewResult Message_Data()
        {
            PageSetting setting = msgBll.SelPage(CPage, PSize, DataConverter.CLng(Request["ztype"]), Request["skey"]);
            return PartialView("Message_List", setting);
        }
        public void Message_Add() 
        {
            function.WriteSuccessMsg("操作成功","Message"); return;
        }
        public int Message_Del(string ids) 
        {
            msgBll.DelByIDS(ids,mu.UserID);
            return Success;
        }
        public ActionResult MessageRead()
        { return View(); }
        public ActionResult MessageDraftbox()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult MessageGarbagebox()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult MessageOutbox()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult MessageSend()
        {
            return View();
        }
        public ActionResult Mobile() { return View(); }
        
         

    }
}
