using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using NotesFor.HtmlToOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Visitors;
using ZoomLa.AppCode.Controls;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Controls;
using ZoomLa.Model;
using ZoomLa.Model.Exam;
using ZoomLa.Model.User;
using ZoomLa.Safe;
using ZoomLa.SQLDAL;
using ZoomLa.SQLDAL.SQL;
using ZoomLaCMS.Models.Exam;
using BH = ZoomLa.BLL.Helper;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class ExamController : ZLCtrl
    {
        B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
        B_Exam_Sys_Papers paperBll = new B_Exam_Sys_Papers();
        B_Exam_PaperNode nodeBll = new B_Exam_PaperNode();
        B_Exam_Class clsBll = new B_Exam_Class();
        B_Exam_Version verBll = new B_Exam_Version();
        B_Exam_Answer ansBll = new B_Exam_Answer();
        B_Questions_Knowledge knowBll = new B_Questions_Knowledge();
        B_TempUser tuserBll = new B_TempUser();
        B_Temp tempBll = new B_Temp();
        B_Admin badmin = new B_Admin();
        private int QType { get { return string.IsNullOrEmpty(Request.QueryString["qtype"]) ? 99 : DataConverter.CLng(Request.QueryString["qtype"]); } }//题目类型
        private int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        public void Index()
        {
            RedirectToAction("QuestList");
        }
        #region 试卷相关
        public ActionResult AddPaper()
        {
            M_Exam_Sys_Papers paperMod = new M_Exam_Sys_Papers();
            if (Mid > 0)
            {
                paperMod = paperBll.GetSelect(Mid);
                if (mu.UserID != paperMod.UserID) { function.WriteErrMsg("你无权修改该试卷"); return Content(""); }
            }
            else
            {
                paperMod.p_BeginTime = DateTime.Now;
                paperMod.p_endTime = DateTime.Now.AddMonths(1);
            }
            C_TreeTlpDP treeMod = new C_TreeTlpDP()
            {
                F_ID = "C_id",
                F_Name = "C_ClassName",
                F_Pid = "C_Classid",
                dt = clsBll.Select_All(),
                seled = paperMod.p_class.ToString()
            };
            ViewBag.treeMod = treeMod;
            return View(paperMod);
        }
        public void Paper_Add()
        {
            M_Exam_Sys_Papers paperMod = new M_Exam_Sys_Papers();
            if (Mid > 0)
            {
                paperMod = paperBll.SelReturnModel(Mid);
                if (paperMod.UserID != mu.UserID) { function.WriteErrMsg("你无权修改该试卷"); return; }
            }
            paperMod.p_name = Request.Form["p_name"];
            paperMod.p_class = DataConvert.CLng(Request.Form["TreeTlp_Hid"]);
            paperMod.p_type = DataConverter.CLng(Request.Form["p_type"]);
            paperMod.p_Remark = Request.Form["p_Remark"];
            paperMod.p_UseTime = DataConverter.CDouble(Request.Form["p_UseTime"]);
            paperMod.p_BeginTime = DataConverter.CDate(Request.Form["p_BeginTime"]);
            paperMod.p_endTime = DataConverter.CDate(Request.Form["p_endTime"]);
            paperMod.p_Style = DataConverter.CLng(Request.Form["p_Style"]);
            paperMod.TagKey = Request.Form["tabinput"];
            if (paperMod.id > 0)
            {
                paperMod.UserID = mu.UserID;
                paperBll.UpdateByID(paperMod);
            }
            else
            {
                paperBll.Insert(paperMod);
            }
            function.WriteSuccessMsg("操作成功!", "Papers_System_Manage"); return;
        }
        public ActionResult paper() {
            M_Exam_Sys_Papers paperMod = paperBll.SelReturnModel(Mid);
            DataTable QuestDT = questBll.SelByIDSForExam(paperMod.QIDS, paperMod.id);//获取问题,自动组卷则筛选合适的IDS
            DataTable typeDT = ansBll.GetTypeDT(QuestDT);
            ViewBag.pname = paperMod.p_name;
            ViewBag.questDt = QuestDT;
            ViewBag.typeDt = typeDT;
            return View();
        }
        //试卷下题目管理
        public ActionResult Paper_QuestionManage()
        {
            int Pid = DataConverter.CLng(Request.QueryString["pid"]);
            int QType = DataConverter.CLng(Request.QueryString["qtype"], 99); //题目类型
            M_Exam_Sys_Papers paperMod = paperBll.GetSelect(Pid);
            PageSetting setting = new PageSetting();
            if (!string.IsNullOrEmpty(paperMod.QIDS)) { setting = questBll.SelByIDS(CPage, PSize, paperMod.QIDS, QType, "*"); }
            else { setting.dt = new DataTable(); }
            ViewBag.paperMod = paperMod;
            ViewBag.QType = QType;
            return View(setting);
        }
        public ActionResult PaperCenter()
        {
            M_Temp tempMod = tempBll.SelModelByUid(mu.UserID, 10);
            DataTable questDt = questBll.SelByIDSForExam(tempMod.Str1);
            DataTable typeDt = ansBll.GetTypeDT(questDt);
            ViewBag.title = DateTime.Now.ToString("yyyy年MM月dd日" + mu.UserName + "的组卷");
            ViewBag.questDt = questDt;
            ViewBag.typeDt = typeDt;
            return View();
        }
        public ActionResult PaperCenter_Submit()
        {
            M_Temp tempMod = tempBll.SelModelByUid(mu.UserID, 10);
            M_Exam_Sys_Papers paperMod = new M_Exam_Sys_Papers();
            if (string.IsNullOrEmpty(tempMod.Str1)) { function.WriteErrMsg("试题蓝为空,无法生成试卷!"); return null; }
            paperMod.p_name = Request.Form["title_t"];
            paperMod.p_class = 0;
            paperMod.p_Remark = Request.Form["desc_t"];
            paperMod.p_UseTime = DataConverter.CLng(Request.Form["usetime_t"]);
            paperMod.p_BeginTime = DateTime.Now;
            paperMod.p_endTime = DateTime.Now.AddYears(1);
            paperMod.p_Style = 2;
            paperMod.UserID = mu.UserID;
            paperMod.QIDS = tempMod.Str1;
            paperMod.QuestList = Request.Form["qinfo_hid"];
            paperMod.Price = DataConverter.CLng(Request.Form["price_t"]);
            int newid = paperBll.Insert(paperMod);
            //-------------------------
            tempMod.Str1 = "";
            tempBll.UpdateByID(tempMod);
            ViewBag.pname = paperMod.p_name;
            ViewBag.newid = newid;
            ViewBag.step = 2;
            return View("PaperCenter");
        }
        public ActionResult Papers_System_Manage()
        {
            PageSetting setting = paperBll.SelPage(CPage, PSize, -100, 0);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Papers_System_Manage_List", setting);
            }
            return View(setting);
        }
        public void DownPaper()
        {
            string qids = Request["qids"] ?? "";
            string PaperSize = Request["PaperSize"] ?? "A4";
            bool Orient = string.IsNullOrEmpty(Request["Orient"]) ? true : DataConverter.CBool(Request["Orient"]);
            M_Exam_Sys_Papers paperMod = paperBll.SelReturnModel(Mid);
            if (paperMod == null) { function.WriteErrMsg("试卷不存在"); }
            BH.HtmlHelper htmlHelp = new BH.HtmlHelper();
            StringWriter sw = new StringWriter();
            Server.Execute("/BU/Exam/Paper.aspx?id=" + paperMod.id, sw, false);
            string html = sw.ToString();
            HtmlPage page = htmlHelp.GetPage(html);
            html = page.Body.ExtractAllNodesThatMatch(new HasAttributeFilter("id", "paper"), true).ToHtml();
            string wordDir = "/Log/Storage/Exam/Paper/";
            string wordPath = wordDir + paperMod.id + ".docx";
            string ppath = Server.MapPath(wordPath);
            if (!Directory.Exists(Server.MapPath(wordDir))) { Directory.CreateDirectory(Server.MapPath(wordDir)); }
            byte[] array = Encoding.UTF8.GetBytes(sw.ToString());
            MemoryStream stream = new MemoryStream(array);             //convert stream 2 string      
            using (MemoryStream generatedDocument = new MemoryStream())
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Create(generatedDocument, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = doc.MainDocumentPart;
                    if (mainPart == null)
                    {
                        mainPart = doc.AddMainDocumentPart();
                        new Document(new Body()).Save(mainPart);
                    }
                    HtmlConverter converter = new HtmlConverter(mainPart);

                    //生成格式A4,A3
                    Body docBody = mainPart.Document.Body;
                    SectionProperties sectionProperties = new SectionProperties();
                    PageSize pageSize = new PageSize();
                    PageMargin pageMargin = new PageMargin();
                    //默认为16k大小
                    Columns columns = new Columns() { Space = "220" };//720
                    DocGrid docGrid = new DocGrid() { LinePitch = 100 };//360
                    GetPageSetting(ref pageSize, ref pageMargin, PaperSize, Orient);
                    sectionProperties.Append(pageSize, pageMargin, columns, docGrid);
                    docBody.Append(sectionProperties);

                    var paragraphs = converter.Parse(html);
                    for (int i = 0; i < paragraphs.Count; i++)
                    {
                        docBody.Append(paragraphs[i]);
                    }
                    mainPart.Document.Save();
                }
                SafeC.SaveFile(wordDir, paperMod.id + ".docx", generatedDocument.ToArray());
            }
            SafeSC.DownFile(wordPath, paperMod.p_name + ".docx");
        }
        private void GetPageSetting(ref PageSize pageSize, ref PageMargin pageMargin, string paperSize = "A4", bool isVertical = true)
        {
            //string str_paperSize = "Letter";//A4,B4
            UInt32Value width = 15840U;
            UInt32Value height = 12240U;
            int top = 1440;
            UInt32Value left = 1440U;
            switch (paperSize)
            {
                case "A4":// 210mm×297mm
                    width = 16840U;//80
                    height = 11905U;//40
                    break;
                case "A3"://297mm×420mm
                    width = 23760U;
                    height = 16800U;
                    break;
                case "B4"://257mm×364mm
                    width = 20636U;
                    height = 14570U;
                    break;
                case "16k":
                default:
                    break;
            }
            if (!isVertical)
            {
                UInt32Value sweep = width;
                width = height;
                height = sweep;
                int top_sweep = top;
                top = (int)left.Value;
                left = (uint)top_sweep;
            }

            pageSize.Width = width;
            pageSize.Height = height;
            pageSize.Orient = new EnumValue<PageOrientationValues>(isVertical ? PageOrientationValues.Landscape : PageOrientationValues.Portrait);

            pageMargin.Top = top;
            pageMargin.Bottom = top;
            pageMargin.Left = left;
            pageMargin.Right = left;
            pageMargin.Header = (UInt32Value)720U;
            pageMargin.Footer = (UInt32Value)720U;
            pageMargin.Gutter = (UInt32Value)0U;
        }
        public int Paper_Del(string ids)
        {
            paperBll.DelByIDS(ids, mu.UserID);
            return Success;
        }
        //试卷合并
        public void Paper_Merge()
        {
            string ids = Request.Form["idchk"];
            if (string.IsNullOrEmpty(ids)) { function.WriteErrMsg("请先选定需要合并的试卷"); return; }
            DataTable dt = paperBll.SelByIDS(ids);
            if (dt.Rows.Count < 1) { function.WriteErrMsg("选定的试卷不存在"); return; }
            M_Exam_Sys_Papers paperMod = new M_Exam_Sys_Papers();
            paperMod.UserID = mu.UserID;
            paperMod.p_name = DateTime.Now.ToString("yyyyMMdd") + "合并试卷";
            paperMod.QIDS = "";
            foreach (DataRow dr in dt.Rows)
            {
                paperMod.QIDS += dr["QIDS"] + ",";
            }
            if (string.IsNullOrEmpty(paperMod.QIDS.Replace(",", ""))) { function.WriteErrMsg("试卷中没有添加题目,取消合并"); return; }
            paperMod.p_type = Convert.ToInt32(dt.Rows[0]["p_type"]);
            paperMod.p_class = Convert.ToInt32(dt.Rows[0]["p_class"]);
            paperMod.QIDS = StrHelper.RemoveDupByIDS(paperMod.QIDS);
            paperMod.QIDS = StrHelper.PureIDSForDB(paperMod.QIDS);
            paperBll.Insert(paperMod);
            function.WriteSuccessMsg("试卷合并成功");
            return;
        }
        //为试卷添加试题
        public int Paper_AddQids(int pid, string ids)
        {
            M_Exam_Sys_Papers paperMod = paperBll.GetSelect(pid);
            if (paperMod.UserID != mu.UserID) { return Failed; }
            paperMod.QIDS = string.Join(",", ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
            paperBll.UpdateByID(paperMod);
            return Success;
        }
        //为试卷添加大题
        public string Paper_AddLarge(string ids)
        {
            DataTable dt = questBll.SelByIDSForType(ids);
            return JsonConvert.SerializeObject(dt);
            //function.Script(this, "parent.SelQuestion(" + json + ");");
        }
        #endregion
        #region 试题
        public ActionResult QuestList()
        {
            PageSetting setting = questBll.U_SelByFilter(CPage, PSize, NodeID, QType, "", mu.UserID, 0);
            C_TreeView treeMod = new C_TreeView()
            {
                NodeID = "C_id",
                NodeName = "C_ClassName",
                NodePid = "C_Classid",
                DataSource = clsBll.Select_All(),
                SelectedNode = Request.QueryString["NodeID"]
            };
            ViewBag.treeMod = treeMod;
            ViewBag.QType = QType;
            ViewBag.NodeID = NodeID;
            return View(setting);
        }
        //普通与专业版组卷
        public ActionResult QuestionManage()
        {
            var model = new VM_QuestManage(Request);
            return View(model);
        }
        public void QuestRPT()
        {
            Server.Transfer("/BU/QuestRPT.aspx", true);
        }
        [HttpPost]
        public string QuestionManage_API()
        {
            string action = Request.Form["action"];
            string result = "";
            var model = new VM_QuestManage(Request);
            switch (action)
            {
                case "getknows":
                    {
                        DataTable dt = knowBll.Select_All(DataConvert.CLng(Request.Form["nodeid"]), -1, 1, Request.Form["knowname"]);
                        result = model.GetTreeStr(model.FillKnows(dt), 0, "knows");
                    }
                    break;
                case "GetKnowsByVersion":
                    {
                        int version = Convert.ToInt32(Request["version"]);
                        M_Exam_Version verMod = verBll.SelReturnModel(version);
                        if (verMod == null || string.IsNullOrEmpty(verMod.Knows)) { result = "{}"; }
                        else
                        {
                            DataTable dt = knowBll.SelByIDS(verMod.Knows);
                            dt = dt.DefaultView.ToTable(false, "k_id,k_name".Split(','));
                            result = JsonConvert.SerializeObject(dt);
                        }
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
        //public ActionResult QuestionManage2() {return View(); }
        //public ActionResult AddQuestion() { return View(); }
        public PartialViewResult Quest_Data()
        {
            M_UserInfo mu = buser.GetLogin();
            PageSetting setting = questBll.U_SelByFilter(CPage, PSize, NodeID, QType, Request["skey"], mu.UserID, 0);
            return PartialView("_qlist", setting);
        }
        public int Quest_Del(string ids)
        {
            M_UserInfo mu = buser.GetLogin();
            questBll.DelByIDS(ids);//,mu.UserID
            return Success;
        }
        public ActionResult QuestAPI()
        {
            string action = Request["action"] ?? "";
            int qid = DataConverter.CLng(Request["qid"]);
            int qtype = DataConverter.CLng(Request["qtype"]);
            string qids = (Request["qids"] ?? "").Trim(',');
            while (qids.Contains(",,")) { qids.Replace(",,", ","); }
            int result = Failed;
            M_Temp tempMod = new M_Temp();
            switch (action)
            {
                case "cart_add"://试题篮
                    tempMod = GetCartByUid(mu.UserID);
                    tempMod.Str1 = StrHelper.AddToIDS(tempMod.Str1, qid.ToString());
                    AddORUpdate(tempMod);
                    result = Success;
                    break;
                case "cart_adds":
                    if (string.IsNullOrEmpty(qids)) { break; }
                    tempMod = GetCartByUid(mu.UserID);
                    foreach (string q in qids.Split(','))
                    {
                        if (string.IsNullOrEmpty(q)) continue;
                        tempMod.Str1 = StrHelper.AddToIDS(tempMod.Str1, q);
                    }
                    AddORUpdate(tempMod);
                    result = Success;
                    break;
                case "cart_remove":
                    tempMod = GetCartByUid(mu.UserID);
                    tempMod.Str1 = StrHelper.RemoveToIDS(tempMod.Str1, qid.ToString());
                    AddORUpdate(tempMod);
                    result = Success;
                    break;
                case "cart_clear":
                    tempMod = GetCartByUid(mu.UserID);
                    tempMod.Str1 = "";
                    AddORUpdate(tempMod);
                    result = Success;
                    break;
                case "collect_add"://试题收藏与移除
                    break;
                case "collect_remove":
                    break;
            }
            return Content(result.ToString());
        }
        private M_Temp GetCartByUid(int uid, int usetype = 10)
        {
            M_Temp tempMod = tempBll.SelModelByUid(uid, usetype);
            if (tempMod == null) { tempMod = new M_Temp(); tempMod.UserID = uid; tempMod.UseType = usetype; }
            return tempMod;
        }
        private void AddORUpdate(M_Temp model)
        {
            if (model.ID > 0) { tempBll.UpdateByID(model); }
            else { tempBll.Insert(model); }
        }
        public ActionResult AddEngLishQuestion()
        {
            VM_Question model = new VM_Question(mu);
            model.treeMod = GetTreeMod();
            model.treeMod.seled = model.questMod.p_Class.ToString();
            return View(model);
        }
        public ActionResult AddSmallQuest() { return View(); }
        public ActionResult Question_Class_Manage() { return View(); }
        public ActionResult QuestOption() { return View(); }
        public ActionResult QuestShow() { return View(); }
        public ActionResult QuestView()
        {
            M_Exam_Sys_Questions questMod = questBll.GetSelect(Mid);
            return View(questMod);
        }
        public void Question_Add()
        {
            var model = new VM_Question();
            M_Exam_Sys_Questions questMod = Question_FillMod();
            if (Mid > 0)
            {
                questBll.GetUpdate(questMod);
            }
            else
            {
                questMod.p_id = questBll.GetInsert(questMod);
            }
            SafeSC.WriteFile(questMod.GetOPDir(), Request.Form["Optioninfo_Hid"]);
            if (model.IsSmall > 0)//判断当前是否是添加小题状态
            {
                DataTable dt = questBll.SelByIDSForType(questMod.p_id.ToString());
                string json = JsonConvert.SerializeObject(dt);
                //function.Script(this, "parent.SelQuestion(" + json + ")");
            }
            else { function.WriteSuccessMsg("操作成功!", "QuestList?NodeID=" + model.NodeID); return; }
        }
        private M_Exam_Sys_Questions Question_FillMod()
        {
            M_Exam_Sys_Questions questMod = null;
            if (Mid > 0)
            {
                questMod = questBll.GetSelect(Mid);
            }
            else
            {
                questMod = new M_Exam_Sys_Questions();
                questMod.UserID = mu.UserID;
                questMod.p_Inputer = mu.UserName;
            }
            questMod.p_title = Request.Form["p_title"];
            //questMod.p_Difficulty = DataConverter.CLng(rblDiff.SelectedValue);
            questMod.p_Difficulty = DataConverter.CDouble(Request.Form["Diffcult_T"]);
            questMod.p_Class = DataConverter.CLng(Request.Form["TreeTlp_hid"]);
            //questMod.p_Shipin = QuestType_Hid.Value;
            questMod.p_Views = DataConverter.CLng(Request.Form["Grade_Rad"]);
            questMod.p_Knowledge = DataConverter.CLng(Request.Form["knowname"]);
            string tagkey = Request.Form["Tabinput"];
            if (string.IsNullOrEmpty(tagkey))
            {
                questMod.Tagkey = "";
            }
            else
            {
                int firstid = clsBll.SelFirstNodeID(questMod.p_Class);
                questMod.Tagkey = knowBll.AddKnows(firstid, tagkey, 0, false);
            }
            questMod.p_Type = DataConverter.CLng(Request.Form["qtype_rad"]);
            questMod.p_shuming = string.IsNullOrEmpty(Request.Form["AnswerHtml_T"]) ? Request.Form["Answer_T"] : Request.Form["AnswerHtml_T"];
            if (questMod.p_Type == 10) { questMod.p_Content = Request.Form["Qids_Hid"]; questMod.LargeContent = Request.Form["txtP_Content"]; }
            else { questMod.p_Content = Request.Form["txtP_Content"]; }
            questMod.Qinfo = Request.Form["Qinfo_Hid"];
            questMod.p_ChoseNum = DataConverter.CLng(Request.Form["p_ChoseNum_DP"]);
            questMod.IsBig = 0;
            questMod.IsShare = string.IsNullOrEmpty(Request.Form["IsShare_Chk"]) ? 1 : 0;
            questMod.p_defaultScores = DataConverter.CFloat(Request.Form["p_defaultScores"]);
            questMod.p_Answer = Request.Form["p_Answer"];
            //questMod.p_Optioninfo = Optioninfo_Hid.Value;
            questMod.Jiexi = Request.Form["txtJiexi"];
            questMod.Version = DataConverter.CLng(Request.Form["Version_Rad"]);
            return questMod;
        }
        #endregion
        public ActionResult SchoolShow() { return View(); }
        public ActionResult SelKnowledge() { return View(); }
        public ActionResult SelQuestion()
        {
            int issmall = 0;
            int IsLage = DataConverter.CLng(Request.QueryString["islage"]);
            string skey = Request["skey"];
            if (IsLage == 1) { issmall = 1; }
            ViewBag.selids = Request.QueryString["selids"];
            PageSetting setting = new PageSetting();
            if (badmin.CheckLogin())
            {
                setting = questBll.U_SelByFilter(CPage, PSize, 0, QType, skey, 0, issmall);
            }
            else if (buser.IsTeach())
            {
                setting = questBll.U_SelByFilter(CPage, PSize, 0, QType, skey, mu.UserID, issmall);
            }
            else { function.WriteErrMsg("当前页面只有教师才可以访问!"); return null; }
            if (Request.IsAjaxRequest()) { return PartialView("SelQuestion_List", setting); }
            ViewBag.qtype = QType;
            ViewBag.islage = IsLage;//checkbox已选择的 
            return View(setting);
        }
        public ActionResult SelTearcher()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult StudentManage()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult StudentPic() { return View(); }
        public ActionResult ToScore()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult ExamDetail()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        #region 版本,班级,学校等
        public ActionResult AddVersion()
        {
            Response.Redirect("/BU/Exam/AddVersion.aspx"); return null;
            //M_Exam_Version verMod = new M_Exam_Version();
            //if (Mid > 0)
            //{
            //    verMod = verBll.SelReturnModel(Mid);
            //}
            //C_TreeTlpDP treeMod = GetTreeMod();
            //ViewBag.treeMod = treeMod;
            //ViewBag.gradeDT = B_GradeOption.GetGradeList(6, 0);
            //return View(verMod);
        }
        public ActionResult VersionList() { return View(); }
        public ActionResult ClassManage()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        public ActionResult AddClass() { return View(); }
        public ActionResult ClassShow() { return View(); }
        public ActionResult SelSchool()
        {
            PageSetting setting = new PageSetting();
            setting.dt = new DataTable();
            return View(setting);
        }
        //获取科目下拉模型
        private C_TreeTlpDP GetTreeMod()
        {
            return new C_TreeTlpDP()
            {
                F_ID = "C_id",
                F_Name = "C_ClassName",
                F_Pid = "C_Classid",
                dt = clsBll.Select_All(),
            };
        }
        #endregion
    }
}
