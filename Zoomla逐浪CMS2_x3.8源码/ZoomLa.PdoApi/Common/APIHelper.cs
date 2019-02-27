using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ZoomLa.PdoApi
{
    //后期整合Html上传附件等
    internal class APIHelper
    {
        /// <summary>
        /// 调用接口,并获取返回的数据(使用见WXAPI)(推荐)
        /// </summary>
        /// <param name="url">目标链接</param>
        /// <param name="method">提交方式</param>
        /// <param name="postdata">需要提交的数据(json或字符串)</param>
        /// <returns></returns>
        public static string GetWebResult(string url, string method = "GET", string postdata = "")
        {
            string result = "";
            if (!string.IsNullOrEmpty(postdata))
            {
                WebClient client = new WebClient();
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                client.Encoding = Encoding.UTF8;
                result = client.UploadString(new Uri(url), method, postdata);
            }
            else
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                WebResponse respose = request.GetResponse();
                StreamReader reader = new StreamReader(respose.GetResponseStream());
                result = reader.ReadToEnd();
                reader.Close();
            }
            return result;
        }
    }
}
