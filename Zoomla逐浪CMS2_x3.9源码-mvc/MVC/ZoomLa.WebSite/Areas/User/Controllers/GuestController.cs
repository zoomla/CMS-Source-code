using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL.Message;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class GuestController : ZLCtrl
    {
        B_BaikeEdit editBll = new B_BaikeEdit();
        public ActionResult Index()
        {
            return View();
        }
        public void Default() { Response.Redirect("Index"); }
        public ActionResult BaikeContribution()
        {
            DataTable dt = editBll.U_Sel(mu.UserID, -100);
            return View();
        }
    }
}
