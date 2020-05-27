using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.SQLDAL;

namespace ZoomLa.BLL.Helper
{  
   /*
    * 1,Hour是从0开始
    * 2,DayInWeek:周日为0,0-6
    */
    public class DateHelper
    {
        /// <summary>
        /// 间隔是否超过一天,昨天未满24小时也算
        /// </summary>
        /// <param name="time"></param>
        /// <returns>true,超过</returns>
        public static bool IsMoreThanOne(DateTime time)
        {
            return IsMoreThanOne(time,DateTime.Now);
        }
        public static bool IsMoreThanOne(DateTime stime, DateTime etime)
        {
            bool flag = false;
            double days = (etime - stime).TotalDays;
            if (days >= 1) { flag = true; }
            else
            {
                if (stime.Day != etime.Day) { flag = true; }
            }
            return flag;
        }
        public static void GetWeekSE(DateTime time, ref string stime, ref string etime)
        {
            int today = (int)time.DayOfWeek;
            if (today == 0) today = 7;//以周一至周日算
            stime = time.AddDays(-today + 1).ToString("yyyy-MM-dd 00:00");
            etime = time.AddDays(7 - today).ToString("yyyy-MM-dd 23:59");
        }
        public static void GetMonthSE(DateTime time, ref string stime, ref string etime)
        {
            stime = time.ToString("yyyy/MM/01 00:00:00");
            etime = time.AddMonths(1).ToString("yyyy/MM/01 00:00:00");
        }
        /// <summary>
        /// 返回开始与结束时间
        /// today,lastmonth,thismonth,thisweek,lastweek,stime&etime
        /// </summary>
        public static void GetSETime(string dateStr, ref string stime, ref string etime)
        {
            if (string.IsNullOrWhiteSpace(dateStr)) { return; }
            dateStr = dateStr.ToLower();
            switch (dateStr)
            {
                case "today":
                    stime = DateTime.Now.ToString("yyyy/MM/dd 00:00:00");
                    etime = DateTime.Now.ToString("yyyy/MM/dd 23:59:59");
                    break;
                case "thismonth":
                    stime = DateTime.Now.ToString("yyyy/MM/01 00:00:00");
                    etime = DateTime.Now.AddMonths(1).ToString("yyyy/MM/01 00:00:00");
                    break;
                case "lastmonth":
                    stime = DateTime.Now.AddMonths(-1).ToString("yyyy/MM/01 00:00:00");
                    etime = DateTime.Now.ToString("yyyy/MM/01 00:00:00");
                    break;
                case "lastweek"://上个自然周,周一至周日 
                    GetWeekSE(DateTime.Now.AddDays(-7), ref stime, ref etime);
                    break;
                case "thisweek":
                    GetWeekSE(DateTime.Now, ref stime, ref etime);
                    break;
            }
            //最近多少天(含今日)
            if (dateStr.Contains("last-"))
            {
                int days = (DataConvert.CLng(dateStr.Split('-')[1]) - 1);
                if (days > 0)
                {
                    stime = DateTime.Now.AddDays(-days).ToString("yyyy/MM/dd 00:00:00");
                    etime = DateTime.Now.ToString("yyyy/MM/dd 23:59:59");
                }
            }
            if (dateStr.Contains("|"))
            {
                DateTime date = new DateTime();
                if (DateTime.TryParse(dateStr.Split('|')[0], out date)) { stime = date.ToString(); }
                if (DateTime.TryParse(dateStr.Split('|')[1], out date)) { etime = date.ToString(); }
            }
        }
        /// <summary>
        /// 以标准格式输出时间,主用于前端
        /// </summary>
        public static string ToDate(object o, string format = "yyyy年MM月dd日 HH:mm")
        {
            return DataConvert.CDate(o).ToString(format);
        }
    }
}
