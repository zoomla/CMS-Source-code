using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Winista.Text.HtmlParser.Tags;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Visitors;
using ZoomLa.BLL.Helper;

namespace ZoomLa.BLL.Sentiment
{
    /*
     * 从主流新闻站获取文章,用于一键转载,与yu情监测
     */ 
    public class B_Con_GetArticle
    {
        HtmlHelper htmlHelper = new HtmlHelper();
        public string GetArticleFromWeb(string html, string url)
        {
            string titleTlp = "<h1 style='text-align: center;'>{0}</h1>";
            url = url.ToLower();
            HtmlPage page = htmlHelper.GetPage(html);
            NodeList nodelist = htmlHelper.GetTagList(html, "div");
            string result = "";
            if (url.Contains(".sina."))
            {
                result = GetSinaBody(nodelist);
            }
            else if (url.Contains(".ifeng."))
            {
                result = GetFengBody(nodelist);
            }
            else if (url.Contains(".qq."))
            {
                result = GetQQBody(nodelist);
            }
            else if (url.Contains(".sohu"))
            {
                result = GetSohuBody(nodelist);
            }
            else if (url.Contains(".people."))
            {
                result = getPeopleBody(nodelist);
            }
            else if (url.Contains(".xinhuanet"))
            {
                result = getXinhuanetBody(nodelist);
            }
            else if (url.Contains(".163."))
            {
                result = Get163Body(nodelist);
            }
            else if (url.Contains(".cntv."))
            {
                result = GetCctvBody(nodelist);
            }
            else if (url.Contains("baijia.baidu."))
            {
                result = GetBaijiaBody(nodelist);
            }
            else if (url.Contains(".baidu."))
            {
                result = GetBaiduBody(nodelist);
            }
            if (string.IsNullOrEmpty(result))
            {
                result = htmlHelper.GetBodyHtml(html);
            }
            result = htmlHelper.ReplaceChinaChar(result);
            return string.Format(titleTlp, page.Title) + result;
        }
        //腾讯
        public string GetQQBody(NodeList nodelist)
        {
            string result = "";
            for (int i = 0; i < nodelist.Size(); i++)
            {
                Div div = (Div)nodelist[i];
                string id = div.GetAttribute("id");
                string css = div.GetAttribute("class");
                id = string.IsNullOrEmpty(id) ? "" : id.ToLower();
                css = string.IsNullOrEmpty(css) ? "" : css.ToLower();
                if (id.Equals("Cnt-Main-Article-QQ".ToLower()))//要闻
                {
                    result = div.ToHtml(); break;
                }
                else if (id.Equals("articleContent".ToLower()))//今日话题
                {
                    result = div.ToHtml(); break;
                }
                else if (css.Contains("article_mod".ToLower()))
                {
                    result = div.ToHtml(); break;
                }
                else if (id.Equals("slide_bigimage_temp".ToLower()))//单图浏览
                {
                    result = div.ToHtml(); break;
                }
                else if (css.Contains("box-left"))
                {
                    result = div.ToHtml(); break;
                }

            }
            return result;
        }
        //凤凰网
        public string GetFengBody(NodeList nodelist)
        {
            //HasAttributeFilter filter = new HasAttributeFilter("id", "artical_real");
            //nodelist = nodelist.ExtractAllNodesThatMatch(filter);
            //return nodelist.AsHtml();
            string result = "";
            for (int i = 0; i < nodelist.Size(); i++)
            {
                Div div = (Div)nodelist[i];
                string id = div.GetAttribute("id");
                string css = div.GetAttribute("class");
                id = string.IsNullOrEmpty(id) ? "" : id.ToLower();
                css = string.IsNullOrEmpty(css) ? "" : css.ToLower();
                if (id.Equals("artical_real"))//资讯、财经、娱乐、体育、时尚、科技、读书、教育、历史、军事、佛教、旅游、游戏、数码、健康、亲子、家居、星座
                {
                    result = div.ToHtml(); break;
                }
                else if (id.Equals("artical"))//文化
                {
                    result = div.ToHtml();
                }
                else if (id.Equals("blog_article_content"))//博客
                {
                    result = div.ToHtml();
                }
                else if (css.Contains("gc-art-body"))//汽车的介绍
                {
                    result = div.ToHtml(); break;
                }
                else if (css.Contains("ce-main mt12"))//汽车的评价规范
                {
                    result = div.ToHtml(); break;
                }
                else if (css.Contains("arl-mian fl"))//汽车的文章
                {
                    result = div.ToHtml();
                }
                else if (css.Contains("article") && id != "")//房产的文章
                {
                    result = div.ToHtml();
                }
                else if (css.Contains("e_i_deta_LX"))//彩票
                {
                    result = div.ToHtml();
                }
            }
            return result;
        }
        //搜狐
        public string GetSohuBody(NodeList nodelist)
        {
            string result = "";
            for (int i = 0; i < nodelist.Size(); i++)
            {
                Div div = (Div)nodelist[i];
                string id = div.GetAttribute("id");
                string css = div.GetAttribute("class");
                id = string.IsNullOrEmpty(id) ? "" : id.ToLower();
                css = string.IsNullOrEmpty(css) ? "" : css.ToLower();
                if (id.Equals("entry"))//博客
                {
                    result = div.ToHtml();
                }
                else if (css.Contains("text clear"))//新闻、军事、文化、历史、体育、社会评论、读书、财经、股票、科技、汽车、时尚、健康、教育、母婴、旅游、美食、星座
                {
                    result = div.ToHtml();
                }
                else if (css.Contains("new-detail-left"))//房产
                {
                    //result = div.ToHtml();
                }

            }
            return result;
        }
        //人民网
        public string getPeopleBody(NodeList nodelist)
        {
            string result = "";
            for (int i = 0; i < nodelist.Size(); i++)
            {
                Div div = (Div)nodelist[i];
                string id = div.GetAttribute("id");
                string css = div.GetAttribute("class");
                id = string.IsNullOrEmpty(id) ? "" : id.ToLower();
                css = string.IsNullOrEmpty(css) ? "" : css.ToLower();
                if (id.Equals("p_content"))//新闻：时政、社会、法治；地方：领导、旅游、人物；财经：理财、股票、能源；央企：环保、公益、彩票；教育：科技、文史、收藏、娱乐；观点：传媒、舆情；国际：台湾、港澳、军事；汽车：IT、通信、家电；房产：食品、健康、时尚；文化：读书、体育、游戏、红木、棋牌
                {
                    result = div.ToHtml();
                }
                else if (id.Equals("ft_contwrap"))//访谈
                {
                    result = div.ToHtml();
                }
                else if (css.Contains("text_show"))//理论
                {
                    result = div.ToHtml();
                }
            }

            return result;
        }
        //新华网
        public string getXinhuanetBody(NodeList nodelist)
        {
            string result = "";
            for (int i = 0; i < nodelist.Size(); i++)
            {
                Div div = (Div)nodelist[i];
                string id = div.GetAttribute("id");
                string css = div.GetAttribute("class");

                id = string.IsNullOrEmpty(id) ? "" : id.ToLower();
                css = string.IsNullOrEmpty(css) ? "" : css.ToLower();
                if (id.Equals("article")) //时政、法治；国际；财经、汽车、娱乐、时尚、信息化、人事、理论、港澳、日本、教育、科技、能源、食品、旅游、健康、公益、舆情、人才、更多
                {
                    result = div.ToHtml();
                }
                else if (id.Equals("content"))//国际、华人、军事、房产、体育、资料、高层、港澳、台湾、新加坡
                {
                    result = div.ToHtml();
                }
                else if (id.Equals("matrix"))//博客
                {
                    result = div.ToHtml();
                }
                else if (id.Equals("contentblock"))//资料
                {
                    result = div.ToHtml();
                }
                else if (id.Equals("news_content"))//炫空间                
                {
                    result = div.ToHtml();
                }
                else if (id.Equals("detail-content"))//马来西亚
                {
                    result = div.ToHtml();
                }
                else if (id.Equals("message_1"))//读史
                {
                    result = div.ToHtml();
                }
                else if (css.Contains("main pagewidth")) //地方
                {
                    result = div.ToHtml();
                }
                else if (css.Contains("main_content_wrap"))
                {
                    result = div.ToHtml();
                }
                else if (css.Contains("wrap"))//微观中国
                {
                    result = div.ToHtml();
                }
                //else if (css.Equals("content"))//信息化
                //{
                //    result = div.ToHtml();
                //}
            }

            return result;
        }
        //网易
        public string Get163Body(NodeList nodelist)
        {
            string result = "";
            for (int i = 0; i < nodelist.Size(); i++)
            {
                Div div = (Div)nodelist[i];
                string id = div.GetAttribute("id");
                string css = div.GetAttribute("class");
                id = string.IsNullOrEmpty(id) ? "" : id.ToLower();
                css = string.IsNullOrEmpty(css) ? "" : css.ToLower();
                if (id.Equals("endText".ToLower()))//普通内容
                {
                    result = div.ToHtml(); break;
                }
                else if (css.Contains("con_1".ToLower()))
                {
                    result = div.ToHtml(); break;
                }
                else if (css.Contains("vdb-plr8".ToLower()))
                {
                    result = div.ToHtml(); break;
                }
                else if (css.Contains("m-introbox".ToLower()))
                {
                    result = div.ToHtml(); break;
                }
                else if (css.Contains("txtcont".ToLower()))
                {
                    result = div.ToHtml(); break;
                }
            }
            return result;
        }
        //cctv
        public string GetCctvBody(NodeList nodelist)
        {
            string result = "";
            for (int i = 0; i < nodelist.Size(); i++)
            {
                Div div = (Div)nodelist[i];
                string id = div.GetAttribute("id");
                string css = div.GetAttribute("class");
                id = string.IsNullOrEmpty(id) ? "" : id.ToLower();
                css = string.IsNullOrEmpty(css) ? "" : css.ToLower();
                if (id.Equals("content_body"))//少儿 
                {
                    result = div.ToHtml();
                }
                else if (id.Equals("content"))//电影、彩票
                {
                    result = div.ToHtml();
                }
                else if (css.Contains("col_w660"))//新闻、国内、国际；经济、军事、评论、娱乐、少儿、书画、旅游、汽车、时尚、历史、农业、健康、综艺、戏曲、音乐、
                {
                    result = div.ToHtml();
                }
                else if (css.Contains("col_650"))//评论
                {
                    result = div.ToHtml();
                }
                else if (css.Contains("textcontent"))//游戏
                {
                    result = div.ToHtml();
                }
            }

            return result;
        }
        //百度新闻
        public string GetBaiduBody(NodeList nodelist)
        {
            string result = "";
            for (int i = 0; i < nodelist.Size(); i++)
            {
                Div div = (Div)nodelist[i];
                string id = div.GetAttribute("id");
                string css = div.GetAttribute("class");
                id = string.IsNullOrEmpty(id) ? "" : id.ToLower();
                css = string.IsNullOrEmpty(css) ? "" : css.ToLower();
                if (id.Equals("artical"))//普通新闻
                {
                    result = div.ToHtml();
                }

            }

            return result;
        }
        //百度百家
        public string GetBaijiaBody(NodeList nodelist)
        {
            string result = "";
            for (int i = 0; i < nodelist.Size(); i++)
            {
                Div div = (Div)nodelist[i];
                string id = div.GetAttribute("id");
                string css = div.GetAttribute("class");
                id = string.IsNullOrEmpty(id) ? "" : id.ToLower();
                css = string.IsNullOrEmpty(css) ? "" : css.ToLower();
                if (id.Equals("page"))//百度百家
                {
                    result = div.ToHtml();
                }

            }

            return result;
        }
        //新浪
        public string GetSinaBody(NodeList nodelist)
        {
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
                    result = div.ToHtml(); break;
                }
                else if (css.Contains("articalcontent"))//历史板块文章,博客内容采集
                {
                    result = div.ToHtml(); break;
                }
                //else if (id.Equals("si_cont"))//图库,这个是其加载完成后才会载入的,需要用其他方式获取Html,如Iframe方法,ONLoad后再取其值
                //{
                //    NodeList imgList = GetTagList(div.Children, "IMG");
                //}
            }
            return result;
        }

    }
}
