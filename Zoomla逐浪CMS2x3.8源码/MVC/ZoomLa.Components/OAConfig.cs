using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using System.IO;
using ZoomLa.Components;
using System.Xml.Serialization;



namespace ZoomLa.Components
{
    public class OAConfig
    {
        private static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Config\OA.config";
        private static OAConfigInfo _stInfo = null;
        //获取实例
        private static OAConfigInfo GetInstance()
        {
            if (_stInfo == null)
            {
                _stInfo = ConfigReadFromFile();
            }
            return _stInfo;
        }
        /// <summary>
        /// 使用时实例= ConfigReadFromFile();
        /// </summary>
        /// <returns></returns>
        private static OAConfigInfo ConfigReadFromFile()
        {
            using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(OAConfigInfo));
                return (OAConfigInfo)serializer.Deserialize(stream);
            }
        }
        public static void Update()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(OAConfigInfo));
                using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("", "");//加上这句，否则会自动在根节点上加两个属性
                    serializer.Serialize(stream, GetInstance(), namespaces);
                    stream.Close();
                    //_stInfo = ConfigReadFromFile();
                }
            }
            finally { }
        }
        public static void ReInstance() 
        {
            _stInfo = ConfigReadFromFile();
        }
        public static string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }
        /// <summary>
        /// 配置用户名显示格式,用户名 真名(呢称) 真名 真名(ID) 1,2,3,4
        /// </summary>
        public static string UNameConfig 
        {
            get 
            {
                return GetInstance().UNameConfig;
            }
            set 
            {
                GetInstance().UNameConfig = value;
            }
        }
        /// <summary>
        /// 允许用户发送短信1:允许，0禁止，下同
        /// </summary>
        public static string AllowMsg
        {
            get
            {
                return GetInstance().AllowMsg;
            }
            set
            {
                GetInstance().AllowMsg = value;
            }
        }
        /// <summary>
        /// 是否允许用户自定义OA UI界面(即个人设置)
        /// </summary>
        public static string AllowUI
        {
            get
            {
                return GetInstance().AllowUI;
            }
            set
            {
                GetInstance().AllowUI = value;
            }
        }
        /// <summary>
        /// 签章上传路径(虚拟)
        /// </summary>
        public static string SignPath
        {
            get
            {
                return GetInstance().SignPath;
            }
            set
            {
                GetInstance().SignPath = value;
            }
        }
        /// <summary>
        /// OA标题
        /// </summary>
        public static string OATitle
        {
            get
            {
                return GetInstance().OATitle;
            }
            set
            {
                GetInstance().OATitle = value;
            }
        }
        /// <summary>
        /// Logo虚拟路径
        /// </summary>
        public static string OALogo
        {
            get
            {
                return GetInstance().OALogo;
            }
            set
            {
                GetInstance().OALogo = value;
            }
        }
        /// <summary>
        /// OA节点与后台节点的对应关系,格式: 党办:1,护理:100
        /// </summary>
        public static string NodeMap
        {
            get
            {
                return GetInstance().NodeMap;
            }
            set
            {
                GetInstance().NodeMap = value;
            }
        }
        /// <summary>
        /// OA文档所绑定的模型ID
        /// </summary>
        public static int ModelID
        {
            get
            {
                return GetInstance().ModelID;
            }
            set
            {
                GetInstance().ModelID = value;
            }
        }
        //------------------------选项
        /// <summary>
        /// 机密选项
        /// </summary>
        public static string Secret
        {
            get
            {
                return GetInstance().Secret;
            }
            set
            {
                GetInstance().Secret = value;
            }
        }
        /// <summary>
        /// 紧急程度选项
        /// </summary>
        public static string Urgency
        {
            get
            {
                return GetInstance().Urgency;
            }
            set
            {
                GetInstance().Urgency = value;
            }
        }
        /// <summary>
        /// 重要程度选项
        /// </summary>
        public static string Importance
        {
            get
            {
                return GetInstance().Importance;
            }
            set
            {
                GetInstance().Importance = value;
            }
        }
        /// <summary>
        /// 将字符串转化为Dictionary，便于绑定，取值
        /// </summary>
        public static Dictionary<string,string> StrToDic(string v)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] arr = v.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in arr)
            {
                dic.Add(s.Split(':')[0],s.Split(':')[1]);
            }
            return dic;
        }
        //public static DataTable StrToDT(string v)
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add(new DataColumn("ID",typeof(int)));
        //    dt.Columns.Add(new DataColumn("Value", typeof(string)));
        //    string[] arr = v.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //    foreach (string s in arr)
        //    {
        //        DataRow dr = dt.NewRow();
        //        dr[0] = s.Split(':')[0];
        //        dr[1] = s.Split(':')[1];
        //        dt.Rows.Add(dr);
        //    }
        //    return dt;
        //}
        /// <summary>
        /// 邮箱容量,为0不限制(仅发送,收件不限制)
        /// </summary>
        public static int MailSize 
        {
            get { return GetInstance().MailSize; }
            set { GetInstance().MailSize = value; }
        }
        public static int Interval
        {
            get
            {
                return Convert.ToInt32(GetInstance().Interval);
            }
            set
            {
                GetInstance().Interval = value.ToString();
            }
        }
        public static bool EnableTool
        {
            get
            {
                return Convert.ToBoolean(GetInstance().EnableTool);
            }
            set
            {
                GetInstance().EnableTool = value.ToString();
            }
        }
        public static string LeaderSignTemplate
        {
            get {
                return GetInstance().LeaderSignTemplate;
            }
            set { GetInstance().LeaderSignTemplate = value; }
        }
        public static string ParterSignTemplate 
        {
            get
            {
                return GetInstance().ParterSignTemplate;
            }
            set { GetInstance().ParterSignTemplate = value; }
        }
    }
    [Serializable, XmlRoot("OABasic")]
    public class OAConfigInfo
    {
        public string UNameConfig { get; set; }
        public string AllowMsg { get; set; }
        public string AllowUI { get; set; }
        public string SignPath { get; set; }
        public string OATitle { get; set; }
        public string OALogo { get; set; }
        public string NodeMap { get; set; }
        public int ModelID { get; set; }
        public string Secret { get; set; }
        public string Urgency { get; set; }
        public string Importance { get; set; }
        public int MailSize { get; set; }
        public string Interval { get; set; }
        public string EnableTool { get; set; }
        public string LeaderSignTemplate { get; set; }
        public string ParterSignTemplate { get; set; }
    }
}
