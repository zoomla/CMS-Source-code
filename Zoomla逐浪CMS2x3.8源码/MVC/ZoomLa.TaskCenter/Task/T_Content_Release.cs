using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Model;

namespace ZoomLa.HtmlLabel.Task
{
    /*
     * 定时发布,每十分钟检测一次
     */
    public class T_Content_Release : BaseTask
    {
        B_Admin badmin = new B_Admin();
        B_Create cBll = new B_Create();
        B_Content conBll = new B_Content();
        public T_Content_Release(M_Content_ScheTask scheMod)
        {
            base.TaskFlag = "T_Content_Release";
            base.scheMod = scheMod;
            base.SetInterval();
            base.ExecuteTask += new ElapsedEventHandler(ExecuteFunc);
        }
        public override void ExecuteFunc(object sender, System.Timers.ElapsedEventArgs e)
        {
            conBll.UpdateStatus(scheMod.TaskContent, 99);
            scheBll.UpdateStatus(scheMod.ID.ToString(),100);
        }
    }
}