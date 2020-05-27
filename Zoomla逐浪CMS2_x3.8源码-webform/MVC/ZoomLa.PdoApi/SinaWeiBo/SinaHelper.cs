using NetDimension.OpenAuth.Sina;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;

namespace ZoomLa.PdoApi.SinaWeiBo
{
    public class SinaHelper
    {
        SinaWeiboClient client = null;
        public SinaHelper(string token)
        {
            client = new SinaWeiboClient(PlatConfig.SinaKey, PlatConfig.SinaSecret, PlatConfig.SinaCallBack, token);
        }
        /// <summary>
        /// 发送一条带图片的微博,未带图片路径,则发送文字微博
        /// </summary>
        /// <param name="status">信息</param>
        /// <param name="img">图片虚拟路径</param>
        public string PostStatus(string status, string imgpath = "")
        {
            var imgFile = string.IsNullOrEmpty(imgpath) ? null : new FileInfo(function.VToP(imgpath));
            string result = "";
            HttpResponseMessage response = null;
            if (!client.IsAuthorized) { return "PostStatus:新浪未授权,无法发送消息"; }
            if (imgFile != null && imgFile.Exists)
            {
                // 调用发图片微博api
                // 参考：http://open.weibo.com/wiki/2/statuses/upload
                response = client.HttpPost("statuses/upload.json", new
                {
                    status = status,
                    pic = imgFile //imgFile: 对于文件上传，这里可以直接传FileInfo对象
                });
            }
            else
            {
                // 调用发微博api
                // 参考：http://open.weibo.com/wiki/2/statuses/update
                response = client.HttpPost("statuses/update.json", new
                {
                    status = status
                });
            }
            //400为Token不正确
            if (response.IsSuccessStatusCode) { result = "发送成功," + response.Content.ReadAsStringAsync().Result; }
            else { result = "发送失败," + response.ToString(); }
            return result;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public JObject GetUserState(string sinaUid)
        {
            var json = new JObject();
            json["authorized"] = false;
            if (!client.IsAuthorized) { return json; }
            // 调用获取获取用户信息api
            // 参考：http://open.weibo.com/wiki/2/users/show
            var response = client.HttpGet("users/show.json", new { uid = sinaUid });
            int statusCode=(int)response.StatusCode;
            if (statusCode == 400 || statusCode==401||statusCode==403)
            {
                return json;
            }
            if (response.IsSuccessStatusCode)
            {
                json["authorized"] = true;
                json["data"] = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                json["data"] = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                json["authorized"] = true;
            }
            return json;
        }
        public string GetUid() 
        {
            // 调用OAuth授权之后，获取授权用户的UID api
            // 参考：http://open.weibo.com/wiki/2/account/get_uid
            if (!client.IsAuthorized) { return ""; }
            var response = client.HttpGet("account/get_uid.json");
            string result = response.Content.ReadAsStringAsync().Result;
            string uid = "";
            if (result.Contains("uid"))
            {
                JObject obj = JsonConvert.DeserializeObject<JObject>(result);
                uid = obj["uid"].ToString() ;
            }
            return uid; 
        }
        /// <summary>
        /// 根据code获取openid
        /// </summary>
        public string GetUidByCode(string code)
        {
            client.GetAccessTokenByCode(code);
            return client.UID;
        }
        /// <summary>
        /// 获取最新微博
        /// </summary>
        /// <returns></returns>
        public string GetPublicTimeline()
        {
            if (!client.IsAuthorized) { return ""; }
            // 调用获取当前登录用户及其所关注用户的最新微博api
            // 参考：http://open.weibo.com/wiki/2/statuses/friends_timeline
            var response = client.HttpGet("statuses/friends_timeline.json");
            return response.Content.ReadAsStringAsync().Result;
        }
        //-----------------------------------------Tools
        /// <summary>
        /// 返回获取code的登录链接
        /// </summary>
        public string GetAuthUrl()
        {
            return client.GetAuthorizationUrl();
        }
        public bool CheckToken() 
        {
            if (!client.IsAuthorized) { return false; }
            var response = client.HttpGet("account/rate_limit_status.json");
            //throw new Exception(response.ToString()+":"+response.Content.ReadAsStringAsync().Result);
            return response.IsSuccessStatusCode;
        }

        public string GetTokenByCode(string code)
        {
            client.GetAccessTokenByCode(code);
            return client.AccessToken;
        }
    }
}
