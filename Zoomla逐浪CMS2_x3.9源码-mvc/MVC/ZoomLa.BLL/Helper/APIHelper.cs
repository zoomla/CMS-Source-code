using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ZoomLa.Components;
using ZoomLa.Model;
namespace ZoomLa.BLL
{
    public class APIHelper
    {
        /// <summary>
        /// 调用接口,并获取返回的数据(使用见WXAPI)(推荐)
        /// </summary>
        /// <param name="url">目标链接</param>
        /// <param name="method">提交方式</param>
        /// <param name="postdata">需要提交的数据(json或字符串){\"Identifier\":\"test223\"}</param>
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
        /// <summary>
        /// 见QQHelper,仅用于Post提交参数
        /// </summary>
        public static string UploadStr(string url, string data)
        {
            WebClient client = new WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            client.Encoding = Encoding.UTF8;
            string result = client.UploadString(new Uri(url), "Post", data);
            return result;
        }
    }
}
