using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ZoomLa.AppCode.Common;
using ZoomLa.BLL;

public class Ctrl_Admin : Ctrl_Base
{
    B_Admin badmin = new B_Admin();
    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        base.OnActionExecuting(filterContext);
        if (!B_Admin.CheckIsLogged(Request.RawUrl)) { filterContext.Result = new EmptyResult(); }
    }
}