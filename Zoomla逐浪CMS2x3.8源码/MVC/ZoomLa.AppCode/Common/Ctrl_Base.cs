using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ZoomLa.Common;

namespace ZoomLa.AppCode.Common
{
    public class Ctrl_Base : Controller
    {
        public int Success = 1;
        public int Failed = -1;
        public string err = "";//给予ref使用
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
    }
}
