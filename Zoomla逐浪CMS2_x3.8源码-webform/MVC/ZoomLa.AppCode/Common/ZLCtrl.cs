using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.AppCode.Common;
public class ZLCtrl : Ctrl_Base
{
    //专用于用户中心
    public B_User buser = new B_User();
    public M_UserInfo mu { get { if (ViewBag.mu == null) { ViewBag.mu = buser.GetLogin(); } return ViewBag.mu; } set { ViewBag.mu = value; } }
    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        base.OnActionExecuting(filterContext);
        if (!B_User.CheckIsLogged(Request.RawUrl)) { filterContext.Result = new EmptyResult(); }
        else if (!B_User.CheckUserStatus(mu, ref err)) { function.WriteErrMsg(err); filterContext.Result = new EmptyResult(); }
    }
}