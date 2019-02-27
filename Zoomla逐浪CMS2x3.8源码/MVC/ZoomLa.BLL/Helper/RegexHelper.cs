using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using ZoomLa.SQLDAL;
namespace ZoomLa.BLL
{
    public class RegexHelper
    {
        #region 取值
        /// <summary>
        /// 截取起始与结束之间的内容,查找到第一个后即返回
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="s">起始字符串</param>
        /// <param name="e">结束字符串</param>
        /// <param name="flag">true:包含起始与结束字符,false：不包含</param>
        /// <returns>之间的值</returns>
        public string GetValueBySE(string str, string s, string e, bool flag = true)
        {
            //?<=断言自身出现的位置的前面能匹配表达式exp,?=断言自身出现的位置的后面能匹配表达式exp
            string result = "";
            if (flag)
            {
                result = Regex.Match(str, "(?=(" + s + "))[.\\s\\S]*?(?<=(" + e + "))", RegexOptions.IgnoreCase).Value;
            }
            else
            {
                result = Regex.Match(str, "(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.IgnoreCase).Value;

            }
            return result;
        }
        /// <summary>
        /// 返回 title|url$title2|url2
        /// </summary>
        public string[] GetImgUrl(string content, int count)
        {
            string result = "";
            MatchCollection mcs = GetValuesBySE(content, "<img", "/>", false);//匹配图片文件
            for (int i = 0; i < mcs.Count && i < count; i++)
            {
                string imgstr = mcs[i].Value.ToLower();
                if (imgstr.Contains("/attachment/") && imgstr.Contains("/emotion/")) { continue; }//不存表情
                result += GetValueBySE(imgstr, "src=\"", "\"", false) + "|";
            }
            return result.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        }
        public MatchCollection GetValuesBySE(string str, string s, string e, bool flag = true)
        {
            if (flag)
                return Regex.Matches(str, "(?=(" + s + "))[.\\s\\S]*?(?<=(" + e + "))", RegexOptions.IgnoreCase);
            else
                return Regex.Matches(str, "(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 获取Html标签 GetHtmlLabel(html,"head"),必须以<>开头,</>结尾,不能有空格等
        /// </summary>
        public MatchCollection GetHtmlLabel(string input, string label)
        {
            return GetValuesBySE(input, "<" + label + ">", "</" + label + ">", true);
        }
        public string GetHtmlAttr(string label, string name)
        {
            string reg = "(?<=([\\s]|<)" + name + @"="")([\s\S]){0,}?((""[\s]|""/>))";
            return Regex.Match(label, reg, RegexOptions.IgnoreCase).Value.Replace("/>", "").Replace("\"", "").Replace(" ", "");
        }
        /// <summary>
        /// 从文本中筛选http与https链接返回
        /// </summary>
        public MatchCollection GetUrlsByStr(string str)
        {
            string reg = "((http[s]{0,1}|ftp)://[a-zA-Z0-9\\.\\-]+\\.([a-zA-Z]{2,4})(:\\d+)?(/[a-zA-Z0-9\\.\\-~!@#$%^&*+?:_/=<>]*)?)|(www.[a-zA-Z0-9\\.\\-]+\\.([a-zA-Z]{2,4})(:\\d+)?(/[a-zA-Z0-9\\.\\-~!@#$%^&*+?:_/=<>]*)?)";
            return Regex.Matches(str, reg, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// //获取指定链接,如内容,商品中的主键ID值 如,/Item/60.(aspx|html),/Shop/2.(aspx|html)
        /// </summary>
        /// <param name="str">Url</param>
        /// <returns></returns>
        public int GetGidByUrl(string str)
        {
            string value = GetValueBySE(str, "/[0-9]", "\\.").Replace("/", "").Replace(".", "");
            return DataConvert.CLng(value);
        }
        //需要转义的[如未加上\,则程序中帮其加上
        private void DelChar() { }
        #endregion
        #region 限制区
        /// <summary>
        /// 清除文本中的链接与空格,主用于签名等
        /// </summary>
        public static string ClearUrl(string msg)
        {
            msg = msg.Replace(" ", "");
            MatchCollection mcs = new RegexHelper().GetUrlsByStr(msg);
            foreach (Match m in mcs)
            {
                msg = msg.Replace(m.Value, "");
            }
            msg = msg.Replace(".", "");
            return msg;
        }
        #endregion
        #region 校验区
        public static bool IsEmail(string s) { if (string.IsNullOrEmpty(s))return false; return Regex.IsMatch(s, S_Email); }
        //手机号码
        public static bool IsMobilPhone(string s) { if (string.IsNullOrEmpty(s))return false; return Regex.IsMatch(s, S_Mobile); }
        //身份证格式
        public static bool IsIDCard(string s) { if (string.IsNullOrEmpty(s))return false; return Regex.IsMatch(s, @"\d{15}|\d{18}"); }
        //邮政编码
        public static bool IsPostCode(string s) { if (string.IsNullOrEmpty(s))return false; return Regex.IsMatch(s, @"[1-9]\d{5}(?!\d)"); }
        //Http或Https地址,示例:http://www.z01.com,可以/结尾
        public static bool IsHttpUrl(string s) { if (string.IsNullOrEmpty(s))return false; return Regex.IsMatch(s, @"^(http|https)://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$", RegexOptions.IgnoreCase); }
        #endregion
        #region 用于服务端控件的字符串
        public static string S_Email = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        public static string S_Mobile = @"^1\d{10}$";
        #endregion
    }
}
