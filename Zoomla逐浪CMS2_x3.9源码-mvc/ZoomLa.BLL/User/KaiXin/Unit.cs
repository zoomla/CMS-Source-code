using System;
using System.Collections.Generic;

using System.Text;

namespace SDK
{
    /// <summary>
    /// 返回数据
    /// </summary>
    public abstract class KxReturn { }


    /// <summary>
    /// AccessToken
    /// </summary>
    public  class AccessToken : KxReturn
    {
        /// <summary>
        /// access_token值
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string expires_in { get; set; }

        /// <summary>
        /// 更新token
        /// </summary>
        public string refresh_token { get; set; }

        /// <summary>
        /// scope设置
        /// </summary>
        public string scope { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jsonString">json的string，json返回的accesstoken串</param>
        public AccessToken(string jsonString)
        {
            Dictionary<string, object> newobj = (Dictionary<string, object>)fastJSON.JSON.Instance.ToObject(jsonString);

            //parse
            this.access_token = (string)newobj["access_token"];
            this.expires_in = (string)newobj["expires_in"];
            this.refresh_token = (string)newobj["refresh_token"];
            this.scope = (string)newobj["scope"];
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="expires_in"></param>
        /// <param name="refresh_token"></param>
        /// <param name="scope"></param>
        public AccessToken(string access_token, string expires_in, string refresh_token, string scope)
        {
            this.access_token = access_token;
            this.expires_in = expires_in;
            this.refresh_token = refresh_token;
            this.scope = scope;
        }
    }
}
