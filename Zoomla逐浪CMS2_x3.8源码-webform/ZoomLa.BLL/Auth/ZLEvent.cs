using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
 * 惰性事件队列,不主动触发事件
 */
namespace ZoomLa.BLL.Auth
{
    public class ZLEvent
    {
        public enum EventT { UAction };
        public static List<M_ZLEvent> ZLEventList = new List<M_ZLEvent>();
        public static void AddEvent(M_ZLEvent model)
        {
            ZLEventList.Add(model);
        }
        /// <summary>
        /// 取出符合条件的事件,并移除事件
        /// </summary>
        public static List<M_ZLEvent> SelByFlag(EventT type)
        {
            List<M_ZLEvent> list = ZLEventList.Where(p => p.MyType == type).ToList();
            RemoveByList(list);
            return list;
        }
        //注意name的大小写
        public static List<M_ZLEvent> SelByFlag(EventT type, string name)
        {
            List<M_ZLEvent> list = ZLEventList.Where(p => p.MyType == type && p.Name.Equals(name)).ToList();
            RemoveByList(list);
            return list;
        }
        private static void RemoveByList(List<M_ZLEvent> list)
        {
            if (list.Count < 1) { return; }
            foreach (var model in list)
            {
                ZLEventList.Remove(model);
            }
        }
    }
    public class M_ZLEvent
    {
        //过期时间,秒为单位,如未执行自动消除
        //public int ElapseTime = 600;
        [JsonIgnore]
        public ZLEvent.EventT MyType;
        public string Name = "";
        public string Value = "";
        public string Param = "";
    }
}
