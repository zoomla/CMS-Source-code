using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using ZoomLa.BLL;
using ZoomLa.Model;

namespace ZoomLa.HtmlLabel
{
    public class T_ExamTask : BaseTask
    {
        public T_ExamTask(M_Content_ScheTask scheMod)
        {
            base.TaskFlag = "AutoCountDifficult";
            base.scheMod = scheMod;
            base.SetInterval();
            base.ExecuteTask += new ElapsedEventHandler(ExecuteFunc);
        }
        public override void ExecuteFunc(object sender, System.Timers.ElapsedEventArgs e)
        {
            ////获取上一次执行日志,如间隔大于一周,则执行
            //B_Exam_Sys_Questions questBll = new B_Exam_Sys_Questions();
            //logMod = logBll.SelLastModelByFlag(TaskFlag);
            //if (logMod == null || (DateTime.Now - logMod.CDate).TotalDays >= 7)
            //{
            //    questBll.CountDiffcult();
            //    logBll.Insert(new M_Ex_ExecuteLog()
            //    {
            //        Name = "教育_计算试题难度系数",
            //        Flag = TaskFlag
            //    });
            //}
        }
    }
}
