using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using ZoomLa.BLL.Third;
using ZoomLa.Model.Third;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Net;
using System.Security.Cryptography;

namespace ZoomLa.PdoApi.TencentMVS
{
    public enum FolderPattern { File = 0, Folder, Both };
    public enum HttpMethod { Get, Post };
    //腾讯微视频
    public class C_TencentMVS
    {
        const string VIDEOAPI_CGI_URL = "http://web.video.myqcloud.com/files/v1/";
        private int appId;
        private string secretId;
        private string secretKey;
        private int timeOut;
        
        /// <summary>
        /// VideoCloud 构造方法
        /// </summary>
        /// <param name="timeOut">网络超时,默认60秒</param>
        public C_TencentMVS(int timeOut = 60)
        {
            this.timeOut = timeOut * 1000;
            M_Third_PlatInfo infoMod = B_Third_PlatInfo.SelByFlag("微视频");
            if (infoMod == null) { function.WriteErrMsg("未设置微视频信息,请先<a href='/" + SiteConfig.SiteOption.ManageDir + "/Config/PlatInfoList.aspx'>完成配置</a>"); }
            appId = DataConverter.CLng(infoMod.APPID);
            secretId = infoMod.APPSecret;
            secretKey = infoMod.APPKey;

            //---发出请求测试是否key、id等是否正确
            string result = GetFolderStat("", "");
            JObject obj = JsonConvert.DeserializeObject<JObject>(result);
            if(!obj["code"].ToString().Equals("0"))
            {
                function.WriteErrMsg("微视频配置信息不正确,请先<a href='/" + SiteConfig.SiteOption.ManageDir + "/Config/PlatInfoList.aspx'>完成配置</a>");
            }
        }

        /// <summary>
        /// 远程路径Encode处理
        /// </summary>
        /// <param name="remotePath"></param>
        /// <returns></returns>
        private string EncodeRemotePath(string remotePath)
        {
            if (remotePath == "/")
            {
                return remotePath;
            }
            var endWith = remotePath.EndsWith("/");
            String[] part = remotePath.Split('/');
            remotePath = "";
            foreach (var s in part)
            {
                if (s != "")
                {
                    if (remotePath != "")
                    {
                        remotePath += "/";
                    }
                    remotePath += HttpUtility.UrlEncode(s);
                }
            }
            remotePath = (remotePath.StartsWith("/") ? "" : "/") + remotePath + (endWith ? "/" : "");
            return remotePath;
        }

        /// <summary>
        /// 标准化远程路径
        /// </summary>
        /// <param name="remotePath">要标准化的远程路径</param>
        /// <returns></returns>
        private string StandardizationRemotePath(string remotePath)
        {
            if (!remotePath.StartsWith("/"))
            {
                remotePath = "/" + remotePath;
            }
            if (!remotePath.EndsWith("/"))
            {
                remotePath += "/";
            }
            return remotePath;
        }

        /// <summary>
        /// 更新文件夹信息
        /// </summary>
        /// <param name="bucketName"> bucket名称</param>
        /// <param name="remotePath">远程文件夹路径</param>
        /// <param name="bizAttribute">更新信息</param>
        /// <returns></returns>
        public string UpdateFolder(string bucketName, string remotePath, string bizAttribute)
        {
            remotePath = StandardizationRemotePath(remotePath);
            return UpdateFile(bucketName, remotePath, bizAttribute, null, null);
        }

