using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZoomLa.Components;

/*
 * 博客,空间,QQ等操作,微博|QQ
 */ 
namespace ZoomLa.BLL.Helper
{
    public class QQHelper
    {
        //返回json可直接访问xml需要转换到下一级访问qqinfo["nick"],qqinfo["data"]["nick"]
        private string UserAuthUrl = "https://graph.qq.com/user/";
        private string BlogAuthUrl = "https://graph.qq.com/t/";
        //private string QZoneAuthUrl = "";
        public string Token{get;set;}
        public string OpenID{get;set;}
        public QQHelper(string token, string openid)
        {
            Token = token;
            OpenID = openid;
        }
        /// <summary>
        /// 发布一条博客
        /// </summary>
        public string AddBlog(string msg)
        {
            string authUrl = BlogAuthUrl + "add_t";
            string data = "access_token=" + Token + "&oauth_consumer_key="+PlatConfig.QQKey+"&openid=" + OpenID + "&format=json&content=" + msg;
            return APIHelper.UploadStr(authUrl, data);
        }
        public string AddBlog(string msg, string imgurl)
        {
            string authUrl = BlogAuthUrl + "add_pic_t";
            HttpHelper http = new HttpHelper();
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("access_token", Token);
            param.Add("oauth_consumer_key",PlatConfig.QQKey);
            param.Add("openid",OpenID);
            param.Add("content",msg);
            if (!string.IsNullOrEmpty(imgurl))
            {
                FileParameter file = new FileParameter(GetFileByte(imgurl), Path.GetFileName(imgurl), GetImgContentType(imgurl));
                param.Add("pic", file);
            }
            HttpResult result = http.UploadParam(authUrl, param);
            return result.Html;
        }
        //获取图片Content-Type
        public string GetImgContentType(string imgurl)
        {
            string ExName = Path.GetExtension(imgurl);
            switch (ExName)
            {
                case "png":
                    return "image/png";
                case "gif":
                    return "image/gif";
                case "bmp":
                    return "image/bmp";
                case "ico":
                    return "image/ico";
                default:
                    return "image/jpeg";
            }
        }
        private byte[] GetFileByte(string url)
        {
            FileStream fs = new FileStream(ZoomLa.Common.function.VToP(url), FileMode.Open);
            byte[] imagebyte = new byte[fs.Length];
            fs.Read(imagebyte, 0, imagebyte.Length);
            fs.Close();
            return imagebyte;
        }
        /// <summary>
        /// 获取QQ用户信息
        /// </summary>
        public JObject GetUserInfo()
        {
            string result = "";
            try
            {
                string authUrl = UserAuthUrl + "get_info";
                string data = "access_token=" + Token + "&oauth_consumer_key=" + PlatConfig.QQKey + "&openid=" + OpenID + "&format=json";
                result = APIHelper.UploadStr(authUrl, data);
                return (JObject)JsonConvert.DeserializeObject(result);
            }
            catch (Exception ex) { throw new Exception(ex.Message + "," + result); }
        }
        public JObject GetInfoFromQQ() 
        {
            string authUrl = UserAuthUrl + "get_user_info";
            string data = "access_token=" + Token + "&oauth_consumer_key=" + PlatConfig.QQKey + "&openid=" + OpenID + "&format=json";
            string result = APIHelper.UploadStr(authUrl, data);
            return (JObject)JsonConvert.DeserializeObject(result);
        }
        /// <summary>
        /// Token是否过期
        /// </summary>
        /// <returns>True有效</returns>
        public Boolean TokenIsValid()
        { 
            JObject obj= GetUserInfo();
            return (!(obj == null || obj["data"] == null));
        }
    }
}
