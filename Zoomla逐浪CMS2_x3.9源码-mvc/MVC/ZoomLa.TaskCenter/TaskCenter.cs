using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.HtmlLabel.Task;
using ZoomLa.Model;
using ZoomLa.SQLDAL;
/*
 * 任务中心,在此读取配置文件,按需启动任务
 * 支持自定义任务,系统任务
 */
namespace ZoomLa.HtmlLabel
{
    public class TaskCenter
    {
        public static List<BaseTask> TaskList = new List<BaseTask>();
        static TaskCenter()
        {
        }
        public static void Init()
        {
            TaskList.Clear();
            B_Content_ScheTask scheBll = new B_Content_ScheTask();
            List<M_Content_ScheTask> list = scheBll.SelTaskList();
            for (int i = 0; i < list.Count; i++)
            {
                M_Content_ScheTask scheMod = list[i];
                var task = CreateTask(scheMod);
                if (task != null) { TaskList.Add(task); }
            }
        }
        public static void Start()
        {
            foreach (BaseTask task in TaskList) { task.Start(); }
        }
        public static void Stop()
        {
            foreach (BaseTask task in TaskList) { task.Stop(); }

        }
        //------------------------------
        /// <summary>
        /// 添加或更新一个任务,如果已存在(TaskID),则会将其加入
        /// </summary>
        public static void AddTask(M_Content_ScheTask scheMod)
        {
            //已完成或禁用任务不进入初始化队例
            if (scheMod.Status != 0) { return; }
            if (scheMod.ID < 1)
            {
                TaskList.Add(TaskCenter.CreateTask(scheMod));
            }
            else
            {
                BaseTask model = TaskList.FirstOrDefault(p => p.scheMod.ID == scheMod.ID);
                if (model != null) { TaskCenter.RemoveTask(model.scheMod.ID); }
                model = TaskCenter.CreateTask(scheMod);
                TaskList.Add(model);
                model.Start();
            }
        }
        public static void RemoveTask(int scheID)
        {
            BaseTask model = TaskList.FirstOrDefault(p => p.scheMod.ID == scheID);
            if (model != null) { model.Stop(); TaskList.Remove(model); }
        }
        //------------------------------
        private static BaseTask CreateTask(M_Content_ScheTask model)
        {
            BaseTask task = null;
            switch (model.TaskType)
            {
                case (int)M_Content_ScheTask.TaskTypeEnum.ExecuteSQL:
                    task = new T_ExecuteSQL(model);
                    break;
                //case (int)M_Content_ScheTask.TaskTypeEnum.Release:
                //    break;
                case (int)M_Content_ScheTask.TaskTypeEnum.Content:
                    task = new T_Content_Release(model);
                    break;
            }
            return task;
        }
    }
}
