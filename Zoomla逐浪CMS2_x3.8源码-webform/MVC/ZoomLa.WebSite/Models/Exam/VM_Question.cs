using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoomLa.AppCode.Controls;
using ZoomLa.BLL;
using ZoomLa.BLL.Exam;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLaCMS.Models.Exam
{
    //前后台添加与修改试题
    public class VM_Question : VM_Base
    {
        B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
        B_Exam_Sys_Papers bps = new B_Exam_Sys_Papers();
        B_Exam_Class nodeBll = new B_Exam_Class();
        B_Exam_Type bqt = new B_Exam_Type();
        B_ExamPoint bep = new B_ExamPoint();
        B_Exam_Version verBll = new B_Exam_Version();
        B_Questions_Knowledge knowBll = new B_Questions_Knowledge();
        B_User buser = new B_User();
        public int Mid { get { return DataConverter.CLng(Request.QueryString["id"]); } }
        //1:大题添加小题
        public int IsSmall { get { return DataConverter.CLng(Request.QueryString["issmall"]); } }
        public int NodeID { get { return DataConverter.CLng(Request.QueryString["NodeID"]); } }
        //--------------------------------------
        public M_Exam_Sys_Questions questMod = new M_Exam_Sys_Questions();
        public DataTable verDT = null;
        public DataTable gradeDT = null;
        public C_TreeTlpDP treeMod = null;
        public VM_Question() { }
        public VM_Question(M_UserInfo mu)
        {
            gradeDT = B_GradeOption.GetGradeList(6, 0);
            verDT = verBll.Sel();
            if (Mid > 0)
            {
                questMod = questBll.GetSelect(Mid);
                if (questMod.UserID != mu.UserID) {function.WriteErrMsg("你无权修改该试题"); }
            }
        }
        public MvcHtmlString GetQTypeRad()
        {
            return MVCHelper.H_Radios("qtype_rad", "单选题,多选题,填空题,解析题,完形填空,大题".Split(','), "0,1,2,3,4,10".Split(','), questMod.p_Type.ToString());
        }
        //存入字段,给予JS调用
        public string GetOPInfo()
        {
            return SafeSC.ReadFileStr(questMod.GetOPDir());
        }
    }
}