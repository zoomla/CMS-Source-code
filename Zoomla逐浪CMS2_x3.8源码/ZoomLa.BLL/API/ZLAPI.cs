using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoomLa.SQLDAL.SQL;

namespace ZoomLa.BLL.API
{
    public class M_APIResult
    {
        public M_APIResult() { }
        public M_APIResult(int code) { retcode = code; }
        /// <summary>
        /// -1失败,1成功
        /// </summary>
        public int retcode = Success;
        /// <summary>
        /// 返回的报错或提示信息
        /// </summary>
        public string retmsg = "";
        /// <summary>
        /// 返回结果,Json格式
        /// </summary>
        public string result = "";
        /// <summary>
        /// 存储附加的返回信息,如数据总数,Json或字符串格式
        /// </summary>
        public object addon = null;
        /// <summary>
        /// 调用申请的action
        /// </summary>
        public string action = null;
        /// <summary>
        /// 分页信息,当前第几页,总页数,总数据数
        /// </summary>
        public object page = null;
        //是否jsonp调用
        [JsonIgnore]
        public string callback = "";
        public override string ToString()
        {
            string json = JsonConvert.SerializeObject(this, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            if (!string.IsNullOrEmpty(callback))
            {
                json = callback + "(" + json + ")";
            }
            return json;
        }
        public string AddJson(string name, object val)
        {
            return "{\"" + name + "\":\"" + val + "\"}";
        }
        public static int Success = 1;
        public static int Failed = -1;
    }
    /// <summary>
    /// 分页返回结果
    /// </summary>
    public class M_API_Page
    {
        public int cpage = 1;
        public int psize = 10;
        public int itemCount = 0;
        public int pageCount = 0;
        public M_API_Page() { }
        public M_API_Page(PageSetting setting)
        {
            cpage = setting.cpage;
            psize = setting.psize;
            itemCount = setting.itemCount;
            pageCount = setting.pageCount;
        }
    }
}
