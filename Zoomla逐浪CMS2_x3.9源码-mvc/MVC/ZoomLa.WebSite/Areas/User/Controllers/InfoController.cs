using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Model;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class InfoController : Controller
    {
        M_UserInfo mu = new M_UserInfo();
        B_User buser = new B_User();
        B_Group gpBll = new B_Group();
        B_PointGrounp pointBll = new B_PointGrounp();
        //用户的信息模块展示处理,修改置于User中
        public ActionResult Index()
        {
            mu = buser.GetLogin();
            ViewBag.mu = mu;
            ViewBag.gpMod = gpBll.SelReturnModel(mu.GroupID);
            ViewBag.pointMod = pointBll.SelectPintGroup(mu.UserExp);
            return View();
        }
        public ActionResult UserBase() { return View(); }
        public ActionResult DredgeVip() { return View(); }
        public ActionResult DbCardActivate() { return View(); }
    }
}
