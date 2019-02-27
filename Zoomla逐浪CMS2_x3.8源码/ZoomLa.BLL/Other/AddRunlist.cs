using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Xml;
/*
 * Disuse
 */ 
namespace ZoomLa.BLL
{
    public class AddRunlist
    {
        string Mailfilepath = AppDomain.CurrentDomain.BaseDirectory+@"\Config\MailSendList.config";
        string Salefilepath = AppDomain.CurrentDomain.BaseDirectory+@"\Config\SaleSendList.config";
        string Collectpath = AppDomain.CurrentDomain.BaseDirectory+@"\Config\CollectSendList.config";
        string Makepath = AppDomain.CurrentDomain.BaseDirectory+@"\Config\MakeSendList.config";
        string ComePaht = AppDomain.CurrentDomain.BaseDirectory+@"\Config\ComeSendList.config";//商城竞拍

        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="mailinfo"></param>
        public void AddMail(M_MailInfo mailinfo)
        {
            StringBuilder str = GetMailxml(mailinfo);
            FileSystemObject.WriteFile(Mailfilepath, str.ToString());
        }

        /// <summary>
        /// 采集
        /// </summary>
        /// <param name="mailinfo"></param>
        public void AddCollectio(M_CollectionItem Collectioinfo)
        {
            StringBuilder str = GetCollectXml(Collectioinfo);
            FileSystemObject.WriteFile(Collectpath, str.ToString());
        }

        /// <summary>
        /// 添加商城竞拍
        /// </summary>
        /// <param name="str"></param>
        public void AddCome(string strxml,int pid)
        {
            XmlDocument xml = new XmlDocument();
            if (!FileSystemObject.IsExist(ComePaht, FsoMethod.File))
            {
                XmlElement xmlelem = xml.CreateElement("xml");
                XmlElement str = GetCome(xml, strxml,pid);
                xmlelem.AppendChild(str);
                xml.AppendChild(xmlelem);
                xml.Save(ComePaht);
            }
            else
            {
                XmlNode xn = null;
                if (FileSystemObject.ReadFile(ComePaht).Length > 0)
                {
                    xml.Load(ComePaht);
                    xn = xml.SelectSingleNode("xml");
                }
                XmlElement str = GetCome(xml, strxml, pid);
                if (xn != null)
                {
                    xn.AppendChild(str);
                }
                else
                {
                    XmlElement xmlelem = xml.CreateElement("xml");
                    xmlelem.AppendChild(str);
                    xml.AppendChild(xmlelem);
                }
                xml.Save(ComePaht);
            }
        }

        public void UpdateCome(string xmlstr,int pid)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(ComePaht);
            XmlNode docxn = xmlDoc.SelectSingleNode("xml");
            XmlNodeList doclist = docxn.ChildNodes;

            xmlDoc.Save(ComePaht);
        }

        private XmlElement GetCome(XmlDocument xml, string str,int pid)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(str);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("GuessXML").ChildNodes;
            XmlElement xmlelem = xml.CreateElement("GuessXML");

            XmlElement xmlel1 = xml.CreateElement("StartBiddingDate");
            XmlText xmltext = xml.CreateTextNode(function.GetXmlNode(nodeList, "StartBiddingDate"));
            xmlel1.AppendChild(xmltext);
            xmlelem.AppendChild(xmlel1);

            XmlElement xmlel2 = xml.CreateElement("EndBiddingDate");
            XmlText xmltext2 = xml.CreateTextNode(function.GetXmlNode(nodeList, "EndBiddingDate"));
            xmlel2.AppendChild(xmltext2);
            xmlelem.AppendChild(xmlel2);

            XmlElement xmlel3 = xml.CreateElement("PID");
            XmlText xmltext3 = xml.CreateTextNode(pid+"");
            xmlel3.AppendChild(xmltext3);
            xmlelem.AppendChild(xmlel3);

            XmlElement xmlel4 = xml.CreateElement("DIR");
            XmlText xmltext4 = xml.CreateTextNode(System.Web.HttpContext.Current.Server.MapPath("/Config/"+function.GetXmlNode(nodeList, "Number")+".config"));
            xmlel4.AppendChild(xmltext4);
            xmlelem.AppendChild(xmlel4);

            return xmlelem;
        }
        /// <summary>
        /// 生成文件
        /// </summary>
        /// <param name="mailinfo"></param>
        public void AddMake(string dir, string date)
        {
            XmlDocument xml = new XmlDocument();
            if (!FileSystemObject.IsExist(Makepath, FsoMethod.File))
            {
                XmlElement xmlelem = xml.CreateElement("xml");
                XmlElement str = GetMakeXml(xml, dir, date);
                xmlelem.AppendChild(str);
                xml.AppendChild(xmlelem);
                xml.Save(Makepath);
            }
            else
            {
                XmlNode xn = null;
                if (FileSystemObject.ReadFile(Makepath).Length > 0)
                {
                    xml.Load(Makepath);
                    xn = xml.SelectSingleNode("xml");
                }
                XmlElement newChild = GetMakeXml(xml, dir, date);
                if (xn != null)
                {
                    xn.AppendChild(newChild);
                }
                else
                {
                    XmlElement xmlelem = xml.CreateElement("xml");
                    xmlelem.AppendChild(newChild);
                    xml.AppendChild(xmlelem);
                }
                xml.Save(Makepath);
            }
        }
        /// <summary>
        /// 邮件
        /// </summary>
        /// <param name="mailinfo"></param>
        /// <returns></returns>
        private static StringBuilder GetMailxml(M_MailInfo mailinfo)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("<XmlType>");
            str.AppendLine("<SendTime>");
            str.AppendLine(mailinfo.MailSendTime.ToString());
            str.AppendLine("</SendTime>");
            str.AppendLine("</XmlType>");
            return str;
        }
        /// <summary>
        /// 采集(暂时未做定时采集)
        /// </summary>
        /// <param name="mailinfo"></param>
        /// <returns></returns>
        private static StringBuilder GetCollectXml(M_CollectionItem mailinfo)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("<XmlType>");
            str.AppendLine("<CollectTime>");
            str.AppendLine(mailinfo.LastTime.ToString());
            str.AppendLine("</CollectTime>");
            str.AppendLine("</XmlType>");
            return str;
        }
        /// <summary>
        /// 生成静态(暂时未做定时生成静态)
        /// </summary>
        /// <param name="mailinfo"></param>
        /// <returns></returns>
        private static XmlElement GetMakeXml(XmlDocument xml, string dir,string date)
        {
            XmlElement xmlelem = xml.CreateElement("XmlType");
            XmlElement xmlelem2 = xml.CreateElement("Maketime");
            XmlText xmltext = xml.CreateTextNode(date);
            xmlelem2.AppendChild(xmltext);
            xmlelem.AppendChild(xmlelem2);


            XmlElement xmlelem3 = xml.CreateElement("FileDir");
            XmlText xmltext2 = xml.CreateTextNode(dir);
            xmlelem3.AppendChild(xmltext2);
            xmlelem.AppendChild(xmlelem3);

            return xmlelem;
        }
    }
}
