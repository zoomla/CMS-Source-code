using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace ZoomLa.BLL
{
    /// <summary>
    /// 全局使用的缓存模板,包装后置入,设置Guid和过期时间
    /// </summary>
    public class CacheCenter
    {
        private static Timer UserTimer = new Timer();
        private static List<M_CacheMod> list = new List<M_CacheMod>();
        static CacheCenter() 
        {
            UserTimer.Interval = 5 * 60 * 1000;//1Minute
            UserTimer.Start();
            UserTimer.Elapsed += new ElapsedEventHandler(ExpireCache);
        }
        public static string Push(object obj, string type)
        {
            M_CacheMod model = new M_CacheMod();
            model.obj = obj;
            model.Type = type;
            list.Add(model);
            return model.id;
        }
        /// <summary>
        /// 取出但不移除
        /// </summary>
        public static M_CacheMod Get(string guid)
        {
            if (string.IsNullOrEmpty(guid)) { return null; }
            return list.FirstOrDefault(p => p.id.Equals(guid));
        }
        public static object Pop(string guid)
        {
            M_CacheMod model = Get(guid);
            if (model != null) { list.Remove(model); }
            return model.obj;
        }
        private static void ExpireCache(object sender, System.Timers.ElapsedEventArgs e)
        {
            List<M_CacheMod> expired = list.Where(p => (DateTime.Now - p.CDate).TotalMinutes > p.Expire).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var model = list[i];
                if ((DateTime.Now - model.CDate).TotalMinutes > model.Expire)
                {
                    list.Remove(model);
                }
            }
        }
    }
    public class M_CacheMod 
    {
        public object obj;
        public DateTime CDate = DateTime.Now;
        public string id = Guid.NewGuid().ToString();
        /// <summary>
        /// 多少分钟过期
        /// </summary>
        public int Expire = 10;
        /// <summary>
        /// 用于存储标志,使用除Guid之外的方法获取数据
        /// </summary>
        public string Remind = "";
        /// <summary>
        /// 用于细分存储的元素
        /// </summary>
        public string Type = "";
    }
}
