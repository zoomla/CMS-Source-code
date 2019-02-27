using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Model;
public class UserCtrl : Controller
{
    //专用于用户中心
    public B_User buser = new B_User();
    public int Success = 1;
    public int Failed = -1;
    public M_UserInfo mu { get { if (ViewBag.mu == null) { ViewBag.mu = buser.GetLogin(); } return ViewBag.mu; } }
    public int Mid
    {
        get { if (ViewBag.Mid == null) { ViewBag.Mid = DataConverter.CLng(Request["ID"]); } return ViewBag.Mid; }
        set { ViewBag.Mid = value; }
    }
    public int PSize
    {
        get
        {
            return DataConverter.CLng(Request["psize"]);
        }
    }
    public int CPage
    {
        get { int _cpage = DataConverter.CLng(Request["cpage"]); if (_cpage < 1) { _cpage = 1; } return _cpage; }
    }
    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        base.OnActionExecuting(filterContext);
        B_User.CheckIsLogged(Request.RawUrl);
    }
}

