using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace ZoomLa.BLL.API
{
    public class Proxy : IHttpHandler
    {
        private string apiSecret = "64B1B6E7B7A24C00B6EECE0962FD22F8";
        private string apiUrl = "https://api2.tradologic.net";//https://api2.tradologic.net  https://b2b-api.tradologic.net
        public bool IsReusable
        {
            get { return true; }
        }
        public void ProcessRequest(HttpContext context)
        {
            // Handle custom request
            //if (context.Request.Url.LocalPath.IndexOf("language") > 0) { string str = SafeSC.ReadFileStr(context.Server.MapPath("/test.txt")); context.Response.Write(str); return; }
            string reqstr=context.Request.Url.LocalPath.Substring(5);//省去/edge
            switch (reqstr)//如果请求在这里命中,则直接返回,否则以后面的方式,通用传输
            {
                // Get token
                case "/v1/token"://获取accessToken,不需要用户名与密码
                    {
                        string token = this.GetToken(context);
                        context.Response.Write(token);
                    }
                    return;
                // Get anonymous token
                case "/v1/token/anonymous":
                    this.GetAnonymousToken(context);
                    return;
                case "/v1/merge":
                    this.ProcessMerge(context);
                    break;
            }

            string responseFromServer = null;

            // Add query params if we must
            string url = this.apiUrl + context.Request.Url.LocalPath.Substring(5) + "?" + context.Request.QueryString.ToString();
            url = url.TrimEnd('?');
            HttpWebRequest client = HttpWebRequest.Create(url) as HttpWebRequest;

            // Forward the real user IP address to the API
            context.Request.Headers["X-Forwarded-For"] = context.Request.UserHostAddress;

            // Forward the headers from the client to te Api
            foreach (var header in context.Request.Headers.AllKeys)
            {
                var value = context.Request.Headers[header];
                switch (header.ToLower())
                {
                    case "content-length":
                    case "connection":
                    case "host":
                    case "accept-encoding":
                    case "expect":
                        //these headers are ignored, they don't have corresponing properties in the "HttpWebRequest" client 
                        //and we can't modify them in the "default" of this switch
                        break;
                    case "content-type":
                        client.ContentType = value;
                        break;
                    case "user-agent":
                        client.UserAgent = value;
                        break;
                    case "accept":
                        client.Accept = value;
                        break;
                    case "referer":
                        client.Referer = value;
                        break;
                    default:
                        // All headers that don't have corresponding properties in the "HttpWebRequest" client are handled here
                        //指定的值含有无效的控制字符,原因里面不能包含部分中文,如必须包含,则必须转码HttpUtility.UrlEncode()
                        client.Headers.Add(header, HttpUtility.UrlEncode(value));
                        break;
                }
            }
            //context.Request.Headers.Add("Authorization", "oauth oauth_token=" + GetToken(context));
            // Forward the HTTP method from the client to the Api
            client.Method = context.Request.HttpMethod;

            // Handle POST, PUT, DELETE
            if (client.Method != "GET")
            {
                StreamReader requestStream = new StreamReader(context.Request.InputStream);
                byte[] byteArray = Encoding.UTF8.GetBytes(requestStream.ReadToEnd());
                client.ContentLength = byteArray.Length;
                Stream dataStream = client.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }

            // Do the request
            try
            {
                // Handle 2xx response codes

                // Do the request
                HttpWebResponse response = (HttpWebResponse)client.GetResponse();

                // Get the response from Api
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                // Cleanup resources
                reader.Close();
                dataStream.Close();
                response.Close();
                // Forward the status code from the Api to the client
                context.Response.StatusCode = (int)response.StatusCode;
            }
            catch (WebException e)
            {
                // Handle response errors (the "HttpWebRequest" client raises an exception when the response is different from 2xx)
                responseFromServer = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();

                // When there is 4xx error code, the correct status code is returnded in the API response
                context.Response.StatusCode = 200;
            }
     
            // Forward API response to the client
            context.Response.Write(responseFromServer);
        }
        public string GetToken(HttpContext context)
        {
            string responsebody = "";
            using (WebClient client = new WebClient())
            {
                try
                {
                    System.Collections.Specialized.NameValueCollection reqparam = new System.Collections.Specialized.NameValueCollection();
                    reqparam.Add("secret", this.apiSecret);
                    Guid sessionID;
                    if (context.Request.QueryString.AllKeys.Contains("sessionId") && Guid.TryParse(context.Request.QueryString["sessionId"], out sessionID))
                    {
                        reqparam.Add("sessionId", sessionID.ToString());
                    }

                    byte[] responsebytes = client.UploadValues(this.apiUrl + "/v1/authorize", "POST", reqparam);
                    responsebody = Encoding.UTF8.GetString(responsebytes);
                }
                catch (WebException e)
                {
                    // Handle response errors (the "HttpWebRequest" client raises an exception when the response is different from 2xx)
                    responsebody = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();

                    // Forward the status code from the Api to the client

                    // When there is 4xx error code, the correct status code is returnded in the API response
                    context.Response.StatusCode = 200;// (int)(e.Response as HttpWebResponse).StatusCode; 
                }
            }
            return responsebody;

        }
        /**
         * Get new anonymous user token
         */
        public void GetAnonymousToken(HttpContext context)
        {
            string responsebody = "";
            using (WebClient client = new WebClient())
            {
                try
                {
                    System.Collections.Specialized.NameValueCollection reqparam = new System.Collections.Specialized.NameValueCollection();
                    reqparam.Add("secret", this.apiSecret);
                    reqparam.Add("mapKey", context.Request.QueryString["mapKey"]);
                    reqparam.Add("currency", context.Request.QueryString["currency"]);

                    byte[] responsebytes = client.UploadValues(this.apiUrl + "/v1/authorize/anonymous", "POST", reqparam);
                    responsebody = Encoding.UTF8.GetString(responsebytes);
                }
                catch (WebException e)
                {
                    // Handle response errors (the "HttpWebRequest" client raises an exception when the response is different from 2xx)
                    responsebody = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();

                    // Forward the status code from the Api to the client

                    // When there is 4xx error code, the correct status code is returnded in the API response
                    context.Response.StatusCode = 200;// (int)(e.Response as HttpWebResponse).StatusCode; 
                }
            }
            context.Response.Write(responsebody);
        }
        /// <summary>
        /// Appends the api secret key to the token object
        /// </summary>
        /// <param name="context"></param>
        public void ProcessMerge(HttpContext context)
        {
            //JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            //Dictionary<string, Dictionary<string, object>> resourcesDictionary = jsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(context.Request.QueryString["resources"]);

            //if (resourcesDictionary.ContainsKey("token"))
            //{
            //    resourcesDictionary["token"].Add("secret", this.apiSecret);
            //}

            //// this is a hack so that we can set query string parameters to controllers' requests
            //PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            //isreadonly.SetValue(context.Request.QueryString, false, null);
            //context.Request.QueryString.Set("resources", jsonSerializer.Serialize(resourcesDictionary));
        }

        //----------------------
        public string BeginRequest(HttpContext context, string reqstr, string query)
        {
            string responseFromServer = null;

            // Add query params if we must
            string url = (this.apiUrl + reqstr + "?" + query).TrimEnd('?');
            HttpWebRequest client = HttpWebRequest.Create(url) as HttpWebRequest;

            // Forward the real user IP address to the API
            context.Request.Headers["X-Forwarded-For"] = context.Request.UserHostAddress;
            // Forward the headers from the client to te Api
            foreach (var header in context.Request.Headers.AllKeys)
            {
                var value = context.Request.Headers[header];
                switch (header.ToLower())
                {
                    case "content-length":
                    case "connection":
                    case "host":
                    case "accept-encoding":
                    case "expect":
                        //these headers are ignored, they don't have corresponing properties in the "HttpWebRequest" client 
                        //and we can't modify them in the "default" of this switch
                        break;
                    case "content-type":
                        client.ContentType = value;
                        break;
                    case "user-agent":
                        client.UserAgent = value;
                        break;
                    case "accept":
                        client.Accept = value;
                        break;
                    case "referer":
                        client.Referer = value;
                        break;
                    default:
                        // All headers that don't have corresponding properties in the "HttpWebRequest" client are handled here
                        client.Headers.Add(header, value);
                        break;
                }
            }
            //context.Request.Headers.Add("Authorization", "oauth oauth_token=" + token);
            // Forward the HTTP method from the client to the Api
            client.Method = context.Request.HttpMethod;

            // Handle POST, PUT, DELETE
            if (client.Method != "GET")
            {
                StreamReader requestStream = new StreamReader(context.Request.InputStream);
                byte[] byteArray = Encoding.UTF8.GetBytes(requestStream.ReadToEnd());
                client.ContentLength = byteArray.Length;
                Stream dataStream = client.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }

            // Do the request
            try
            {
                // Do the request
                HttpWebResponse response = (HttpWebResponse)client.GetResponse();

                // Get the response from Api
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                // Cleanup resources
                reader.Close();
                dataStream.Close();
                response.Close();
                // Forward the status code from the Api to the client
                context.Response.StatusCode = (int)response.StatusCode;
            }
            catch (WebException e)
            {
                // Handle response errors (the "HttpWebRequest" client raises an exception when the response is different from 2xx)
                responseFromServer = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
            }
            return responseFromServer;
        }
    }
}
