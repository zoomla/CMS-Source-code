using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Xml.Serialization;
using System.Security;

namespace ZoomLa.Components
{

    public class ShopOption
    {
        private string filePath = "";
        public ShopOption()
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                this.filePath = current.Server.MapPath("~/Config/ShopConfig.config");
            }
            else
            {
                this.filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/ShopConfig.config");
            }

        }


        public ShopConfigInfo ShopCoinfig()
        {
            using (Stream stream = new FileStream(this.filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ShopConfigInfo));
                return (ShopConfigInfo)serializer.Deserialize(stream);
            }
        }

        public void Update(ShopConfigInfo info)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ShopConfigInfo));
                using (Stream stream = new FileStream(this.filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");
                    serializer.Serialize(stream, info, namespaces);
                }
            }
            catch (SecurityException exception)
            {
                throw new SecurityException(exception.Message, exception.DenySetInstance, exception.PermitOnlySetInstance, exception.Method, exception.Demanded, exception.FirstPermissionThatFailed);
            }
        }
    }

    [Serializable]
    public class ShopConfigInfo
    {
        public ShopConfigInfo()
        {
            //构造函数
        }

        protected string m_start;
        protected string m_mnum;
        protected string m_pnum;
        protected string m_snum;
        protected string m_Times;
        protected string m_Interval;
        protected string m_Price;
        protected string m_User;

        /// <summary>
        /// 状态
        /// </summary>
        public string Start
        {
            get { return this.m_start; }
            set { this.m_start = value; }
        }
        /// <summary>
        /// 按金钱干扰
        /// </summary>
        public string Mnum
        {
            get { return this.m_mnum; }
            set { this.m_mnum = value; }
        }
        /// <summary>
        /// 按人数干扰
        /// </summary>
        public string Pnum
        {
            get { return this.m_pnum; }
            set { this.m_pnum = value; }
        }
        /// <summary>
        /// 按倒计时干扰
        /// </summary>
        public string Snum
        {
            get { return this.m_snum; }
            set { this.m_snum = value; }
        }
        /// <summary>
        /// 干扰次数
        /// </summary>
        public string Times
        {
            get { return this.m_Times; }
            set { this.m_Times = value; }
        }
        /// <summary>
        /// 干扰间隔时间
        /// </summary>
        public string Interval
        {
            get { return this.m_Interval; }
            set { this.m_Interval = value; }
        }
        /// <summary>
        /// 干扰价格
        /// </summary>
        public string Price
        {
            get { return this.m_Price; }
            set { this.m_Price = value; }
        }
        /// <summary>
        /// 干扰用户
        /// </summary>
        public string User
        {
            get { return this.m_User; }
            set { this.m_User = value; }
        }

    }
}