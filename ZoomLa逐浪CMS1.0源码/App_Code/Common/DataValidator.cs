namespace ZoomLa.Common
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// DataValidator 用户输入数据有效性检验类
    /// </summary>
    public abstract class DataValidator
    {
        /// <summary>
        /// DataValidator构造函数
        /// </summary>
        public DataValidator()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 校验地区代码是否有效
        /// </summary>
        public static bool IsAreaCode(string input)
        {
            if ((!IsNumber(input) || (input.Length < 3)) || (input.Length > 5))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 校验数据是否有效的十进制数
        /// </summary>
        public static bool IsDecimal(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, "^[0-9]+[.]?[0-9]+$");
        }
        /// <summary>
        /// 校验数据是否有效的十进制数，允许带有正负号
        /// </summary>
        public static bool IsDecimalSign(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, "^[+-]?[0-9]+[.]?[0-9]+$");
        }
        /// <summary>
        /// 校验输入的Email格式是否正确
        /// </summary>
        public static bool IsEmail(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
        /// <summary>
        /// 校验输入的数据是否有效的IP
        /// </summary>
        public static bool IsIP(string input)
        {
            return (!string.IsNullOrEmpty(input) && Regex.IsMatch(input.Trim(), @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$"));
        }
        /// <summary>
        /// 校验输入的数据是否有效的整形数字 正数
        /// </summary>
        public static bool IsNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, "^[0-9]+$");
        }
        /// <summary>
        /// 校验输入的数据是否有效的正负整形数字
        /// </summary>
        public static bool IsNumberSign(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, "^[+-]?[0-9]+$");
        }
        /// <summary>
        /// 校验输入的邮政编码是否有效
        /// </summary>
        public static bool IsPostCode(string input)
        {
            if (!(IsNumber(input) && (input.Length == 6)))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 校验输入的数据是否有效的URL
        /// </summary>
        public static bool IsUrl(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$");
        }
        /// <summary>
        /// 校验输入数据是否有效ID
        /// </summary>
        public static bool IsValidId(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            input = input.Replace("|", "").Replace(",", "").Replace("-", "").Replace(" ", "").Trim();
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return IsNumber(input);
        }
        /// <summary>
        /// 校验用户名是否有效
        /// </summary>
        public static bool IsValidUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }
            if (userName.Length > 20)
            {
                return false;
            }
            if (userName.Trim().Length == 0)
            {
                return false;
            }
            if (userName.Trim(new char[] { '.' }).Length == 0)
            {
                return false;
            }
            string str = "\\/\"[]:|<>+=;,?*@";
            for (int i = 0; i < userName.Length; i++)
            {
                if (str.IndexOf(userName[i]) >= 0)
                {
                    return false;
                }
            }
            return true;
        }
        
    }
}
