namespace ZoomLa.Common
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// 数据安全类
    /// </summary>
    public class DataSecurity
    {
        /// <summary>
        /// 
        /// </summary>
        public DataSecurity()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 将html代码转换成JS格式
        /// </summary>
        public static string ConvertToJavaScript(string str)
        {
            str = str.Replace(@"\", @"\\");
            str = str.Replace("\n", @"\n");
            str = str.Replace("\r", @"\r");
            str = str.Replace("\"", "\\\"");
            return str;
        }
        /// <summary>
        /// 将字符串中的特殊符号过滤
        /// </summary>
        public static string FilterBadChar(string strchar)
        {
            string str2 = "";
            if (string.IsNullOrEmpty(strchar))
            {
                return "";
            }
            string str = strchar;
            string[] strArray = new string[] { 
                "+", "'", "--", "%", "^", "&", "?", "(", ")", "<", ">", "[", "]", "{", "}", "/", 
                "\"", ";", ":", "Chr(34)", "Chr(0)"
             };
            StringBuilder builder = new StringBuilder(str);
            for (int i = 0; i < strArray.Length; i++)
            {
                str2 = builder.Replace(strArray[i], "").ToString();
            }
            return builder.Replace("@@", "@").ToString();
        }
        /// <summary>
        /// 根据序号取得字符串数组指定序号的字符串
        /// </summary>
        public static string GetArrayValue(int index, string[] field)
        {
            if ((field != null) && ((index >= 0) && (index < field.Length)))
            {
                return field[index];
            }
            return string.Empty;
        }
        /// <summary>
        /// 根据序号取得字符串数据集中指定序号的字符串
        /// </summary>
        public static string GetArrayValue(int index, Collection<string> field)
        {
            if ((index >= 0) && (index < field.Count))
            {
                return field[index];
            }
            return string.Empty;
        }
        /// <summary>
        /// 将HTML代码对象转换成文本格式
        /// </summary>
        public static string HtmlDecode(object o)
        {
            if (o == null)
            {
                return null;
            }
            return HtmlDecode(o.ToString());
        }
        /// <summary>
        /// 将HTML代码转换成文本格式
        /// </summary>
        public static string HtmlDecode(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("<br>", "\n");
                str = str.Replace("&gt;", ">");
                str = str.Replace("&lt;", "<");
                str = str.Replace("&nbsp;", " ");
                str = str.Replace("&#39;", "'");
                str = str.Replace("&quot;", "\"");
            }
            return str;
        }
        /// <summary>
        /// 将数据转换成HTML代码字符串
        /// </summary>
        public static string HtmlEncode(object o)
        {
            if (o == null)
            {
                return null;
            }
            return HtmlEncode(o.ToString());
        }
        /// <summary>
        /// 将文本代码转换成HTML代码字符串
        /// </summary>
        public static string HtmlEncode(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
                str = str.Replace(" ", "&nbsp;");
                str = str.Replace("'", "&#39;");
                str = str.Replace("\"", "&quot;");
                str = str.Replace("\r\n", "<br>");
                str = str.Replace("\n", "<br>");
            }
            return str;
        }
        /// <summary>
        /// 获取文本字符串的长度
        /// </summary>
        public static int Len(string str)
        {
            int num = 0;
            foreach (char ch in str)
            {
                if (ch > '\x007f')
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
            }
            return num;
        }
        /// <summary>
        /// 获取字符串中指定长度的子字符串
        /// </summary>
        /// <param name="str">被操作的字符串</param>
        /// <param name="len">指定的长度</param>
        /// <returns>子字符串</returns>
        public static string GetSubString(string str, int len)
        {
            int num = 0;
            string result = "";
            foreach (char ch in str)
            {
                if (ch > '\x007f')
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                if (num <= len)
                    result += ch.ToString();
                else
                    break;
            }
            return result;
        }
        /// <summary>
        /// 生成文件名 格式20080225+随机字符串
        /// </summary>
        public static string MakeFileRndName()
        {
            return (DateTime.Now.ToString("yyyyMMddHHmmss") + MakeRandomString("0123456789", 4));
        }
        /// <summary>
        /// 生成文件夹名 格式200802
        /// </summary>
        public static string MakeFolderName()
        {
            return DateTime.Now.ToString("yyyyMM");
        }
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        public static string MakeRandomString(int pwdlen)
        {
            return MakeRandomString("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_*", pwdlen);
        }
        /// <summary>
        /// 生成随机字符串，返回值是由参数pwdchars中的字符组成
        /// </summary>
        public static string MakeRandomString(string pwdchars, int pwdlen)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < pwdlen; i++)
            {
                int num = random.Next(pwdchars.Length);
                builder.Append(pwdchars[num]);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 生成4位随机数
        /// </summary>
        public static string RandomNum()
        {
            return RandomNum(4);
        }
        /// <summary>
        /// 生成指定长度随机数
        /// </summary>
        public static string RandomNum(int intlong)
        {
            Random random = new Random();
            StringBuilder builder = new StringBuilder("");
            for (int i = 0; i < intlong; i++)
            {
                builder.Append(random.Next(10));
            }
            return builder.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        public static string RestrictedUrl(Uri url)
        {
            Uri uri;
            if (url == null)
            {
                return null;
            }
            Uri.TryCreate(url.AbsolutePath, UriKind.Absolute, out uri);
            return (RestrictedUrl(uri) + url.Query);
        }
        /// <summary>
        /// 
        /// </summary>
        public static string RngCspNum(int strLength)
        {
            if (strLength > 0)
            {
                strLength--;
            }
            else
            {
                strLength = 5;
            }
            byte[] data = new byte[strLength];
            new RNGCryptoServiceProvider().GetBytes(data);
            return BitConverter.ToInt32(data, 0).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        public static string Strings(string ichar, int i)
        {
            StringBuilder builder = new StringBuilder("");
            for (int j = 0; j < i; j++)
            {
                builder.Append(ichar);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2234:PassSystemUriObjectsInsteadOfStrings"), SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        public static string UnrestrictedUrl(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            if (VirtualPathUtility.IsAppRelative(path))
            {
                path = VirtualPathUtility.ToAbsolute(path);
            }
            int num = 80;
            string host = HttpContext.Current.Request.Url.Host;
            string str2 = (num != 80) ? string.Format(":{0}", num) : "";
            Uri baseUri = new Uri(string.Format("http://{0}{1}", host, str2));
            return new Uri(baseUri, path).ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        public static string UrlEncode(object urlObj)
        {
            if (urlObj == null)
            {
                return null;
            }
            return UrlEncode(urlObj.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings"), SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        public static string UrlEncode(string urlStr)
        {
            if (string.IsNullOrEmpty(urlStr))
            {
                return null;
            }
            return Regex.Replace(urlStr, @"[^a-zA-Z0-9,-_\.]+", new MatchEvaluator(DataSecurity.UrlEncodeMatch));
        }
        /// <summary>
        /// 
        /// </summary>
        private static string UrlEncodeMatch(Match match)
        {
            string str = match.ToString();
            if (str.Length < 1)
            {
                return str;
            }
            StringBuilder builder = new StringBuilder();
            foreach (char ch in str)
            {
                if (ch > '\x007f')
                {
                    builder.AppendFormat("%u{0:X4}", (int)ch);
                }
                else
                {
                    builder.AppendFormat("%{0:X2}", (int)ch);
                }
            }
            return builder.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        public static string XmlEncode(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("&", "&amp;");
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
                str = str.Replace("'", "&apos;");
                str = str.Replace("\"", "&quot;");
            }
            return str;
        }
        /// <summary>
        /// 分析用户请求是否正常
        /// </summary>
        /// <param name="Str">传入用户提交数据</param>
        /// <returns>返回是否含有SQL注入式攻击代码</returns>
        private static bool ProcessSqlStr(string Str)
        {
            bool ReturnValue = true;
            try
            {
                if (Str != "")
                {
                    string SqlStr = "and |exec |insert |select |delete |update |count |chr |mid |master |truncate |char |declare ";
                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.IndexOf(ss) >= 0)
                        {
                            ReturnValue = false;
                        }
                    }
                }
            }
            catch
            {
                ReturnValue = false;
            }
            return ReturnValue;
        }
        /// <summary>
        /// 处理数据
        /// </summary>
        public static void StartProcessRequest()
        {
            try
            {
                string getkeys = "";
                
                if (System.Web.HttpContext.Current.Request.QueryString != null)
                {

                    for (int i = 0; i < System.Web.HttpContext.Current.Request.QueryString.Count; i++)
                    {
                        getkeys = System.Web.HttpContext.Current.Request.QueryString.Keys[i];
                        if (!ProcessSqlStr(System.Web.HttpContext.Current.Request.QueryString[getkeys]))
                        {
                            function.WriteErrMsg("数据不能包含SQL注入代码!");
                            System.Web.HttpContext.Current.Response.End();
                        }
                    }
                }
                if (System.Web.HttpContext.Current.Request.Form != null)
                {
                    for (int i = 0; i < System.Web.HttpContext.Current.Request.Form.Count; i++)
                    {
                        getkeys = System.Web.HttpContext.Current.Request.Form.Keys[i];
                        if (!ProcessSqlStr(System.Web.HttpContext.Current.Request.Form[getkeys]))
                        {
                            function.WriteErrMsg("数据不能包含SQL注入代码!");
                            System.Web.HttpContext.Current.Response.End();
                        }
                    }
                }
            }
            catch
            {
                // 错误处理: 处理用户提交信息!
                function.WriteErrMsg("处理数据时出现异常!");
                System.Web.HttpContext.Current.Response.End();
            }
        }


    }
}