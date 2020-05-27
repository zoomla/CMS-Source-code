using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
/*
 * 用于缓存通知消息,所有人读过或超时后移除
 * 前端使用短轮循或长连接获取
 */
namespace ZoomLa.BLL.User
{
    public class B_User_Notify
    {
        public static List<M_User_Notify> NotifyList = new List<M_User_Notify>();
        /// <summary>
        /// 添加一条新通知,Title限定60字符,内容:4000
        /// </summary>
        public static void Add(M_User_Notify model)
        {
            if (model.ReceUsers.Count < 1) { return; }
            if (model.ExpireTime < 1) { model.ExpireTime = 10; }
            model.Title = StringHelper.SubStr(model.Title);
            model.Content = StringHelper.SubStr(model.Content, 4000);
            NotifyList.Add(model);
        }
        public static void Add(string title, string content, string receUser)
        {
            M_User_Notify model = new M_User_Notify();
            model.Title = title;
            model.Content = content;
            model.AppendReceUser(receUser);
            Add(model);
        }
        /// <summary>
        /// 移除已过期或全部人都已读过的信息
        /// </summary>
        public static void RemoveExpired()
        {
            NotifyList.RemoveAll(p => p.ReceUsers.Count < 1 || (DateTime.Now - p.CDate).Minutes > p.ExpireTime);
        }
        /// <summary>
        /// 用户已读某些信息(服务端返回时即调用)
        /// </summary>
        public static List<M_User_Notify> ReadNotify(string uid)
        {
            RemoveExpired();
            List<M_User_Notify> list = NotifyList.Where(p => p.ReceUsers.Contains(uid)).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].ReceUsers.Remove(uid);
                if (list[i].ReceUsers.Count < 1) { NotifyList.Remove(list[i]); }
            }
            return list;
        }
    }
    public class M_User_Notify
    {
        public M_User_Notify()
        {
            NType = 1;
            CDate = DateTime.Now;
            Gid = Guid.NewGuid().ToString();
        }
        public string Title { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 接收人Uids,读后将其从这里移除
        /// </summary>
        [JsonIgnore]
        public List<string> ReceUsers = new List<string>();
        /// <summary>
        /// 标识
        /// </summary>
        [JsonIgnore]
        public string Gid { get; set; }
        //------------------来源信息
        public int InfoID { get; set; }
        public int CUser { get; set; }
        public string CUName { get; set; }
        public string Remind { get; set; }
        /// <summary>
        /// 用于分类信息
        /// </summary>
        public int NType { get; set; }
        //------------------
        public DateTime CDate { get; set; }
        /// <summary>
        /// 默认十分钟有效期,0为无限期
        /// </summary>
       [JsonIgnore]
        public int ExpireTime = 10;
        //------------------Tools
        public void AppendReceUser(string uids)
        {
            string[] arr = uids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            ReceUsers.AddRange(arr);
        }
    }
}
