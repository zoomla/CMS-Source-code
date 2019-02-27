<%@ WebHandler Language="C#" Class="Translation" %>

using System;
using System.Web;
using System.Net;
using System.Xml;
using ZoomLa.Common;
using System.Web.Security;

public class Translation : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string action = context.Request.Form["action"];
        string result = "";
        switch (action)
        {
            case "bdtrans"://百度翻译
                WebClient web = new WebClient();
                result = web.DownloadString(GetBaiduUrl(context,context.Request.Form["text"],context.Request.Form["from"],context.Request.Form["to"]));
                break;
            default:
                break;
        }
        context.Response.Write(result); context.Response.Flush(); context.Response.End();
    }
    public string GetBaiduUrl(HttpContext context,string text,string from,string to)
    {
        string transurl = "http://api.fanyi.baidu.com/api/trans/vip/translate";
        XmlDocument Xml = new XmlDocument();
        Xml.Load(context.Server.MapPath("/config/Suppliers.xml"));
        XmlNode keynode = Xml.SelectSingleNode("SuppliersList/BaiduTrans");
        string appid = keynode.Attributes["AppID"].Value;
        string key = keynode.Attributes["Key"].Value;
        string randomstr = function.GetRandomString(8, 2);
        string signstr = appid+ text+ randomstr+key;
        string sign= FormsAuthentication.HashPasswordForStoringInConfigFile(signstr, "MD5").ToLower();
        string urlparam = "q="+context.Server.UrlEncode(text)+"&from="+from+"&to="+to+"&appid="+appid+"&salt="+randomstr+"&sign="+sign;
        return transurl+"?"+urlparam;
    }
    public string GetBaiduKey(HttpContext context)
    {
        XmlDocument Xml = new XmlDocument();
        Xml.Load(context.Server.MapPath("/config/Suppliers.xml"));
        XmlNode keynode = Xml.SelectSingleNode("SuppliersList/BaiduTrans");
        return keynode.Attributes["Key"].Value;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}