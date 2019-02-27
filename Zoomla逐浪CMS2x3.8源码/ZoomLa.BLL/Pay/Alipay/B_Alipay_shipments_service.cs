using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;

namespace ZoomLa.BLL
{
    /// <summary>
    /// 类名：alipay_service
    /// 功能：支付宝外部服务接口控制
    /// 详细：该页面是请求参数核心处理文件，不需要修改
    /// 版本：3.1
    /// 修改日期：2010-12-17
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考
    /// </summary>
    public class B_Alipay_shipments_service
    {
        private string gateway = "";                //网关地址
        private string _key = "";                   //交易安全校验码
        private string _input_charset = "";         //编码格式
        private string _sign_type = "";             //签名方式
        private string mysign = "";                 //签名结果
        private Dictionary<string, string> sPara = new Dictionary<string, string>();//要签名的字符串

        /// <summary>
        /// 构造函数
        /// 从配置文件及入口文件中初始化变量
        /// </summary>
        /// <param name="partner">合作身份者ID</param>
        /// <param name="trade_no">支付宝交易号。它是登陆支付宝网站在交易管理中查询得到，一般以8位日期开头的纯数字（如：20100419XXXXXXXXXX） </param>
        /// <param name="logistics_name">物流公司名称</param>
        /// <param name="invoice_no">物流发货单号</param>
        /// <param name="transport_type">物流发货时的运输类型，三个值可选：POST（平邮）、EXPRESS（快递）、EMS（EMS）</param>
        /// <param name="seller_ip">卖家本地电脑IP地址</param>
        /// <param name="key">安全检验码</param>
        /// <param name="input_charset">字符编码格式 目前支持 gbk 或 utf-8</param>
        /// <param name="sign_type">加密方式 不需修改</param>
        public B_Alipay_shipments_service(string partner,
            string trade_no,
            string logistics_name,
            string invoice_no,
            string transport_type,
            string seller_ip,
            string key,
            string input_charset,
            string sign_type)
        {
            gateway = "https://www.alipay.com/cooperate/gateway.do?";
            _key = key.Trim();
            _input_charset = input_charset.ToLower();
            _sign_type = sign_type.ToUpper();
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();

            //构造签名参数数组
            sParaTemp.Add("service", "send_goods_confirm_by_platform");
            sParaTemp.Add("partner", partner);
            sParaTemp.Add("trade_no", trade_no);
            sParaTemp.Add("logistics_name", logistics_name);
            sParaTemp.Add("invoice_no", invoice_no);
            sParaTemp.Add("transport_type", transport_type);
            sParaTemp.Add("seller_ip", seller_ip);
            sParaTemp.Add("_input_charset", _input_charset);
            sPara = B_Alipay_function.Para_filter(sParaTemp);
            //获得签名结果
            mysign = B_Alipay_function.Build_mysign(sPara, _key, _sign_type, _input_charset);
        }

        /// <summary>
        /// 构造表单提交HTML
        /// </summary>
        /// <returns>输出 表单提交HTML文本</returns>
        public string Build_Form()
        {
            StringBuilder sbHtml = new StringBuilder();

            //GET方式传递
            //sbHtml.Append("<form id=\"alipaysubmit\" name=\"alipaysubmit\" action=\"" + gateway + "_input_charset=" + _input_charset + "\" method=\"get\">");

            //POST方式传递（GET与POST二必选一）
            sbHtml.Append("<form id=\"alipaysubmit\" name=\"alipaysubmit\" action=\"" + gateway + "_input_charset=" + _input_charset + "\" method=\"post\">");

            foreach (KeyValuePair<string, string> temp in sPara)
            {
                sbHtml.Append("<input type=\"hidden\" name=\"" + temp.Key + "\" value=\"" + temp.Value + "\"/>");
            }

            sbHtml.Append("<input type=\"hidden\" name=\"sign\" value=\"" + mysign + "\"/>");
            sbHtml.Append("<input type=\"hidden\" name=\"sign_type\" value=\"" + _sign_type + "\"/>");

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type=\"submit\" value=\"发货\"></form>");

            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="sArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public string Create_linkstring_urlencode(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + HttpUtility.UrlEncode(temp.Value) + "&");
            }

            return prestr.ToString();
        }

        /// <summary>
        /// 构造请求URL
        /// </summary>
        /// <returns>请求url</returns>
        public string Create_url()
        {
            string strUrl = gateway;
            string arg = Create_linkstring_urlencode(sPara);	//把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            strUrl = strUrl + arg + "sign=" + mysign + "&sign_type=" + _sign_type;
            return strUrl;
        }
    }
}