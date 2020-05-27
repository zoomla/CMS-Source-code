using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

public class GroupPic
{
    public static string GetExtName(string fileName)
    {
        if (fileName.Split('.').Length < 2) return "";
        else
        {
            return fileName.Split('.')[(fileName.Split('.').Length - 1)];
        }
    }
    public static string GetShowExtension(string ext)
    {
        ext = ext.Replace(".", "").Trim().ToLower();
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add("jpeg", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/img.gif'>");
        dictionary.Add("jpe", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/img.gif'>");
        dictionary.Add("bmp", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/img.gif'>");
        dictionary.Add("png", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/img.gif'>");
        dictionary.Add("swf", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Ftype_flash.gif'>");
        dictionary.Add("jpg", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/jpg.gif'>");
        dictionary.Add("gif", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/gif.gif'>");
        dictionary.Add("dll", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/img.gif'>");
        dictionary.Add("vbp", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/sys.gif'>");
        dictionary.Add("wmv", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Ftype_media.gif'>");
        dictionary.Add("avi", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Ftype_media.gif'>");
        dictionary.Add("mp4", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/mp4.png'>");
        dictionary.Add("asf", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Ftype_media.gif'>");
        dictionary.Add("mpg", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Ftype_media.gif'>");
        dictionary.Add("rm", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Ftype_rm.gif'>");
        dictionary.Add("ra", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Ftype_rm.gif'>");
        dictionary.Add("ram", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Ftype_rm.gif'>");
        dictionary.Add("rar", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/zip.gif'>");
        dictionary.Add("zip", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/zip.gif'>");
        dictionary.Add("xml", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/xml.gif'>");
        dictionary.Add("txt", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/txt.gif'>");
        dictionary.Add("exe", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/exe.gif'>");
        dictionary.Add("doc", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/doc.gif'>");
        dictionary.Add("docx", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/doc.gif'>");
        dictionary.Add("html", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/html.gif'>");
        dictionary.Add("htm", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/htm.gif'>");
        dictionary.Add("xls", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/xls.gif'>");
        dictionary.Add("xlsx", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/xls.gif'>");
        dictionary.Add("asp", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/asp.gif'>");
        dictionary.Add("mp3", "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/Ftype_media.gif'>");
        if (dictionary.ContainsKey(ext))
        {
            return dictionary[ext];
        }
        return "<img src='/App_Themes/AdminDefaultTheme/Images/Folder/other.gif'>";
    }
    /// <summary>
    /// 传入文件名或后缀名
    /// </summary>
    public static string GetExtNameMini(string ext)
    {
        if (ext.IndexOf(".") > 0) { ext = Path.GetExtension(ext); }
        ext = ext.Replace(".", "").Trim().ToLower();
        string pos = "";
        string icon = "<div class='ficon_mini' style='background-position:0 {0}'> </div>";
        if (ext.Equals("docx") || ext.Equals("doc"))
        {
            pos = "0px;"; ;
        }
        else if (ext.Equals("pdf"))
        {
            pos = "-35px;";
        }
        else if (ext.Equals("ppt"))
        {
            pos = "-70px;";
        }
        else if (ext.Equals("psd"))
        {
            pos = "-105px;";
        }
        else if (ext.Equals("rar"))
        {
            pos = "-140px;";
        }
        else if (ext.Equals("tmp"))
        {
            pos = "-175px;";
        }
        else if (ext.Equals("xlsx") || ext.Equals("xls"))
        {
            pos = "-210px;";
        }
        else if (ext.Equals("zip")||ext.Equals("bak"))
        {
            pos = "-245px;";
        }
        else if (ext.Equals("mp3"))
        {
            pos = "-280px;";
        }
        else if (ext.Equals("rmvb") || ext.Equals("mp4") || ext.Equals("3gp") || ext.Equals("avi"))
        {
            pos = "-315px;";
        }
        else if (ext.Equals("jpg") || ext.Equals("pdd") || ext.Equals("gif") || ext.Equals("eps") || ext.Equals("raw") || ext.Equals("sct") || ext.Equals("tja") || ext.Equals("vda") || ext.Equals("icb") || ext.Equals("VST") || ext.Equals("jpeg") || ext.Equals("bmp") || ext.Equals("rle") || ext.Equals("png"))
        {
            pos = "-385px;";
        }
        else if (ext.Equals("filefolder"))
        {
            pos = "-420px;";
        }
        else if (ext.Equals("accdb"))
        {
            pos = "-455px;";
        }
        else if (ext.Equals("pub"))
        {
            pos = "-490px;";
        }
        else if (ext.Equals("html"))
        {
            pos = "-525px;";
        }
        else if (ext.Equals("txt"))
        {
            pos = "-560px;";
        }
        else if (ext.Equals("vote"))
        {
            pos = "-595px;";
        }
        else if (ext.Equals("flv"))
        {
            pos = "-630px;";
        }
        else if (ext.Equals("exe"))
        {
            pos = "-665px;";
        }
        else if (ext.Equals("wiki"))
        {
            pos = "-700px;";
        }
        else
        {
            pos = "-350px;";
        }
        return string.Format(icon, pos);
    }
}
