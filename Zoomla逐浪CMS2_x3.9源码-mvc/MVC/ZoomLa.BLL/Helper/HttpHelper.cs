using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using ZoomLa.Common;
/*
 *Http文传上传,下载等代码
 *扩展:上传文件能接收返回信息
 *备注:手机上也支持上传下载,详见WP8版
 */ 
namespace ZoomLa.BLL.Helper
{
    public class HttpConfig
    {
        public bool IsAsync=false;
        public string fname = "media";//很多API会有指定文件名规范
    }
    public class HttpHelper
    {
       public  HttpConfig config=new HttpConfig();
        /// <summary>
        /// 以Html形式发送给服务端,服务端以Request.Files[0]接收,文件名TeamFile
        /// </summary>
        /// <param name="argStream">需上传的文件流</param>
        /// <param name="url">目标Url</param>
        /// <param name="fname">上传文件名</param>
        /// <returns>同步模式下会返回信息,异步下用callback</returns>
       public HttpResult UploadFile(string url, Stream argStream, string fname, HttpMultipartFormRequest.ResponseCallback callback = null)
       {
           byte[] bytes = new byte[argStream.Length];
           argStream.Seek(0, SeekOrigin.Begin);
           while (argStream.Read(bytes, 0, bytes.Length) > 0) ;//全部读完
           Dictionary<string, object> postParameters = new Dictionary<string, object>();
           string contentType = "multipart/form-data";//"image/jpeg";//"multipart/form-data";//"application/octetstream";//"image/jpeg";//"multipart/form-data";  
           FileParameter param = new FileParameter(bytes, fname, contentType);
           postParameters.Add(config.fname, param);
           HttpMultipartFormRequest req = new HttpMultipartFormRequest();
           req.config = config;
           HttpResult result = req.AsyncHttpRequest(url, postParameters, callback);
           argStream.Dispose();
           return result;
       }
       public HttpResult UploadParam(string url, Dictionary<string, string> postParameters)
       {
           return UploadParam(url, postParameters);
       }
        //Html必须编码后再传输,html默认的是将其UrlEncode后传递,服务端再编辑,直接传是不能的
       public HttpResult UploadParam(string url, Dictionary<string, object> postParameters)
       {
           HttpMultipartFormRequest req = new HttpMultipartFormRequest();
           req.config = config;
           HttpResult result = new HttpResult();
           result = req.AsyncHttpRequest(url, postParameters, null);
           return result;
       }
       /// <summary>
       /// 从指定服务器上下载文件,支持断点续传
       /// </summary>
       /// <param name="url">目标Url</param>
       /// <param name="vpath">本地虚拟路径</param>
       /// <param name="begin">开始位置,默认为0</param>
       public void DownFile(string url, string vpath, int begin = 0)
       {
           vpath = SafeSC.PathDeal(vpath);
           if (SafeSC.FileNameCheck(vpath)) { throw new Exception("不支持下载[" + Path.GetFileName(vpath) + "]文件"); }
           string ppath = function.VToP(vpath);
           
           //已完成的,1%长度
           int CompletedLength = 0;
           long percent = 0; string temp = "0";
           long sPosstion = 0;//磁盘现盘文件的长度
           //long count = 0;// count += sPosstion,从指定位置开始写入字节
           FileStream FStream;
           if (File.Exists(ppath))
           {
               FStream = File.OpenWrite(ppath);//打开继续写入,并从尾部开始,用于断点续传(如果不需要,则应该删除其)
               sPosstion = FStream.Length;
               FStream.Seek(sPosstion, SeekOrigin.Current);//移动文件流中的当前指针
           }
           else
           {
               string dir = Path.GetDirectoryName(ppath);
               if (!Directory.Exists(dir))
               {
                   Directory.CreateDirectory(dir);
               }
               FStream = new FileStream(ppath, FileMode.Create);
               sPosstion = 0;
           }
           //打开网络连接
           try
           {
               HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);
               //if (CompletedLength > 0)
               //    myRequest.AddRange((int)CompletedLength);//设置Range值,即头，从指定位置开始接收文件..
               //向服务器请求，获得服务器的回应数据流
               HttpWebResponse webResponse = (HttpWebResponse)myRequest.GetResponse();
               long FileLength = webResponse.ContentLength;//文件大小
               percent = FileLength / 100;
               Stream myStream = webResponse.GetResponseStream();
               byte[] btContent = new byte[1024];
               //if (count <= 0) count += sPosstion;//
               //开始写入
               int count = 0;
               while ((count = myStream.Read(btContent, 0, 1024)) > 0)//返回读了多少字节,为0表示全部读完
               {
                   FStream.Write(btContent, 0, count);//知道有多少个数字节后再写入
                   CompletedLength += count;
                   if (!(CompletedLength / percent).ToString().Equals(temp))
                   {
                       temp = (CompletedLength / percent).ToString();
                       //progStatus = temp;
                   }
               }
               myStream.Close();
           }
           finally
           {
               FStream.Close();
           }
       }
       /// <summary>
       /// 主用于采集,处理成正确的路径方便采集
       /// </summary>
       /// <param name="baseurl">当前网址路径</param>
       /// <param name="url">路径名,如为带http的则不处理</param>
       /// <param name="vpath">本地保存的路径</param>
       /// <returns>虚拟路径</rereturns>
       public string DownFile(string cururl, string url, string vpath)
       {
           string baseurl = StrHelper.GetUrlRoot(cururl).TrimEnd('/') + "/";
           string urlpath = cururl.Substring(0, cururl.LastIndexOf("/")).TrimEnd('/') + "/";
           string strurl = url.ToLower();//不更改原大小写
           if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(vpath)) return url;
           if (strurl.Contains("file:")) return url;//如果对方网站弄错路径,则不下载
           if (strurl.StartsWith("http:") || strurl.StartsWith("https:"))//不需做任何处理
           {

           }
           else if (url.StartsWith("/"))//   /image/123.jpg,根目录
           {
               url = (baseurl + url.TrimStart('/'));
           }
           else if (url.StartsWith("../"))//上一级目录,暂只处理一级,多级使用for循环
           {
               string parent = (urlpath.TrimEnd('/'));
               parent = parent.Substring(0, parent.LastIndexOf("/")) + "/";
               url = parent + url.Replace("../", "");
           }
           else if (url.StartsWith("./")) //当前目录
           {
               url = urlpath + url.Replace("./", "");
           }
           else //同于./   image/123.jpg
           {
               url = url.Replace("../", "").Replace("./", "");
               url = (baseurl + url.TrimStart('/'));
           }
           DownFile(url, vpath);
           return vpath;
       }
    }
    // 文件类型数据的内容参数
    public class FileParameter
    {
        // 文件内容
        public byte[] File { get; set; }
        // 文件名
        public string FileName { get; set; }
        // 文件内容类型
        public string ContentType { get; set; }

        public FileParameter(byte[] file) : this(file, null) { }

        public FileParameter(byte[] file, string filename) : this(file, filename, null) { }

        public FileParameter(byte[] file, string filename, string contentType)
        {
            File = file;
            FileName = filename;
            ContentType = contentType;
        }
    }
    /// <summary>
    /// 文件上传类(数据与文件http请求)
    /// </summary>
    public class HttpMultipartFormRequest
    {
        private readonly Encoding DefaultEncoding = Encoding.UTF8;
        private ResponseCallback m_Callback;
        private byte[] m_FormData;
        public HttpConfig config=new HttpConfig();
        public HttpMultipartFormRequest()
        {
        }
        public delegate void ResponseCallback(string msg);
        /// <summary>
        /// 传多个文件
        /// </summary>
        /// <param name="postUri">请求的URL</param>
        /// <param name="postParameters">[filename,FileParameter]</param>
        /// <param name="callback">回掉函数</param>
        public HttpResult AsyncHttpRequest(string postUri, Dictionary<string, object> postParameters, ResponseCallback callback)
        {
            HttpResult result = new HttpResult();
            // 随机序列，用作防止服务器无法识别数据的起始位置
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            // 设置contentType
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;
            // 将数据转换为byte[]格式
            m_FormData = GetMultipartFormData(postParameters, formDataBoundary);
            // 回调函数
            m_Callback = callback;
            // 创建http对象
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(postUri));
            // 设为post请求
            request.Method = "POST";
            request.ContentType = contentType;
            // 请求写入数据流
            if (config.IsAsync)
            {
                request.BeginGetRequestStream(GetRequestStreamCallback, request);
                result.Html = "Async";
                return result;
            }
            else//不开启异步
            {
                var postStream = request.GetRequestStream();
                postStream.Write(m_FormData, 0, m_FormData.Length);
                postStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream());
                result.Html = sr.ReadToEnd();
                result.StatusCode = response.StatusCode;
                result.StatusDescription = response.StatusDescription;
                result.ResponseUri = response.ResponseUri.AbsoluteUri;
                response.Close();
                return result;
            }
        }
        private void GetRequestStreamCallback(IAsyncResult ar)
        {
            HttpWebRequest request = ar.AsyncState as HttpWebRequest;
            using (var postStream = request.EndGetRequestStream(ar))
            {
                postStream.Write(m_FormData, 0, m_FormData.Length);
                postStream.Close();
            }
            request.BeginGetResponse(GetResponseCallback, request);
        }
        private void GetResponseCallback(IAsyncResult ar)
        {
            // 处理Post请求返回的消息
            //try
            //{
            HttpWebRequest request = ar.AsyncState as HttpWebRequest;
            HttpWebResponse response = request.EndGetResponse(ar) as HttpWebResponse;

            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string msg = reader.ReadToEnd();

                if (m_Callback != null)
                {
                    m_Callback(msg);
                }
            }
            //}
            //catch (Exception e)
            //{
            //    string a = e.ToString();
            //    if (m_Callback != null)
            //    {
            //        m_Callback(string.Empty);
            //    }
            //}
        }
        private byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                if (needsCLRF)
                {
                    formDataStream.Write(DefaultEncoding.GetBytes("\r\n"), 0, DefaultEncoding.GetByteCount("\r\n"));
                }
                needsCLRF = true;
                if (param.Value is FileParameter)
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key, // param.Key, //此处如果是请求的php，则需要约定好 存取一致 php:$_FILES['img']['name']
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    // 将与文件相关的header数据写到stream中
                    formDataStream.Write(DefaultEncoding.GetBytes(header), 0, DefaultEncoding.GetByteCount(header));
                    // 将文件数据直接写到stream中
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(DefaultEncoding.GetBytes(postData), 0, DefaultEncoding.GetByteCount(postData));
                }
            }

            string tailEnd = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(DefaultEncoding.GetBytes(tailEnd), 0, DefaultEncoding.GetByteCount(tailEnd));

            // 将Stream数据转换为byte[]格式
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }
    }
    /// <summary>
    /// Http返回参数类
    /// </summary>
    public class HttpResult
    {
        /// <summary>
        /// Http请求返回的Cookie
        /// </summary>
        public string Cookie { get; set; }
        /// <summary>
        /// Cookie对象集合
        /// </summary>
        public CookieCollection CookieCollection { get; set; }
        private string _html = string.Empty;
        /// <summary>
        /// 返回的String类型数据 只有ResultType.String时才返回数据，其它情况为空
        /// </summary>
        public string Html
        {
            get { return _html; }
            set { _html = value; }
        }
        /// <summary>
        /// 返回的Byte数组 只有ResultType.Byte时才返回数据，其它情况为空
        /// </summary>
        public byte[] ResultByte { get; set; }
        /// <summary>
        /// header对象
        /// </summary>
        public WebHeaderCollection Header { get; set; }
        /// <summary>
        /// 返回状态说明
        /// </summary>
        public string StatusDescription { get; set; }
        /// <summary>
        /// 返回状态码,默认为OK
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// 最后访问的URl
        /// </summary>
        public string ResponseUri { get; set; }
    }
}
