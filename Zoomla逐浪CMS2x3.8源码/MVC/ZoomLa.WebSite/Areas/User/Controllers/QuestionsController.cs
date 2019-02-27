using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Controls;
using ZoomLa.Model;
using ZoomLa.Model.Exam;
using ZoomLa.SQLDAL.SQL;
using ZoomLaCMS.Models.Exam;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class QuestionsController : ZLCtrl
    {
        B_ClassRoom ClassBll = new B_ClassRoom();
        B_Group groupBll = new B_Group();
        B_Student stuBll = new B_Student();
        B_Exam_Sys_Papers paperBll = new B_Exam_Sys_Papers();
        B_Exam_Class bqc = new B_Exam_Class();
        B_Exam_Answer answerBll = new B_Exam_Answer();
        B_UserCourse busercourse = new B_UserCourse();
        B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
        B_TempUser tuserBll = new B_TempUser();
        private int NOAUDIT = 0, RIGHT = 1, ERROR = 2;
        public void Index()
        {
            Response.Redirect("MyMarks"); return;
        }
        #region 班级
                public ActionResult ClassView()
        {
            PageSetting setting = ClassBll.SelPage_U(CPage, PSize, mu.UserID, Request["skey"]);
            return View(setting);
        }
        public PartialViewResult ClassView_Data()
        {
            PageSetting setting = ClassBll.SelPage_U(CPage, PSize, mu.UserID, Request["skey"]);
            return PartialView("ClassView_List", setting);
        }
        //申请加入班级
        public void Class_Apply()
        {
            M_Group groupMod = groupBll.GetByID(mu.GroupID);
            int roomid = Convert.ToInt32(Request["roomid_hid"]);
            DataTable dt = ClassBll.SelByUid(mu.UserID, -1, roomid);
            if (dt.Rows.Count > 0) { function.WriteErrMsg("你已经申请过班级了!"); return; }
            M_Student stuMod = new M_Student();
            stuMod.Addtime = DateTime.Now;
            stuMod.UserID = mu.UserID;
            stuMod.UserName = mu.UserName;
            stuMod.StudentType = 1;
            if (groupMod.Enroll.Contains("isteach")) { stuMod.StudentType = 2; }
            if (groupMod.Enroll.Contains("isfamily")) { stuMod.StudentType = 3; }
            stuMod.AuditingContext = Request["remind_t"];
            stuMod.RoomID = roomid;
            stuBll.insert(stuMod);
            function.WriteSuccessMsg("申请班级成功!", "/User/Exam/ClassManage"); return;
        }
        public ActionResult MyClass()
        {
            return View();
        }
        #endregion
        #region 考试结果
        public ActionResult MyMarks()
        {
            int cid = DataConverter.CLng(Request["cid"]);
            C_TreeView treeMod = new C_TreeView()
            {
                NodeID = "C_id",
                NodeName = "C_ClassName",
                NodePid = "C_Classid",
                DataSource = bqc.Select_All(),
                liAllTlp = "<a class='filter_class' data-val='0' href='MyMarks'>全部</a>",
                LiContentTlp = "<a class='filter_class' data-val='@ID' href='MyMarks?cid=@NodeID'>@NodeName</a>",
                SelectedNode = cid.ToString()
            };
            ViewBag.treeMod = treeMod;
            PageSetting setting = paperBll.SelPage(CPage, PSize, cid);
            return View(setting);
        }
        public PartialViewResult MyMarks_Data()
        {
            int cid = DataConverter.CLng(Request["cid"]);
            PageSetting setting = paperBll.SelPage(CPage, PSize, cid);
            return PartialView("MyMarks_List", setting);
        }
        //考试结果
        public ActionResult MyExamResult()
        {
            PageSetting setting = answerBll.SelPage(CPage, PSize, mu.UserID);
            return View(setting);
        }
        public PartialViewResult MyExamResult_Data()
        {
            PageSetting setting = answerBll.SelPage(CPage, PSize, mu.UserID);
            return PartialView("MyExamResult_List", setting);
        }
        //我的课程
        public ActionResult MyCoruse()
        {
            PageSetting setting = busercourse.SelPage(CPage, PSize, mu.UserID, 1, DataConverter.CLng(Request["cid"]));
            return View(setting);
        }
        public PartialViewResult MyCoruse_Data()
        {
            PageSetting setting = busercourse.SelPage(CPage, PSize, mu.UserID, 1, DataConverter.CLng(Request["cid"]));
            return PartialView("MyCoruse_List",setting);
        }
        public void MyCoruse_Study()
        {
            B_ExAttendance bExAttendance = new B_ExAttendance();
            M_ExAttendance mExAttendance = new M_ExAttendance();
            mExAttendance.Stuid = mu.UserID;
            mExAttendance.StuName = mu.UserName;
            mExAttendance.LogTime = DateTime.Now;
            mExAttendance.Logtimeout = 0;
            mExAttendance.Location = Request["fileurl"] ?? "";
            bExAttendance.insert(mExAttendance);
        }
        #endregion
        #region 考试组卷
        //学生考试(根据条件动态组卷)
        public ActionResult ExamDetail()
        {
            string Qids = Request.QueryString["qids"] ?? "";
            VM_ExamDetail model = null;
            if (!string.IsNullOrEmpty(Qids))
            {
                M_Exam_Sys_Papers paperMod = new M_Exam_Sys_Papers();
                paperMod.p_name = "临时组卷";
                paperMod.QIDS = Qids;
                paperMod.p_UseTime = 0;
                model = ExamDetail_MyBind(paperMod);
            }
            else
            {
                model = ExamDetail_MyBind();
            }
            return View(model);
        }
        public void ExamDetail_Submit()
        {
            string Qids = Request.QueryString["qids"] ?? "";
            JArray arr = JsonConvert.DeserializeObject<JArray>(Request["QuestDT_Hid"]);
            M_UserInfo mu = tuserBll.GetLogin();
            M_Exam_Sys_Papers paperMod = paperBll.SelReturnModel(Mid);
            M_Exam_Answer firstMod = new M_Exam_Answer();
            List<M_Exam_Answer> answerList = new List<M_Exam_Answer>();
            bool isfrist = true;//首条记录用于存储批注信息,预估得分,人工批阅后才是真正得分
            int totalscore = 0;//
            string flowid = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            foreach (JObject obj in arr)
            {
                M_Exam_Answer answerMod = new M_Exam_Answer();
                answerMod.UserID = mu.UserID;
                answerMod.UserName = mu.UserName;
                answerMod.QID = DataConverter.CLng(obj["p_id"].ToString());
                answerMod.QTitle = obj["p_title"].ToString();
                answerMod.QType = obj["p_Type"].ToString();
                answerMod.Score = DataConverter.CLng(obj["p_defaultScores"].ToString());
                answerMod.Answer = obj["answer"] == null ? "" : obj["answer"].ToString();
                answerMod.PaperID = Mid;
                answerMod.PaperName = paperMod != null ? paperMod.p_name : "临时组卷";
                answerMod.FlowID = flowid;
                answerMod = CheckIsRight(answerMod);
                answerMod.Remind = "0";
                answerMod.pid = DataConverter.CLng(obj["pid"].ToString());
                if (answerMod.IsRight == RIGHT)
                {
                    totalscore += answerMod.Score;
                }
                if (isfrist)
                {
                    answerMod.Remind = "1";
                    isfrist = false;
                    firstMod = answerMod;
                    firstMod.UserName = Request.Form["UName_T"];
                    if (string.IsNullOrEmpty(firstMod.UserName)) { firstMod.UserName = mu.UserName; }
                    firstMod.MySchool = Request.Form["MySchool_T"];
                    firstMod.MyClass = Request.Form["MyClass_T"];
                }
                answerList.Add(answerMod);
            }
            firstMod.TotalScore = totalscore;
            foreach (M_Exam_Answer model in answerList)
            {
                model.StartDate = DateTime.Now.AddSeconds(-DataConverter.CLng(Request.Form["QuestTime_Hid"]));//开始答题时间
                answerBll.Insert(model);
            }
            string url = "ExamDetail?action=view&FlowID=" + flowid;
            url += Mid < 1 ? "" : "&ID=" + Mid;
            url += string.IsNullOrEmpty(Qids) ? "" : "&qids=" + Qids;
            function.WriteSuccessMsg("提交答案成功", url); return;
        }
        private VM_ExamDetail ExamDetail_MyBind(M_Exam_Sys_Papers paperMod = null)
        {
            M_UserInfo mu = tuserBll.GetLogin();
            if (Mid > 0) { paperMod = paperBll.SelReturnModel(Mid); }
            if (DateTime.Now < paperMod.p_BeginTime) { function.WriteErrMsg("还未到考试时间!"); return null; }
            if (paperMod.p_endTime < DateTime.Now) { function.WriteErrMsg("考试时间已过!"); return null; }
            if (string.IsNullOrEmpty(paperMod.QIDS)) { function.WriteErrMsg("该试卷没有添加题目!"); return null; }
            VM_ExamDetail model = new VM_ExamDetail(mu, paperMod, Request);
            return model;
        }
            //自动校验答案
        private M_Exam_Answer CheckIsRight(M_Exam_Answer answerMod)
        {
            M_Exam_Sys_Questions questMod = questBll.GetSelect(answerMod.QID);
            if (string.IsNullOrEmpty(questMod.p_Answer)) { answerMod.IsRight = NOAUDIT; return answerMod; }
            switch (questMod.MyQType)
            {
                case M_Exam_Sys_Questions.QType.Radio:
                    answerMod.IsRight = answerMod.Answer.Equals(questMod.p_Answer) ? RIGHT : ERROR;
                    break;
                case M_Exam_Sys_Questions.QType.Multi:
                    //检测是否包含指定选项,数量不能有差异,顺序可以不计
                    {
                        answerMod.Answer = answerMod.Answer.TrimEnd(',');
                        string[] answerArr = answerMod.Answer.Split(',');
                        string[] rightArr = questMod.p_Answer.Split('|');
                        if (answerArr.Length != rightArr.Length) { answerMod.IsRight = ERROR; break; }
                        foreach (string answer in answerArr)
                        {
                            if (!rightArr.Contains(answer)) { answerMod.IsRight = ERROR; break; }
                        }
                        answerMod.IsRight = RIGHT;
                    }
                    break;
                case M_Exam_Sys_Questions.QType.FillBlank:
                    {
                        //检测是否包含指定选项,数量不能有差异,需要按顺序
                        answerMod.Answer = answerMod.Answer.TrimEnd("|".ToCharArray());
                        string[] answerArr = Regex.Split(answerMod.Answer, Regex.Escape("|||"));
                        string[] rightArr = questMod.p_Answer.Split('|');
                        if (answerArr.Length != rightArr.Length) { answerMod.IsRight = ERROR; break; }
                        for (int i = 0; i < answerArr.Length; i++)
                        {
                            if (answerArr[i] != rightArr[i]) { answerMod.IsRight = ERROR; break; }
                        }
                        answerMod.IsRight = RIGHT;
                    }
                    break;
                case M_Exam_Sys_Questions.QType.Answer:
                    {
                        //解析题如果答案相符则计对,否则人工审核
                        answerMod.Answer = answerMod.Answer.Trim().TrimStart("解：".ToCharArray());
                        answerMod.IsRight = answerMod.Answer.Equals(questMod.p_Answer) ? RIGHT : NOAUDIT;
                    }
                    break;
            }
            return answerMod;
        }
        #endregion
    }
}