        /// <summary>
        /// 更新文件信息
        /// </summary>
        /// <param name="bucketName">bucket名称</param>
        /// <param name="remotePath">远程文件路径</param>
        /// <param name="bizAttribute">更新信息</param>
        /// <param name="title">标题</param>
        /// <param name="desc">描述</param>
        /// <returns></returns>
        public string UpdateFile(string bucketName, string remotePath, string bizAttribute, string videoCover = null, string title = null, string desc = null)
        {
            var url = VIDEOAPI_CGI_URL + appId + "/" + bucketName + EncodeRemotePath(remotePath);
            int flag = 0;
            if (title != null && desc != null && bizAttribute != null && videoCover != null)
            {
                flag = 0x0f;
            }
            else
            {
                if (title != null)
                {
                    flag |= 0x02;
                }
                if (desc != null)
                {
                    flag |= 0x04;
                }
                if (bizAttribute != null)
                {
                    flag |= 0x01;
                }
                if (videoCover != null)
                {
                    flag |= 0x08;
                }
            }
            var data = new Dictionary<string, string>();
            data.Add("op", "update");
            data.Add("biz_attr", bizAttribute);
            if (videoCover != null)
            {
                data.Add("video_cover", videoCover);
            }
            if (title != null)
            {
                data.Add("video_title", title);
            }
            if (desc != null)
            {
                data.Add("video_desc", desc);
            }
            data.Add("flag", flag.ToString());

            var sign = SignatureOnce(appId, secretId, secretKey, (remotePath.StartsWith("/") ? "" : "/") + remotePath, bucketName);
            var header = new Dictionary<string, string>();
            header.Add("Authorization", sign);
            header.Add("Content-Type", "application/json");
            return SendRequest(url, data, HttpMethod.Post, header, timeOut);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="bucketName">bucket名称</param>
        /// <param name="remotePath">远程文件夹路径</param>
        /// <returns></returns>
        public string DeleteFolder(string bucketName, string remotePath)
        {
            remotePath = StandardizationRemotePath(remotePath);
            return DeleteFile(bucketName, remotePath);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="bucketName">bucket名称</param>
        /// <param name="remotePath">远程文件路径</param>
        /// <returns></returns>
        public string DeleteFile(string bucketName, string remotePath)
        {
            var url = VIDEOAPI_CGI_URL + appId + "/" + bucketName + EncodeRemotePath(remotePath);
            var data = new Dictionary<string, string>();
            data.Add("op", "delete");
            var sign = SignatureOnce(appId, secretId, secretKey, (remotePath.StartsWith("/") ? "" : "/") + remotePath, bucketName);
            var header = new Dictionary<string, string>();
            header.Add("Authorization", sign);
            header.Add("Content-Type", "application/json");
            return SendRequest(url, data, HttpMethod.Post, header, timeOut);
        }

        /// <summary>
        /// 获取文件夹信息
        /// </summary>
        /// <param name="bucketName">bucket名称</param>
        /// <param name="remotePath">远程文件夹路径</param>
        /// <returns></returns>
        public string GetFolderStat(string bucketName, string remotePath)
        {
            remotePath = StandardizationRemotePath(remotePath);
            return GetFileStat(bucketName, remotePath);
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="bucketName">bucket名称</param>
        /// <param name="remotePath">远程文件路径</param>
        /// <returns></returns>
        public string GetFileStat(string bucketName, string remotePath)
        {
            var url = VIDEOAPI_CGI_URL + appId + "/" + bucketName + EncodeRemotePath(remotePath);
            var data = new Dictionary<string, string>();
            data.Add("op", "stat");
            var expired = GetUnixTime() / 1000 + 60;
            var sign = Signature(appId, secretId, secretKey, expired, bucketName);
            var header = new Dictionary<string, string>();
            header.Add("Authorization", sign);
            return SendRequest(url, data, HttpMethod.Get, header, timeOut);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="bucketName">bucket名称</param>
        /// <param name="remotePath">远程文件夹路径</param>
        /// <param name="bizAttribute">附加信息</param>
        /// <returns></returns>
        public string CreateFolder(string bucketName, string remotePath, string bizAttribute = "")
        {
            remotePath = StandardizationRemotePath(remotePath);
            var url = VIDEOAPI_CGI_URL + appId + "/" + bucketName + EncodeRemotePath(remotePath);
            var data = new Dictionary<string, string>();
            data.Add("op", "create");
            data.Add("biz_attr", bizAttribute);
            var expired = GetUnixTime() / 1000 + 60;
            var sign = Signature(appId, secretId, secretKey, expired, bucketName);
            var header = new Dictionary<string, string>();
            header.Add("Authorization", sign);
            header.Add("Content-Type", "application/json");
            return SendRequest(url, data, HttpMethod.Post, header, timeOut);
        }

        /// <summary>
        /// 目录列表,前缀搜索
        /// </summary>
        /// <param name="bucketName">bucket名称</param>
        /// <param name="remotePath">远程文件夹路径</param>
        /// <param name="num">拉取的总数</param>
        /// <param name="context">透传字段,用于翻页,前端不需理解,需要往前/往后翻页则透传回来</param>
        /// <param name="order">默认正序(=0), 填1为反序</param>
        /// <param name="pattern">拉取模式:只是文件，只是文件夹，全部</param>
        /// <param name="prefix">读取文件/文件夹前缀</param>
        /// <returns></returns>
        public string GetFolderList(string bucketName, string remotePath, int num, string context, int order, FolderPattern pattern, string prefix = "")
        {
            remotePath = StandardizationRemotePath(remotePath);
            var url = VIDEOAPI_CGI_URL + appId + "/" + bucketName + EncodeRemotePath(remotePath) + HttpUtility.UrlEncode(prefix);
            var data = new Dictionary<string, string>();
            data.Add("op", "list");
            data.Add("num", num.ToString());
            data.Add("context", context);
            data.Add("order", order.ToString());
            string[] patternArray = { "eListFileOnly", "eListDirOnly", "eListBoth" };
            data.Add("pattern", patternArray[(int)pattern]);
            var expired = GetUnixTime() / 1000 + 60;
            var sign = Signature(appId, secretId, secretKey, expired, bucketName);
            var header = new Dictionary<string, string>();
            header.Add("Authorization", sign);
            return SendRequest(url, data, HttpMethod.Get, header, timeOut);
        }

        /// <summary>
        /// 单个文件上传
        /// </summary>
        /// <param name="bucketName">bucket名称</param>
        /// <param name="remotePath">远程文件路径</param>
        /// <param name="localPath">本地文件路径</param>
        /// <param name="bizAttribute">附加信息</param>
        /// <param name="title">标题</param>
        /// <param name="desc">描述</param>
        /// <param name="magicContext">透传字段，业务设置回调url的话，会把这个字段通过回调url传给业务</param>
        /// <returns></returns>
        public string UploadFile(string bucketName, string remotePath, string localPath, string videoCover = "", string bizAttribute = "", string title = "", string desc = "", string magicContext = "")
        {
            var url = VIDEOAPI_CGI_URL + appId + "/" + bucketName + EncodeRemotePath(remotePath);
            var sha1 = GetSHA1(localPath);
            var data = new Dictionary<string, string>();
            data.Add("op", "upload");
            data.Add("sha", sha1);
            data.Add("video_cover", videoCover);
            data.Add("video_title", title);
            data.Add("video_desc", desc);
            data.Add("magicContext", magicContext);
            data.Add("biz_attr", bizAttribute);
            var expired = GetUnixTime() / 1000 + 60;
            var sign = Signature(appId, secretId, secretKey, expired, bucketName);
            var header = new Dictionary<string, string>();
            header.Add("Authorization", sign);
            return SendRequest(url, data, HttpMethod.Post, header, timeOut, localPath);
        }

        /// <summary>
        /// 分片上传第一步
        /// </summary>
        /// <param name="bucketName">bucket名称</param>
        /// <param name="remotePath">远程文件路径</param>
        /// <param name="localPath">本地文件路径</param>
        /// <param name="videoCover">视频封面Url</param>
        /// <param name="bizAttribute">附加信息</param>
        /// <param name="title">标题</param>
        /// <param name="desc">描述</param>
        /// <param name="magicContext">透传字段，业务设置回调url的话，会把这个字段通过回调url传给业务</param>
        /// <param name="sliceSize">切片大小（字节）</param>
        /// <returns></returns>
        public string SliceUploadFirstStep(string bucketName, string remotePath, string localPath, string videoCover, string bizAttribute, string title,
            string desc, string magicContext, int sliceSize)
        {
            var url = VIDEOAPI_CGI_URL + appId + "/" + bucketName + EncodeRemotePath(remotePath);
            var sha1 = GetSHA1(localPath);
            var fileSize = new FileInfo(localPath).Length;
            var data = new Dictionary<string, string>();
            data.Add("op", "upload_slice");
            data.Add("sha", sha1);
            data.Add("filesize", fileSize.ToString());
            data.Add("slice_size", sliceSize.ToString());
            data.Add("video_cover", videoCover);
            data.Add("video_title", title);
            data.Add("video_desc", desc);
            data.Add("magicContext", magicContext);
            data.Add("biz_attr", bizAttribute);
            var expired = GetUnixTime() / 1000 + 60;
            var sign = Signature(appId, secretId, secretKey, expired, bucketName);
            var header = new Dictionary<string, string>();
            header.Add("Authorization", sign);
            return SendRequest(url, data, HttpMethod.Post, header, timeOut);
        }

        /// <summary>
        /// 分片上传后续步骤
        /// </summary>
        /// <param name="bucketName">bucket名称</param>
        /// <param name="remotePath">远程文件路径</param>
        /// <param name="localPath">本地文件路径</param>
        /// <param name="sessionId">分片上传会话ID</param>
        /// <param name="offset">文件分片偏移量</param>
        /// <param name="sliceSize">切片大小（字节）</param>
        /// <returns></returns>
        public string SliceUploadFollowStep(string bucketName, string remotePath, string localPath,
                string sessionId, int offset, int sliceSize)
        {
            var url = VIDEOAPI_CGI_URL + appId + "/" + bucketName + EncodeRemotePath(remotePath);
            var data = new Dictionary<string, string>();
            data.Add("op", "upload_slice");
            data.Add("session", sessionId);
            data.Add("offset", offset.ToString());
            var expired = GetUnixTime() / 1000 + 60;
            var sign = Signature(appId, secretId, secretKey, expired, bucketName);
            var header = new Dictionary<string, string>();
            header.Add("Authorization", sign);
            return SendRequest(url, data, HttpMethod.Post, header, timeOut, localPath, offset, sliceSize);
        }

        /// <summary>
        /// 分片上传
        /// </summary>
        /// <param name="bucketName">bucket名称</param>
        /// <param name="remotePath">远程文件路径</param>
        /// <param name="localPath">本地文件路径</param>
        /// <param name="videoCover">视频封面Url</param>
        /// <param name="bizAttribute">附加信息</param>
        /// <param name="title">标题</param>
        /// <param name="desc">描述</param>
        /// <param name="magicContext">透传字段，业务设置回调url的话，会把这个字段通过回调url传给业务</param>
        /// <param name="sliceSize">切片大小（字节）</param>
        /// <returns></returns>
        public string SliceUploadFile(string bucketName, string remotePath, string localPath, string videoCover = "", string bizAttribute = "",
            string title = "", string desc = "", string magicContext = "", int sliceSize = 512 * 1024)
        {
            var result = SliceUploadFirstStep(bucketName, remotePath, localPath, videoCover, bizAttribute, title, desc, magicContext, sliceSize);
            var obj = (JObject)JsonConvert.DeserializeObject(result);
            var code = (int)obj["code"];
            if (code != 0)
            {
                return result;
            }
            var data = obj["data"];
            if (data["access_url"] != null)
            {
                var accessUrl = data["access_url"];
                return result;
            }
            else
            {
                var sessionId = data["session"].ToString();
                sliceSize = (int)data["slice_size"];
                var offset = (int)data["offset"];
                var retryCount = 0;
                while (true)
                {
                    result = SliceUploadFollowStep(bucketName, remotePath, localPath, sessionId, offset, sliceSize);
                    obj = (JObject)JsonConvert.DeserializeObject(result);
                    code = (int)obj["code"];
                    if (code != 0)
                    {
                        //当上传失败后会重试3次
                        if (retryCount < 3)
                        {
                            retryCount++;
                        }
                        else
                        {
                            return result;
                        }
                    }
                    else
                    {
                        data = obj["data"];
                        if (data["offset"] != null)
                        {
                            offset = (int)data["offset"] + sliceSize;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// Http请求调用方法
        /// </summary>
        /// <param name="url">请求Url</param>
        /// <param name="data">请求的参数字典</param>
        /// <param name="requestMethod"></param>
        /// <param name="header"></param>
        /// <param name="timeOut"></param>
        /// <param name="localPath"></param>
        /// <param name="offset"></param>
        /// <param name="sliceSize"></param>
        /// <returns></returns>
        private string SendRequest(string url, Dictionary<string, string> data, HttpMethod requestMethod,
            Dictionary<string, string> header, int timeOut, string localPath = null, int offset = -1, int sliceSize = 0)
        {
            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                if (requestMethod == HttpMethod.Get)
                {
                    var paramStr = "";
                    foreach (var key in data.Keys)
                    {
                        paramStr += string.Format("{0}={1}&", key, HttpUtility.UrlEncode(data[key].ToString()));
                    }
                    paramStr = paramStr.TrimEnd('&');
                    url += (url.EndsWith("?") ? "&" : "?") + paramStr;
                }

                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Accept = "*/*";
                request.KeepAlive = true;
                request.UserAgent = "qcloud-dotnet-sdk";
                request.Timeout = timeOut;
                foreach (var key in header.Keys)
                {
                    if (key == "Content-Type")
                    {
                        request.ContentType = header[key];
                    }
                    else
                    {
                        request.Headers.Add(key, header[key]);
                    }
                }
                if (requestMethod == HttpMethod.Post)
                {
                    request.Method = requestMethod.ToString().ToUpper();
                    var memStream = new MemoryStream();
                    if (header.ContainsKey("Content-Type") && header["Content-Type"] == "application/json")
                    {
                        var json = JsonConvert.SerializeObject(data);
                        var jsonByte = Encoding.GetEncoding("utf-8").GetBytes(json.ToString());
                        memStream.Write(jsonByte, 0, jsonByte.Length);
                    }
                    else
                    {
                        var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
                        var beginBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                        var endBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                        request.ContentType = "multipart/form-data; boundary=" + boundary;

                        var strBuf = new StringBuilder();
                        foreach (var key in data.Keys)
                        {
                            strBuf.Append("\r\n--" + boundary + "\r\n");
                            strBuf.Append("Content-Disposition: form-data; name=\"" + key + "\"\r\n\r\n");
                            strBuf.Append(data[key].ToString());
                        }
                        var paramsByte = Encoding.GetEncoding("utf-8").GetBytes(strBuf.ToString());
                        memStream.Write(paramsByte, 0, paramsByte.Length);

                        if (localPath != null)
                        {
                            memStream.Write(beginBoundary, 0, beginBoundary.Length);
                            var fileInfo = new FileInfo(localPath);
                            var fileStream = new FileStream(localPath, FileMode.Open, FileAccess.Read);

                            const string filePartHeader =
                                "Content-Disposition: form-data; name=\"fileContent\"; filename=\"{0}\"\r\n" +
                                "Content-Type: application/octet-stream\r\n\r\n";
                            var headerText = string.Format(filePartHeader, fileInfo.Name);
                            var headerbytes = Encoding.UTF8.GetBytes(headerText);
                            memStream.Write(headerbytes, 0, headerbytes.Length);

                            if (offset == -1)
                            {
                                var buffer = new byte[1024];
                                int bytesRead;
                                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                {
                                    memStream.Write(buffer, 0, bytesRead);
                                }
                            }
                            else
                            {
                                var buffer = new byte[sliceSize];
                                int bytesRead;
                                fileStream.Seek(offset, SeekOrigin.Begin);
                                bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                                memStream.Write(buffer, 0, bytesRead);
                            }
                        }
                        memStream.Write(endBoundary, 0, endBoundary.Length);
                    }
                    request.ContentLength = memStream.Length;
                    var requestStream = request.GetRequestStream();
                    memStream.Position = 0;
                    var tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();

                    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                    requestStream.Close();
                }
                var response = request.GetResponse();
                using (var s = response.GetResponseStream())
                {
                    var reader = new StreamReader(s, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                if (we.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var s = we.Response.GetResponseStream())
                    {
                        var reader = new StreamReader(s, Encoding.UTF8);
                        return reader.ReadToEnd();
                    }
                }
                else
                {
                    throw we;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 获取哈希签名字符串
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string GetSHA1(string filePath)
        {
            var strResult = "";
            var strHashData = "";
            byte[] arrbytHashValue;
            FileStream oFileStream = null;
            SHA1CryptoServiceProvider osha1 = new SHA1CryptoServiceProvider();
            try
            {
                oFileStream = new FileStream(filePath.Replace("\"", ""), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                arrbytHashValue = osha1.ComputeHash(oFileStream); //计算指定Stream 对象的哈希值
                oFileStream.Close();
                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
                strHashData = System.BitConverter.ToString(arrbytHashValue);
                //替换-
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return strResult;
        }

        private string Signature(int appId, string secretId, string secretKey, long expired, string fileId, string bucketName)
        {
            if (secretId == "" || secretKey == "")
            {
                return "-1";
            }
            var now = GetUnixTime() / 1000;
            var rand = new Random();
            var rdm = rand.Next(Int32.MaxValue);
            var plainText = "a=" + appId + "&k=" + secretId + "&e=" + expired + "&t=" + now + "&r=" + rdm + "&f=" + fileId + "&b=" + bucketName;

            using (HMACSHA1 mac = new HMACSHA1(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hash = mac.ComputeHash(Encoding.UTF8.GetBytes(plainText));
                var pText = Encoding.UTF8.GetBytes(plainText);
                var all = new byte[hash.Length + pText.Length];
                Array.Copy(hash, 0, all, 0, hash.Length);
                Array.Copy(pText, 0, all, hash.Length, pText.Length);
                return Convert.ToBase64String(all);
            }
        }

        public string Signature(int appId, string secretId, string secretKey, long expired, string bucketName)
        {
            return Signature(appId, secretId, secretKey, expired, "", bucketName);
        }

        public string SignatureOnce(int appId, string secretId, string secretKey, string remotePath, string bucketName)
        {
            var fileId = "/" + appId + "/" + bucketName + remotePath;
            return Signature(appId, secretId, secretKey, 0, fileId, bucketName);
        }
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <param name="nowTime"></param>
        /// <returns></returns>
        public long GetUnixTime()
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            return (long)Math.Round((DateTime.Now - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }
    }
}
