using CDO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Tags;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Visitors;
using ZoomLa.Common;
using System.Windows.Forms;
using System.Text.RegularExpressions;
/*
* Html相关,基于HtmlParser
*/
namespace ZoomLa.BLL.Helper
{
    public class HtmlHelper
    {
        RegexHelper regHelper = new RegexHelper();
        HttpHelper httpHelp = new HttpHelper();
        public string baseurl = "";
        public string vdir = "/UploadFiles/DownFile/";
        public string GetHtmlFromSite(string url)
        {
            //获取或设置用于对向   internet   资源的请求进行身份验证的网络凭据。（可有可无） 
            //wb.credentials=credentialcache.defaultcredentials;  
            url = url.Trim();//勿修改Url避免大小写敏感
            if (string.IsNullOrEmpty(url)) { throw new Exception("链接地址为空"); }
            if (!url.ToLower().StartsWith("http://") && !url.ToLower().StartsWith("https://")) { throw new Exception(url + "并非有效的Http地址链接"); }
            try
            {
                WebClient wb = new WebClient();
                byte[] htmlData = wb.DownloadData(url);
                string result = Encoding.UTF8.GetString(htmlData);
                string head = regHelper.GetValueBySE(result, "<head>", "</head>").ToLower();
                foreach (Match item in regHelper.GetValuesBySE(head, "<meta", ">"))
                {
                    string metastr = item.Value;
                    if (metastr.Contains("charset=gb2312") || metastr.Contains("charset=\"gb2312\"") || metastr.Contains("charset=gbk") || metastr.Contains("charset=\"gbk\""))
                    {
                        result = Encoding.Default.GetString(htmlData);
                        break;
                    }
                }
                return result;
            }
            catch (Exception ex) { throw new Exception(url + ":" + ex.Message); }
        }
        //根据各种筛选条件,获取到需要的元素,后其看是否改为全Filter
        public string GetByFilter(string html, FilterModel model)//OR与AND都只能同时接受两个
        {
            string result = "";
            if (model.EType.ToLower().Equals("title"))
            {
                return GetTitle(html);
            }
            NodeList nodes = GetTagList(html, model.EType);
            if (!string.IsNullOrEmpty(model.ID))
            {
                HasAttributeFilter filter = new HasAttributeFilter("id", model.ID);
                nodes = nodes.ExtractAllNodesThatMatch(filter);
            }
            if (!string.IsNullOrEmpty(model.CSS))
            {
                HasAttributeFilter filter = new HasAttributeFilter("class", model.CSS);
                nodes = nodes.ExtractAllNodesThatMatch(filter);
            }
            if (!model.AllowScript)
            {
                TagNameFilter filter = new TagNameFilter("script");
                nodes.ExtractAllNodesThatMatch(filter, true);
            }
            //将图片文件本地化
            {
                TagNameFilter filter = new TagNameFilter("img");
                NodeList imgs = nodes.ExtractAllNodesThatMatch(filter, true);
                for (int i = 0; i < imgs.Count; i++)
                {
                    ImageTag img = imgs[i] as ImageTag;
                    string savepath = function.VToP(vdir + Path.GetFileName(img.ImageURL));
                    if (File.Exists(savepath)) { continue; }//避免图片重复下载
                    img.ImageURL = httpHelp.DownFile(baseurl, img.ImageURL, savepath);
                }
            }
            result = nodes.AsHtml();
            if (!string.IsNullOrWhiteSpace(model.Start) && !string.IsNullOrWhiteSpace(model.End))
            {
                result = regHelper.GetValueBySE(result, model.Start, model.End);
            }
            return result;
        }
        /// <summary>
        /// 替换其中的中文符号,如左右括号等
        /// </summary>
        public string ReplaceChinaChar(string article)
        {
            article = article.Replace("&#65288;", "(").Replace("&#65289;", ")").Replace("&#65292;", ",").Replace("&#65306;", "：").Replace("&#65311;", "?").Replace("&#65281;", "!");
            return article;
        }
        /// <summary>
        /// 获取BodyHtml,去除Script
        /// </summary>
        public string GetBodyHtml(string html)
        {
            HtmlPage page = GetPage(html);
            NodeList nodelist = page.Body;
            NodeFilter filter = new TagNameFilter("script");
            NodeList childnode = nodelist.ExtractAllNodesThatMatch(filter, true);
            for (int i = 0; i < childnode.Size(); i++)
            {
                nodelist.Remove(childnode[i]);
            }
            return nodelist.ToHtml();
        }
        /// <summary>
        /// 将img图片路径转为网路完整的图片路径
        /// </summary>
        /// <param name="html">需要转换的内容</param>
        /// <param name="url">替换站点路径:http://www.z01.com</param>
        /// <returns></returns>
        public string ConvertImgUrl(string html, string url)
        {
            if (string.IsNullOrEmpty(html)||string.IsNullOrEmpty(url)) return html;
            HtmlPage page = GetPage("<html><body>" + html + "</body></html>");
            Winista.Text.HtmlParser.Util.NodeList nodes = page.Body.ExtractAllNodesThatMatch(new TagNameFilter("IMG"), true);
            for (int i = 0; i < nodes.Count; i++)
            {
                ImageTag image = (ImageTag)nodes[i];
                if (!image.ImageURL.ToLower().Contains("://"))
                {
                    image.ImageURL = url.TrimEnd('/') + ("/" + image.ImageURL.TrimStart('/'));
                }
            }
            return page.Body.ToHtml();
        }
        //获取标题
        public string GetTitle(string html)
        {
            HtmlPage page = GetPage(html);
            return page.Title;
        }
        /// <summary>
        /// 从Html中获取所有超链接,必须以<html>包裹,
        /// </summary>
        /// <param name="html">需要筛选的Html代码</param>
        /// <param name="pre">链接前加</param>
        /// <param name="end">链接后加</param>
        /// <returns></returns>
        public string GetAllLink(string html, string pre = "", string end = "")
        {
            string list = "";
            NodeList nodeList = GetTagList(html, "A");
            for (int i = 0; i < nodeList.Size(); i++)
            {
                ATag link = (ATag)nodeList.ElementAt(i);
                string href = link.GetAttribute("href");
                if (string.IsNullOrEmpty(href) || href.ToLower().IndexOf("javascript") > -1) { continue; }
                list += (pre + link.GetAttribute("href") + end) + "\n";
            }
            return list;
        }
        /// <summary>
        /// 传入整个Html返回匹配的节点
        /// </summary>
        /// <param name="html">整页Html</param>
        /// <param name="tag">标记类型  IMG,A</param>
        /// <returns></returns>
        public NodeList GetTagList(string html, string tag)
        {
            HtmlPage page = GetPage(html);
            NodeList nodelist = page.Body;
            return GetTagList(nodelist, tag);
        }
        private NodeList GetTagList(NodeList nodelist, string tag)
        {
            nodelist = nodelist.ExtractAllNodesThatMatch(new TagNameFilter(tag), true);
            return nodelist;
        }
        public HtmlPage GetPage(string html)
        {
            return WistaHelper.GetPage(html);
        }
        //下载并存为mht格式
        public string DownToMHT(string url, string vpath)
        {
            if (!url.Contains("://")) { throw new Exception("地址必须以http或https开头"); }
            vpath = SafeSC.PathDeal(vpath).Replace("#", "井").Replace("-", "");//去除掉空格等,否则客户端打开会报错
            string ppath = function.VToP(vpath);
            string dir = Path.GetDirectoryName(ppath);
            if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
            CDO.Message msg = new CDO.MessageClass();
            CDO.Configuration c = new CDO.ConfigurationClass();
            msg.Configuration = c;
            msg.CreateMHTMLBody(url, CdoMHTMLFlags.cdoSuppressNone, "", "");//cdoSuppressNone=将全部资源都打包进入,CdoMHTMLFlags.cdoSuppressAll=只保留纯页面
            msg.GetStream().SaveToFile(ppath,ADODB.SaveOptionsEnum.adSaveCreateOverWrite);
            return vpath;
        }
        #region 获取指定网站的新闻信息,已固定
        //------------------
        //获取新浪的内容页,id=artibody
        //------------------
        public string GetSinaArticle(string html)
        {
            string titleTlp = "<h1 style='text-align: center;'>{0}</h1>";
            HtmlPage page = GetPage(html);
            NodeList nodelist = GetTagList(html, "div");
            string result = "";
            for (int i = 0; i < nodelist.Size(); i++)
            {
                Div div = (Div)nodelist[i];
                string id = div.GetAttribute("id");
                string css = div.GetAttribute("class");
                id = string.IsNullOrEmpty(id) ? "" : id.ToLower();
                css = string.IsNullOrEmpty(css) ? "" : css.ToLower();
                if (id.Equals("artibody"))//普通文章
                {
                    result = string.Format(titleTlp, page.Title) + div.ToHtml(); break;
                }
                else if (css.Contains("articalcontent"))//历史板块文章,博客内容采集
                {
                    result = string.Format(titleTlp, page.Title) + div.ToHtml(); break;
                }
                //else if (id.Equals("si_cont"))//图库,这个是其加载完成后才会载入的,需要用其他方式获取Html,如Iframe方法,ONLoad后再取其值
                //{
                //    NodeList imgList = GetTagList(div.Children, "IMG");
                //}
            }
            if (string.IsNullOrEmpty(result))
            {
                result = GetBodyHtml(html);
            }
            result = ReplaceChinaChar(result);
            return result;
        }
        #endregion
    }
    public class FilterModel
    {
        //不允许为空,可多选
        public string EType { get; set; }
        public string ID { get; set; }
        public string CSS { get; set; }
        //开始与结束字符串,最后筛远队列
        public string Start { get; set; }
        public string End { get; set; }
        public bool AllowScript { get; set; }
    }
    //使用内置的IE浏览 器完成操作
    public class IEBrowHelper
    {
        Bitmap m_Bitmap;
        private string html, stype = "img";
        string m_Url;
        int m_BrowserWidth, m_BrowserHeight, m_ThumbnailWidth, m_ThumbnailHeight;
        public IEBrowHelper() { }
        public IEBrowHelper(string Url, int BrowserWidth, int BrowserHeight, int ThumbnailWidth, int ThumbnailHeight)
        {
            m_Url = Url;
            m_BrowserHeight = BrowserHeight;
            m_BrowserWidth = BrowserWidth;
            m_ThumbnailWidth = ThumbnailWidth;
            m_ThumbnailHeight = ThumbnailHeight;
        }
        public Bitmap GetWebSiteThumbnail(string Url, int BrowserWidth, int BrowserHeight, int ThumbnailWidth, int ThumbnailHeight)
        {
            IEBrowHelper thumbnailGenerator = new IEBrowHelper(Url, BrowserWidth, BrowserHeight, ThumbnailWidth, ThumbnailHeight);
            return thumbnailGenerator.GenerateWebSiteThumbnailImage();
        }
        //将指定网页生成图片,注意点见帮助手册
        public Bitmap GenerateWebSiteThumbnailImage()
        {
            stype = "img";
            Thread m_thread = new Thread(new ThreadStart(_GenerateWebSiteThumbnailImage));
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
            m_thread.Join();
            return m_Bitmap;
        }
        public string GetHtmlFromSite(string url)
        {
            m_Url = url;
            stype = "html";
            Thread m_thread = new Thread(new ThreadStart(_GenerateWebSiteThumbnailImage));
            m_thread.SetApartmentState(ApartmentState.STA);
            m_thread.Start();
            m_thread.Join();
            return html;
        }
        private void _GenerateWebSiteThumbnailImage()
        {
            WebBrowser m_WebBrowser = new WebBrowser();
            m_WebBrowser.ScrollBarsEnabled = false;
            m_WebBrowser.Navigate(m_Url);
            m_WebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WebBrowser_DocumentCompleted);
            while (m_WebBrowser.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();
            m_WebBrowser.Dispose();
        }
        private void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser m_WebBrowser = (WebBrowser)sender;
            switch (stype)
            {
                case "img":
                    m_WebBrowser.ClientSize = new Size(this.m_BrowserWidth, this.m_BrowserHeight);
                    m_WebBrowser.ScrollBarsEnabled = false;
                    m_Bitmap = new Bitmap(m_WebBrowser.Bounds.Width, m_WebBrowser.Bounds.Height);
                    m_WebBrowser.BringToFront();
                    m_WebBrowser.DrawToBitmap(m_Bitmap, m_WebBrowser.Bounds);
                    m_Bitmap = (Bitmap)m_Bitmap.GetThumbnailImage(m_ThumbnailWidth, m_ThumbnailHeight, null, IntPtr.Zero);
                    break;
                case "html":
                    StreamReader sr = new StreamReader(m_WebBrowser.DocumentStream);
                    html = sr.ReadToEnd();
                    break;
            }
        }
    }
}
