using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;

namespace ZoomLa.Model
{
    [Serializable]
    public class M_Alipay_config
    {
        //定义变量（无需改动）
        private string input_charset = "utf-8";
        private string sign_type = "MD5";
        private string transport = "https";

        public string Notify_url
        {
            get;
            set;
        }
        public string Seller_email { get; set; }

        public string Mainname
        {
            get;
            set;
        }

        public string Show_url
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置会员登录后跳转的页面路径
        /// </summary>
        public string Return_url
        {
            get;set;
        }

        /// <summary>
        /// 获取或设置字符编码格式
        /// </summary>
        public string Input_charset
        {
            get { return input_charset; }
            set { input_charset = value; }
        }

        /// <summary>
        /// 获取或设置签名方式
        /// </summary>
        public string Sign_type
        {
            get { return sign_type; }
            set { sign_type = value; }
        }

        /// <summary>
        /// 获取或设置访问模式
        /// </summary>
        public string Transport
        {
            get { return transport; }
            set { transport = value; }
        }
        public M_Alipay_config() { }
        public M_Alipay_config(String url)
        {
            //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

            

            //会员登录后跳转的页面 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
            Return_url = url;
            //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

           

        }
        public M_Alipay_config(String sellerEmail, String showUrl, String mainName)
        {
            this.Seller_email = sellerEmail;
            this.Show_url = showUrl;
            this.Mainname = mainName;
        }
    }
}