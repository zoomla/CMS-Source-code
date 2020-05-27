using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class UserZoneController : Controller
    {
        //
        // GET: /User/UserZone/

        public ActionResult Index()
        {
            Response.Redirect("Structure");
            return View();
        }
        public ActionResult Structure() { return View(); }

    }
}
