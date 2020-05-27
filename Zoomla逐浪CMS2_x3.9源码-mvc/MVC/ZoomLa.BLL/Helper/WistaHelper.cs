using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Visitors;
/*
 * doms.AsString():只输出doms中的文本信息,同于.text();
 * doms.AsHtml():包含所筛选到的标签的html
 * doms.ToHtml()==doms.AsHtml();
 * GetChild(),获取元素下所有子元素html
 */

namespace ZoomLa.BLL.Helper
{
    public class WistaHelper
    {
        /// <summary>
        /// 示例:"#test_div span .need [disabled='disabled']"
        /// //不支持多条件筛选如:div[name='test'],不支持>筛选
        /// </summary>
        /// <param name="html">html字符串,需要以html标签包裹</param>
        /// <param name="queryStr">同于jquery筛选</param>
        /// <returns></returns>
        public static NodeList GetDomsFromHtml(string html, string queryStr)
        {
            //id与class等测试大小写不敏感
            HtmlPage page = GetPage(html);
            NodeList doms = page.Body;
            string[] queryArr = queryStr.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < queryArr.Length; i++)
            {
                string query = queryArr[i];
                if (query.StartsWith("#"))
                {
                    string id = query.Replace("#", "");
                    doms = doms.ExtractAllNodesThatMatch(new HasAttributeFilter("id", id), true);
                }
                else if (query.StartsWith("."))
                {
                    string css = query.Replace(".", "");
                    doms = doms.ExtractAllNodesThatMatch(new HasAttributeFilter("class", css), true);
                }
                else if (query.StartsWith("["))
                {
                    // "[name='test']"
                    string[] attr = query.Substring(1, query.Length - 2).Split('=');
                    string name = attr[0];
                    string value = attr[1].Substring(1, attr[1].Length - 2);//去除单引号
                    doms = doms.ExtractAllNodesThatMatch(new HasAttributeFilter(name, value), true);
                }
                else //按标签查询
                {
                    string tag = query;
                    doms = doms.ExtractAllNodesThatMatch(new TagNameFilter(tag), true);
                }
            }
            return doms;
        }
        public static string GetChild(NodeList nodes)
        {
            string html = nodes.ToHtml();
            int start = html.IndexOf(">") + 1;
            int end = html.LastIndexOf("<");
            return html.Substring(start, end - start);
        }
        public static HtmlPage GetPage(string html)
        {
            if (!html.StartsWith("<html", StringComparison.CurrentCultureIgnoreCase))
            {
                html = "<html><body>" + html + "</body></html>";
            }
            Parser parser = Parser.CreateParser(html, "UTF-8");
            HtmlPage page = new HtmlPage(parser);
            parser.VisitAllNodesWith(page);
            return page;
        }
    }
}
