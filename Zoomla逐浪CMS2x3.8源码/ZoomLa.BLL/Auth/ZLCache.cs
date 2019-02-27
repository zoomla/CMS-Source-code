using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using ZoomLa.BLL.Helper;
using ZoomLa.Model;
namespace ZoomLa.BLL
{
    /*
     * 有多个信息,是否需要合并更新?
     */
    public class M_Cache
    {
        public M_Cache(M_UserInfo mu)
        {
            AddTime = DateTime.Now;
            LastUpdateTime = DateTime.Now;
            UserID = mu.UserID;
            model = mu;
        }
        public M_Cache(M_AdminInfo adminMod)
        {
            AddTime = DateTime.Now;
            LastUpdateTime = DateTime.Now;
            UserID = adminMod.AdminId;
            model = adminMod;
        }
        //添加时间,最近更新时间,缓存模型
        public DateTime AddTime;
        public DateTime LastUpdateTime;
        public int UserID = 0;
        public M_Base model = null;
    }
    public class ZLCache
    {
        private const int ActiveSpan = 10;
        private static Timer UserTimer = new Timer();
        static ZLCache()
        {
            UserTimer.Interval = ActiveSpan * 60 * 1000;//1Minute
            UserTimer.Start();
            UserTimer.Elapsed += new ElapsedEventHandler(ExpireCache);

        }
        //----用户
        public static Dictionary<string, M_Cache> UserSession = new Dictionary<string, M_Cache>();
        /// <summary>
        /// 无则插入,有则更新缓存
        /// </summary>
        public static void AddUser(string key, M_UserInfo mu)
        {
            if (UserSession.ContainsKey(key))
            {
                M_Cache model = (M_Cache)UserSession[key];
                model.LastUpdateTime = DateTime.Now;
                model.model = mu;
            }
            else
            {
                M_Cache model = new M_Cache(mu);
                UserSession.Add(key, model);
            }
        }
        public static M_UserInfo GetUser(string key)
        {
            if (UserSession.ContainsKey(key) && UserSession[key] != null)
            {
                var item = UserSession[key];
                item.LastUpdateTime = DateTime.Now;
                return (M_UserInfo)item.model;
            }
            return null;
        }
        //最终用于清除的方法,过期与主动都调用其
        public static void ClearByKeys(string keys)
        {
            keys = keys.TrimEnd(',');
            if (!string.IsNullOrEmpty(keys))
            {
                try
                {
                    foreach (string key in keys.Split(','))
                    {
                        //后期改为缓存事件分发
                        M_Cache model = UserSession[key];
                        B_User.UpdateField("LastActiveTime", (DateTime.Now.AddMinutes(-ActiveSpan)).ToString(), model.UserID, false);
                        UserSession.Remove(key);
                    }
                }
                catch (Exception ex) { ZLLog.L(Model.ZLEnum.Log.labelex, "用户缓存出错:" + ex.Message); }
            }
        }
        /// <summary>
        /// 主动清除用户
        /// </summary>
        public static void ClearByIDS(int uid)
        {
            ClearByIDS(uid.ToString());
        }
        /// <summary>
        /// 按uids清除缓存,用于通知用户重新获取
        /// </summary>
        public static void ClearByIDS(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return;
            ids = StrHelper.IdsFormat(ids);
            string keys = "";
            //后期改为搜索而不遍历
            foreach (var item in UserSession)
            {
                if (ids.Contains("," + item.Value.UserID + ",")) { keys += item.Key + ","; }
            } 
            ClearByKeys(keys);
        }
        //----通用模块
        //清空过期缓存
        public static void ExpireCache(object sender, System.Timers.ElapsedEventArgs e)
        {
            string keys = "";
            foreach (var item in UserSession)
            {
                if ((DateTime.Now - item.Value.LastUpdateTime).TotalMinutes > ActiveSpan)
                {
                    keys += item.Key + ",";
                }
            }
            //ZLLog.L(Model.ZLEnum.Log.safe, "现有" + UserSession.Count + "个缓存数据,已清除" + keys + "清除时间" + DateTime.Now);
            ClearByKeys(keys);
        }
    }
}
