using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Areas.Mis.Controllers
{
    public class MemoController : ZLCtrl
    {
        B_MisInfo miBll = new B_MisInfo();
        public ActionResult AddMemo()
        {
            int ID = DataConverter.CLng(Request.QueryString["ID"]);
            M_MisInfo miMod = miBll.SelReturnModel(ID) ?? new M_MisInfo();
            if (miMod.ID > 0) { ViewBag.ltlTitle = "修改提醒"; }
            else{ ViewBag.ltlTitle = "新建提醒"; }
            ViewBag.uname = mu.UserName;
            return View(miMod);
        }
        public ActionResult Default()
        {
            ViewBag.uname = mu.UserName;
            return View(new DataTable());
        }
        public ActionResult Memo_left()
        {
            return View();
        }
        public ActionResult MemoDetail()
        {
            int id = Convert.ToInt32(Request["MID"]);
            DataTable dt = miBll.Sel(id);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (string.IsNullOrEmpty(dt.Rows[0]["IsShare"].ToString()))
                {
                    ViewBag.show = true;
                }
                else
                {
                    ViewBag.show = false;
                }
            }
            return View();
        }
        public ActionResult MemoView()
        {
            return View();
        }
    }
}
