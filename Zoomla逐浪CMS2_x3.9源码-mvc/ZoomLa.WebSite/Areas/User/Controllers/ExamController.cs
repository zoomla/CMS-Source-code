using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Controls;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class ExamController : ZLCtrl
    {
        B_Exam_Class bqc = new B_Exam_Class();
        B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
        private int QType { get { return string.IsNullOrEmpty(Request.QueryString["qtype"]) ? 99 : DataConverter.CLng(Request.QueryString["qtype"]); } }//题目类型
        private int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        public void Index()
        {
            RedirectToAction("QuestList");
        }
        public ActionResult QuestList()
        {
            M_UserInfo mu = buser.GetLogin();
            PageSetting setting = questBll.U_SelByFilter(1, 10, NodeID, QType, "", mu.UserID, 0);
            C_TreeView treeMod = new C_TreeView()
            {
                NodeID = "C_id",
                NodeName = "C_ClassName",
                NodePid = "C_Classid",
                DataSource = bqc.Select_All(),
                SelectedNode = Request.QueryString["NodeID"]
            };
            ViewBag.treeMod = treeMod;
            ViewBag.QType = QType;
            ViewBag.NodeID = NodeID;
            return View(setting);
        }
        public void QuestionManage() 
        {
            Response.Redirect("QuestList");
        }
        public ActionResult AddQuestion() { return View(); }
        public PartialViewResult Quest_Data()
        {
            M_UserInfo mu = buser.GetLogin();
            PageSetting setting = questBll.U_SelByFilter(CPage, PSize, NodeID, QType, Request["skey"], mu.UserID, 0);
            return PartialView("_qlist", setting);
        }
        public void Quest_Add()
        {
 
        }
        public int Quest_Del(int id)
        {
            M_UserInfo mu = buser.GetLogin();
            questBll.DelByIDS(id.ToString());//,mu.UserID
            return Success;
        }
    }
}
