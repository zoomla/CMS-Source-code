using System;
using System.Collections.Generic;

using System.Text;

namespace SDK
{
    public class KxApi
    {
        public string Users_Me(string access_token)
        {

            HTTPBase httpManager = new HTTPBase();
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            queryParams.Add("access_token", access_token);
            return httpManager.Get("https://api.kaixin001.com/users/me.json", queryParams);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="access_token"></param>
        /// <param name="pame">信息列表，隔开</param>
        /// <returns></returns>
        public string Userinfo(string key,string access_token,string pame)
        {
            string value = "http://api.kaixin001.com/users/me.json?fields=uid%2Cname%2Cgender%2Cbirthday&oauth_consumer_key=" + key + "&oauth_nonce=2304841ce09fbe32cf7238012967aeec&oauth_signature=Oy876Y1CB1ZOCE%2FDI0HnhIIyhks%3D&oauth_signature_method=HMAC-SHA1&oauth_timestamp=1334300988&oauth_token=" + access_token + "&oauth_version=1.0";
            HTTPBase http = new HTTPBase();
            Dictionary<string, string> Dic = new Dictionary<string, string>();
            Dic.Add("fields", pame);
            Dic.Add("oauth_consumer_key", key);
            Dic.Add("oauth_nonce", "2304841ce09fbe32cf7238012967aeec&oauth_signature=Oy876Y1CB1ZOCE/DI0HnhIIyhks=&oauth_signature_method=HMAC-SHA1");
            Dic.Add("oauth_timestamp", DateTime.Now.ToString("hhssffff"));
            Dic.Add("oauth_token", "140845678_40ddd9c6ab3a9eae962e173eb614d3");
            Dic.Add("oauth_version", "1.0");
            Dic.Add("scope", "basic");

            Dic.Add("access_token", access_token);
            return http.Get("http://api.kaixin001.com/users/me.json", Dic);
        }
    }
}
