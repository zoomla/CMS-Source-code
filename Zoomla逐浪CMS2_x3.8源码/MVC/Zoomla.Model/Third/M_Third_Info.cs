using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ZoomLa.Model.Third
{
    /// <summary>
    /// 存储路径/Config/Suppliers.xml
    /// </summary>
    public class M_Third_Info
    {
        /// <summary>
        /// Baidu,Netease,Renren,Kaixin,QQ,Sina,SohuChat(搜狐畅言),BaiduTrans(百度翻译)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 目标帐号名(QQ等使用)
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// APIKey
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// APISecret
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// 回调地址
        /// </summary>
        public string CallBackUrl { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        //-----------------------------------------
        public string dom = "";
        public M_Third_Info GetModelFrom(XmlNode node)
        {
            M_Third_Info model = new M_Third_Info();
            model.Name = node.Name;
            foreach (XmlAttribute attr in node.Attributes)
            {
                switch (attr.Name)
                {
                    case "ID":
                        model.ID = attr.Value;
                        break;
                    case "Key":
                        model.Key = attr.Value;
                        break;
                    case "Secret":
                        model.Secret = attr.Value;
                        break;
                    case "CallBackUrl":
                        model.CallBackUrl = attr.Value;
                        break;
                    case "Enable":
                        model.Enabled = attr.Value == "1" ? true : false;
                        break;
                }
            }
            return model;
        }
    }
}
