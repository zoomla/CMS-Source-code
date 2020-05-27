using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using ZoomLa.SQLDAL;

/*
 * 字符串方面通用方法
 */ 
namespace ZoomLa.BLL.Helper
{
    public class StrHelper
    {
        #region 普通字符串
        /// <summary>
        /// 是否包含空字符串
        /// </summary>
        /// <returns>true:是,false:否</returns>
        public static bool StrNullCheck(params string[] strs)
        {
            foreach (string str in strs)
            {
                if (string.IsNullOrEmpty(str)||string.IsNullOrEmpty(str.Replace(" ",""))) { return true; }
            }
            return false;
        }
        /// <summary>
        /// 将阿拉伯数字转为中文
        /// </summary>
        public static string ConvertIntegral(int num)
        {
            string strIntegral = num.ToString();
            char[] integral = ((long.Parse(strIntegral)).ToString()).ToCharArray();
            // 定义结果字符串
            StringBuilder strInt = new StringBuilder();
            int digit = integral.Length - 1;
            char[] chnText = new char[] { '零', '一', '二', '三', '四', '五', '六', '七', '八', '九' };
            char[] chnDigit = new char[] { '十', '百', '千', '万', '亿' };
            // integral.Length- 1=处理最高位到十位的所有数字
            for (int i = 0; i < integral.Length; i++)
            {
                // 添加数字
                strInt.Append(chnText[integral[i] - '0']);
                // 添加数位
                if (0 == digit % 4)     // '万' 或 '亿'
                {
                    if (4 == digit || 12 == digit)
                    {
                        strInt.Append(chnDigit[3]); // '万'
                    }
                    else if (8 == digit)
                    {
                        strInt.Append(chnDigit[4]); // '亿'
                    }
                }
                else         // '十'，'百'或'千'
                {
                    strInt.Append(chnDigit[digit % 4 - 1]);
                }
                digit--;
            }
            return strInt.ToString();
        }
        /// <summary>
        /// 移除符号之间的数据,用于话题
        /// </summary>
        public static string RemoveBySE(string msg, string schar = "#", string echar = "#")
        {
            while (msg.Contains(schar) && msg.Contains(echar))
            {
                int start = msg.IndexOf(schar);
                int end = msg.IndexOf(echar, (start + 1));
                msg = msg.Remove(start, (end - start) + 1);
            }
            return msg;
        }
        /// <summary>
        /// 英文算一个字符,中文算两个字符
        /// </summary>
        public static int GetLength(string msg)
        {
            int len = 0;
            for (int i = 0; i < msg.Length; i++)
            {
                byte[] byte_len = Encoding.Default.GetBytes(msg.Substring(i, 1));
                if (byte_len.Length > 1)
                    len += 2;  //如果长度大于1，是中文，占两个字节，+2
                else
                    len += 1;  //如果长度等于1，是英文，占一个字节，+1
            }
            return len;
        }
        #endregion
        #region 字符串压缩
        /// <summary>
        /// 字符串压缩,数组在8倍左右(但无法转字符串后存),base64 5倍
        /// </summary>
        public static byte[] Compress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
                zip.Write(data, 0, data.Length);
                zip.Close();
                byte[] buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();
                return buffer;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static byte[] Decompress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true);
                MemoryStream msreader = new MemoryStream();
                byte[] buffer = new byte[0x1000];
                while (true)
                {
                    int reader = zip.Read(buffer, 0, buffer.Length);
                    if (reader <= 0)
                    {
                        break;
                    }
                    msreader.Write(buffer, 0, reader);
                }
                zip.Close();
                ms.Close();
                msreader.Position = 0;
                buffer = msreader.ToArray();
                msreader.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static string CompressString(string str)
        {
            string compressString = "";
            byte[] compressBeforeByte = Encoding.UTF8.GetBytes(str);
            compressString = Convert.ToBase64String(Compress(compressBeforeByte));
            return compressString;
        }
        public static string DecompressString(string str)
        {
            string compressString = "";
            byte[] compressBeforeByte = Convert.FromBase64String(str);
            byte[] compressAfterByte = Decompress(compressBeforeByte);
            compressString = Encoding.UTF8.GetString(compressAfterByte);
            return compressString;
        }
        #endregion
        #region 网址相关
        //如未带http或https,则自动加上http
        public static string UrlDeal(string url, string protol = "http://")
        {
            url = url.ToLower().Replace(" ", "");
            if (!string.IsNullOrEmpty(url) && !(url.Contains("http://") || url.Contains("https://")))
            {
                url = protol + url;
            }
            return url;
        }
        /// <summary>
        /// 获取网址根路径 http://www.z01.com
        /// </summary>
        public static string GetUrlRoot(string url)
        {
            url = UrlDeal(url);
            int end = url.Replace("://", "$$$").IndexOf("/");
            return end < 8 ? url : url.Substring(0, end);
        }
        /// <summary>
        /// 从网址中取出指定参数
        /// </summary>
        /// <param name="url">带?和参数的网址</param>
        /// <param name="param">参数名</param>
        public static string GetValFromUrl(string url, string param)
        {
            string result = "";
            if (url.Contains("?") && url.Contains("="))
            {
                url = url.Split('?')[1];
                foreach (string query in url.Split('&'))
                {
                    string name = query.Split('=')[0];
                    string value = query.Split('=')[1];
                    if (name.Equals(param, StringComparison.CurrentCultureIgnoreCase))
                    {
                        result = value; break;
                    }
                }
            }
            return result;
        }
        #endregion
        #region IDS
        /// <summary>
        /// 将IDS静化为数据库查询所用的字符串
        /// </summary>
        public static string PureIDSForDB(string ids) 
        {
            return IdsFormat(ids,",","dbquery");
        }
        /// <summary>
        /// 整理IDS格式
        /// </summary>
        /// <param name="ids">1,2,3</param>
        /// <param name="str">前后是否需要加字符</param>
        public static string IdsFormat(string ids, string str = ",", string method = "")
        {
            ids = string.IsNullOrEmpty(ids) ? "" : ids.Replace(" ", "");
            if (string.IsNullOrEmpty(ids)) return "";
            //------------------------------
            string dupStr = str + str;
            while (!string.IsNullOrEmpty(str) && ids.Contains(dupStr)) { ids = ids.Replace(dupStr, str); }
            if (str.Length == 1)
            {
                ids = ids.Trim(str.ToCharArray()[0]);
            }
            else//如果传入的是一个字符串
            {
                if (ids.StartsWith(str)) { ids = ids.Remove(0, str.Length); }
                if (ids.EndsWith(str)) { ids = ids.Substring(0, ids.Length - str.Length); }
            }
            switch (method)
            {
                case "dbquery"://用于查询
                    break;
                default://用于页面与数据库中
                    if (!string.IsNullOrEmpty(ids)) ids = str + ids + str;
                    break;
            }
            return ids;
        }
        /// <summary>
        /// IDS中是否包含指定数值
        /// </summary>
        public static bool IsContain(string ids, object id)
        {
            if (string.IsNullOrEmpty(ids) || string.IsNullOrEmpty(id.ToString())) return false;
            ids = IdsFormat(ids);
            string sid = IdsFormat(id.ToString());
            return ids.Contains(sid);
        }
        /// <summary>
        /// 去除数组或ids中的重复元素
        /// </summary>
        public static string RemoveDupByIDS(string ids)
        {
            try
            {
                if (string.IsNullOrEmpty(ids)) return ids;
                string[] stringArray = ids.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                string result = "";
                Array.Sort(stringArray);//排序数组
                string[] sourceData = stringArray;
                int MaxLine = stringArray.Length;
                //单独计算第一个
                if (sourceData[0] != stringArray[1])
                {
                    result += stringArray[0] + ",";
                }
                for (int i = 1; i < MaxLine; i++)
                {
                    if (sourceData[i] != stringArray[i - 1])
                    {
                        result += stringArray[i] + ",";
                    }
                }
                return result.TrimEnd(',');
            }
            catch (Exception ex) { throw new Exception(ids+",ex:"+ex.Message); }
        }
        /// <summary>
        /// 将b数组中的元素加入a,如果存在则不加
        /// </summary>
        public static string AddToIDS(string ids, string[] b)
        {
            ids = IdsFormat(ids);
            for (int i = 0; i < b.Length; i++)
            {
                if (!ids.Contains(IdsFormat(b[i]))) { ids += b[i] + ","; }
            }
            return ids;
        }
        /// <summary>
        /// 添加进ids,不允许重复
        /// </summary>
        public static string AddToIDS(string ids, string id)
        {
            ids = IdsFormat(ids);
            if (!ids.Contains("," + id + ",")) { ids += "," + id; ids = IdsFormat(ids); }
            return ids;
        }
        /// <summary>
        /// 从ids中移除指定id(贪婪模式)
        /// </summary>
        public static string RemoveToIDS(string ids, string id)
        {
            ids = IdsFormat(ids);
            id = "," + id + ",";
            ids = ids.Replace(id, "");
            return IdsFormat(ids);
        }
        //-----数据库相关
        /// <summary>
        /// 将str1,str2,str3转化为(@str1,@str2,@str3)
        /// </summary>
        /// <returns></returns>
        public static SqlParameter[] GetStrSP(string ids, out string paramStr)
        {
            List<SqlParameter> sp = new List<SqlParameter>();
            paramStr = "";
            if (string.IsNullOrEmpty(ids)) { return sp.ToArray(); }
            string[] paramArr = ids.Split(',');
            for (int i = 0; i < paramArr.Length; i++)
            {
                if (string.IsNullOrEmpty(paramArr[i])) continue;
                paramStr += "@sp" + i + ",";
                sp.Add(new SqlParameter("sp" + i, paramArr[i]));
            }
            paramStr = paramStr.TrimEnd(',');
            return sp.ToArray();
        }
        #endregion
        /// <summary>
        /// 去除a数组与b数组中值相同的元素,用于OA会签,显示未会签人(同样用于移除IDS中指定ID)
        /// </summary>
        public static string RemoveRepeat(string[] a, string[] b)
        {
            string result = "";
            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    if (a[j] == b[i] && a[j] != "")
                        a[j] = "";
                }
            }
            foreach (string s in a)
            {
                if (!string.IsNullOrEmpty(s))
                    result += s + ",";
            }
            return result.TrimEnd(',');
        }
        /// <summary>
        /// 对ConnectionString格式类字符串操作
        /// </summary>
        /// <param name="constr">连接字符串</param>
        /// <param name="name">属性名,示例:Data Source</param>
        /// <returns>=号右边的值</returns>
        public static string GetAttrByStr(string constr, string name)
        {
            return DBHelper.GetAttrByStr(constr, name);
        }
        /// <summary>
        /// 从DataTable中取出指定字段,并以,号切割返回
        /// </summary>
        public static string GetIDSFromDT(DataTable dt, string field)
        {
            string result="";
            foreach (DataRow dr in dt.Rows)
            {
                result += dr[field] + ",";
            }
            return result.Trim(',');
        }
        //-----数组相关
        /// <summary>
        /// 数组合并
        /// </summary>
        public int[] Arr_Merge(int[] arr, int[] arr2)
        {
            int[] x = new int[arr.Length + arr2.Length];
            arr.CopyTo(x, 0);
            arr2.CopyTo(x, arr.Length);
            return x;
        }
    }
}
