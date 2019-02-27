namespace ZoomLa.BLL
{
    using System.Web;
    using System.Text;
    using System.IO;
    using System.Net;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// 类名：alipay_service
    /// 功能：支付宝外部服务接口控制
    /// 详细：该页面是请求参数核心处理文件，不需要修改
    /// 版本：3.1
    /// 修改日期：2010-11-26
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考
    /// </summary>
    public class B_Alipay_user_service
    {
        private string gateway = "";                //网关地址
        private string _key = "";                    //交易安全校验码
        private string _input_charset = "";         //编码格式
        private string _sign_type = "";              //签名方式
        private string mysign = "";                 //签名结果
        private Dictionary<string, string> sPara = new Dictionary<string, string>();//要签名的参数组

        /// <summary>
        /// 构造函数
        /// 从配置文件及入口文件中初始化变量
        /// </summary>
        /// <param name="partner">合作身份者ID</param>
        /// <param name="return_url">付完款后跳转的页面 要用 以http开头格式的完整路径，不允许加?id=123这类自定义参数</param>
        /// <param name="email">会员通用登录时，会员的支付宝账号</param>
        /// <param name="key">安全检验码</param>
        /// <param name="input_charset">字符编码格式 目前支持 gbk 或 utf-8</param>
        /// <param name="sign_type">签名方式 不需修改</param>
        public B_Alipay_user_service(string partner,
            string return_url,
            string email,
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
            sParaTemp.Add("service", "user_authentication");
            sParaTemp.Add("partner" , partner);
            sParaTemp.Add("return_url" , return_url);
            sParaTemp.Add("email" , email);
            sParaTemp.Add("_input_charset" , _input_charset);

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
            sbHtml.Append("<form id=\"alipaysubmit\"  name=\"alipaysubmit\" action=\"" + gateway + "_input_charset=" + _input_charset + "\" method=\"get\">");

            //POST方式传递（GET与POST二必选一）
            //sbHtml.Append("<form id=\"alipaysubmit\" name=\"alipaysubmit\" action=\"" + gateway + "_input_charset=" + _input_charset + "\" method=\"post\">");

            foreach (KeyValuePair<string, string> temp in sPara)
            {
                sbHtml.Append("<input type=\"hidden\" name=\"" + temp.Key + "\" value=\"" + temp.Value + "\"/>");
            }

            sbHtml.Append("<input type=\"hidden\" name=\"sign\" value=\"" + mysign + "\"/>");
            sbHtml.Append("<input type=\"hidden\" name=\"sign_type\" value=\"" + _sign_type + "\"/>");

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<a href=\"javascript:void(0);\" onclick=\"document.alipaysubmit.submit()\">支付宝登录</a></form>");

            //sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }
    }
}