using System;
using System.Collections.Generic;
using System.Text;

namespace  ZoomLa.BLL.Alipay
{
    /// <summary>
    /// 类名：alipay_service
    /// 功能：支付宝外部服务接口控制
    /// 详细：该页面是请求参数核心处理文件，不需要修改
    /// 版本：3.1
    /// 修改日期：2010-11-25
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考
    /// </summary>
    public class B_Alipay_trade_service
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
        /// <param name="seller_email">签约支付宝账号或卖家支付宝帐户</param>
        /// <param name="return_url">付完款后跳转的页面 要用 以http开头格式的完整路径，不允许加?id=123这类自定义参数</param>
        /// <param name="notify_url">交易过程中服务器通知的页面 要用 以http开格式的完整路径，不允许加?id=123这类自定义参数</param>
        /// <param name="show_url">网站商品的展示地址，不允许加?id=123这类自定义参数</param>
        /// <param name="out_trade_no">请与贵网站订单系统中的唯一订单号匹配</param>
        /// <param name="subject">订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。</param>
        /// <param name="body">订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里</param>
        /// <param name="price">订单总金额，显示在支付宝收银台里的“商品单价”里</param>
        /// <param name="logistics_fee">物流费用，即运费。</param>
        /// <param name="logistics_type">物流类型，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）</param>
        /// <param name="logistics_payment">物流支付方式，三个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）</param>
        /// <param name="quantity">商品数量，建议默认为1，不改变值，把一次交易看成是一次下订单而非购买一件商品。</param>
        /// <param name="receive_name">收货人姓名，如：张三</param>
        /// <param name="receive_address">收货人地址，如：XX省XXX市XXX区XXX路XXX小区XXX栋XXX单元XXX号</param>
        /// <param name="receive_zip">收货人邮编，如：123456</param>
        /// <param name="receive_phone">收货人电话号码，如：0571-81234567</param>
        /// <param name="receive_mobile">收货人手机号码，如：13312341234</param>
        /// <param name="logistics_fee_1">第二组物流费用，即运费。</param>
        /// <param name="logistics_type_1">第二组物流类型，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）</param>
        /// <param name="logistics_payment_1">第二组物流支付方式，三个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）</param>
        /// <param name="logistics_fee_2">第三组物流费用，即运费。</param>
        /// <param name="logistics_type_2">第三组物流类型，三个值可选：EXPRESS（快递）、POST（平邮）、EMS（EMS）</param>
        /// <param name="logistics_payment_2">第三组物流支付方式，三个值可选：SELLER_PAY（卖家承担运费）、BUYER_PAY（买家承担运费）</param>
        /// <param name="buyer_email">默认买家支付宝账号</param>
        /// <param name="discount">折扣，是具体的金额，而不是百分比。若要使用打折，请使用负数，并保证小数点最多两位数</param>
        /// <param name="key">安全检验码</param>
        /// <param name="input_charset">字符编码格式 目前支持 gbk 或 utf-8</param>
        /// <param name="sign_type">加密方式 不需修改</param>
        public B_Alipay_trade_service(string partner,
            string seller_email,
            string return_url,
            string notify_url,
            string show_url,
            string out_trade_no,
            string subject,
            string body,
            string price,
            string logistics_fee,
            string logistics_type,
            string logistics_payment,
            string quantity,
            string receive_name,
            string receive_address,
            string receive_zip,
            string receive_phone,
            string receive_mobile,
            string logistics_fee_1,
            string logistics_type_1,
            string logistics_payment_1,
            string logistics_fee_2,
            string logistics_type_2,
            string logistics_payment_2,
            string buyer_email,
            string discount,
            string key,
            string input_charset,
            string sign_type)
        {
            gateway = "https://www.alipay.com/cooperate/gateway.do?";
            _key = key.Trim();
            _input_charset = input_charset.ToLower();
            _sign_type = sign_type.ToUpper();
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();

            //构造加密参数数组，以下顺序请不要更改（由a到z排序）
            sParaTemp.Add("_input_charset", _input_charset);
            sParaTemp.Add("body", body);
            sParaTemp.Add("buyer_email", buyer_email);
            sParaTemp.Add("discount", discount);
            sParaTemp.Add("logistics_fee", logistics_fee);
            sParaTemp.Add("logistics_fee_1", logistics_fee_1);
            sParaTemp.Add("logistics_fee_2", logistics_fee_2);
            sParaTemp.Add("logistics_payment", logistics_payment);
            sParaTemp.Add("logistics_payment_1", logistics_payment_1);
            sParaTemp.Add("logistics_payment_2", logistics_payment_2);
            sParaTemp.Add("logistics_type", logistics_type);
            sParaTemp.Add("logistics_type_1", logistics_type_1);
            sParaTemp.Add("logistics_type_2", logistics_type_2);
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("partner", partner);
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("price", price);
            sParaTemp.Add("quantity", quantity);
            sParaTemp.Add("receive_address", receive_address);
            sParaTemp.Add("receive_mobile", receive_mobile);
            sParaTemp.Add("receive_name", receive_name);
            sParaTemp.Add("receive_phone", receive_phone);
            sParaTemp.Add("receive_zip", receive_zip);
            sParaTemp.Add("return_url", return_url);
            sParaTemp.Add("seller_email", seller_email);
            sParaTemp.Add("service", "trade_create_by_buyer");
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("subject", subject);
            //构造加密参数数组，以上顺序请不要更改（由a到z排序）
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
            sbHtml.Append("<form id=\"alipaysubmit\" name=\"alipaysubmit\" action=\"" + gateway + "_input_charset=" + _input_charset + "\" method=\"get\">");

            //POST方式传递（GET与POST二必选一）
            //sbHtml.Append("<form id=\"alipaysubmit\" name=\"alipaysubmit\" action=\"" + gateway + "_input_charset=" + _input_charset + "\" method=\"post\">");

            foreach (KeyValuePair<string, string> temp in sPara)
            {
                sbHtml.Append("<input type=\"hidden\" name=\"" + temp.Key + "\" value=\"" + temp.Value + "\"/>");
            }
            sbHtml.Append("<input type=\"hidden\" name=\"sign\" value=\"" + mysign + "\"/>");
            sbHtml.Append("<input type=\"hidden\" name=\"sign_type\" value=\"" + _sign_type + "\"/>");
            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type=\"submit\" value=\"确认支付\"></form>");
            return sbHtml.ToString();
        }
    }
}
