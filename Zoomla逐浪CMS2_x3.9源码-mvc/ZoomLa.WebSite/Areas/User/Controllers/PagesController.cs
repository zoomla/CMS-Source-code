using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class PagesController : Controller
    {
        //
        // GET: /User/Pages/

        public ActionResult Index()
        {
            return View();
        }
        public void Default() { Response.Redirect("Index"); }

    }
}
