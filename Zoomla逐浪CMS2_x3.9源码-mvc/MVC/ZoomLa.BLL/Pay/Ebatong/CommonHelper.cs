using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Xml;


namespace ZoomLa.BLL.Ebatong
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class CommonHelper
    {

        public CommonHelper()
        {
        }

        /// <summary>
        /// 构造参数（将http请求参数用&连接）
        /// </summary>
        /// <param name="s">例如：{"service=query_timestamp", "partner=201208211326476324"}</param>
        /// <returns>例如：service=query_timestamp&partner=201208211326476324</returns>
        public string BuildParamString(string[] s)
        {
            StringBuilder temp = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (i == s.Length - 1)
                {
                    temp.Append(s[i]);
                }
                else
                {
                    temp.Append(s[i] + "&");
                }
            }
            string paramStr = temp.ToString();
            return paramStr;
        }

        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public string[] BubbleSort(string[] r)
        {
            int i, j; //交换标志 
            string temp;

            bool exchange;

            for (i = 0; i < r.Length; i++) //最多做R.Length-1趟排序 
            {
                exchange = false; //本趟排序开始前，交换标志应为假

                for (j = r.Length - 2; j >= i; j--)
                {
                    if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)　//交换条件
                    {
                        temp = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = temp;

                        exchange = true; //发生了交换，故将交换标志置为真 
                    }
                }

                if (!exchange) //本趟排序未发生交换，提前终止算法 
                {
                    break;
                }

            }
            return r;
        }

       

        /// <summary>
        /// 转换为16进制的字符串形式
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public string ToHexStr(byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }

            StringBuilder hexStr = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                hexStr.Append(bytes[i].ToString("X2"));
            }
            return hexStr.ToString();
        }

        /// <summary>
        /// MD5摘要
        /// </summary>
        /// <param name="input_charset"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string md5(string input_charset, string plainText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] byteArray = md5.ComputeHash(Encoding.GetEncoding(input_charset).GetBytes(plainText));
            // 将字节数组转化为16进制字符串形式，作为MD5摘要
            string encryptText = ToHexStr(byteArray);
            return encryptText;
        }

    }
}