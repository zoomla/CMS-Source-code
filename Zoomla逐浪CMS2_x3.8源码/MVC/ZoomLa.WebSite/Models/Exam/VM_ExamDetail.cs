using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Model.Exam;

namespace ZoomLaCMS.Models.Exam
{
    /// <summary>
    /// 用于学生考试与老师批阅
    /// </summary>
    public class VM_ExamDetail
    {
        B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
        B_Exam_Answer answerBll = new B_Exam_Answer();
        public M_UserInfo mu = null;
        public M_Exam_Sys_Papers paperMod = null;
        public string Action = "", FlowID = "";
        public int ExTime = 0;
        public string AngularJS = "";
        public string totalScore = "";//试卷得分
        public string useTime = "";
        //-------------
        public DataTable QuestDT = null;
        public string QuestDT_Hid = "";
        //先绑定按Type显示,然后再按大题显示
        public DataTable typeDT = null;
        //查看答案时填充
        public DataTable AnswerDT = null;
        public string Answer_Hid = "";
        public M_Exam_Answer answerMod = new M_Exam_Answer();

        public VM_ExamDetail(M_UserInfo mu, M_Exam_Sys_Papers paperMod, HttpRequestBase Request)
        {
            this.mu = mu;
            this.paperMod = paperMod;
            Action = (Request.QueryString["action"] ?? "").ToLower();
            FlowID = Request.QueryString["FlowID"] ?? "";
            QuestDT = questBll.SelByIDSForExam(paperMod.QIDS, paperMod.id);//获取问题,自动组卷则筛选合适的IDS
            QuestDT.DefaultView.RowFilter = "";
            QuestDT_Hid = JsonConvert.SerializeObject(QuestDT.DefaultView.ToTable(false, "p_id,p_title,p_type,p_defaultScores,istoshare,pid".Split(',')));
            typeDT = answerBll.GetTypeDT(QuestDT);
            ExTime = DataConverter.CLng(paperMod.p_UseTime);
            if (Action.Equals("view"))//显示答案
            {
                AnswerDT = answerBll.SelByPid(mu.UserID, paperMod.id, FlowID);
                if (AnswerDT.Rows.Count < 1) { function.WriteErrMsg("你尚未完成答卷"); }
                AnswerDT.DefaultView.RowFilter = "";
                Answer_Hid = JsonConvert.SerializeObject(AnswerDT.DefaultView.ToTable(true, "ID,QID,QType,QTitle,Answer,IsRight,Remark".Split(',')));
                answerMod = answerBll.SelMainModel(FlowID);
                //-----显示得分
                //MySchool_T.Enabled = false;
                //MyClass_T.Enabled = false;
                //UName_T.Enabled = false;
                useTime = "用时 " + (answerMod.CDate - answerMod.StartDate).TotalMinutes.ToString("f0") + " 分钟";
                totalScore = "得分:" + DataConverter.CLng(AnswerDT.Select("Remind=1")[0]["TotalScore"]);
            }
        }
        //试题标题,用于学生考试界面
        public string GetPTitle()
        {
            string result = DateTime.Now.ToString("yyyy年MM月dd日  ") + "[" + paperMod.p_name + "]";
            result += "  考试时长:(" + (paperMod.p_UseTime > 0 ? paperMod.p_UseTime + "分钟)" : "不限定)");
            return result;
        }
        //根据试题类型,获取试卷中属于该类型的试题
        public DataTable GetByType(DataRow drv)
        {
            DataTable dt = null;
            string normFilter = "p_type=" + drv["QType"] + " AND (pid=0 OR pid IS NULL)";
            string bigfilter = "pid=" + drv["QType"];//big下,qtype为其id
            if (Action.Equals("view"))//查看答案
            {
                if (drv["IsBig"].ToString().Equals("0"))
                { AnswerDT.DefaultView.RowFilter = normFilter; }
                else
                { AnswerDT.DefaultView.RowFilter = bigfilter; }
                dt = AnswerDT.DefaultView.ToTable();
            }
            else
            {
                //是否为大题,如果是的话,加载入小题,这里完成排序
                if (drv["IsBig"].ToString().Equals("0"))
                {
                    QuestDT.DefaultView.RowFilter = normFilter;
                }
                else
                {
                    QuestDT.DefaultView.RowFilter = bigfilter;
                }
                dt = QuestDT.DefaultView.ToTable();
            }
            if (dt.Columns.Contains("order"))
            {
                dt.DefaultView.Sort = "order asc";
            }
            return dt.DefaultView.ToTable();
        }
        //-----------前端使用
        public MvcHtmlString GetContent(DataRow dr)
        {
            return MvcHtmlString.Create(questBll.GetContent(DataConverter.CLng(dr["p_id"]), DataConverter.CLng(dr["p_Type"]), dr["p_Content"].ToString()));
        }
        //显示回答区
        public MvcHtmlString GetSubmit(DataRow dr)
        {
            return MvcHtmlString.Create(questBll.GetSubmit(DataConverter.CLng(dr["p_id"]), DataConverter.CLng(dr["p_Type"]), ref AngularJS));
        }
        //该试题是否已被批改
        public MvcHtmlString GetIsRight(DataRow dr)
        {
            string tlp = "";
            switch (dr["IsRight"].ToString())
            {
                case "0":
                    tlp = "<span class='fa fa-cloud' style='font-size:1.2em;' title='尚未批阅'></span><span></span>";
                    break;
                case "1":
                    tlp = "<span class='fa fa-check' style='font-size:1.2em;color:green;' title='正确'></span><span>(" + dr["Score"] + "分)</span>";
                    break;
                case "2":
                    tlp = "<span class='fa fa-remove' style='font-size:1.2em;color:red;' title='错误'></span>";
                    break;
                default:
                    break;
            }
            return MvcHtmlString.Create(tlp);
        }
    }
}