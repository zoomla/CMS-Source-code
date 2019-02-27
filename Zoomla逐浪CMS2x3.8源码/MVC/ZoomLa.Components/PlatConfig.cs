using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ZoomLa.Components
{
    public class PlatConfig
    {
        private static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\Plat.config";
        private static PlatModel model = null;
        //获取实例
        private static PlatModel GetInstance()
        {
            if (model == null)
            {
                model = ConfigReadFromFile();
            }
            return model;
        }
        /// <summary>
        /// 使用时实例= ConfigReadFromFile();
        /// </summary>
        /// <returns></returns>
        private static PlatModel ConfigReadFromFile()
        {
            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PlatModel));
                return (PlatModel)serializer.Deserialize(stream);
            }
        }
        public static void Update()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PlatModel));
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");
                    serializer.Serialize(stream, GetInstance(), namespaces);
                    stream.Close();
                    //_stInfo = ConfigReadFromFile();
                }
            }
            finally { }
        }
        public static void ReInstance()
        {
            model = ConfigReadFromFile();
        }
        public static string SinaKey { get { return GetInstance().SinaKey; } set { GetInstance().SinaKey = value; } }
        public static string SinaSecret { get { return GetInstance().SinaSecret; } set { GetInstance().SinaSecret = value; } }
        public static string SinaCallBack { get { return GetInstance().SinaCallBack; } set { GetInstance().SinaCallBack = value; } }
        public static string QQKey { get { return GetInstance().QQKey; } set { GetInstance().QQKey = value; } }
        public static string QQCallBack { get { return GetInstance().QQCallBack; } set { GetInstance().QQCallBack = value; } }
        public static string WXPay_APPID { get { return GetInstance().WXPay_APPID; } set { GetInstance().WXPay_APPID = value; } }
        public static string WXPay_APPSecret { get { return GetInstance().WXPay_APPSecret; } set { GetInstance().WXPay_APPSecret = value; } }
        /// <summary>
        /// 商户号
        /// </summary>
        public static string WXPay_MCHID { get { return GetInstance().WXPay_MCHID; } set { GetInstance().WXPay_MCHID = value; } }
        /// <summary>
        /// 商户Key  商户后台--API安全中手动配置
        /// </summary>
        public static string WXPay_Key { get { return GetInstance().WXPay_Key; } set { GetInstance().WXPay_Key = value; } }
    }
}
