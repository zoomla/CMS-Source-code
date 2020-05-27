namespace ZoomLa.Common
{
    using System;

    public abstract class DataConverter
    {
        /// <summary>
        /// 将字符串转换成布尔型
        /// </summary>
        public static bool CBool(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                input = input.Trim();
                return (((string.Compare(input, "true", true) == 0) || (string.Compare(input, "yes", true) == 0)) || (string.Compare(input, "1", true) == 0));
            }
            return false;
        }
        /// <summary>
        /// 将数据转换成日期时间类型
        /// </summary>
        public static DateTime CDate(object input)
        {
            return ((Convert.IsDBNull(input) || object.Equals(input, null)) ? DateTime.Now : CDate(input.ToString()));
        }
        /// <summary>
        /// 将字符串转换成日期时间类型
        /// </summary>
        public static DateTime CDate(string input)
        {
            DateTime now;
            if (!DateTime.TryParse(input, out now))
            {
                now = DateTime.Now;
            }
            return now;
        }
        /// <summary>
        /// 将字符串转换成日期时间类型 如果字符串不能转换则返回提供的时间参数
        /// </summary>
        public static DateTime CDate(string input, DateTime outTime)
        {
            DateTime time;
            if (!DateTime.TryParse(input, out time))
            {
                return outTime;
            }
            return time;
        }
        /// <summary>
        /// 将数据转换成十进制数字，十进制数是由符号、数值和比例因子组成的浮点值，数值的每一位的范围都是 0 到 9，比例因子指示分隔数值的整数和小数部分的浮点小数点的位置
        /// </summary>
        public static decimal CDecimal(object input)
        {
            return ((Convert.IsDBNull(input) || object.Equals(input, null)) ? 0M : CDecimal(input.ToString()));
        }
        /// <summary>
        /// 将数字的字符串表现形式转换成十进制数字
        /// </summary>
        public static decimal CDecimal(string input)
        {
            decimal num;
            decimal.TryParse(input, out num);
            return num;
        }
        /// <summary>
        /// 将输入数据转换成十进制数字，如果数据格式不正确，则返回输入的默认值
        /// </summary>
        public static decimal CDecimal(string input, decimal defaultValue)
        {
            decimal num;
            if (decimal.TryParse(input, out num))
            {
                return num;
            }
            return defaultValue;
        }
        /// <summary>
        /// 将数据转换成双精度数字
        /// </summary>
        public static double CDouble(object input)
        {
            return ((Convert.IsDBNull(input) || object.Equals(input, null)) ? 0.0 : CDouble(input.ToString()));
        }
        /// <summary>
        /// 将数据转换成双精度数字
        /// </summary>
        public static double CDouble(string input)
        {
            double num;
            double.TryParse(input, out num);
            return num;
        }
        /// <summary>
        /// 将数据转换成浮点型数字
        /// </summary>
        public static float CFloat(object input)
        {
            return ((Convert.IsDBNull(input) || object.Equals(input, null)) ? 0f : CFloat(input.ToString()));
        }
        /// <summary>
        /// 将数据转换成浮点型数字
        /// </summary>
        public static float CFloat(string input)
        {
            float num;
            float.TryParse(input, out num);
            return num;
        }
        /// <summary>
        /// 将数据转换成长整型数字，如果数据为空，则返回输入的默认值
        /// </summary>
        public static int CLng(object input, int defaultValue)
        {
            if (Convert.IsDBNull(input) || object.Equals(input, null)) { return defaultValue; }
            else
            {
                double num;
                if (double.TryParse(input.ToString(), out num))
                {
                    return Convert.ToInt32(num);
                }
            }
            return defaultValue;
        }
        public static int CLng(string input) { return CLng(input,0); }
        public static int CLng(object input) { return CLng(input, 0); }
    }
}