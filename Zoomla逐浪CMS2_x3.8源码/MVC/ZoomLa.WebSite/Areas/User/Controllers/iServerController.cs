using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class iServerController :ZLCtrl
    {
        public void Index()
        {
            Response.Redirect("FiServer"); return;
        }
        public ActionResult FiServer() 
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult AddQuestion()
        { return View(); }
        public ActionResult FiServerInfo()
        { return View(); }
        public ActionResult SelectiServer()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
    }
}
