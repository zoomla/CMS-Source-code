namespace ZoomLaCMS.Common.API
{
    using System;
    using System.Text;
    using System.Web;
    using System.Net;
    using System.IO;
    using ZoomLa.BLL;
    using ZoomLa.BLL.Helper;
    using ZoomLa.Common;
    using ZoomLa.Model;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    //需完善:1,过期检测
    //       2,超过1024个字符,需要切割
    public class Sounder : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpResponse Response = context.Response;
            HttpRequest Request = context.Request;
            if (function.isAjax())
            {
                string text = Request.Form["tex"];
                string result = ZL_API_Sound.GetSoundPath(text);
                Response.Write(result); Response.Flush(); Response.End();
            }
        }

        public bool IsReusable { get { return false; } }
    }
    public class ZL_API_Sound
    {
        static string TokenInfo = "";
        static string Token = "";
        static string vpath = "/UploadFiles/Sounder/";
        /// <summary>
        /// 返回音频文件路径
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetSoundPath(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            string md5 = StringHelper.MD5(text);
            string savePath = vpath + md5 + ".mp3";
            if (File.Exists(function.VToP(savePath))) { return savePath; }
            else//开始合成语音 
            {
                savePath = GetSoundFromBiadu(text, md5);
            }
            return savePath;
        }
        //合成文本长度必须小于1024字节，如果本文长度较长，可以采用多次请求的方式。切忌不可文本长度超过限制。
        public static string GetSoundFromBiadu(string text, string md5)
        {
            text = text.Trim();
            //string token = "24.7a59ff42a21e6a68093775145c531e78.2592000.1452835747.282335-7499115";
            M_API_BaiduSound apiMod = new M_API_BaiduSound();
            apiMod.tok = GetToken();
            apiMod.tex = text;
            apiMod.cuid = "zoomla_api_tosound";
            string data = apiMod.ToParamStr();
            string url = "http://tsn.baidu.com/text2audio";
            HttpWebResponse rep = ProcessRequest(url, data);
            Stream dataStream = rep.GetResponseStream();
            byte[] file = IOHelper.StreamToBytes(dataStream);
            string fpath = SafeSC.SaveFile(vpath, md5 + ".mp3", file);
            dataStream.Close();
            rep.Close();
            return fpath;
        }
        private static HttpWebResponse ProcessRequest(string url, string data, string Method = "POST")
        {
            //url = "https://api2.tradologic.net/v2/users/login";
            HttpWebRequest client = HttpWebRequest.Create(url) as HttpWebRequest;
            client.Accept = "*/*";
            //client.Headers.Add("Accept-Encoding", "gzip, deflate");
            client.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6,ja;q=0.4");
            //client.Headers.Add("Authorization", "oauth oauth_token=" + token);
            client.Headers.Add("Cache-Control", "no-cache");
            client.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            client.Headers.Add("Origin", "http://demo.z01.com");
            client.Headers.Add("Pragma", "no-cache");
            client.Referer = "http://demo.z01.com/test/test2.aspx";
            client.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.99 Safari/537.36 2345chrome v3.0.0.8180";
            //client.Host = "demo.z01.com";

            //CookieContainer cookie = new CookieContainer();
            //cookie.Add(new Cookie("ASP.NET_SessionId", "hlup5bm0wlcmhojvyctyn4hw", "/", "demo.z01.com"));
            //client.CookieContainer = cookie;
            //client.CookieContainer.Add(client.RequestUri, new Cookie("ASP.NET_SessionId", "hlup5bm0wlcmhojvyctyn4hw"));
            client.Headers.Add("X-Requested-With", "XMLHttpRequest");

            client.Method = Method;
            //client.Headers["X-Forwarded-For"] = "115.148.181.206";
            //写入Json传递
            //string data = "username=whatclass8%40163.com&password=123123aa&method=post&dataType=json&format=json";
            if (!Method.ToUpper().Equals("GET") && !string.IsNullOrEmpty(data))
            {
                //if (!string.IsNullOrEmpty(data)) { data += "&method=post&dataType=json&format=json"; }
                byte[] bs = Encoding.UTF8.GetBytes(data);
                client.ContentLength = bs.Length;//附带的数据内容的长度
                {
                    Stream dataStream = client.GetRequestStream();
                    dataStream.Write(bs, 0, bs.Length);
                    dataStream.Close();
                }
            }
            HttpWebResponse response = (HttpWebResponse)client.GetResponse();
            return response;
        }
        //需扩展,增加过期检测
        private static string GetToken()
        {
            if (string.IsNullOrEmpty(TokenInfo)) { TokenInfo = GetTokenFromServer(); }
            if (string.IsNullOrEmpty(Token))
            {
                JObject jobj = JsonConvert.DeserializeObject<JObject>(TokenInfo);
                Token = jobj["access_token"].ToString();
            }
            return Token;
        }
        private static string GetTokenFromServer()
        {
            string api = "agxQBTyg86TIcwSwc3pMQTIW";
            string secret = "e5827a3628a81bd2c1432a38f5f508d9";
            string s = APIHelper.UploadStr("https://openapi.baidu.com/oauth/2.0/token", "grant_type=client_credentials&client_id=" + api + "&client_secret=" + secret);
            return s;
        }
    }
    public class M_API_BaiduSound
    {
        public string tex = "";
        public string lan = "zh";
        public string tok = "";
        public string cuid = "";//服务端MAC 固定
        public int ctp = 1;
        public int spd = 5;
        public int vol = 5;
        public int per = 0;
        public string ToParamStr()
        {
            string str = "tex=" + HttpUtility.UrlEncode(tex) + "&lan=" + lan + "&tok=" + tok + "&cuid=" + cuid;
            str += "&ctp=" + ctp + "&spd=" + spd + "&vol=" + vol + "&per=" + per;
            return str;
        }
    }
}