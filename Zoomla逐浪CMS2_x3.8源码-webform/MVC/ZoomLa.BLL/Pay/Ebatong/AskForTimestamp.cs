using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Xml;

namespace ZoomLa.BLL.Ebatong
{

    /// <summary>
    /// 请求时间戳及其验证
    /// </summary>
    public class AskForTimestamp
    {

        public AskForTimestamp()
        {

        }

        /// <summary>
        /// 请求服务器时间戳
        /// </summary>
        public string askFor(string Partner,string Key)
        {
            CommonHelper CommonHelper = new CommonHelper();
            // ebatong商户网关
            string ask_for_time_stamp_gateway = "https://www.ebatong.com/gateway.htm";
            // 时间戳请求参数
            string service = "query_timestamp"; // 服务名称：请求时间戳
            string partner =Partner; // 合作者商户ID
            string input_charset = "UTF-8"; // 字符集
            string sign_type = "MD5"; // 摘要签名算法

            // 按请求参数名排序（升序）
            string[] ori = { 
                       "service=" + service,
                       "partner=" + partner,
                       "input_charset=" + input_charset,
                       "sign_type=" + sign_type
                       };
            string[] sortedStr = CommonHelper.BubbleSort(ori);
            string sortedParamStr = CommonHelper.BuildParamString(sortedStr);

            string merKey = Key; // 商户加密字符串
            string plaintextMergeMerKey = sortedParamStr + merKey; // 将商户加密字符串附加在请求参数字符串之后，形成明文

            // 对上述明文作MD5摘要
            string sign = CommonHelper.md5(input_charset, plaintextMergeMerKey);
            sign=sign.ToLower();
            // 生成请求
            string uri = ask_for_time_stamp_gateway + "?" + "service=" + service + "&partner=" + partner + "&input_charset=" + input_charset + "&sign_type=" + sign_type + "&sign=" + sign;
            Console.WriteLine(uri);
            WebClient wc = new WebClient();
            Stream st = wc.OpenRead(uri);
            StreamReader sr = new StreamReader(st);
            string res = sr.ReadToEnd();
            sr.Close();
            st.Close();
            //Console.WriteLine("HTTP Response is ");
            //Console.WriteLine(res); // 取得返回的时间戳信息

            XmlDocument xmlDom = new XmlDocument();
            xmlDom.LoadXml(res);


            string timestamp = "";
            //判断时间戳请求是否成功
            if ("T".Equals(xmlDom.SelectSingleNode("ebatong/is_success/text()").Value))
            {
                //去除时间戳及签名
                string time_ebatong = xmlDom.SelectSingleNode("ebatong/response/timestamp/encrypt_key/text()").Value;
                string valid_sign = xmlDom.SelectSingleNode("ebatong/sign/text()").Value;
                string new_sign=CommonHelper.md5(input_charset,time_ebatong+merKey).ToLower();
                //判断签名是否正确
                if (new_sign.Equals(valid_sign))
                {
                    timestamp = time_ebatong;
                }
            }
           
            return timestamp;
        }

        
    }
}