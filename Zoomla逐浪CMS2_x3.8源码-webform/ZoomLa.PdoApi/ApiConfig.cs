namespace ZoomLa.API
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;
    using System.Web;
    using System.Xml;

    public class ApiConfig
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="path"></param>
        public ApiConfig(string path){}
        /// <summary>
        /// 从Api配置文件里读取配置信息
        /// </summary>
        /// <param name="path"></param>
        private void ReadConfig(string path)
        {

        }
        public void SaveConfig()
        {
           
        }
        /// <summary>
        /// 是否启用API接口
        /// </summary>
        public string ApiEnable
        {
            get;
            set;
        }        
        /// <summary>
        /// Api接口程序ID 除PDO三方外的系统都是other
        /// </summary>
        public string AppID
        {
            get;
            set;
        }
        /// <summary>
        /// 系统配置键
        /// </summary>
        public string SysKey
        {
            get;
            set;
        }
        /// <summary>
        /// 发送信息接收的URL组合
        /// </summary>
        public string Urls
        {
            get;
            set;
        }
        /// <summary>
        /// 是否启用DiscuzNT论坛
        /// </summary>
        public string DisCuzz
        {
            get;
            set;
        }
        /// <summary>
        /// 是否启用Discuz Php论坛
        /// </summary>
        public string Discuz
        {
            get;
            set;
        }
    }
}