using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace ZoomLa.Components
{
    public class OrderConfig
    {
        private static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\OrderStatus.xml";
        private static XmlDocument orderconfig = null;
        private static object Lockobj = new object();

        private OrderConfig() { }
        private static XmlDocument GetOrderConfig()
        {
            if (orderconfig == null)
            {
                orderconfig = ConfigReadFromFile();
            }
            return orderconfig;
        }
        public static void Update()
        {
            lock (Lockobj)
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    XmlDocument xmldoc = GetOrderConfig();
                    xmldoc.Save(stream);
                    stream.Close();
                }
            }
        }
        //从配置文件获取配置信息
        private static XmlDocument ConfigReadFromFile()
        {
            if (!File.Exists(filePath))
            {
                return new XmlDocument();
            }
            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (stream.Length <= 0)
                { return new XmlDocument(); }
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(stream);
                return xdoc;
            }
        }
        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetOrderStatus(int status)
        {
            return GetStatus(status, "OrderStatus");
        }
        public static void SetOrderStatus(int status,string value)
        {
            SetStatus(status, value, "OrderStatus");
        }
        /// <summary>
        /// 获取物流状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetExpStatus(int status)
        {
            return GetStatus(status, "ExpStatus");
        }
        public static void SetExpStatus(int status,string value)
        {
            SetStatus(status, value, "ExpStatus");
        }
        /// <summary>
        /// 获取支付状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetPayStatus(int status)
        {
            return GetStatus(status, "PayStatus");
        }
        public static void SetPayStatus(int status,string value)
        {
             SetStatus(status, value, "PayStatus");
        }
        /// <summary>
        /// 获取支付方式
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetPayType(int type)
        {
            return GetStatus(type, "PayType");
        }
        public static void SetPayType(int type,string value)
        {
            SetStatus(type, value, "PayType");
        }

        public static string GetStatus(int status,string nodename)
        {
            XmlDocument xmldoc = GetOrderConfig();
            XmlNode node = xmldoc.SelectSingleNode("//"+ nodename + "/Status[@code='" + status + "']");
            if (node != null) { return node.InnerText; }
            return "";
        }
        public static void SetStatus(int status,string value,string nodename)
        {
            XmlDocument xmldoc = GetOrderConfig();
            XmlNode node = xmldoc.SelectSingleNode("//" + nodename + "/Status[@code='" + status + "']");
            if (node != null) { node.InnerText = value; }
        }
    }
}
