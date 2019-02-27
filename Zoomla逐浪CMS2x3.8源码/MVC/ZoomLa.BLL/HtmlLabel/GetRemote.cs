using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MSXML2;
using System.Text.RegularExpressions;
using System;
using ZoomLa.Common;
namespace ZoomLa.BLL
{
    /// <summary>
    /// 远程文件抓取类
    /// </summary>
    public class GetRemoteObj
    {
        #region 构造与析构函数
        public GetRemoteObj()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        ~GetRemoteObj()
        {
            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region 日期随机函数
        /**********************************
         * 函数名称:DateRndName
         * 功能说明:日期随机函数
         * 参    数:ra:随机数
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          Random ra = new Random();
         *          string s = o.DateRndName(ra);
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/
        /// <summary>
        /// 日期随机函数
        /// </summary>
        /// <param name="ra">随机数</param>
        /// <returns></returns>
        public string DateRndName(Random ra)
        {
            DateTime d = DateTime.Now;
            string s = null, y, m, dd, h, mm, ss;
            y = d.Year.ToString();
            m = d.Month.ToString();
            if (m.Length < 2) m = "0" + m;
            dd = d.Day.ToString();
            if (dd.Length < 2) dd = "0" + dd;
            h = d.Hour.ToString();
            if (h.Length < 2) h = "0" + h;
            mm = d.Minute.ToString();
            if (mm.Length < 2) mm = "0" + mm;
            ss = d.Second.ToString();
            if (ss.Length < 2) ss = "0" + ss;
            s += y + m + dd + h + mm + ss;
            s += ra.Next(100, 999).ToString();
            return s;
        }
        #endregion

        #region 取得文件后缀
        /**********************************
         * 函数名称:GetFileExtends
         * 功能说明:取得文件后缀
         * 参    数:filename:文件名称
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string url = @"http://www.z01.com/images/zhaobus.jpg";
         *          string s = o.GetFileExtends(url);
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/
        /// <summary>
        /// 取得文件后缀
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns></returns>
        public string GetFileExtends(string filename)
        {
            string ext = null;
            if (filename.IndexOf('.') > 0)
            {
                string[] fs = filename.Split('.');
                ext = fs[fs.Length - 1];
            }
            return ext;
        }
        #endregion

        #region 获取远程文件源代码
        /**********************************
         * 函数名称:GetRemoteHtmlCode
         * 功能说明:获取远程文件源代码
         * 参    数:Url:远程url
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string url = @"http://www.baidu.com";
         *          string s = o.GetRemoteHtmlCode(url);
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/
        /// <summary>
        /// 获取远程文件源代码
        /// </summary>
        /// <param name="url">远程url</param>
        /// <returns></returns>
        public string GetRemoteHtmlCode(string Url)
        {
            string s = "";
            MSXML2.XMLHTTP _xmlhttp = new MSXML2.XMLHTTPClass();
            if (Url.Contains("?"))
            {
                Url = Url + "&dt=" + DateTime.Now.ToShortTimeString() + DateTime.Now.Millisecond;
            }
            else
            {
                Url = Url + "?dt=" + DateTime.Now.ToShortTimeString() + DateTime.Now.Millisecond;
            }
            _xmlhttp.open("GET", Url, false, null, null);
            _xmlhttp.send("");
            if (_xmlhttp.readyState == 4)
            {
                s = System.Text.Encoding.UTF8.GetString((byte[])_xmlhttp.responseBody);
            }
            return s;
        }

        #endregion

        #region 保存远程文件
        /**********************************
         * 函数名称：RemoteSave
         * 功能说明：保存远程文件
         * 参    数：Url:远程url;Path:保存到的路径
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string s = "";
         *          string url = @"http://www.z01.com/images/zhaobus.jpg";
         *          string path =Server.MapPath("Html/");
         *          s = o.RemoteSave(url,path);
         *          Response.Write(s);
         *          o.Dispose();         
         * ******************************/
        /// <summary>
        /// 保存远程文件
        /// </summary>
        /// <param name="Url">远程url</param>
        /// <param name="Path">保存到的路径</param>
        /// <returns></returns>
        public string RemoteSave(string Url, string Path)
        {
            Random ra = new Random();
            string StringFileName = DateRndName(ra);// + "." + GetFileExtends(Url);
            string StringFilePath = Path;// + StringFileName;
            MSXML2.XMLHTTP _xmlhttp = new MSXML2.XMLHTTPClass();
            _xmlhttp.open("GET", Url, false, null, null);
            _xmlhttp.send("");
            if (_xmlhttp.readyState == 4)
            {
                if (System.IO.File.Exists(StringFilePath))
                    System.IO.File.Delete(StringFilePath);
                System.IO.FileStream fs = new System.IO.FileStream(StringFilePath, System.IO.FileMode.Create);
                System.IO.BinaryWriter w = new System.IO.BinaryWriter(fs);
                w.Write((byte[])_xmlhttp.responseBody);
                w.Close();
                fs.Close();
            }
            else
                throw new Exception(_xmlhttp.statusText);
            return StringFilePath;
        }

        /// <summary>
        /// 保存远程文件(自动获取扩展名)Ext任填True/False不影响任何结果
        /// </summary>
        /// <param name="Url">远程url</param>
        /// <param name="Path">保存到的路径</param>
        /// <returns></returns>
        public string RemoteSave(string Url, string Path,bool Ext)
        {
            Random ra = new Random();
            string StringFileName = DateRndName(ra) + "." + GetFileExtends(Url);
            string StringFilePath = Path + StringFileName;
            MSXML2.XMLHTTP _xmlhttp = new MSXML2.XMLHTTPClass();
            _xmlhttp.open("GET", Url, false, null, null);
            _xmlhttp.send("");
            if (_xmlhttp.readyState == 4)
            {
                if (System.IO.File.Exists(StringFilePath))
                    System.IO.File.Delete(StringFilePath);
                System.IO.FileStream fs = new System.IO.FileStream(StringFilePath, System.IO.FileMode.Create);
                System.IO.BinaryWriter w = new System.IO.BinaryWriter(fs);
                w.Write((byte[])_xmlhttp.responseBody);
                w.Close();
                fs.Close();
            }
            else
                throw new Exception(_xmlhttp.statusText);
            return StringFilePath;
        }
        /// <summary>
        /// 将内容保存文件
        /// </summary>
        /// <param name="HtmlContent">文件内容</param>
        /// <param name="Path">路径</param>
        /// <returns></returns>
        public string SaveContent(string HtmlContent, string Path)
        {
            string StringFilePath = Path;// + StringFileName;
            if (HtmlContent!="")
            {
                if (System.IO.File.Exists(StringFilePath))
                    System.IO.File.Delete(StringFilePath);
                FileSystemObject.WriteFile(StringFilePath, HtmlContent);
            }
            return StringFilePath;
        }
        #endregion

        #region 替换网页中的换行和引号
        /**********************************
         * 函数名称:ReplaceEnter
         * 功能说明:替换网页中的换行和引号
         * 参    数:HtmlCode:html源代码
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string Url = @"http://www.z01.com";
         *          strion HtmlCode = o.GetRemoteHtmlCode(Url);
         *          string s = o.ReplaceEnter(HtmlCode);
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/
        /// <summary>
        /// 替换网页中的换行和引号
        /// </summary>
        /// <param name="HtmlCode">HTML源代码</param>
        /// <returns></returns>
        public string ReplaceEnter(string HtmlCode)
        {
            string s = "";
            if (HtmlCode == null || HtmlCode == "")
                s = "";
            else
                s = HtmlCode.Replace("\"", "");
            s = s.Replace("\r\n", "");
            return s;
        }

        #endregion

        #region 执行正则提取出值
        /**********************************
         * 函数名称:GetRegValue
         * 功能说明:执行正则提取出值
         * 参    数:HtmlCode:html源代码
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string Url = @"http://www.z01.com";
         *          strion HtmlCode = o.GetRemoteHtmlCode(Url);
         *          string s = o.ReplaceEnter(HtmlCode);
         *          string Reg="<title>.+?</title>";
         *          string GetValue=o.GetRegValue(Reg,HtmlCode)
         *          Response.Write(GetValue);
         *          o.Dispose();
         * ********************************/
        /// <summary>
        /// 执行正则提取出值
        /// </summary>
        /// <param name="RegexString">正则表达式</param>
        /// <param name="RemoteStr">HtmlCode源代码</param>
        /// <returns></returns>
        public string GetRegValue(string RegexString, string RemoteStr)
        {
            string MatchVale = "";
            Regex r = new Regex(RegexString);
            Match m = r.Match(RemoteStr);
            if (m.Success)
            {
                MatchVale = m.Value;
            }
            return MatchVale;
        }
        #endregion

        #region 替换HTML源代码
        /**********************************
         * 函数名称:RemoveHTML
         * 功能说明:替换HTML源代码
         * 参    数:HtmlCode:html源代码
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string Url = @"http://www.z01.com";
         *          strion HtmlCode = o.GetRemoteHtmlCode(Url);
         *          string s = o.ReplaceEnter(HtmlCode);
         *          string Reg="<title>.+?</title>";
         *          string GetValue=o.GetRegValue(Reg,HtmlCode)
         *          Response.Write(GetValue);
         *          o.Dispose();
         * ********************************/
        /// <summary>
        /// 替换HTML源代码
        /// </summary>
        /// <param name="HtmlCode">html源代码</param>
        /// <returns></returns>
        public string RemoveHTML(string HtmlCode)
        {
            string MatchVale = HtmlCode;
            foreach (Match s in Regex.Matches(HtmlCode, "<.+?>"))
            {
                MatchVale = MatchVale.Replace(s.Value, "");
            }
            return MatchVale;
        }

        #endregion

        #region 匹配页面的链接
        /**********************************
         * 函数名称:GetHref
         * 功能说明:匹配页面的链接
         * 参    数:HtmlCode:html源代码
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string Url = @"http://www.z01.com";
         *          strion HtmlCode = o.GetRemoteHtmlCode(Url);
         *          string s = o.GetHref(HtmlCode);
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/
        /// <summary>
        /// 获取页面的链接正则
        /// </summary>
        /// <param name="HtmlCode"></param>
        /// <returns></returns>
        public string GetHref(string HtmlCode)
        {
            string MatchVale = "";
            string Reg = @"(h|H)(r|R)(e|E)(f|F) *= *('|"")?((\w|\\|\/|\.|:|-|_)+)('|""| *|>)?";
            foreach (Match m in Regex.Matches(HtmlCode, Reg))
            {
                MatchVale += (m.Value).ToLower().Replace("href=", "").Trim() + "||";
            }
            return MatchVale;
        }
        #endregion

        #region 匹配页面的图片地址
        /**********************************
         * 函数名称:GetImgSrc
         * 功能说明:匹配页面的图片地址
         * 参    数:HtmlCode:html源代码;imgHttp:要补充的http.当比如:<img src="bb/x.gif">则要补充http://www.z01.com/,当包含http信息时,则可以为空
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string Url = @"http://www.z01.com";
         *          strion HtmlCode = o.GetRemoteHtmlCode(Url);
         *          string s = o.GetImgSrc(HtmlCode,"http://www.z01.com/");
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/
        /// <summary>
        /// 匹配页面的图片地址
        /// </summary>
        /// <param name="HtmlCode"></param>
        /// <param name="imgHttp">要补充的http://路径信息</param>
        /// <returns></returns>
        public string GetImgSrc(string HtmlCode, string imgHttp)
        {
            string MatchVale = "";
            string Reg = @"<img.+?>";
            foreach (Match m in Regex.Matches(HtmlCode, Reg))
            {
                MatchVale += GetImg((m.Value).ToLower().Trim(), imgHttp) + "||";
            }
            return MatchVale;
        }
        /// <summary>
        /// 匹配<img src="" />中的图片路径实际链接
        /// </summary>
        /// <param name="ImgString"><img src="" />字符串</param>
        /// <returns></returns>
        public string GetImg(string ImgString, string imgHttp)
        {
            string MatchVale = "";
            string Reg = @"src=.+\.(bmp|jpg|gif|png|)";
            foreach (Match m in Regex.Matches(ImgString.ToLower(), Reg))
            {
                MatchVale += (m.Value).ToLower().Trim().Replace("src=", "");
            }
            return (imgHttp + MatchVale);
        }

        #endregion

        #region 替换通过正则获取字符串所带的正则首尾匹配字符串
        /**********************************
         * 函数名称:GetHref
         * 功能说明:匹配页面的链接
         * 参    数:HtmlCode:html源代码
         * 调用示例:
         *          GetRemoteObj o = new GetRemoteObj();
         *          string Url = @"http://www.z01.com";
         *          strion HtmlCode = o.GetRemoteHtmlCode(Url);
         *          string s = o.RegReplace(HtmlCode,"<title>","</title>");
         *          Response.Write(s);
         *          o.Dispose();
         * ********************************/
        /// <summary>
        /// 替换通过正则获取字符串所带的正则首尾匹配字符串
        /// </summary>
        /// <param name="RegValue">要替换的值</param>
        /// <param name="regStart">正则匹配的首字符串</param>
        /// <param name="regEnd">正则匹配的尾字符串</param>
        /// <returns></returns>
        public string RegReplace(string RegValue, string regStart, string regEnd)
        {
            string s = RegValue;
            if (RegValue != "" && RegValue != null)
            {
                if (regStart != "" && regStart != null)
                {
                    s = s.Replace(regStart, "");
                }
                if (regEnd != "" && regEnd != null)
                {
                    s = s.Replace(regEnd, "");
                }
            }
            return s;
        }
        #endregion
    }
}

