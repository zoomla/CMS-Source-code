
using System;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using ZoomLa.Common;
using ZoomLa.Components;
using System.IO;
using System.Linq;
using ZoomLa.BLL;
using System.Collections.Generic;
using ZoomLa.Safe;
using ZoomLa.Model;
public class ZoomlaSecurityCenter
{
    private static string[] ExName = new string[] { "cer", "exe", "vbs", "bat", "com", "asp", "aspx","cshtml","cs", "php", "jsp", "py", "asa", "asax", "ascx", "ashx", "asmx", "axd", "java", "jsl", "js", "vb", "resx", "html", "htm", "shtml", "shtm" };
    //ViewState,控件,AJAX提交
    public static bool PostData()
    {
        bool result = false;
        for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
        {
            result = CheckData(HttpContext.Current.Request.Form[i].ToString());
            if (result)
            {
                break;
            }
        }
        return result;
    }
    //地址栏参,Get方式提交数据
    public static bool GetData()
    {
        bool result = false;
        for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
        {
            result = CheckData(HttpContext.Current.Request.QueryString[i]);
            if (result)
            {
                break;
            }
        }
        return result;
    }
    //Cookie中的参数
    public static bool CookieData()
    {
        bool result = false;
        for (int i = 0; i < HttpContext.Current.Request.Cookies.Count; i++)
        {
            result = CheckData(HttpContext.Current.Request.Cookies[i].Value.ToLower());
            if (result)
            {
                break;
            }
        }
        return result;

    }
    //来源
    public static bool referer()
    {
        bool result = false;
        return result = CheckData(HttpContext.Current.Request.UrlReferrer.ToString());
    }
    public static bool CheckData(string inputData)
    {
        return SafeC.CheckData(inputData);
    }
    /// <summary>
    /// 后缀名检测,如aspx等返回True,不带,用于全局上传检测
    /// </summary>
    public static bool ExNameCheck(string ext)
    {
        ext = ext.ToLower().Replace(";","");//避免低IIS版本,通过;阻断方式绕过验证
        return ExName.Contains(ext);
    }
    //----------------------------------------
    //----上传文件检测,首先多重判断，避免对普通页面的影响,然后再对安全进行检测,如带多个后缀名,则每个都检测
    public static void CheckUpladFiles()
    {
        HttpRequest Request = HttpContext.Current.Request;
        HttpResponse Response = HttpContext.Current.Response;
        if (HttpContext.Current.Request.ContentType.ToLower().IndexOf("multipart/form-data") > -1)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile file = Request.Files[i];
                string fname = Path.GetFileName(file.FileName).ToLower();
                if (file.ContentLength < 1 || string.IsNullOrEmpty(fname)) { continue; }
                if (fname.IndexOf(".") < 0) { Response.Write("取消上传,原因:文件必须有后缀名,请勿上传可疑文件"); Response.End(); }
                string[] filearr = fname.Split('.');//多重后缀名检测,用于处理低版本IIS安全问题(IIS7以上可不需)
                string UploadFileExts = SiteConfig.SiteOption.UploadFileExts.ToLower();
                for (int o = 1; o < filearr.Length; o++)
                {
                    ZLLog.L(ZLEnum.Log.fileup, "By:Global_CheckUpladFiles,文件名：" + file.FileName);
                    string fileext = filearr[o].ToString().ToLower();
                    if (!ExNameCheck(fileext))//;号检测
                    {
                        string exname = Path.GetExtension(fname).ToLower().Replace(".", "");
                        if (!StringHelper.FoundCharInArr(UploadFileExts, exname, "|"))
                        {
                            Response.Write("取消上传,原因:上传的文件不是符合扩展名" + UploadFileExts + "的文件");
                            Response.End();
                        }
                    }
                    else
                    {
                        Response.Write("取消上传,原因:请勿上传可疑文件!");
                        Response.End();
                    }
                }
            }//for end;
        }
    }
    //----------------------------------------
   /// <summary>
   /// 安全域名功能
   /// </summary>
   /// <param name="host">用户指定的安全域名</param>
   /// <param name="url">网址</param>
    public static bool IsSafeDomain(string url)
    {
        bool flag = false;
        if (string.IsNullOrEmpty(url)) return true;
        else
        {
            if (url.Contains(HttpContext.Current.Request.Url.Host.ToLower()))
            {
                flag = true;
            }
            return flag;
        }
    }
    /// <summary>
    /// 验证码校验,无论对错，必须清除，避免安全漏洞
    /// </summary>
    /// <returns>通过返回True,未通过False</returns>
    public static bool VCodeCheck(string key, string s)
    {
        bool flag = true;
        Dictionary<string, string> codeDic = ((Dictionary<string, string>)HttpContext.Current.Session["CodeDic"]);
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(s) || !codeDic.ContainsKey(key) || string.IsNullOrEmpty(codeDic[key])) { flag = false; }
        else if (!s.ToLower().Equals(codeDic[key].ToLower()))
        {
            flag = false;
        }
        if (!string.IsNullOrEmpty(key) && codeDic.ContainsKey(key))
        { codeDic.Remove(key); }
        return flag;
    }
}
