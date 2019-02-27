using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ZoomLa.Common;
using ZoomLa.Model.Third;

namespace ZoomLa.BLL.Third
{
    //后期增加缓存,避免多用户读写时报错
    public class B_Third_Info
    {
        //不使用枚举,便于扩展
        public string xmlPath = function.VToP("/Config/Suppliers.xml");
        public XmlDocument GetXmlDoc()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            return xmlDoc;
        }
        /// <summary>
        /// 根据节点名称选元素
        /// </summary>
        public M_Third_Info SelModelByName(string dom)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            XmlNode node = xmlDoc.SelectSingleNode("//SuppliersList/" + dom);
            M_Third_Info model = new M_Third_Info();
            if (node != null)
            {
                model.dom = dom;
                model = model.GetModelFrom(node);
            }
            else { model = null; }
            return model;
        }
 
        public void UpdateToXml(XmlDocument xmlDoc, M_Third_Info model)
        {
            //先检测是否存在,如果不存在则新建,存在则更新
            XmlNode node = xmlDoc.SelectSingleNode("//SuppliersList/" + model.dom);
            //if (node == null) 
            //{
            //    XmlNode node = new XmlNode();
            //    node.Attributes.add
            //}
            if (node != null)
            {
                //node.Attributes["ID"].Value = model.ID.Trim();
                node.Attributes["Key"].Value = model.Key.Trim();
                node.Attributes["Secret"].Value = model.Secret.Trim();
                //node.Attributes["CallBackUrl"].Value = model.CallBackUrl.Trim();
                //node.Attributes["Enable"].Value = (model.Enabled ? 1 : 0).ToString();
            }
        }
        /// <summary>
        /// 更新缓存中的XML信息,最后再由xml负责写入(需要更多则调用模型)
        /// </summary>
        public void UpdateToXml(XmlDocument xmlDoc, string dom, string key, string secret, string callbackUrl = "")
        {
            M_Third_Info model = new M_Third_Info();
            model.dom = dom;
            model.Key = key;
            model.Secret = secret;
            model.CallBackUrl = callbackUrl;
            UpdateToXml(xmlDoc, model);
        }
    }
}
