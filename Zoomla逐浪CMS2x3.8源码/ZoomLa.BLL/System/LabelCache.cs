using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.Model;

namespace ZoomLa.BLL
{
    //系统方法等不缓存
    public class LabelCache
    {
        public static Hashtable Labelht = new Hashtable();
        //从缓存中获取标签,为空则是没有,则执行标签解析
        public static string GetLabel(string ialbel)
        {
            string result = "";
            if (Labelht.Contains(ialbel))
            {
                result = Labelht[ialbel].ToString();
            }
            return result;
        }
        //添加标签进入缓存
        public static void AddLabel(string ialbel, string result)
        {
            try
            {
                if (!Labelht.Contains(ialbel))
                {
                    Labelht.Add(ialbel, result);
                }
            }
            catch (Exception ex) { ZLLog.L(ZLEnum.Log.labelex, "标签添入缓存报错,标签名:" + ialbel + ",原因:" + ex.Message); }
        }
        public static void Clear() 
        {
            Labelht.Clear();
        }

    //{ZL.Label Filter="NodeID=100" UserName="Admin" /} 支持   Select NodeID From ZL_CommonModel Where NodeID in(1,2,100)  
    //载入整张表,使用时取出数据，再过滤,存入键的时候,去除Filter
        //----------
         /*
          * 1,永久缓存区
          * 2,时效缓存区(例如5分钟更新一次)
          * 3,临时缓存区(只在当页有效)
          */ 
        //----Temp测试使用
        //public static int Count1 = 0;
        //public static int Count2 = 0;
        //public static int GetHtmlCount = 0;
        //public static string count1Name = "";
        //public static string count2Name = "";
        //public static string GetHtmlName = "";
        //public static int LabelTCount = 0;
    }
}
