using System;
using System.Collections.Generic;

using System.Text;
using System.Net;
using System.IO;

namespace SDK
{
   public class HTTPBase
    {
        /// <summary>
        /// 发送GET请求
        /// </summary>
        /// <param name="url">请求url</param>
        /// <param name="queryParams">请求参数</param>
        /// <returns>返回请求内容</returns>
        public string Get(string url, Dictionary<string, string> queryParams)
        {
            string realUrl = url;
            if (queryParams.Count > 0)
            {
                realUrl = url + "?";
                foreach (KeyValuePair<string, string> item in queryParams)
                {
                    realUrl +=  item.Key + "=" + item.Value;
                    realUrl += "&";
                }
                realUrl = realUrl.TrimEnd('&');
            }
            WebRequest request = WebRequest.Create(realUrl);
            request.Method = "GET";
            try
            {
                WebResponse webResponse = request.GetResponse();
                Stream newStream = webResponse.GetResponseStream();

                StreamReader rdr = new StreamReader(newStream);
                return rdr.ReadToEnd();
            }
            catch (WebException e)
            {
                WebResponse response = e.Response;
                Stream newStream = response.GetResponseStream();

                StreamReader rdr = new StreamReader(newStream);
                string content = rdr.ReadToEnd();
                return content;
            }
        }

        /// <summary>
        /// 发送POST请求
        /// </summary>
        /// <param name="url">请求的url</param>
        /// <param name="queryParams">请求参数</param>
        /// <returns>返回的请求内容</returns>
        public string Post(string url, Dictionary<string, string> queryParams)
        {
            WebRequest request = WebRequest.Create(url);

            string data = "";
            if (queryParams.Count > 0)
            {
                foreach (KeyValuePair<string, string> item in queryParams)
                {
                    data += item.Key + "=" + item.Value;
                    data += "&";
                }
                data = data.TrimEnd('&');
            }

            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            try
            {
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(Encoding.Default.GetBytes(data), 0, Encoding.Default.GetByteCount(data));
                dataStream.Close();
                WebResponse webResponse = request.GetResponse();
                Stream newStream = webResponse.GetResponseStream();

                StreamReader rdr = new StreamReader(newStream);
                return rdr.ReadToEnd();
            }
            catch (WebException e)
            {
                WebResponse response = e.Response;
                Stream newStream = response.GetResponseStream();

                StreamReader rdr = new StreamReader(newStream);
                string content = rdr.ReadToEnd();
                return content;
            }
        }
    }
}
