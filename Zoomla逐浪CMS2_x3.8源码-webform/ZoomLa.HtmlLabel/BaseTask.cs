using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using ZoomLa.BLL;
using ZoomLa.BLL.Content;
using ZoomLa.Model;
using ZoomLa.Model.Content;
using ZoomLa.SQLDAL;

namespace ZoomLa.HtmlLabel
{
    /// <summary>
    /// 1,任务类父类,暂不使用单例模式,后期或改为单例
    /// 2,需要扩展支持每日定时处理
    /// </summary>
    public abstract class BaseTask
    {
        //需要执行的逻辑模型
        public M_Content_ScheTask scheMod = new M_Content_ScheTask();
        protected B_Content_ScheTask scheBll = new B_Content_ScheTask();
        protected B_Content_ScheLog logBll = new B_Content_ScheLog();
        private Timer _timer = null;
        // 任务标志字符串,必须重写
        public string TaskFlag = "";//或改为Guid
        // 运行标识,如设为false则不执行具体功能
        public bool flag = false;
        // 间隔时间
        public int Interval { get; set; }
        //设定间隔时间,也可自定义
        public void SetInterval()
        {
            switch (scheMod.ExecuteType)
            {
                case (int)M_Content_ScheTask.ExecuteTypeEnum.JustOnce:
                    //每分钟检测一次,看是否到时间
                    Interval = 60 * 1000;
                    break;
                case (int)M_Content_ScheTask.ExecuteTypeEnum.EveryDay:
                    //每分钟检测一次看是否到时间
                    Interval = 60 * 1000;
                    break;
                case (int)M_Content_ScheTask.ExecuteTypeEnum.Interval:
                    int time = DataConvert.CLng(scheMod.Interval) * 60 * 1000;
                    if (time <= 0)
                    {
                        Interval = int.MaxValue;
                        ZLLog.L(scheMod.TaskName + ",时间设定不正确");
                    }
                    else { Interval = time; }
                    break;
                case (int)M_Content_ScheTask.ExecuteTypeEnum.Passive:
                    Interval = int.MaxValue;
                    break;
            }
        }
        public void Start()
        {
            if (_timer == null)
            {
                _timer = new Timer(Interval);
                _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
                _timer.Enabled = true;
                _timer.Start();
                flag = true;
            }
        }
        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
                flag = false;
            }
        }
        protected event ElapsedEventHandler ExecuteTask;
        // 到达时间,执行任务
        protected void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                bool flag = false;
                switch (scheMod.ExecuteType)
                {
                    case (int)M_Content_ScheTask.ExecuteTypeEnum.JustOnce:
                        if (DateTime.Now >= scheMod.ExecuteTime2 && scheMod.Status == 0)
                        {
                            ExecuteFunc(sender, e);
                            flag = true;
                            //scheMod.Status = 100;
                            //DBCenter.UpdateSQL(scheMod.TbName, "Status=100", "ID=" + scheMod.ID);
                            TaskCenter.RemoveTask(scheMod.ID);
                        }
                        break;
                    case (int)M_Content_ScheTask.ExecuteTypeEnum.EveryDay:
                        //已到时间,且今天未执行过
                        if (string.IsNullOrEmpty(scheMod.LastTime) || scheMod.LastTime2.DayOfYear != DateTime.Now.DayOfYear)
                        {
                            if (DateTime.Now >= scheMod.ExecuteTime2)
                            {
                                ExecuteFunc(sender, e);
                                flag = true;
                            }
                        }
                        break;
                    case (int)M_Content_ScheTask.ExecuteTypeEnum.Interval:
                        {
                            ExecuteFunc(sender, e);
                            flag = true;
                        }
                        break;
                }
                if (flag)
                {
                    //更新任务状态和执行日志
                    scheMod.LastTime = DateTime.Now.ToString();
                    DBCenter.UpdateSQL(scheMod.TbName, "LastTime='" + DateTime.Now + "'", "ID=" + scheMod.ID);
                    M_Content_ScheLog logMod = new M_Content_ScheLog();
                    logMod.TaskID = scheMod.ID;
                    logMod.TaskName = scheMod.TaskName;
                    logMod.Result = 1;
                    logBll.Insert(logMod);
                }
            }
            catch (Exception ex) { ZLLog.L("TaskCenter-->[" + scheMod.TaskName + "]出错,原因" + ex.Message); }
        }
        // 主体逻辑,子类必须将其实现,由父类触发调用
        public abstract void ExecuteFunc(object sender, System.Timers.ElapsedEventArgs e);
    }
}
