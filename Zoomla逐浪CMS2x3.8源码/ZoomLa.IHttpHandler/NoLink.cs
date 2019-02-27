/* 
 *  
 * 防盗链IHttpHandler 
 *  
 *  
 * 增加了对文件关键字的选择(即仅对文件名存在某些关键字或不存在某些关键字进行过滤) 
 * 设置web.config中<appSettings>节以下值 
 * string ZoomLa_NoLink    如果文件名符合该正确表态式将进行过滤(不设置对所有进行过滤) 
 * string ZoomLa_AllowLink            如果文件名符合该正确表态式将不进行过滤(优先权高于AllowLink,不设置则服从AllowLink) 
 * bool ZoomLa_AllowOnlyFile        如果为False,(默认true)则不允许用户直接对该文件进行访问建议为true 
 *  
 *  
 * :)以下设置均可省略,设置只是为了增加灵活性与体验 
 * ZoomLa_NoLink_Message    错误信息提示:默认为Link From:域名 
 * ZoomLa_Error_Width        错误信息提示图片宽 
 * ZoomLa_Error_Height        错误信息提示图片高 
 *  
 */


using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.SessionState;
using ZoomLa.BLL;

namespace ZoomLa
{
    public class NoLink : IHttpHandler, IReadOnlySessionState
    {
        private string ZoomLa_NoLink = string.Empty;
        //private string ZoomLa_AllowLink = string.Empty;
        private bool ZoomLa_AllowOnlyFile = true;
        private string ZoomLa_NoLink_Message = string.Empty;
        private bool error = false;
        
        public NoLink()
        {
            // 
            // TODO: 在此处添加构造函数逻辑 
            // 
        }

        public void ProcessRequest(HttpContext context)
        {

            ZoomLa_NoLink_Message = ConfigurationManager.AppSettings["ZoomLa_NoLink_Message"];

            string myDomain = string.Empty;

            error = errorLink(context, out myDomain);

            if (Empty(ZoomLa_NoLink_Message))
            {
                ZoomLa_NoLink_Message = "错误！请求来源地址 :" + myDomain;
            }

            if (error)
            {
                Jpg(context.Response, ZoomLa_NoLink_Message);
            }
            else
            {
                Real(context.Response, context.Request);
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }


        /// <summary> 
        /// 输出错误信息 
        /// </summary> 
        /// <param name="Response"></param> 
        /// <param name="_word"></param> 
        private void Jpg(HttpResponse Response, string _word)
        {


            int myErrorWidth = _word.Length * 15;
            int myErrorHeight = 22;
            try
            {
                int _myErrorWidth = Convert.ToInt32(ConfigurationManager.AppSettings["ZoomLa_Error_Width"]);
                if (_myErrorWidth > 0)
                {
                    myErrorWidth = _myErrorWidth;
                }

            }
            catch
            {

            }
            try
            {
                int _myErrorHeight = Convert.ToInt32(ConfigurationManager.AppSettings["ZoomLa_Error_Height"]);
                if (_myErrorHeight > 0)
                {
                    myErrorHeight = _myErrorHeight;
                }
            }
            catch
            {

            }
            Bitmap Img = null;
            Graphics g = null;
            MemoryStream ms = null;
            Img = new Bitmap(myErrorWidth, myErrorHeight);
            g = Graphics.FromImage(Img);
            g.Clear(Color.White);
            Font f = new Font("Arial", 9);
            SolidBrush s = new SolidBrush(Color.Red);
            g.DrawString(_word, f, s, 3, 3);
            ms = new MemoryStream();
            Img.Save(ms, ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            Img.Dispose();
            Response.End();
        }

        /// <summary> 
        /// 输出真实文件 
        /// </summary> 
        /// <param name="response"></param> 
        /// <param name="context"></param> 
        private void Real(HttpResponse response, HttpRequest request)
        {
            FileInfo file = new System.IO.FileInfo(request.PhysicalPath);
            response.Clear();
            response.AddHeader("Content-Disposition", "filename=" + file.Name);
            response.AddHeader("Content-Length", file.Length.ToString());
            string fileExtension = file.Extension.ToLower();
            //这里选择输出的文件格式 
            switch (fileExtension)
            {
                case "mp3":
                    response.ContentType = "audio/mpeg3";
                    break;
                case "mpeg":
                    response.ContentType = "video/mpeg";
                    break;
                case "jpg":
                    response.ContentType = "image/jpeg";
                    break;
                case "bmp":
                    response.ContentType = "image/bmp";
                    break;
                case "gif":
                    response.ContentType = "image/gif";
                    break;
                case "doc":
                    response.ContentType = "application/msword";
                    break;
                case "css":
                    response.ContentType = "text/css";
                    break;
                case "wmv":
                    response.ContentType = "video/x-ms-wmv";
                    break;
                case "flv":
                    response.ContentType = "video/x-flv";
                    break;
                default:
                    response.ContentType = "application/octet-stream";
                    break;
            }
            response.WriteFile(file.FullName);
            response.End();
        }


        /// <summary> 
        /// 确认字符串是否为空 
        /// </summary> 
        /// <param name="_value"></param> 
        /// <returns></returns> 
        private bool Empty(string _value)
        {
            if (_value == null | _value == string.Empty | _value == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary> 
        /// 检查是否是非法链接 
        /// </summary> 
        /// <param name="context"></param> 
        /// <param name="_myDomain"></param> 
        /// <returns></returns> 
        private bool errorLink(HttpContext context, out string _myDomain)
        {
            HttpResponse response = context.Response;
            string myDomain = context.Request.ServerVariables["SERVER_NAME"];
            _myDomain = myDomain;
            string myDomainIp = context.Request.UserHostAddress;

            ZoomLa_NoLink = ConfigurationManager.AppSettings["ZoomLa_NoLink"];

            try
            {
                ZoomLa_AllowOnlyFile = Convert.ToBoolean(ConfigurationManager.AppSettings["ZoomLa_AllowOnlyFile"]);
            }
            catch
            {
                ZoomLa_AllowOnlyFile = true;
            }


            B_CreateHtml see = new B_CreateHtml();

 
            if (see.UrlReferrer != "")
            {
                //判定referDomain是否存在网站的IP或域名 
                string referDomain = see.UrlReferrer;
                string myPath = context.Request.RawUrl;

                if (referDomain.IndexOf(myDomainIp) >= 0 | referDomain.IndexOf(myDomain) >= 0)
                {
                    return false;

                }
                else
                {
                    //这里使用正则表达对规则进行匹配 
                    try
                    {
                        Regex myRegex;

                        //检查允许匹配 
                        //if (!Empty(ZoomLa_AllowLink))
                        //{
                        //    myRegex = new Regex(ZoomLa_AllowLink);

                        //    if (myRegex.IsMatch(myPath))
                        //    {
                        //        return false;
                        //    }

                        //}


                        //检查禁止匹配 
                        if (!Empty(ZoomLa_NoLink))
                        {
                            myRegex = new Regex(ZoomLa_NoLink);
                            if (myRegex.IsMatch(myPath))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        }

                        return true;

                    }
                    catch
                    {
                        //如果匹配出错，链接错误 
                        return true;
                    }
                }
            }
            else
            {
                //是否允许直接访问文件 
                if (ZoomLa_AllowOnlyFile)
                {
                    return false;
                }
                else//????判断下载工具
                {
                    return true;// true;
                }
            }

        }

    }

}



