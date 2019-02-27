namespace ZoomLa.Common
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using ZoomLa.Components;

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
        /// 生成4位随机数(Disuse)
        /// </summary>
        public static string RandomNum()
        {
            return RandomNum(4);
        }
        /// <summary>
        /// 生成指定长度随机数(Disuse)
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
    }
}