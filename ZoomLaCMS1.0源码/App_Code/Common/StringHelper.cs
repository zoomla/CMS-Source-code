namespace ZoomLa.Common
{
    using Microsoft.VisualBasic;
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// StringHelper 的摘要说明
    /// </summary>
    public static class StringHelper
    {
        
        /// <summary>
        /// 将字符串添加到StringBuilder中形成逗号隔开的数组
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="append">要添加的字符串</param>
        public static void AppendString(StringBuilder sb, string append)
        {
            AppendString(sb, append, ",");
        }
        /// <summary>
        /// 将字符串添加到StringBuilder中形成用指定分隔符隔开的字符串
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="append">要添加的字符串</param>
        /// <param name="split">在字符串后添加的分隔符</param>
        public static void AppendString(StringBuilder sb, string append, string split)
        {
            if (sb.Length == 0)
            {
                sb.Append(append);
            }
            else
            {
                sb.Append(split);
                sb.Append(append);
            }
        }

        public static string Base64StringDecode(string input)
        {
            byte[] bytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string Base64StringEncode(string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        public static string CollectionFilter(string conStr, string tagName, int fType)
        {
            string input = conStr;
            switch (fType)
            {
                case 1:
                    return Regex.Replace(input, "<" + tagName + "([^>])*>", "", RegexOptions.IgnoreCase);

                case 2:
                    return Regex.Replace(input, "<" + tagName + "([^>])*>.*?</" + tagName + "([^>])*>", "", RegexOptions.IgnoreCase);

                case 3:
                    return Regex.Replace(Regex.Replace(input, "<" + tagName + "([^>])*>", "", RegexOptions.IgnoreCase), "</" + tagName + "([^>])*>", "", RegexOptions.IgnoreCase);
            }
            return input;
        }

        public static string DecodeIP(long ip)
        {
            string[] strArray = new string[] { ((ip >> 0x18) & 0xffL).ToString(), ".", ((ip >> 0x10) & 0xffL).ToString(), ".", ((ip >> 8) & 0xffL).ToString(), ".", (ip & 0xffL).ToString() };
            return string.Concat(strArray);
        }

        public static string DecodeLockIP(string lockIP)
        {
            StringBuilder builder = new StringBuilder(0x100);
            if (!string.IsNullOrEmpty(lockIP))
            {
                try
                {
                    string[] strArray = lockIP.Split(new string[] { "$$$" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        string[] strArray2 = strArray[i].Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
                        builder.Append(DecodeIP(Convert.ToInt64(strArray2[0])) + "----" + DecodeIP(Convert.ToInt64(strArray2[1])) + "\n");
                    }
                    return builder.ToString().TrimEnd(new char[] { '\n' });
                }
                catch (IndexOutOfRangeException)
                {
                    return builder.ToString();
                }
            }
            return builder.ToString();
        }

        public static double EncodeIP(string sip)
        {
            if (string.IsNullOrEmpty(sip))
            {
                return 0.0;
            }
            string[] strArray = sip.Split(new char[] { '.' });
            long num = 0L;
            foreach (string str in strArray)
            {
                byte num2;
                if (byte.TryParse(str, out num2))
                {
                    num = (num << 8) | num2;
                }
                else
                {
                    return 0.0;
                }
            }
            return num;
        }

        public static string EncodeLockIP(string ipList)
        {
            StringBuilder builder = new StringBuilder(0x100);
            if (!string.IsNullOrEmpty(ipList.Trim()))
            {
                string[] strArray = ipList.Split(new char[] { '\n' });
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (!string.IsNullOrEmpty(strArray[i]) && strArray[i].Contains("----"))
                    {
                        string[] strArray2 = strArray[i].Split(new string[] { "----" }, StringSplitOptions.RemoveEmptyEntries);
                        if (strArray2.Length < 2)
                        {
                            throw new ArgumentException("IP值无效");
                        }
                        if (!(DataValidator.IsIP(strArray2[0]) && DataValidator.IsIP(strArray2[1])))
                        {
                            throw new ArgumentException("IP值无效");
                        }
                        if (i == 0)
                        {
                            builder.Append(EncodeIP(strArray2[0]) + "----" + EncodeIP(strArray2[1]));
                        }
                        else
                        {
                            builder.Append(string.Concat(new object[] { "$$$", EncodeIP(strArray2[0]), "----", EncodeIP(strArray2[1]) }));
                        }
                    }
                }
            }
            return builder.ToString();
        }

        public static string FilterScript(string conStr, string filterItem)
        {
            string str = conStr.Replace("\r", "{$Chr13}").Replace("\n", "{$Chr10}");
            string[] strArray = filterItem.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str2 in strArray)
            {
                switch (str2)
                {
                    case "Iframe":
                        str = CollectionFilter(str, str2, 2);
                        break;

                    case "Object":
                        str = CollectionFilter(str, str2, 2);
                        break;

                    case "Script":
                        str = CollectionFilter(str, str2, 2);
                        break;

                    case "Style":
                        str = CollectionFilter(str, str2, 2);
                        break;

                    case "Div":
                        str = CollectionFilter(str, str2, 3);
                        break;

                    case "Span":
                        str = CollectionFilter(str, str2, 3);
                        break;

                    case "Table":
                        str = CollectionFilter(CollectionFilter(CollectionFilter(CollectionFilter(CollectionFilter(str, str2, 3), "Tbody", 3), "Tr", 3), "Td", 3), "Th", 3);
                        break;

                    case "Img":
                        str = CollectionFilter(str, str2, 1);
                        break;

                    case "Font":
                        str = CollectionFilter(str, str2, 3);
                        break;

                    case "A":
                        str = CollectionFilter(str, str2, 3);
                        break;

                    case "Html":
                        str = StripTags(str);
                        goto Label_0218;
                }
            }
        Label_0218:
            return str.Replace("{$Chr13}", "\r").Replace("{$Chr10}", "\n");
        }

        public static bool FoundCharInArr(string checkStr, string findStr)
        {
            return FoundCharInArr(checkStr, findStr, ",");
        }

        public static bool FoundCharInArr(string checkStr, string findStr, string split)
        {
            bool flag = false;
            if (string.IsNullOrEmpty(split))
            {
                split = ",";
            }
            if (string.IsNullOrEmpty(checkStr))
            {
                return false;
            }
            if (checkStr.IndexOf(split) != -1)
            {
                string[] strArray;
                if (findStr.IndexOf(split) != -1)
                {
                    strArray = checkStr.Split(new char[] { Convert.ToChar(split) });
                    string[] strArray2 = findStr.Split(new char[] { Convert.ToChar(split) });
                    foreach (string str in strArray)
                    {
                        foreach (string str2 in strArray2)
                        {
                            if (string.Compare(str, str2) == 0)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            return flag;
                        }
                    }
                    return flag;
                }
                strArray = checkStr.Split(new char[] { Convert.ToChar(split) });
                foreach (string str in strArray)
                {
                    if (string.Compare(str, findStr) == 0)
                    {
                        return true;
                    }
                }
                return flag;
            }
            if (string.Compare(checkStr, findStr) == 0)
            {
                flag = true;
            }
            return flag;
        }

        public static bool FoundInArr(string checkStr, string findStr, string split)
        {
            bool flag = false;
            if (checkStr.IndexOf(findStr) != -1)
            {
                string[] strArray = checkStr.Split(new string[] { split }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string str in strArray)
                {
                    if (string.Compare(str, findStr) == 0)
                    {
                        return true;
                    }
                }
                return flag;
            }
            if (string.Compare(checkStr, findStr) == 0)
            {
                flag = true;
            }
            return flag;
        }

        public static string MD5(string input)
        {
            using (MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider())
            {
                return BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", "").ToLower();
            }
        }

        public static int MD5D(string strText)
        {
            using (MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider())
            {
                byte[] bytes = Encoding.Default.GetBytes(strText);
                bytes = provider.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte num in bytes)
                {
                    builder.Append(num.ToString("D").ToLower());
                }
                string input = builder.ToString();
                if (input.Length >= 9)
                {
                    input = "9" + input.Substring(1, 8);
                }
                else
                {
                    input = "9" + input;
                }
                provider.Clear();
                return DataConverter.CLng(input);
            }
        }

        public static string MD5gb2312(string input)
        {
            using (MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider())
            {
                return BitConverter.ToString(provider.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(input))).Replace("-", "").ToLower();
            }
        }

        public static string RemoveXss(string input)
        {
            string str;
            input = Regex.Replace(input, @"(&#*\w+)[\x00-\x20]+;", "$1;");
            input = Regex.Replace(input, "(&#x*[0-9A-F]+);*", "$1;", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "&(amp|lt|gt|nbsp|quot);", "&amp;$1;");
            input = HttpUtility.HtmlDecode(input);
            input = Regex.Replace(input, @"[\x00-\x08\x0b-\x0c\x0e-\x19]", "");
            input = Regex.Replace(input, "(<[^>]+[\\x00-\\x20\"'/])(on|xmlns)[^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "([a-z]*)[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*)[\\x00-\\x20]*j[\\x00-\\x20]*a[\\x00-\\x20]*v[\\x00-\\x20]*a[\\x00-\\x20]*s[\\x00-\\x20]*c[\\x00-\\x20]*r[\\x00-\\x20]*i[\\x00-\\x20]*p[\\x00-\\x20]*t[\\x00-\\x20]*:", "$1=$2nojavascript...", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "([a-z]*)[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*)[\\x00-\\x20]*v[\\x00-\\x20]*b[\\x00-\\x20]*s[\\x00-\\x20]*c[\\x00-\\x20]*r[\\x00-\\x20]*i[\\x00-\\x20]*p[\\x00-\\x20]*t[\\x00-\\x20]*:", "$1=$2novbscript...", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "(<[^>]+)style[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*).*expression[\\x00-\\x20]*\\([^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "(<[^>]+)style[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*).*behaviour[\\x00-\\x20]*\\([^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, "(<[^>]+)style[\\x00-\\x20]*=[\\x00-\\x20]*([`'\"]*).*s[\\x00-\\x20]*c[\\x00-\\x20]*r[\\x00-\\x20]*i[\\x00-\\x20]*p[\\x00-\\x20]*t[\\x00-\\x20]*:*[^>]*>", "$1>", RegexOptions.IgnoreCase);
            input = Regex.Replace(input, @"</*\w+:\w[^>]*>", "");
            do
            {
                str = input;
                input = Regex.Replace(input, "</*(applet|meta|xml|blink|link|style|script|embed|object|iframe|frame|frameset|ilayer|layer|bgsound|title|base)[^>]*>", "", RegexOptions.IgnoreCase);
            }
            while (str != input);
            return input;
        }

        public static string ReplaceIgnoreCase(string input, string oldValue, string newValue)
        {
            return Strings.Replace(input, oldValue, newValue, 1, -1, CompareMethod.Text);
        }

        public static string SHA1(string input)
        {
            using (SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider())
            {
                return BitConverter.ToString(provider.ComputeHash(Encoding.UTF8.GetBytes(input))).Replace("-", "").ToLower();
            }
        }

        public static string StripTags(string input)
        {
            Regex regex = new Regex("<([^<]|\n)+?>");
            return regex.Replace(input, "");
        }

        public static string SubString(string demand, int length, string substitute)
        {
            if (Encoding.Default.GetBytes(demand).Length <= length)
            {
                return demand;
            }
            ASCIIEncoding encoding = new ASCIIEncoding();
            length -= Encoding.Default.GetBytes(substitute).Length;
            int num = 0;
            StringBuilder builder = new StringBuilder();
            byte[] bytes = encoding.GetBytes(demand);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                if (num > length)
                {
                    break;
                }
                builder.Append(demand.Substring(i, 1));
            }
            builder.Append(substitute);
            return builder.ToString();
        }

        public static int SubStringLength(string demand)
        {
            if (string.IsNullOrEmpty(demand))
            {
                return 0;
            }
            ASCIIEncoding encoding = new ASCIIEncoding();
            int num = 0;
            byte[] bytes = encoding.GetBytes(demand);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0x3f)
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

        public static string Trim(string returnStr)
        {
            if (!string.IsNullOrEmpty(returnStr))
            {
                return returnStr.Trim();
            }
            return string.Empty;
        }
        /// <summary>
        /// 将字符串转换成大写拼音首字母组成的字符串
        /// </summary>
        /// <param name="str">要转换的汉字字符串</param>
        /// <returns></returns>
        public static string ChineseToPY(string str)
        {
            string tempStr = "";
            foreach (char c in str)
            {
                if ((int)c >= 48 && (int)c <= 122)
                {//字母和符号原样保留

                    tempStr += c.ToString().ToUpper();
                }
                else
                {//累加拼音声母
                    tempStr += GetPYChar(c.ToString()).ToUpper();
                }
            }
            return tempStr;
        }
        /// <summary>
        /// 取字符的拼音声母
        /// </summary>
        /// <param name="c">要转换的汉字</param>
        /// <returns>转换后的结果</returns>
        private static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));

            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "a";
            if (i < 0xB2C1) return "b";
            if (i < 0xB4EE) return "c";
            if (i < 0xB6EA) return "d";
            if (i < 0xB7A2) return "e";
            if (i < 0xB8C1) return "f";
            if (i < 0xB9FE) return "g";
            if (i < 0xBBF7) return "h";
            if (i < 0xBFA6) return "j";
            if (i < 0xC0AC) return "k";
            if (i < 0xC2E8) return "l";
            if (i < 0xC4C3) return "m";
            if (i < 0xC5B6) return "n";
            if (i < 0xC5BE) return "o";
            if (i < 0xC6DA) return "p";
            if (i < 0xC8BB) return "q";
            if (i < 0xC8F6) return "r";
            if (i < 0xCBFA) return "s";
            if (i < 0xCDDA) return "t";
            if (i < 0xCEF4) return "w";
            if (i < 0xD1B9) return "x";
            if (i < 0xD4D1) return "y";
            if (i < 0xD7FA) return "z";
            return "*";
        }    
        public static bool ValidateMD5(string password, string md5Value)
        {
            return ((string.Compare(password, md5Value) == 0) || (string.Compare(password, md5Value.Substring(8, 0x10)) == 0));
        }
        public static string ShowPage(int totalnumber, int MaxPerPage, int CurrentPage, bool ShowTotal, string strUnit)
        {
            StringBuilder strTemp = new StringBuilder();
            string strRaw = string.Empty;
            string strUrl = string.Empty;
            strRaw = HttpContext.Current.Request.RawUrl;
            string[] strArray = strRaw.Split(new char[] { '/' });
            if (strArray.Length > 0)
            {
                strRaw = strArray[strArray.Length - 1];
            }
            if (strRaw.IndexOf('?') > 0)
            {
                strUrl = strRaw.Substring(0, strRaw.IndexOf('?'));
                string strParam = strRaw.Substring(strRaw.IndexOf('?') + 1);
                string[] strArray1 = strParam.Split(new char[] { '&' });
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < strArray1.Length; k++)
                {
                    //int pos = strArray1[k].IndexOf("=");
                    string pa = strArray1[k].Substring(0, strArray1[k].IndexOf("="));
                    if (string.Compare(pa, "p", true) != 0)
                    {
                        sb.Append(strArray1[k] + "&");
                    }
                }
                strUrl = strUrl + "?" + sb.ToString();
            }
            else
            {
                strUrl = strRaw + "?";
            }
            
            int TotalPage;

            if (totalnumber == 0 || MaxPerPage == 0)
            {
                return "";
            }
            if (totalnumber % MaxPerPage == 0)
                TotalPage = totalnumber / MaxPerPage;
            else
                TotalPage = totalnumber / MaxPerPage + 1;

            if (CurrentPage > TotalPage)
                CurrentPage = TotalPage;
            strTemp.Append("<table width=\"99%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" class=\"border\" align=\"center\">");
            strTemp.Append("<tr>	<td valign=\"bottom\" align=\"center\" nowrap=\"nowrap\" style=\"width:40%;\">");

            if (ShowTotal)
                strTemp.Append("共 <b>" + totalnumber.ToString() + "</b> " + strUnit + "&nbsp;&nbsp;");
            strTemp.Append("页次：<strong><font color=red>" + CurrentPage.ToString() + "</font>/" + TotalPage.ToString() + "</strong>页&nbsp; ");
            if (CurrentPage == 1)
                strTemp.Append("首页 上一页&nbsp;");
            else
            {
                strTemp.Append("<a href='" + strUrl + "p=1'>首页</a>&nbsp;");
                strTemp.Append("<a href='" + strUrl + "p=" + (CurrentPage - 1) + "'>上一页</a>&nbsp;");
            }
            if (CurrentPage >= TotalPage)
                strTemp.Append("下一页 尾页");
            else
            {
                strTemp.Append("<a href='" + strUrl + "p=" + (CurrentPage + 1) + "'>下一页</a>&nbsp;");
                strTemp.Append("<a href='" + strUrl + "p=" + TotalPage + "'>尾页</a>");
            }
            strTemp.Append("</td>		</tr>	</table>");
            return strTemp.ToString();
        }
    }
}