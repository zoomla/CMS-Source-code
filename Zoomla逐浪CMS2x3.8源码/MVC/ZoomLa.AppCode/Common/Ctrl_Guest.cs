using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.AppCode.Common;
using ZoomLa.BLL;
using ZoomLa.Model;

public class Ctrl_Guest:Ctrl_Base
{
    public B_User buser = new B_User();
    public M_UserInfo mu { get { if (ViewBag.mu == null) { ViewBag.mu = buser.GetLogin(); } return ViewBag.mu; } }
}