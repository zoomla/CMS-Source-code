namespace ZoomLa.Web
{
    using System;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.UI.WebControls;
    using ZoomLa.Common;

    /// <summary>
    /// 页面公共方法
    /// </summary>
    internal class Utility
    {
        private Utility()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 访问路径是否正确
        /// <param name="accessingurl">访问路径URL</param>
        /// <param name="path">路径开头</param>
        /// </summary>
        public static bool AccessingPath(string accessingurl, string path)
        {
            bool flag = accessingurl.StartsWith(path, StringComparison.CurrentCultureIgnoreCase);
            bool flag2 = accessingurl.EndsWith("aspx", StringComparison.CurrentCultureIgnoreCase);
            bool flag3 = accessingurl.EndsWith("/");
            return (flag && (flag2 || flag3));
        }        
        /// <summary>
        /// 生成完整的URL
        /// </summary>
        public static string CombineRawUrl(string url)
        {
            if ((url[0] != '~') && (url.IndexOf(':') < 0))
            {
                string rawUrl = HttpContext.Current.Request.RawUrl;
                if (rawUrl.IndexOf('?') > 0)
                {
                    rawUrl = rawUrl.Split(new char[] { '?' })[0];
                }
                if (url.IndexOf('?') > 0)
                {
                    string[] strArray = url.Split(new char[] { '?' });
                    url = VirtualPathUtility.Combine(rawUrl, strArray[0]) + "?" + strArray[1];
                }
                else
                {
                    url = VirtualPathUtility.Combine(rawUrl, url);
                }
            }
            return url;
        }
        /// <summary>
        /// 获取文件执行目录 将"/" 追加到虚拟路径后面(如果路径结尾尚不存在"/")
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetBasePath(HttpRequest request)
        {
            if (request == null)
            {
                return "/";
            }
            return VirtualPathUtility.AppendTrailingSlash(request.ApplicationPath);
        }
        /// <summary>
        /// 生成分页链接
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string RebuildPageName(string filename, NameValueCollection query)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return string.Empty;
            }
            string[] strArray = filename.Split(new char[] { '/' });
            if (strArray.Length > 0)
            {
                filename = strArray[strArray.Length - 1];
            }
            if (filename.IndexOf('?') > 0)
            {
                filename = filename.Substring(0, filename.IndexOf('?') - 1);
            }
            StringBuilder builder = new StringBuilder(filename);
            if (query.Count > 0)
            {
                bool flag = false;
                for (int i = 0; i < query.Count; i++)
                {
                    if (i == 0)
                    {
                        builder.Append("?");
                    }
                    else
                    {
                        builder.Append("&");
                    }
                    if (query.GetKey(i) == "page")
                    {
                        builder.Append("page={$pageid/}");
                        flag = true;
                    }
                    else
                    {
                        builder.Append(query.GetKey(i) + "=" + DataSecurity.FilterBadChar(query.Get(i)));
                    }
                }
                if (!flag)
                {
                    if (builder.Length > filename.Length)
                    {
                        builder.Append("&page={$pageid/}");
                    }
                    else
                    {
                        builder.Append("?page={$pageid/}");
                    }
                }
            }
            else
            {
                builder.Append("?page={$pageid/}");
            }
            return builder.ToString();
        }
        /// <summary>
        /// 将请求参数转换成Int32 如果为空或不能转换则赋值默认值
        /// </summary>
        /// <param name="queryItem">URL的请求参数</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>int整形</returns>
        public static int RequestInt32(string queryItem, int defaultValue)
        {
            return DataConverter.CLng(HttpContext.Current.Request.QueryString[queryItem], defaultValue);
        }
        /// <summary>
        /// 获取request的查询参数，如果该参数值为空，则返回默认值
        /// </summary>
        /// <param name="queryItem">QueryString参数</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>string字符串</returns>
        public static string RequestString(string queryItem, string defaultValue)
        {
            string str = HttpContext.Current.Request.QueryString[queryItem];
            if (str == null)
            {
                return defaultValue;
            }
            return str.Trim();
        }
        /// <summary>
        /// 获取request的查询参数的小写值，如果该参数值为空，则返回默认值的小写值
        /// </summary>
        /// <param name="queryItem"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string RequestStringToLower(string queryItem, string defaultValue)
        {
            string str = HttpContext.Current.Request.QueryString[queryItem];
            if (str == null)
            {
                return defaultValue.ToLower(CultureInfo.CurrentCulture);
            }
            return str.Trim().ToLower(CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// 客户端提示文件未找到
        /// </summary>
        public static void ResponseFileNotFound()
        {
            HttpContext.Current.Server.Transfer("NonexistentPage.aspx");
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 客户端重定向
        /// </summary>
        /// <param name="redirecturl">重定向URL</param>
        /// <param name="endResponse">Response是否End</param>
        public static void ResponseRedirect(string redirecturl, bool endResponse)
        {
            redirecturl = CombineRawUrl(redirecturl);
            HttpContext.Current.Response.Redirect(redirecturl, endResponse);
        }
        /// <summary>
        /// 让列表控件选中某选项
        /// </summary>
        /// <param name="listControl">列表控件</param>
        /// <param name="selectValue">要选中的选项值</param>
        public static void SetSelectedIndexByValue(ListControl listControl, string selectValue)
        {
            if (listControl != null)
            {
                listControl.SelectedIndex = listControl.Items.IndexOf(listControl.Items.FindByValue(selectValue));
            }
        }
        /// <summary>
        /// 重定向输出错误信息给用户
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        /// <param name="returnurl">界面返回链接</param>
        public static void WriteErrMsg(string errorMessage, string returnurl)
        {
            HttpContext.Current.Items["ErrorMessage"] = errorMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Manage/Prompt/ShowError.aspx");
        }
        /// <summary>
        /// 重定向输出信息
        /// </summary>
        /// <param name="message">输出的信息</param>
        /// <param name="returnurl">返回链接</param>
        /// <param name="messageTitle">信息标题</param>
        public static void WriteMessage(string message, string returnurl, string messageTitle)
        {
            HttpContext.Current.Items["Message"] = message;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Items["MessageTitle"] = messageTitle;
            HttpContext.Current.Server.Transfer("~/Manage/Prompt/ShowMessage.aspx");
        }
        /// <summary>
        /// 重定向输出成功信息
        /// </summary>
        /// <param name="successMessage">成功信息</param>
        /// <param name="returnurl">返回链接</param>
        public static void WriteSuccessMsg(string successMessage, string returnurl)
        {
            HttpContext.Current.Items["SuccessMessage"] = successMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Manage/Prompt/ShowSuccess.aspx");
        }
        /// <summary>
        /// 输出用户错误信息
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        /// <param name="returnurl">返回链接</param>
        public static void WriteUserErrMsg(string errorMessage, string returnurl)
        {
            HttpContext.Current.Items["ErrorMessage"] = errorMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowError.aspx");
        }
        /// <summary>
        /// 输出给用户成功信息
        /// </summary>
        /// <param name="successMessage">成功信息</param>
        /// <param name="returnurl">返回链接</param>        
        public static void WriteUserSuccessMsg(string successMessage, string returnurl)
        {
            HttpContext.Current.Items["SuccessMessage"] = successMessage;
            HttpContext.Current.Items["ReturnUrl"] = returnurl;
            HttpContext.Current.Server.Transfer("~/Prompt/ShowSuccess.aspx");
        }
    }
}
