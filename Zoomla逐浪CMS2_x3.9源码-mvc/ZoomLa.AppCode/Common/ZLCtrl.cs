using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Common;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Model;
public class ZLCtrl : Controller
{
    public B_User buser = new B_User();
    public int Success = 1;
    public int Failed = -1;
    public M_UserInfo mu { get { if (ViewBag.mu == null) { ViewBag.mu = buser.GetLogin(); } return ViewBag.mu; } }
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
}