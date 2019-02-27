using Aspose.Words;
using Aspose.Words.Saving;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Tags;
using Winista.Text.HtmlParser.Visitors;
using ZoomLa.BLL;
using ZoomLa.BLL.ECharts;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Sentiment;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model.Sentiment;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.Sentiment
{
    public partial class GetReport : System.Web.UI.Page
    {
        HtmlHelper htmlHelp = new HtmlHelper();
        IEBrowHelper ieHelp = new IEBrowHelper();
        B_Con_GetArticle getacBll = new B_Con_GetArticle();
        B_Sen_Data sdataBll = new B_Sen_Data();
        public string Skey { get { return ViewState["SKey"] as string; } set { ViewState["SKey"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_User.CheckIsLogged(Request.RawUrl);
            if (!IsPostBack)
            {
                //Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "Default.aspx'>企业办公</a></li><li><a href='Default.aspx'>舆情监测</a></li><li class='active'>数据图表</li>");
                Skey = HttpUtility.UrlDecode(Request["SKey"].TrimEnd(','));
                MyBind();
            }
        }
        public void MyBind()
        {
            string[] keyArr = Skey.Split(',');
            for (int i = 0; i < keyArr.Length; i++)
            {
                AnalyModel model = new AnalyModel();
                model.Skey = keyArr[i];
                DataTable dt = new DataTable();
                DateTime stime = DateTime.Now.AddDays(-31);
                M_Sen_Data sdataMod = sdataBll.SelLastModel(keyArr[i]);
                if (sdataMod != null)
                {
                    stime = sdataMod.CollDate > stime ? sdataMod.CollDate : stime;
                }
                if ((DateTime.Now - stime).TotalHours < 24)//24小时内已采集过,直接读数据库
                {
                    dt = sdataBll.SelByKey(keyArr[i], "", stime);
                }
                else
                {
                    dt = GetDatas(keyArr[i], 100, stime, model);
                    SaveToServer(dt);
                }
                model.CollDT = dt;
                model.FromNews = dt.Select("Source='新闻'").Length;
                model.FromBlog = dt.Select("Source='微博'").Length;
                model.FromWx = dt.Select("Source='微信'").Length;
                model.SumPie = CreateSumPie(keyArr[i], dt);
                model.TimeLine = CreateLine(keyArr[i], dt);
                model.TimePie = CreatePie(keyArr[i], dt);
                analyList.Add(model);
            }
            RPT.DataSource = analyList;
            RPT.DataBind();
        }
        public DataTable GetDatas(string key, int count, DateTime time, AnalyModel model)
        {
            DataTable dt = new DataTable();
            dt = GetBaiduNews(key, count, time);
            dt.Merge(GetBlogByBaidu(key, count, time));
            //dt.Merge(GetWXBySogou(key, count, time));
            return dt;
        }
        /*
         * pn:页数,值为10的等数差列,是过滤掉前多少个贴子
         * lm:指定时间内百度收录,值1为最近24小时,7为7天
         * rn:搜索结果显示条数,取值范围10-100之间,缺少为10(无用)
         */
        /// <summary>
        /// 抓取百度新闻关键词数据
        /// </summary>
        /// <param name="key">需要抓取的关键词</param>
        /// <param name="count">最多抓取多少条数据</param>
        /// <param name="time">抓取该时间段之后的数据</param>
        /// <returns></returns>
        public DataTable GetBaiduNews(string key, int count, DateTime time)
        {
            if (string.IsNullOrEmpty(key)) { return null; }
            int pageSize = 10;
            string baseurl = "http://news.baidu.com/ns?bs=%B7%F6%C0%CF%C8%CB&sr=0&cl=2&tn=news&ct=0&clk=sortbytime&rn=10&pn={0}&word={1}";
            DataTable dt = GetStruct(key); DateTime cdate = DateTime.Now;
            //1,根据关键词从百度上获取相关信息
            for (int p = 0; p * pageSize < count; p++)
            {
                //样本
                //<div class="result" id="1"><h3 class="c-title"><a href="http://news.qudong.com/article/329733.shtml" target="_blank" data-click="{&#10;      'f0':'77A717EA',&#10;      'f1':'9F63F1E4',&#10;      'f2':'4CA6DD6E',&#10;      'f3':'54E5343F',&#10;      't':'1462862118'&#10;      }">1万亿美金的累计收入!继苹果之后<em>微软</em>也做到了</a></h3><div class="c-summary c-row c-gap-top-small"><div class="c-span6"><a class="c_photo" href="http://news.qudong.com/article/329733.shtml" target="_blank"><img class="c-img c-img6" alt="" src="http://t12.baidu.com/it/u=2227277846,1917450397&amp;fm=82&amp;s=B78AA7E278021ED6942CE89C0300509B&amp;w=121&amp;h=81&amp;img.JPEG"></a></div><div class="c-span18 c-span-last"><p class="c-author">驱动中国&nbsp;&nbsp;1小时前</p>1万亿美金的收入这在寻常企业眼中就是一个天文数字,然而对于科技大鳄苹果<em>微软</em>来说,已然成为可能,根据<em>微软</em>在华盛顿州的纳税记录可以发现,该公司的历史累计收入在上个...  <span class="c-info"><a class="c-cache" href="http://cache.baidu.com/c?m=9d78d513d9d430d84f9e94697c1cc0116f4381132ba1d40209d6843898732f325321a3e52878564291d27d141cb2150bafb12172404067e1c694dd5dddccc375709574743647d71f45ce18afc04324c037902da8f55fb8e4&amp;p=ce7ec816d9c111a05beb8f624c0d&amp;newp=8749c54ad5c51bec17aac7710f5292695912c10e38dc8a563093&amp;user=baidu&amp;fm=sc&amp;query=%CE%A2%C8%ED%D6%D0%B9%FA&amp;qid=cdb5a37a00017e68&amp;p1=1" target="_blank" data-click="{'fm':'sc'}">百度快照</a></span></div></div></div>
                string url = string.Format(baseurl, p * pageSize, HttpUtility.UrlEncode(key));
                string html = htmlHelp.GetHtmlFromSite(url);
                HtmlPage page = htmlHelp.GetPage(html);
                int cpage = GetCurPage(page.Body); if (cpage <= p) { break; }
                Winista.Text.HtmlParser.Util.NodeList nodes = htmlHelp.GetTagList(html, "div");//以前版本为li
                nodes = nodes.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "result"));
                for (int i = 0; i < nodes.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    Div div = (Div)nodes[i].Children[1];
                    ATag a = (ATag)nodes[i].Children[0].Children.ExtractAllNodesThatMatch(new TagNameFilter("a"))[0];
                    dr["Title"] = a.StringText;
                    dr["Link"] = a.Link;
                    string author = ""; cdate = DateTime.Now;
                    GetAuthorAndDate(ref author, ref cdate, div.Children.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "c-author"), true).AsString());
                    dr["Author"] = author;
                    dr["CDate"] = DataConverter.CDate(cdate);
                    dr["Day"] = DataConverter.CDate(cdate).Day;
                    dr["Source"] = "新闻";
                    //dr["Content"] = getacBll.GetArticleFromWeb(htmlHelp.GetHtmlFromSite(a.Link),a.Link); 
                    dt.Rows.Add(dr);
                }
                if (cdate < time) { break; }
            }
            return dt;
        }
        /// <summary>
        /// 利用百度搜索,抓取百度,新浪,网易的博客数据
        /// </summary>
        public DataTable GetBlogByBaidu(string key, int count, DateTime time)
        {
            string baseurl = "http://www.baidu.com/s?rtt=2&tn=baiduwb&pn={0}&wd={1}";
            if (string.IsNullOrEmpty(key)) { return null; }
            int pageSize = 10;
            DataTable dt = GetStruct(key);
            DateTime cdate = DateTime.Now;
            for (int p = 0; p * pageSize < count; p++)//最多采100页即1000条数据
            {
                string url = string.Format(baseurl, p * pageSize, HttpUtility.UrlEncode(key));
                //string html = htmlHelp.GetHtmlFromSite(url);
                string html = htmlHelp.GetHtmlFromSite(url);
                HtmlPage page = htmlHelp.GetPage(html);
                //int cpage = GetCurPage(page.Body); if (cpage <= p) { break; }
                Winista.Text.HtmlParser.Util.NodeList nodes = htmlHelp.GetTagList(html, "li");
                nodes = nodes.ExtractAllNodesThatMatch(new HasAttributeFilter("id"));
                for (int i = 0; i < nodes.Count; i++)
                {
                    Winista.Text.HtmlParser.Util.NodeList cnodes = nodes[i].Children;
                    DataRow dr = dt.NewRow();
                    NodeFilter linkFilter = new AndFilter(new TagNameFilter("a"), new HasAttributeFilter("class", "weibo_all"));
                    ATag a = (ATag)cnodes.ExtractAllNodesThatMatch(linkFilter, true)[0];
                    dr["Title"] = "关键词:" + key;
                    dr["Link"] = a.Link;
                    dr["Author"] = cnodes.ExtractAllNodesThatMatch(new HasAttributeFilter("name", "weibo_rootnick"), true)[0].ToPlainTextString();
                    ATag datea = (ATag)cnodes.ExtractAllNodesThatMatch(new HasParentFilter(new HasAttributeFilter("class", "m")), true)[0];
                    cdate = ConverBDDate(datea.StringText);
                    dr["CDate"] = cdate;
                    dr["Day"] = cdate.Day;
                    dr["Source"] = "微博";
                    //dr["Content"] = getacBll.GetArticleFromWeb(htmlHelp.GetHtmlFromSite(a.Link),a.Link); 
                    dt.Rows.Add(dr);
                }
                if (cdate < time) { break; }
            }
            return dt;
        }
        public DataTable GetWXBySogou(string key, int count, DateTime time)
        {
            string baseurl = "http://weixin.sogou.com/weixin?type=2&query={0}&fr=sgsearch&ie=utf8&_ast=1433216256&_asf=null&w=01059900&cid=null&page={1}";
            if (string.IsNullOrEmpty(key)) { return null; }
            DataTable dt = GetStruct(key); DateTime cdate = DateTime.Now;
            for (int p = 0; p * 10 < count; p++)
            {
                string url = string.Format(baseurl, HttpUtility.UrlEncode(key), p + 1);
                string html = ieHelp.GetHtmlFromSite(url);
                HtmlPage page = htmlHelp.GetPage(html);
                //int cpage = GetCurPage(page.Body); if (cpage <= p) { break; }
                Winista.Text.HtmlParser.Util.NodeList nodes = page.Body.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "wx-rb wx-rb3"), true);
                if (nodes.Count <= 0) { break; }
                //将其序列化为模型并存入相应类中
                for (int i = 0; i < nodes.Count; i++)
                {
                    Winista.Text.HtmlParser.Util.NodeList cnodes = nodes[i].Children;
                    DataRow dr = dt.NewRow();
                    NodeFilter f_title = new AndFilter(new HasParentFilter(new TagNameFilter("h4")), new TagNameFilter("a"));
                    ATag a = (ATag)cnodes.ExtractAllNodesThatMatch(f_title, true)[0];
                    dr["Title"] = a.StringText;
                    dr["Link"] = a.Link;
                    f_title = new AndFilter(new HasAttributeFilter("id", "weixin_account"), new TagNameFilter("a"));
                    ATag author_a = (ATag)cnodes.ExtractAllNodesThatMatch(f_title, true)[0];
                    dr["Author"] = author_a.GetAttribute("title");
                    f_title = new HasAttributeFilter("class", "s-p");
                    Div div = (Div)cnodes.ExtractAllNodesThatMatch(f_title, true)[0];
                    string unixtime = div.GetAttribute("t");
                    dr["Cdate"] = GetDateTime(unixtime);
                    dr["Day"] = GetDateTime(unixtime).Day;
                    dr["Source"] = "微信";
                    dt.Rows.Add(dr);
                }
                if (cdate < time) { break; }
            }
            return dt;
        }
        public void SaveToServer(DataTable dt)
        {
            using (SqlBulkCopy bulk = new SqlBulkCopy(SqlHelper.ConnectionString))
            {
                bulk.BatchSize = 1000;
                bulk.DestinationTableName = "ZL_Sen_Data";
                bulk.ColumnMappings.Add("Title", "Title");//扩大
                bulk.ColumnMappings.Add("Source", "Source");
                bulk.ColumnMappings.Add("Author", "Author");
                bulk.ColumnMappings.Add("Link", "Link");
                bulk.ColumnMappings.Add("CDate", "CDate");
                bulk.ColumnMappings.Add("Day", "Day");
                bulk.ColumnMappings.Add("CollDate", "CollDate");
                bulk.ColumnMappings.Add("TaskID", "TaskID");
                bulk.ColumnMappings.Add("TaskInfo", "TaskInfo");//+
                bulk.WriteToServer(dt);
            }
        }
        /*-----------------------------------------------------------------------------------*/

        //获取百度的当前页码
        public int GetCurPage(Winista.Text.HtmlParser.Util.NodeList nodes)
        {
            nodes = nodes.ExtractAllNodesThatMatch(new HasAttributeFilter("id", "page"), true);
            NodeFilter filter = new AndFilter(new HasAttributeFilter("class", "pc"), new HasSiblingFilter(new HasAttributeFilter("class", "fk fk_cur")));
            nodes = nodes.ExtractAllNodesThatMatch(filter, true);
            return DataConverter.CLng(nodes.AsString());
        }
        //name  时间
        public void GetAuthorAndDate(ref string author, ref DateTime cdate, string html)
        {
            try
            {
                if (string.IsNullOrEmpty(html)) return;
                string[] arr = html.Replace("&nbsp;", "|").Split("|".ToArray(), StringSplitOptions.RemoveEmptyEntries);//包含作者和时间
                author = arr[0];
                if (arr.Length > 1)
                    cdate = ConverBDDate(arr[1]);
            }
            catch (Exception ex) { throw new Exception(html + ":" + ex.Message); }
        }
        // 将百度时间转为正常时间,如1小时前,6小时前
        public DateTime ConverBDDate(string date)
        {
            if (string.IsNullOrEmpty(date) || date.Contains("前")) { return DateTime.Now; }
            else
            {
                return Convert.ToDateTime(date);
            }
        }
        //将时间戳转换为日期
        public DateTime GetDateTime(string unixtime)
        {
            long ltime = Convert.ToInt64(unixtime + "0000000");
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.Add(new TimeSpan(ltime));
        }
        public DataTable GetStruct(string key)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Source", typeof(string));//采集来源
            dt.Columns.Add("Author", typeof(string));
            dt.Columns.Add("Link", typeof(string));
            dt.Columns.Add("CDate", typeof(DateTime));
            dt.Columns.Add("Day", typeof(int));//一月中的第几天
            dt.Columns.Add("CollDate", typeof(DateTime));//采集时间
            dt.Columns.Add("TaskID", typeof(int));//所属任务ID
            dt.Columns.Add("TaskInfo", typeof(string));//所属任务关键词
            dt.Columns["CollDate"].DefaultValue = DateTime.Now;
            dt.Columns["TaskInfo"].DefaultValue = key;
            return dt;
        }
        //生成文章来源饼图
        public string CreateSumPie(string key, DataTable dt)
        {
            ChartTitle title = new ChartTitle() { text = key, subtext = "平台对比" };
            ChartOption option = new PieChartOption(title, "");
            ChartLegend legend = new ChartLegend();
            legend.data = "新闻,微博,微信".Split(',');
            ChartData[] data_mod = new ChartData[legend.data.Length];
            for (int i = 0; i < legend.data.Length; i++)
            {
                data_mod[i] = new ChartData() { name = legend.data[i], value = dt.Select("Source='" + legend.data[i] + "'").Length };
            }
            List<ChartSeries> seriesList = new List<ChartSeries>() {
          new ChartSeries() {name = "平台对比",data_mod = data_mod}
        };
            ((PieChartOption)option).AddData(legend, seriesList, "");
            return option.ToString();
        }
        //根据文章数,按文章发布时间产生折线图
        public string CreateLine(string key, DataTable dt)
        {
            ChartTitle title = new ChartTitle() { text = key, subtext = "最近30天" };
            ChartOption option = null;
            option = new BarChartOption(title, "", "line");
            string[] days = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31".Split(',');
            int[] data_int = new int[days.Length];
            for (int i = 0; i < days.Length; i++)
            {
                data_int[i] = dt.Select("Day=" + (i + 1)).Length;
            }
            List<ChartSeries> seriesList = new List<ChartSeries>() {//这里的统计算法不对,需要统计出文章数
          new ChartSeries() {
                name = "文章数",
                type="line",
                data_int = data_int
            }
        };
            ((BarChartOption)option).AddData(days, seriesList, "");
            option.tooltip.formatter = null;//使用默认格式
            return option.ToString();
        }
        //根据文章发布时间产生扩散速度图
        public string CreatePie(string key, DataTable dt)
        {
            ChartTitle title = new ChartTitle() { text = key, subtext = "扩散速度" };
            ChartOption option = new PieChartOption(title, "");
            ChartLegend legend = new ChartLegend();
            legend.data = "24小时内,72小时内,一周内,30天内,30天外".Split(',');
            ChartData[] data_mod = new ChartData[5];
            DateTime time_24 = DateTime.Now.AddHours(-24), time_72 = DateTime.Now.AddHours(-72), time_d7 = DateTime.Now.AddDays(-7), time_d30 = DateTime.Now.AddMonths(-1);
            string sql = "CDate>=#{0}# AND CDate<=#{1}#";
            data_mod[0] = (new ChartData() { name = legend.data[0], value = dt.Select("CDate>=#" + time_24 + "#").Length });
            data_mod[1] = (new ChartData() { name = legend.data[1], value = dt.Select(string.Format(sql, time_72, time_24)).Length });
            data_mod[2] = (new ChartData() { name = legend.data[2], value = dt.Select(string.Format(sql, time_d7, time_72)).Length });
            data_mod[3] = (new ChartData() { name = legend.data[3], value = dt.Select(string.Format(sql, time_d30, time_d7)).Length });
            data_mod[4] = (new ChartData() { name = legend.data[4], value = dt.Select("CDate<=#" + time_d30 + "#").Length });
            List<ChartSeries> seriesList = new List<ChartSeries>() {
          new ChartSeries() {
                name = "扩散速度",
                data_mod = data_mod
            }
        };
            ((PieChartOption)option).AddData(legend, seriesList, "empy");
            return option.ToString();
        }
        //-------------------页面
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EGV.PageIndex = e.NewPageIndex;
            MyBind();
        }
        protected void Skey_Btn_Click(object sender, EventArgs e)
        {
            Skey = T1.Text;
            MyBind();
        }
        //-------------------
        List<AnalyModel> analyList = new List<AnalyModel>();
        //word模板-->子iframe-->收集base64-->提交-->baseToImg-->替换域-->生成word-->返回下载
        protected void WordReport_Btn_Click(object sender, EventArgs e)
        {
            //data:image/png;base64,
            ImgHelper imgHelp = new ImgHelper();
            string key = skey_hid.Value;
            string title = DateTime.Now.ToString("yyyyMMdd[" + key + "]报告");
            string copyright = "来自于" + Call.SiteName + "," + "http://" + Request.Url.Host + "/";
            string copyright2 = "基于Zoomla!逐浪©CMS大数据挖掘分析平台生成,签名标记:" + SiteConfig.SiteOption.SenSign ?? "";
            sumpie_hid.Value = Regex.Split(sumpie_hid.Value, Regex.Unescape("base64,"))[1];
            timeline_hid.Value = Regex.Split(timeline_hid.Value, Regex.Unescape("base64,"))[1];
            timepie_hid.Value = Regex.Split(timepie_hid.Value, Regex.Unescape("base64,"))[1];
            string sumpie = "/UploadFiles/Report/sumpie" + key + ".jpg", timeline = "/UploadFiles/Report/timeline" + key + ".jpg", timepie = "/UploadFiles/Report/timepie" + key + ".jpg",
                wordpath = "/UploadFiles/Report/" + title + ".docx";
            imgHelp.Base64ToImg(sumpie, sumpie_hid.Value);
            imgHelp.Base64ToImg(timeline, timeline_hid.Value);
            imgHelp.Base64ToImg(timepie, timepie_hid.Value);
            //------生成Word
            var doc = new Aspose.Words.Document(Server.MapPath("tlp.docx"));
            string[] fieldNames = "title,sumstr,sumpie,timeline,timepie,copyright,copyright2".Split(',');
            object[] fieldValues = new object[] { title, sumstr_hid.Value, Server.MapPath(sumpie), Server.MapPath(timeline), Server.MapPath(timepie), copyright, copyright2 };
            doc.MailMerge.Execute(fieldNames, fieldValues);
            DataTable dt = sdataBll.SelByKey(key, "");
            dt = dt.DefaultView.ToTable(false, "Title", "Source", "Link");
            DataRow dr = dt.NewRow();
            dr["Title"] = "标题";
            dr["Source"] = "来源";
            dr["Link"] = "链接";
            dt.Rows.InsertAt(dr, 0);
            doc = SetWord(doc, dt);
            doc.Save(Server.MapPath(wordpath), SaveOptions.CreateSaveOptions(Aspose.Words.SaveFormat.Docx));
            SafeSC.DownFile(wordpath);
        }
        public Document SetWord(Document doc, DataTable dt)
        {
            //要取得目标的DataTable
            Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);
            //List<double> widthList = new List<double>();
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    builder.MoveToCell(0, 0, i, 0); //移动单元格
            //    double width = builder.CellFormat.Width;//获取单元格宽度
            //    widthList.Add(width);
            //}
            builder.MoveToBookmark("table");        //开始添加值
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    builder.InsertCell();// 添加一个单元格                    
                    builder.CellFormat.Borders.LineStyle = LineStyle.Single;
                    builder.CellFormat.Borders.Color = System.Drawing.Color.Black;
                    //builder.CellFormat.Width = width[j];
                    builder.CellFormat.VerticalMerge = Aspose.Words.Tables.CellMerge.None;
                    builder.CellFormat.VerticalAlignment = Aspose.Words.Tables.CellVerticalAlignment.Center;//垂直居中对齐
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//水平居中对齐
                    builder.Write(dt.Rows[i][j].ToString());
                }
                builder.EndRow();
            }
            doc.Range.Bookmarks["table"].Text = "";    // 清掉标示  
            return doc;
        }
        protected void RPT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                AnalyModel dr = (AnalyModel)e.Item.DataItem;
                DataTable dt = sdataBll.SelByKey(dr.Skey, "");
                Repeater rep = e.Item.FindControl("LinkRPT") as Repeater;
                rep.DataSource = dt;
                rep.DataBind();
            }
        }
    }
    public class AnalyModel
    {
        //关键词
        public string Skey;
        public int CollCount { get { return FromNews + FromBlog + FromWx; } }
        public int FromNews;
        public int FromBlog;
        public int FromWx;
        public string SumPie, TimeLine, TimePie;
        //关键词数据表
        public DataTable CollDT = new DataTable();
    }
}