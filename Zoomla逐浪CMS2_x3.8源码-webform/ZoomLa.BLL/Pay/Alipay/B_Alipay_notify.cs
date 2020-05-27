using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;

namespace ZoomLa.BLL
{
    /// <summary>
    /// 类名：alipay_notify
    /// 功能：付款过程中服务器通知类
    /// 详细：该页面是通知返回核心处理文件，不需要修改
    /// 版本：3.1
    /// 修改日期：2010-09-29
    /// '说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// //////////////////////注意/////////////////////////////
    /// 调试通知返回时，可查看或改写log日志的写入TXT里的数据，来检查通知返回是否正常 
    /// </summary>
    public class B_Alipay_notify
    {
        private string gateway = "";                //网关地址
        private string _transport = "";             //访问模式
        private string _partner = "";               //合作身份者ID
        private string _key = "";                   //编码格式
        private string _input_charset = "";         //编码格式
        private string _sign_type = "";             //签名方式
        private string mysign = "";                 //签名结果
        private string responseTxt = "";            //服务器ATN结果
        private Dictionary<string, string> sPara = new Dictionary<string, string>();//要签名的参数组
        private string preSignStr = "";             //待签名的字符串

        /// <summary>
        /// 获取通知返回后计算后（验证）的签名结果
        /// </summary>
        public string Mysign
        {
            get { return mysign; }
        }

        /// <summary>
        /// 获取验证是否是支付宝服务器发来的请求结果
        /// </summary>
        public string ResponseTxt
        {
            get { return responseTxt; }
        }

        /// <summary>
        /// 获取待签名的字符串（调试用）
        /// </summary>
        public string PreSignStr
        {
            get { return preSignStr; }
        }

        /// <summary>
        /// 构造函数
        /// 从配置文件中初始化变量
        /// </summary>
        /// <param name="inputPara">通知返回来的参数数组</param>
        /// <param name="notify_id">验证通知ID</param>
        /// <param name="partner">合作身份者ID</param>
        /// <param name="key">安全校验码</param>
        /// <param name="input_charset">编码格式</param>
        /// <param name="sign_type">签名类型</param>
        /// <param name="transport">访问模式</param>
        public B_Alipay_notify(SortedDictionary<string, string> inputPara, string notify_id, string partner, string key, string input_charset, string sign_type, string transport)
        {
            _transport = transport;
            if (_transport == "https")
            {
                gateway = "https://www.alipay.com/cooperate/gateway.do?";
            }
            else
            {
                gateway = "http://notify.alipay.com/trade/notify_query.do?";
            }

            _partner = partner.Trim();
            _key = key.Trim();
            _input_charset = input_charset;
            _sign_type = sign_type.ToUpper();

            sPara = B_Alipay_function.Para_filter(inputPara);    //过滤空值、sign与sign_type参数
            preSignStr = B_Alipay_function.Create_linkstring(sPara);   //获取待签名字符串（调试用）
            //获得签名结果
            mysign = B_Alipay_function.Build_mysign(sPara, _key, _sign_type, _input_charset);

            //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            responseTxt = Verify(notify_id);
        }

        /// <summary>
        /// 验证是否是支付宝服务器发来的请求
        /// </summary>
        /// <returns>验证结果</returns>
        private string Verify(string notify_id)
        {
            string veryfy_url = "";
            if (_transport == "https")
            {
                veryfy_url = gateway + "service=notify_verify&partner=" + _partner + "&notify_id=" + notify_id;
            }
            else
            {
                veryfy_url = gateway + "partner=" + _partner + "&notify_id=" + notify_id;
            }

            return Get_Http(veryfy_url, 120000);
        }

        /// <summary>
        /// 获取远程服务器ATN结果
        /// </summary>
        /// <param name="strUrl">指定URL路径地址</param>
        /// <param name="timeout">超时时间设置</param>
        /// <returns>服务器ATN结果</returns>
        private string Get_Http(string strUrl, int timeout)
        {
            string strResult;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }

                strResult = strBuilder.ToString();
            }
            catch (Exception exp)
            {

                strResult = "错误：" + exp.Message;
            }

            return strResult;
        }
    }
}