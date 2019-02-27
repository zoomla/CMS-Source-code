using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ComRE
{
    public static string Img_NoPic(string url, string css = "img_50")
    {
        url = (url ?? "").ToLower();
        if (url.Contains("glyphicon ") || url.Contains("fa "))
        {
            return string.Format("<span class=\"{0}\"></span>", url);
        }
        return string.Format("<img src='{0}' class='" + css + "' onerror=\"this.error=null;this.src='/Images/nopic.gif';\" />", url);
    }
    public static string Img_NoFace(string url, string css)
    {
        url = (url ?? "").ToLower();
        string tlp = "<img src='{0}' class='" + css + "' onerror=\"this.error=null;this.src='/Images/userface/noface.png';\" />";
        return string.Format(tlp, url);
    }
    /// <summary>
    /// 主用于别名与用户名的获取
    /// </summary>
    public static string GetNoEmptyStr(params string[] values)
    {
        foreach (string str in values)
        {
            if (!string.IsNullOrWhiteSpace(str)) return str;
        }
        return "";
    }
    public static string Icon_OK = "<span style='color:green;' class='fa fa-check'></span>";
    public static string Icon_Error = "<span style='color:red;' class='fa fa-remove'></span>";
}