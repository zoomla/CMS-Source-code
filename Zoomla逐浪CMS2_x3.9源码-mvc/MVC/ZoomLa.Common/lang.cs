using System;
using System.Collections.Generic;
using System.Text;
using ZoomLa.Components;
using System.Xml;
using System.Web;
using System.Web.Caching;
using System.Collections;
using System.Data;


public class lang
{
    public static string GetLanguage()
    {
        return SiteConfig.SiteOption.Language + ".xml";
    }
    //ZH-CN
    public static string LangOP = "ZH-CN";
    static lang() 
    {
        LangOP =SiteConfig.SiteOption.Language;
    }
    /// <summary>
    /// 获取语言文本资源(为兼容升级，暂保留)
    /// </summary>
    public static string Get(string name)
    {
        XmlDocument xml = new XmlDocument();
        xml.Load(HttpContext.Current.Server.MapPath("~/Language/" + GetLanguage()));
        foreach (XmlNode n in xml.SelectSingleNode("root").ChildNodes)
        {
            if (n.NodeType != XmlNodeType.Comment)
            {
                if (n.Attributes["name"].Value == name)
                {
                    name = n.InnerText;
                }
            }
        }
        return name;
    }

    public static string LF(string name)
    {
        if (!LangOP.Equals("ZH-CN"))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(HttpContext.Current.Server.MapPath("~/Language/EN-US2.xml"));
            XmlNode xmlNode = xml.SelectSingleNode("/root/resource[@name='" + name + "']");
            if (xmlNode != null)
            {
                name = xmlNode.InnerText;
            }
        }
        return name;
    }
}
