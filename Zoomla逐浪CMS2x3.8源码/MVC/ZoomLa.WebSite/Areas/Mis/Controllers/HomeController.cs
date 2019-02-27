using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

namespace ZoomLaCMS.Areas.Mis.Controllers
{
    public class HomeController : ZLCtrl
    {

        B_MisAttendance maBll = new B_MisAttendance();
        B_MisSign msBll = new B_MisSign();
        B_MisInfo miBll = new B_MisInfo();
        B_Mis misBll = new B_Mis();
        public void Index()
        {
            Response.Redirect("Default");return;
        }
        public ActionResult Default()
        {
            ViewBag.username = mu.UserName;
            string userpic = "";
            string pictype = buser.GetUserBaseByuserid(mu.UserID).UserFace.ToLower() ;
            if (pictype.Contains(".gif") || pictype.Contains(".jpg") || pictype.Contains(".png"))
            {
                string delpath = SiteConfig.SiteOption.UploadDir.Replace("/", "") + "/";
                if (pictype.Contains("uploadfiles"))
                {
                    userpic = pictype;
                }
                else if (pictype.StartsWith("http://", true, CultureInfo.CurrentCulture) || pictype.StartsWith("/", true, CultureInfo.CurrentCulture) || pictype.StartsWith(delpath, true, CultureInfo.CurrentCulture) || pictype.StartsWith("~", true, CultureInfo.CurrentCulture))
                    userpic = pictype;
                else
                {
                    userpic = SiteConfig.SiteOption.UploadDir + "/" + pictype;
                }
            }
            else { userpic = "/Images/userface/noface.png"; }
            ViewBag.userpic = userpic;
            ViewBag.logintime = mu.LastLoginTimes.ToString("yyyy/MM/dd HH:mm:ss");
            return View(new DataTable());
        }
        public void Mattendance()
        {
            int status = DataConverter.CLng(Request["status"]);
            M_MisAttendance maMod = new M_MisAttendance();
            maMod.DepartMent = "";
            maMod.BeginTime = DateTime.Now.ToString().Replace("/", "-");
            maMod.UserName = mu.UserName;
            maMod.Comment = "";
            maMod.BeginStatus = status;
            maBll.insert(maMod);
            string message = status == 1 ? "签到成功!!" : "签退成功!!";
            function.WriteSuccessMsg(message, "MisAttendance"); return;
        }
        public ActionResult MisAttendance()
        {
            return View();
        }
        public PartialViewResult MisPunch()
        {
            ViewBag.username = mu.UserName;
            return PartialView();
        }
        public PartialViewResult MisAttDaily()
        {
            return PartialView();
        }
        public PartialViewResult MisAttendanceInfo()
        {
            DataTable dt = buser.Sel();
            string dateNow = DateTime.Now.ToString("yyyy/MM/dd");
            ViewBag.month = dateNow.Split('/')[1];
            ViewBag.year = dateNow.Split('/')[0];
            return PartialView(dt);
        }
        public PartialViewResult MisSignSet()
        {
            return PartialView(msBll.Sel());
        }
        public ActionResult MisSignSetInfo()
        {
            return View();
        }
        public ActionResult AddFiles()
        {
            if (function.IsNumeric(Request.QueryString["ID"]))
            {
                int ID = DataConverter.CLng(Request.QueryString["ID"]);
                ViewBag.txttit = misBll.SelReturnModel(ID).Title;
            }
            return View();
        }
        public void AddFiles_Add()
        {
            M_MisInfo misMod = new M_MisInfo();
            if (function.IsNumeric(Request.QueryString["ID"]))
            {
                int ID = DataConverter.CLng(Request.QueryString["ID"]);
                misMod = miBll.SelReturnModel(ID);
            }
            misMod.Title = Request.Form["txttit"];
            misMod.CreateTime = DateTime.Now;
            misMod.ProID = DataConverter.CLng(Request.QueryString["ProID"]);
            misMod.MID = DataConverter.CLng(Request.QueryString["MID"]);
            misMod.Inputer = buser.GetLogin().UserName;
            misMod.Type = DataConverter.CLng(Request.QueryString["Type"]);
            if (Request.QueryString["ID"] != null)
            {
                miBll.UpdateByID(misMod);
                function.WriteSuccessMsg("修改成功！", "FilesList?ID=" + Request.QueryString["ID"] + "&ProID=" + Request.QueryString["ProID"] + "&MID=" + Request.QueryString["MID"] + "&Type=" + Request.QueryString["Type"]); return;
            }
            else
            {
                miBll.insert(misMod);
                function.WriteSuccessMsg("添加成功！", "FilesList?ProID=" + Request.QueryString["ProID"] + "&MID=" + Request.QueryString["MID"] + "&Type=" + Request.QueryString["Type"]); return;
            }
        }
        public ActionResult AddMis()
        {
            int id = DataConverter.CLng(Request["ID"]);
            M_Mis misMod = misBll.SelReturnModel(id) ?? new M_Mis();
            ViewBag.id = misMod.ID;
            return View(misMod);
        }
        public ActionResult AddMis_Add()
        {
            int ID = DataConverter.CLng(Request["ID"]);
            M_Mis misMod = misBll.SelReturnModel(ID) ?? new M_Mis();
            misMod.Title = Request.Form["Title"] ?? "";
            misMod.Status = DataConverter.CLng(Request.Form["Status"] );
            misMod.Joiner = Request.Form["Joiner"];
            misMod.Type = DataConverter.CLng(Request.Form["Type"]);
            misMod.ComTime = DataConverter.CDate(Request.Form["ComTime"]);
            misMod.limitTime = DataConverter.CDate(Request.Form["limitTime"]);
            misMod.ParentID = DataConverter.CLng(Request.Form["ParentID"]);
            if (function.IsNumeric(Request.QueryString["ID"]))
            {
                misBll.UpdateByID(misMod);
                function.WriteSuccessMsg("修改成功！", "Mis?ID=" + Request["ID"]); return Content("");
            }
            else if (!string.IsNullOrEmpty(misMod.Title))
            {
                int newId = misBll.insert(misMod);
                function.WriteSuccessMsg("添加成功！", "Mis?ID=" + newId); return Content("");
            }
            else
            {
                ViewBag.id = misMod.ID;
                ViewBag.message = "<font color=red>请输入目标信息！</font>";
                return View("AddMis",misMod);
            }
        }
        public ActionResult AddMisInfo()
        {
            int id = DataConverter.CLng(Request["ID"]);
            M_MisInfo miMod = miBll.SelReturnModel(id) ?? new M_MisInfo();
            return View(miMod);
        }
        public void AddMisInfo_Add()
        {
            int id = DataConverter.CLng(Request["ID"]);
            M_MisInfo miMod = miBll.SelReturnModel(id) ?? new M_MisInfo();
            miMod.Title = Request.Form["Title"];
            miMod.Content = Request.Form["Content"];
            miMod.CreateTime = DateTime.Now;
            miMod.ProID = DataConverter.CLng(Request["ProID"]);
            miMod.MID = DataConverter.CLng(Request["MID"]);
            miMod.Inputer = buser.GetLogin().UserName;
            miMod.Type = DataConverter.CLng(Request["Type"]);
            if (miMod.ID>0)
            {
                miBll.UpdateByID(miMod);
                function.WriteSuccessMsg("修改成功！", "MisInfo?ID=" + miMod.ID + "&ProID=" + miMod.ProID + "&MID=" + miMod.MID + "&Type=" + miMod.Type); return;
            }
            else
            {
                miBll.insert(miMod);
                function.WriteSuccessMsg("添加成功！", "MisInfo?ProID=" + miMod.ProID + "&MID=" + miMod.MID + "&Type=" + miMod.Type); return;
            }
        }
        public ActionResult FilesList()
        {
            int ProID = DataConverter.CLng(Request.QueryString["ProID"]);
            return View(new DataTable());
        }
        public ActionResult MisInfo()
        {
            return View(new DataTable());
        }
        public ActionResult MisInfoView()
        {
            int ID = DataConverter.CLng(Request.QueryString["ID"]);
            M_MisInfo miMod = miBll.SelReturnModel(ID) ?? new M_MisInfo();
            return View(miMod);
        }
        public ActionResult Page()
        {
            return View();
        }
    }
}
