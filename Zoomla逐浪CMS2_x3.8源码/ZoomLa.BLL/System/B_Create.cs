using System;
using System.Data;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Web;
using ZoomLa.Model;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Threading;
using System.Xml;
using ZoomLa.BLL.CreateJS;

namespace ZoomLa.BLL
{
    public class B_Create
    {
        private B_Model bmodel = new B_Model();
        private B_Node nodeBll = new B_Node();
        private B_Content bContent = new B_Content();
        private B_CreateHtml bll;
        private B_User buser = new B_User();
        private string SitePhyPath = AppDomain.CurrentDomain.BaseDirectory;
        public B_Create()
        {
            bll = new B_CreateHtml();
        }
        public B_Create(HttpRequest r)
        {
            bll = new B_CreateHtml(r);
        }
        private string GetParentDir(int NodeID)
        {
            string NodeDir = "";
            M_Node nodeinfo = nodeBll.GetNodeXML(NodeID);
            if (!nodeinfo.IsNull)
            {
                if (nodeinfo.ParentID > 0 && nodeinfo.HtmlPosition > 0)
                {
                    NodeDir = GetParentDir(nodeinfo.ParentID) + "/" + nodeinfo.NodeDir;
                }
                else
                {
                    NodeDir = "/" + nodeinfo.NodeDir;
                }
            }
            return NodeDir;
        }
        /// <summary>
        /// 创建所有静态页面
        /// </summary>
        /// <param name="InfoID">信息ID</param>
        /// <param name="NodeID">节点ID</param>
        /// <param name="ModelID">模型ID</param>
        public void CreateInfo(int InfoID, int NodeID, int ModelID)
        {
            //GetTmplate要统一一个方法处理,用于避免获取模板的逻辑不上,具体逻辑见页面
            B_Label labelBll = new B_Label();
            M_Node nodeinfo = nodeBll.GetNodeXML(NodeID);
            string TemplateDir = SiteConfig.SiteOption.TemplateDir + "/";
            string GeneratedDirectory = SiteConfig.SiteOption.GeneratedDirectory;
            //要存放的目录
            string NodeDir = string.IsNullOrEmpty(nodeinfo.NodeDir) ? "" : ("/" + nodeinfo.NodeDir);
            if (nodeinfo.HtmlPosition > 0 && nodeinfo.ParentID > 0)
            {
                NodeDir = GetParentDir(nodeinfo.ParentID) + NodeDir;
            }
            M_CommonData mdata = bContent.GetCommonData(InfoID);
            if (mdata == null || mdata.GeneralID < 1) { return; }
            //获取模板地址以及静态文件名
            if (!string.IsNullOrEmpty(mdata.Template))
            {
                TemplateDir = TemplateDir + mdata.Template;
            }
            else
            {
                if (nodeBll.IsExistTemplate(NodeID, ModelID))
                {
                    TemplateDir = TemplateDir + nodeBll.GetModelTemplate(NodeID, ModelID);
                }
                else
                {
                    TemplateDir = TemplateDir + bmodel.GetModelById(ModelID).ContentModule;
                }
            }
            if (string.IsNullOrEmpty(TemplateDir))
            {
                B_Release.AddResult("未指定内容页模板[" + nodeinfo.NodeName + "]:"); return;
            }
            if (nodeinfo.ContentFileEx == 3)
            {
                B_Release.AddResult("内容页生成略过[" + nodeinfo.NodeName + "]:[" + mdata.Title + "]"); return;
            }
            TemplateDir = SitePhyPath + TemplateDir;
            TemplateDir = TemplateDir.Replace("///", @"\").Replace("/", @"\").Replace("\\/", @"\").Replace(@"\\", @"\");
            if (!FileSystemObject.IsExist(TemplateDir, FsoMethod.File))
            {
                B_Release.AddResult("内容页生成略过[" + nodeinfo.NodeName + "]:内容页模板不存在"); return;
            }
            //-----------------------------------检测End-----------------------------------//
            //根据模板创建分页HTML
            string ContentHtml = bll.CreateHtml(FileSystemObject.ReadFile(TemplateDir), 0, InfoID, "0");
            //设定内容页文件名
            int InfoFileRule = nodeinfo.ContentPageHtmlRule;
            string InfoFile = "";
            string fileex = GetFileEx(nodeinfo.ContentFileEx);
            NodeDir = NodeDir + "/";
            if (GeneratedDirectory != "")
            {
                string gd = GeneratedDirectory;
                if (!gd.StartsWith("/"))
                {
                    gd = "/" + gd;
                }
                NodeDir = gd + NodeDir;
            }
            //内容页文件命名规则
            switch (InfoFileRule)
            {
                case 0:
                    InfoFile = NodeDir + mdata.CreateTime.ToString("yyyy/MM/dd") + "/" + InfoID;
                    break;
                case 1:
                    InfoFile = NodeDir + mdata.CreateTime.ToString("yyyy-MM") + "/" + InfoID;
                    break;
                case 2:
                    InfoFile = NodeDir + InfoID;
                    break;
                case 3:
                    InfoFile = NodeDir + mdata.CreateTime.ToString("yyyy-MM") + "/" + mdata.Title;
                    break;
            }
            //加上文件扩展名
            InfoFile = InfoFile + fileex;
            string HtmlLink = InfoFile;
            string FileLink = HtmlLink;

            /* --------------------判断是否分页并做处理------------------------------------------------*/
            string infoContent = ""; //需要进行处理的内容HTML
            string infotmp = "";//处理后的内容HTML
            string pagelabel = "";//匹配到的分页标签
            string pattern = @"{\#PageCode}([\s\S])*?{\/\#PageCode}";  //手动分页入此
            if (Regex.IsMatch(ContentHtml, pattern, RegexOptions.IgnoreCase))
            {
                #region 自定义分页
                infoContent = Regex.Match(ContentHtml, pattern, RegexOptions.IgnoreCase).Value;
                infotmp = infoContent;
                infoContent = infoContent.Replace("{#PageCode}", "").Replace("{/#PageCode}", "");

                //查找分页标签
                bool isPage = false;
                string pattern1 = @"{ZL\.Page([\s\S])*?\/}";
                if (Regex.IsMatch(ContentHtml, pattern1, RegexOptions.IgnoreCase))
                {
                    pagelabel = Regex.Match(ContentHtml, pattern1, RegexOptions.IgnoreCase).Value;
                    isPage = true;
                }
                if (isPage)
                {
                    if (string.IsNullOrEmpty(infoContent)) //没有设定要分页的字段内容
                    {
                        ContentHtml = ContentHtml.Replace(pagelabel, "");
                        ContentHtml = Gethtmlpageurl(ContentHtml);

                        FileLink = SitePhyPath + FileLink;
                        FileLink = FileLink.Replace("/", @"\").Replace(@"\\", @"\");
                        B_Release.AddResult("生成内容页", FileLink);
                        FileSystemObject.WriteFile(FileLink, ContentHtml);
                        bContent.UpdateCreate(InfoID, HtmlLink);
                    }
                    else//手动分页的内容分页处理
                    {
                        //文件名
                        string fname = HtmlLink.Remove(HtmlLink.LastIndexOf("."));
                        //取分页标签处理结果 返回字符串数组 根据数组元素个数生成几页 
                        string ilbl = pagelabel.Replace("{ZL.Page ", "").Replace("/}", "").Replace("\" ", "\",").Replace(" ", "");
                        string lblContent = "";
                        int NumPerPage = 500;//默认500
                        IList<string> ContentArr = new List<string>();
                        if (string.IsNullOrEmpty(ilbl))
                        {
                            lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                            //ContentArr = bll.GetContentPage(infoContent, NumPerPage);
                            ContentArr = bll.GetContentPage(infoContent);
                        }
                        else
                        {
                            string[] paArr = ilbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (paArr.Length == 0)
                            {
                                lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                //ContentArr = bll.GetContentPage(infoContent, NumPerPage);
                                ContentArr = bll.GetContentPage(infoContent);

                            }
                            else
                            {
                                string lblname = paArr[0].Split(new char[] { '=' })[1].Replace("\"", "");
                                if (paArr.Length > 1)
                                {
                                    NumPerPage = DataConverter.CLng(paArr[1].Split(new char[] { '=' })[1].Replace("\"", ""));
                                }
                                lblContent = labelBll.GetLabelXML(lblname).Content;
                                if (string.IsNullOrEmpty(lblContent))
                                {
                                    lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                }
                                //ContentArr = bll.GetContentPage(infoContent, NumPerPage);
                                ContentArr = bll.GetContentPage(infoContent);

                            }
                        }
                        string Content1 = "";//当前分页的内容
                        string allContent = "";
                        for (int i = 1; i <= ContentArr.Count; i++)//手动分页输出至本地
                        {
                            Content1 = ContentHtml.Replace(infotmp, ContentArr[i - 1]);
                            Content1 = Content1.Replace(pagelabel, bll.GetPage(lblContent, fname, fileex, i, ContentArr.Count, NumPerPage));
                            if (i == 1)
                            {
                                FileLink = fname + fileex;
                            }
                            else
                            {
                                FileLink = fname + "_" + i.ToString() + fileex;
                            }
                            FileLink = SitePhyPath + FileLink;
                            FileLink = FileLink.Replace("/", @"\").Replace(@"\\", @"\");

                            Content1 = Gethtmlpageurl(Content1);
                            Content1 = Content1.Replace("{#Content}", "").Replace("{/#Content}", "").Replace("{#PageCode}", "").Replace("{/#PageCode}", "");
                            B_Release.AddResult("生成内容页", FileLink);
                            FileSystemObject.WriteFile(FileLink, Content1);
                            if (i == 1) { bContent.UpdateCreate(InfoID, HtmlLink); }
                            allContent += ContentArr[i - 1];
                        }
                        //分页，显示全按,内容功能
                        Content1 = ContentHtml.Replace(infotmp, allContent);
                        Content1 = Content1.Replace(pagelabel, bll.GetPage(lblContent, fname, fileex, 0, ContentArr.Count, NumPerPage));
                        FileLink = SitePhyPath + fname + "_0" + fileex;
                        FileLink = FileLink.Replace("/", @"\").Replace(@"\\", @"\");
                        FileSystemObject.WriteFile(FileLink, Content1);//D:\Zoomla6x\ZoomLa.WebSite\html\ttxw\53_3.html:
                        ContentArr.Clear();
                    }
                }
                else  //没有分页标签
                {
                    //如果设定了分页内容字段 将该字段内容的分页标志清除
                    if (!string.IsNullOrEmpty(infoContent))
                    {
                        ContentHtml = ContentHtml.Replace(infotmp, infoContent);
                    }
                    ContentHtml = Gethtmlpageurl(ContentHtml);
                    FileLink = SitePhyPath + HtmlLink;
                    FileLink = FileLink.Replace("/", @"\").Replace(@"\\", @"\");
                    B_Release.AddResult("生成内容页", FileLink);
                    FileSystemObject.WriteFile(FileLink, ContentHtml);
                    bContent.UpdateCreate(InfoID, HtmlLink);
                }
                #endregion
            }
            else
            {
                //string pattern = @"<$Content>([\s\S])*?</$Content>";  //查找要分页的内容
                #region 内容分页,已用的很少
                pattern = @"{#Content}([\s\S])*?{/#Content}";
                if (Regex.IsMatch(ContentHtml, pattern, RegexOptions.IgnoreCase))
                {
                    //自动分页,进此
                    infoContent = Regex.Match(ContentHtml, pattern, RegexOptions.IgnoreCase).Value;
                    infotmp = infoContent;
                    //infoContent = infoContent.Replace("<$Content>", "").Replace("</$Content>", "");
                    infoContent = infoContent.Replace("{#Content}", "").Replace("{/#Content}", "");

                    //查找分页标签
                    bool isPage = false;
                    string pattern1 = @"{ZL\.Page([\s\S])*?\/}";
                    if (Regex.IsMatch(ContentHtml, pattern1, RegexOptions.IgnoreCase))
                    {
                        pagelabel = Regex.Match(ContentHtml, pattern1, RegexOptions.IgnoreCase).Value;
                        isPage = true;
                    }
                    if (isPage)//带{ZL.Page/}进此
                    {
                        if (string.IsNullOrEmpty(infoContent)) //没有设定要分页的字段内容
                        {
                            ContentHtml = ContentHtml.Replace(pagelabel, "");
                            ContentHtml = Gethtmlpageurl(ContentHtml);

                            FileLink = SitePhyPath + FileLink;
                            FileLink = FileLink.Replace("/", @"\").Replace(@"\\", @"\");
                            B_Release.AddResult("生成内容页", FileLink);
                            FileSystemObject.WriteFile(FileLink, ContentHtml);
                            bContent.UpdateCreate(InfoID, HtmlLink);
                        }//(自定义标签暂未入此)
                        else //进行内容分页处理,自动分页标签处理,内容页面的生成与创建html
                        {
                            //文件名
                            string file1 = HtmlLink;
                            file1 = file1.Remove(file1.LastIndexOf("."));
                            //取分页标签处理结果 返回字符串数组 根据数组元素个数生成几页 
                            string ilbl = pagelabel.Replace("{ZL.Page ", "").Replace("/}", "").Replace(" ", ",");
                            string lblContent = "";
                            int NumPerPage = 500;
                            IList<string> ContentArr = new List<string>();

                            //填充ContentArr,ContentArr存解析完成后的内容
                            if (string.IsNullOrEmpty(ilbl))
                            {
                                lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                //ContentArr = bll.GetContentPage(infoContent, NumPerPage);
                                ContentArr = bll.GetContentPage(infoContent);
                            }
                            else
                            {
                                string[] paArr = ilbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (paArr.Length == 0)
                                {
                                    lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                    //ContentArr = bll.GetContentPage(infoContent, NumPerPage);
                                    ContentArr = bll.GetContentPage(infoContent);
                                }
                                else
                                {
                                    //对{ZL.Page/}进行处理
                                    string lblname = paArr[0].Split(new char[] { '=' })[1].Replace("\"", "");
                                    if (paArr.Length > 1)
                                    {
                                        NumPerPage = DataConverter.CLng(paArr[1].Split(new char[] { '=' })[1].Replace("\"", ""));
                                    }
                                    lblContent = labelBll.GetLabelXML(lblname).Content;
                                    if (string.IsNullOrEmpty(lblContent))
                                    {
                                        lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                    }
                                    //ContentArr = bll.GetContentPage(infoContent, NumPerPage);
                                    ContentArr = bll.GetContentPage(infoContent);
                                }
                            }
                            string Content1 = "";
                            string allContent = "";
                            for (int i = 1; i <= ContentArr.Count; i++)//自动分页输出至本地
                            {
                                Content1 = ContentHtml.Replace(infotmp, ContentArr[i - 1]);
                                Content1 = ContentHtml.Replace(pagelabel, bll.GetPage(lblContent, file1, fileex, i, ContentArr.Count, NumPerPage));
                                if (i == 1)
                                {
                                    FileLink = file1 + fileex;
                                }
                                else
                                {
                                    FileLink = file1 + "_" + i.ToString() + fileex;
                                }
                                FileLink = SitePhyPath + FileLink;
                                FileLink = FileLink.Replace("/", @"\").Replace(@"\\", @"\");
                                Content1 = Gethtmlpageurl(Content1);

                                Content1 = Content1.Replace("{#Content}", "").Replace("{/#Content}", "");
                                B_Release.AddResult("生成内容页", FileLink);
                                FileSystemObject.WriteFile(FileLink, Content1);
                                if (i == 1)
                                {
                                    bContent.UpdateCreate(InfoID, HtmlLink);
                                }
                                allContent += ContentArr[i - 1];
                            }
                            //分页，显示全部内容功能
                            Content1 = ContentHtml.Replace(infotmp, allContent);
                            Content1 = ContentHtml.Replace(pagelabel, bll.GetPage(lblContent, file1, fileex, 0, ContentArr.Count, NumPerPage));
                            FileLink = SitePhyPath + file1 + "_0" + fileex;
                            FileLink = FileLink.Replace("/", @"\").Replace(@"\\", @"\");
                            FileSystemObject.WriteFile(FileLink, Content1);//D:\Zoomla6x\ZoomLa.WebSite\html\ttxw\53_3.html:
                            ContentArr.Clear();
                        }//else end;
                    }
                }
                else  //没有分页标签
                {
                    //如果设定了分页内容字段 将该字段内容的分页标志清除
                    if (!string.IsNullOrEmpty(infoContent))
                    {
                        ContentHtml = ContentHtml.Replace(infotmp, infoContent);
                    }
                    ContentHtml = Gethtmlpageurl(ContentHtml);
                    FileLink = SitePhyPath + HtmlLink;
                    FileLink = FileLink.Replace("/", @"\").Replace(@"\\", @"\");
                    B_Release.AddResult("生成内容页", FileLink);
                    //替换掉分页标签占位符
                    Regex regexObj = new Regex(@"\{ZL\.Page([\s\S])*?/\}", RegexOptions.IgnoreCase);
                    ContentHtml = regexObj.Replace(ContentHtml, "");
                    FileSystemObject.WriteFile(FileLink, ContentHtml);
                    bContent.UpdateCreate(InfoID, HtmlLink);
                }
                #endregion
            }
        }
        //------------------------------Tools
        private void ReplaceHref(ref string contentHtml, int pageCount, string FieldExtstr, string pagename = "index") //用于支持生成列表页,CreateNodePage
        {
            contentHtml = Regex.Replace(contentHtml, "CreateHtml1.aspx", pagename + FieldExtstr, RegexOptions.IgnoreCase);
            contentHtml = Regex.Replace(contentHtml, "EditContent1.aspx", pagename + FieldExtstr, RegexOptions.IgnoreCase);
            for (int i = 2; i <= pageCount; i++)
            {
                contentHtml = Regex.Replace(contentHtml, "CreateHtml" + i + ".aspx", pagename + "_" + i + FieldExtstr, RegexOptions.IgnoreCase);
                contentHtml = Regex.Replace(contentHtml, "EditContent" + i + ".aspx", pagename + "_" + i + FieldExtstr, RegexOptions.IgnoreCase);
            }
        }
        //从Html中取出页码计数
        private int GetPageCount(string html)
        {
            int pcount = 0;
            var regexObj = new Regex("id=\"pageDiv\" totalPage=\"\\d+\"", RegexOptions.IgnoreCase);
            var matchResult = regexObj.Match(html);
            if (matchResult.Success)
            {
                //pageType = 1;//BootStrap数字类型分页
                Regex regexObj2 = new Regex(@"\d+");
                Match matchResult2 = regexObj2.Match(matchResult.Value);
                if (matchResult2.Success)
                {
                    pcount = DataConverter.CLng(matchResult2.Value);
                }
            }
            return pcount;
        }
        //------------------------------Tools End; 
        /// <summary>
        /// 用于处理分页
        /// </summary>
        private string GetPagenurl(ref string contents)
        {
            Regex regexObj = new Regex("[\"|\'][-A-Z0-9+&@#/%?=~_|!:,.;]*[A-Z0-9+&@#/%=~_|][\"\']", RegexOptions.IgnoreCase);
            Match matchResults = regexObj.Match(contents);
            string gd = SiteConfig.SiteOption.GeneratedDirectory;
            if (!gd.StartsWith("/"))
            {
                gd = "/" + gd;
            }
            string GeneratedDirectory = gd;//生成路径
            int maxpagenum = 0;
            int thisnodeid = 0;

            while (matchResults.Success)
            {
                if (matchResults.ToString().IndexOf(".aspx") > -1)
                {
                    if (matchResults.ToString().IndexOf("ColumnList.aspx") > -1)
                    {
                        string stringlist = matchResults.ToString();
                        string slist = stringlist;
                        contents = GetPagenurl(ref contents, matchResults, GeneratedDirectory, ref maxpagenum, ref thisnodeid, ref stringlist);
                    }
                }
                matchResults = matchResults.NextMatch();
            }
            return contents;
        }
        private string GetPagenurl(ref string contents, Match matchResults, string GeneratedDirectory, ref int maxpagenum, ref int thisnodeid, ref string stringlist)
        {
            if (stringlist.IndexOf("&p=") > -1)//处理分页内容
            {
                string[] stringlistarr = stringlist.Split(new string[] { "&p=" }, StringSplitOptions.None);
                stringlist = stringlistarr[0];
                int infoid = GetInfoID(stringlist, "nodeid=");
                M_Node nodeinfo = nodeBll.GetNodeXML(infoid);
                thisnodeid = nodeinfo.NodeID;
                string NodeUrl = nodeinfo.NodeUrl;
                int pagenum = 0;
                if (matchResults.ToString().IndexOf("&p=") > -1)//处理分页内容
                {
                    string pageurl = matchResults.ToString();
                    pageurl = pageurl.Replace("'", "");
                    string[] pageurlarr = pageurl.Split(new string[] { "&p=" }, StringSplitOptions.None);

                    if (pageurlarr.Length > 0)
                    {
                        if (pageurlarr[1].IndexOf("&") > -1)
                        {
                            string[] pageurlarrarr = pageurlarr[1].Split(new string[] { "&" }, StringSplitOptions.None);
                            pagenum = DataConverter.CLng(pageurlarrarr[0]);
                        }
                        else
                        {
                            pagenum = DataConverter.CLng(pageurlarr[1]);
                        }
                    }

                    if (pagenum > maxpagenum) { maxpagenum = pagenum; }

                    string filename = "_" + infoid.ToString() + "_" + pagenum.ToString();
                    if (pagenum == 1) { filename = ""; }
                    if (!string.IsNullOrEmpty(NodeUrl))
                    {
                        if (NodeUrl.IndexOf(".") > -1)
                        {
                            string[] nodearr = NodeUrl.Split(new string[] { "." }, StringSplitOptions.None);
                            NodeUrl = nodearr[0].ToString() + filename + "." + nodearr[1].ToString();
                        }
                    }
                    string aspxpage = matchResults.ToString();
                    aspxpage = aspxpage.Replace("'", "");
                    aspxpage = "/" + aspxpage;//动态地址
                    string htmlpage = "/" + GeneratedDirectory + NodeUrl;//静态地址
                    contents = contents.Replace(matchResults.ToString(), htmlpage);
                }
            }
            return contents;
        }
        /// <summary>
        /// 生成首页
        /// </summary>
        /// <param name="IndexDir">模板地址</param>
        /// <param name="indexex">后缀名</param>
        /// <param name="Dir">首页存放地址</param>
        public void CreateIndex(string IndexDir, string indexex, string Dir)
        {
            IndexDir = IndexDir.Replace("/", @"\");
            if (FileSystemObject.IsExist(IndexDir, FsoMethod.File))
            {
                try
                {
                    string IndexHtml = bll.CreateHtml(FileSystemObject.ReadFile(IndexDir), 0, 0, "0");
                    IndexHtml = Gethtmlpageurl(IndexHtml);
                    #region 查找ColumnList
                    StringCollection resultList = new StringCollection();
                    Regex regexObj = new Regex(@"ColumnList\.aspx\?NodeID=[\w]*", RegexOptions.Multiline);
                    Match matchResult = regexObj.Match(IndexHtml);
                    while (matchResult.Success)
                    {
                        string MatchValue = matchResult.Value;
                        try
                        {
                            Regex newregexObj = new Regex("[0-9]*", RegexOptions.IgnorePatternWhitespace);
                            Match matchResultstext = newregexObj.Match(MatchValue);

                            IList<int> nodelist = new List<int>();
                            while (matchResultstext.Success)
                            {
                                if (matchResultstext.ToString() != "")
                                {
                                    int nodeid = DataConverter.CLng(matchResultstext.ToString());
                                    if (nodelist.IndexOf(nodeid) == -1)
                                    {
                                        nodelist.Add(nodeid);
                                    }
                                }
                                matchResultstext = matchResultstext.NextMatch();
                            }
                            /*........................................................*/
                            if (nodelist.Count > 0)
                            {
                                for (int nid = 0; nid < nodelist.Count; nid++)
                                {
                                    int NodeID = DataConverter.CLng(nodelist[nid]);
                                    M_Node ninfo = nodeBll.GetNodeXML(NodeID);

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ZLLog.L(Model.ZLEnum.Log.labelex, "生成首页错误,原因:" + ex.Message);
                        }
                        matchResult = matchResult.NextMatch();
                    }
                    #endregion
                    string objindex = SitePhyPath + Dir + "\\index" + indexex;
                    objindex = objindex.Replace(@"\/", @"\").Replace(@"\\", @"\");
                    FileSystemObject.WriteFile(objindex, IndexHtml);
                    B_Release.AddResult("生成首页", objindex);
                }
                catch (Exception ex)
                {
                    ZLLog.L(Model.ZLEnum.Log.labelex, "生成首页错误,原因:" + ex.Message);
                }
            }
            else
            {
                B_Release.AddResult("首页生成失败——模板文件不存在");
            }
        }
        /// <summary>
        /// 生成栏目节点首页
        /// </summary>
        /// <param name="NodeID">节点ID</param>        
        public void CreateNodePage(int NodeID)
        {
            M_Node nodeinfo = nodeBll.SelReturnModel(NodeID);
            string NodeDir = nodeinfo.NodeDir;
            if (NodeDir != "") { NodeDir = "/" + nodeinfo.NodeDir; }
            if (nodeinfo.HtmlPosition > 0 && nodeinfo.ParentID > 0)
            {
                if (nodeinfo.NodeType == 1)
                    NodeDir = GetParentDir(nodeinfo.ParentID) + NodeDir;
                else
                    NodeDir = GetParentDir(nodeinfo.ParentID);
            }
            string TemplateDir = SiteConfig.SiteOption.TemplateDir;
            TemplateDir += "/" + nodeinfo.IndexTemplate;
            if (!string.IsNullOrEmpty(nodeinfo.IndexTemplate) && nodeinfo.ListPageHtmlEx < 3)// && nodeinfo.Purview.ToLower().Contains("alluser")
            {
                if (SitePhyPath.EndsWith("\\") && TemplateDir.StartsWith("/"))
                {
                    TemplateDir = SitePhyPath.Substring(0, SitePhyPath.Length - 1) + TemplateDir;
                }
                else
                {
                    TemplateDir = SitePhyPath + TemplateDir;
                }

                TemplateDir = TemplateDir.Replace("/", @"\").Replace(@"\\", @"\");
                if (FileSystemObject.IsExist(TemplateDir, FsoMethod.File))
                {
                    string Tempcontent = FileSystemObject.ReadFile(TemplateDir);
                    #region 节点自定义字段
                    Regex regexObjtx = new Regex(@"\{PH\.Label id=""自设节点内容"" nodeid=""@Request_id"" num=""(\d+)"" /\}");
                    Match matchResultsx = regexObjtx.Match(Tempcontent);
                    string Custom = nodeinfo.Custom;

                    if (Custom.IndexOf("{SplitCustom}") > -1)
                    {
                        string[] CustArr = Custom.Split(new string[] { "{SplitCustom}" }, StringSplitOptions.RemoveEmptyEntries);

                        while (matchResultsx.Success)
                        {
                            string NodeItemCount = Regex.Replace(matchResultsx.Value, @"\{PH\.Label id=""自设节点内容"" nodeid=""@Request_id"" num=""(\d+)"" /\}", "$1");
                            int NodeCoustom = DataConverter.CLng(NodeItemCount);
                            string CoustomContent = CustArr[NodeCoustom - 1].ToString();
                            Tempcontent = Tempcontent.Replace(matchResultsx.Value, CoustomContent);
                            matchResultsx = matchResultsx.NextMatch();
                        }
                    }
                    #endregion
                    string ContentHtml = bll.CreateHtml(Tempcontent, 0, NodeID, "1");
                    //判断包含多少个分页
                    StringCollection resultList = new StringCollection();
                    int Pagecount = GetPageCount(ContentHtml);//总页数
                    string InfoFile = "";
                    if (string.IsNullOrEmpty(nodeinfo.NodeUrl))
                    {
                        if (nodeinfo.NodeType == 1) //栏目节点
                        {
                            InfoFile = NodeDir + "/index";
                        }
                        else       //单页
                        {
                            InfoFile = NodeDir;
                        }

                        InfoFile = InfoFile + GetFileEx(nodeinfo.ListPageHtmlEx);
                        nodeinfo.NodeUrl = InfoFile;
                        nodeBll.UpdateNode(nodeinfo);
                    }
                    else
                    {
                        InfoFile = nodeinfo.NodeUrl;
                    }

                    string gd = SiteConfig.SiteOption.GeneratedDirectory;
                    if (!gd.StartsWith("/"))
                    {
                        gd = "/" + gd;
                    }
                    InfoFile = SitePhyPath + gd + InfoFile;
                    InfoFile = InfoFile.Replace("/", @"\").Replace(@"\\", @"\");
                    B_Release.AddResult("栏目节点", InfoFile);
                    string FieldExtstr = GetFileEx(DataConverter.CLng(nodeinfo.ListPageHtmlEx));
                    #region 循环生成分页
                    if (Pagecount > 1)//从第二页开始,第一页在上面已经生成
                    {
                        int counts = 1;
                        for (int c = 1; c < Pagecount; c++)
                        {
                            counts += 1;
                            LabelCache.Clear();
                            string PageContentHtml = bll.CreateHtml(Tempcontent, counts, NodeID, "1");
                            string url = function.PToV(InfoFile.Replace(FieldExtstr, "_" + counts + FieldExtstr).Replace("//", "/"));
                            B_Release.AddResult("栏目节点[分页" + counts + "]" + "<a href='" + url + "' target='_blank'>" + url + "</a>");
                            PageContentHtml = Regex.Replace(PageContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>首页</[a,A]>", "<a href='index" + FieldExtstr + "'>首页</a>", RegexOptions.RightToLeft);
                            if (counts == 2)
                            {
                                PageContentHtml = Regex.Replace(PageContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>上一页</[a,A]>", "<a href='index" + FieldExtstr + "'>上一页</a>", RegexOptions.RightToLeft);
                            }
                            else
                            {
                                PageContentHtml = Regex.Replace(PageContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>上一页</[a,A]>", "<a href='index_" + (counts - 1).ToString() + FieldExtstr + "'>上一页</a>", RegexOptions.RightToLeft);
                            }
                            if (counts < Pagecount)
                            {
                                PageContentHtml = Regex.Replace(PageContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>下一页</[a,A]>", "<a href='index_" + (counts + 1).ToString() + FieldExtstr + "'>下一页</a>", RegexOptions.RightToLeft);
                            }
                            else
                            {
                                PageContentHtml = Regex.Replace(PageContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>下一页</[a,A]>", "下一页", RegexOptions.RightToLeft);
                            }
                            if (counts < Pagecount)
                            {
                                PageContentHtml = Regex.Replace(PageContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>尾页</[a,A]>", "<a href='index_" + Pagecount + FieldExtstr + "'>尾页</a>", RegexOptions.RightToLeft);
                            }
                            else
                            {
                                PageContentHtml = Regex.Replace(PageContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>尾页</[a,A]>", "尾页", RegexOptions.RightToLeft);
                            }
                            ReplaceHref(ref PageContentHtml, Pagecount, FieldExtstr);
                            FileSystemObject.WriteFile(InfoFile.Replace(FieldExtstr, "_" + counts.ToString() + FieldExtstr), PageContentHtml);//将.html replace to _count.html
                            PageContentHtml = "";
                        }
                    }
                    #endregion
                    #region 首页分页
                    ContentHtml = ContentHtml.Replace("当前页面:<strong><font color=red>0</font></strong>/共", "当前页面:<strong><font color=red>1</font></strong>/共");
                    ContentHtml = Regex.Replace(ContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>首页</[a,A]>", "首页", RegexOptions.RightToLeft);
                    ContentHtml = Regex.Replace(ContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>上一页</[a,A]>", "上一页", RegexOptions.RightToLeft);
                    if (Pagecount > 1)
                    {
                        ContentHtml = Regex.Replace(ContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>下一页</[a,A]>", "<a href='index_2" + FieldExtstr + "'>下一页</a>", RegexOptions.RightToLeft);
                        ContentHtml = Regex.Replace(ContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>尾页</[a,A]>", "<a href='index_" + Pagecount + FieldExtstr + "'>尾页</a>", RegexOptions.RightToLeft);
                    }
                    else
                    {
                        ContentHtml = Regex.Replace(ContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>下一页</[a,A]>", "下一页", RegexOptions.RightToLeft);
                        ContentHtml = Regex.Replace(ContentHtml, "<[a,A] [h,H][r,R][e,E][f,F]='.*?'>尾页</[a,A]>", "尾页", RegexOptions.RightToLeft);
                    }
                    ReplaceHref(ref ContentHtml, Pagecount, FieldExtstr);
                    FileSystemObject.WriteFile(InfoFile, ContentHtml);
                    #endregion
                }
            }
            else
            {
                if (nodeinfo.ListPageHtmlEx == 3)
                {
                    B_Release.AddResult(nodeinfo.NodeName + "--栏目节点首页系统设置为动态生成略过");
                    if (string.IsNullOrEmpty(TemplateDir))
                    {
                        B_Release.AddResult(nodeinfo.NodeName + "未指定栏目首页模板");
                    }
                }
            }
        }
        /// <summary>
        /// 创建列表页
        /// </summary>
        /// <param name="NodeID">节点ID</param>
        public void CreateList(int NodeID)
        {
            try
            {
                M_Node nodeinfo = nodeBll.GetNodeXML(NodeID);
                string TemplateDir = SiteConfig.SiteOption.TemplateDir;
                string GeneratedDirectory = SiteConfig.SiteOption.GeneratedDirectory;
                int node = nodeBll.GetContrarily(NodeID, 5);//获取最终父节点
                M_Node Pnid = nodeBll.GetNodeXML(node);
                string NodeDir = "";
                if (nodeinfo.HtmlPosition > 0 && nodeinfo.ParentID > 0)
                {
                    if (nodeinfo.NodeType == 1)
                        NodeDir = GetParentDir(nodeinfo.ParentID) + "/" + nodeinfo.NodeDir;
                    else
                        NodeDir = GetParentDir(nodeinfo.ParentID);
                }
                else
                {
                    NodeDir = "/" + nodeinfo.NodeDir;
                }

                TemplateDir = TemplateDir + "/" + nodeinfo.ListTemplateFile;
                if (!string.IsNullOrEmpty(nodeinfo.ListTemplateFile) && nodeinfo.ListPageHtmlEx < 3)
                {
                    if (SitePhyPath.EndsWith("\\") && TemplateDir.StartsWith("/"))
                    {
                        TemplateDir = SitePhyPath.Substring(0, SitePhyPath.Length - 1) + (nodeinfo.NodeBySite == 5 ? "" : "") + TemplateDir;
                    }
                    else
                    {
                        TemplateDir = SitePhyPath + TemplateDir;
                    }
                    TemplateDir = TemplateDir.Replace("/", @"\").Replace(@"\\", @"\");
                    if (FileSystemObject.IsExist(TemplateDir, FsoMethod.File))
                    {
                        string Tempcontent = FileSystemObject.ReadFile(TemplateDir);
                        #region 节点自定义字段
                        Regex regexObjtx = new Regex(@"\{PH\.Label id=""自设节点内容"" nodeid=""@Request_id"" num=""(\d+)"" /\}");
                        Match matchResultsx = regexObjtx.Match(Tempcontent);
                        string Custom = nodeinfo.Custom;
                        if (Custom.IndexOf("{SplitCustom}") > -1)
                        {
                            string[] CustArr = Custom.Split(new string[] { "{SplitCustom}" }, StringSplitOptions.RemoveEmptyEntries);

                            while (matchResultsx.Success)
                            {
                                string NodeItemCount = Regex.Replace(matchResultsx.Value, @"\{PH\.Label id=""自设节点内容"" nodeid=""@Request_id"" num=""(\d+)"" /\}", "$1");
                                int NodeCoustom = DataConverter.CLng(NodeItemCount);
                                string CoustomContent = CustArr[NodeCoustom - 1].ToString();
                                Tempcontent = Tempcontent.Replace(matchResultsx.Value, CoustomContent);
                                matchResultsx = matchResultsx.NextMatch();
                            }
                        }
                        #endregion
                        string ContentHtml = bll.CreateHtml(Tempcontent, 0, NodeID, "0");
                        //判断包含多少个分页
                        StringCollection resultList = new StringCollection();
                       
                        int Pagecount = GetPageCount(ContentHtml);
                        ContentHtml = Gethtmlpageurl(ContentHtml);
                        string InfoFile = "";
                        if (string.IsNullOrEmpty(nodeinfo.NodeListUrl))
                        {
                            if (nodeinfo.NodeType == 1) //栏目节点
                            {
                                InfoFile = NodeDir + "/listpage";
                            }
                            else       //单页
                            {
                                InfoFile = NodeDir;
                            }
                            //文件扩展名
                            InfoFile = InfoFile + GetFileEx(nodeinfo.ListPageHtmlEx);
                            nodeinfo.NodeListUrl = InfoFile;
                            nodeBll.UpdateNode(nodeinfo);
                        }
                        else
                        {
                            InfoFile = "/" + nodeinfo.NodeListUrl;
                        }
                        string gd = GeneratedDirectory;
                        if (!gd.StartsWith("/"))
                        {
                            gd = "/" + gd;
                        }
                        InfoFile = SitePhyPath + gd + InfoFile;
                        InfoFile = InfoFile.Replace("/", @"\").Replace(@"\\", @"\");
                        B_Release.AddResult("列表页", InfoFile);
                        string FieldExtstr = GetFileEx(DataConverter.CLng(nodeinfo.ListPageHtmlEx));
                        //进行循环生成分页
                        if (Pagecount > 1)
                        {
                            int counts = 1;
                            for (int c = 1; c < Pagecount; c++)
                            {
                                counts += 1;
                                LabelCache.Clear();
                                string PageContentHtml = bll.CreateHtml(Tempcontent, counts, NodeID, "1");
                                B_Release.AddResult("列表页[分页" + counts + "]" + InfoFile.Replace(FieldExtstr, "_" + counts.ToString() + FieldExtstr));
                                PageContentHtml = Regex.Replace(PageContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>首页</a>", "<a href='listpage" + FieldExtstr + "'>首页</a>", RegexOptions.IgnoreCase);
                                if (counts == 2)
                                {
                                    PageContentHtml = Regex.Replace(PageContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>上一页</a>", "<a href='listpage" + FieldExtstr + "'>上一页</a>", RegexOptions.IgnoreCase);
                                }
                                else
                                {
                                    PageContentHtml = Regex.Replace(PageContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>上一页</a>", "<a href='listpage_" + (counts - 1).ToString() + FieldExtstr + "'>上一页</a>", RegexOptions.IgnoreCase);
                                }

                                if (counts < Pagecount)
                                {
                                    PageContentHtml = Regex.Replace(PageContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>下一页</a>", "<a href='listpage_" + (counts + 1).ToString() + FieldExtstr + "'>下一页</a>", RegexOptions.IgnoreCase);
                                    PageContentHtml = Regex.Replace(PageContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>尾页</a>", "<a href='listpage_" + Pagecount + FieldExtstr + "'>尾页</a>", RegexOptions.IgnoreCase);
                                }
                                else
                                {
                                    PageContentHtml = Regex.Replace(PageContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>下一页</a>", "下一页", RegexOptions.IgnoreCase);
                                    PageContentHtml = Regex.Replace(PageContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>尾页</a>", "尾页", RegexOptions.IgnoreCase);
                                }
                                ReplaceHref(ref PageContentHtml, Pagecount, FieldExtstr, "listpage");
                                FileSystemObject.WriteFile(InfoFile.Replace(FieldExtstr, "_" + counts.ToString() + FieldExtstr), PageContentHtml);
                                PageContentHtml = "";
                            }
                        }//end

                        //更新列表首页分页地址进行替换
                        ContentHtml = ContentHtml.Replace("当前页面:<strong><font color=red>0</font></strong>/共", "当前页面:<strong><font color=red>1</font></strong>/共");
                        ContentHtml = Regex.Replace(ContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>首页</a>", "首页", RegexOptions.IgnoreCase);
                        ContentHtml = Regex.Replace(ContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>上一页</a>", "上一页", RegexOptions.IgnoreCase);
                        if (Pagecount > 1)
                        {
                            ContentHtml = Regex.Replace(ContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>下一页</a>", "<a href='listpage_2" + FieldExtstr + "'>下一页</a>", RegexOptions.IgnoreCase);
                            ContentHtml = Regex.Replace(ContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>尾页</a>", "<a href='listpage_" + Pagecount + FieldExtstr + "'>尾页</a>", RegexOptions.IgnoreCase);
                        }
                        else
                        {
                            ContentHtml = Regex.Replace(ContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>下一页</a>", "下一页", RegexOptions.IgnoreCase);
                            ContentHtml = Regex.Replace(ContentHtml, "<a href='(https?|ftp|file)://[-A-Z0-9+&@#/%?=~_|!:,.;]*[-A-Z0-9+&@#/%=~_|]'>尾页</a>", "尾页", RegexOptions.IgnoreCase);
                        }
                        ReplaceHref(ref ContentHtml, Pagecount, FieldExtstr, "listpage");
                        FileSystemObject.WriteFile(InfoFile, ContentHtml);
                    }
                    else
                    {
                        B_Release.AddResult("列表页生成失败--" + nodeinfo.NodeName + "列表页模板不存在");
                    }
                }
                else
                {
                    if (nodeinfo.ListPageHtmlEx == 3)
                    {
                        B_Release.AddResult(nodeinfo.NodeName + "--列表页系统设置为动态生成略过");
                        if (string.IsNullOrEmpty(TemplateDir))
                        {
                            B_Release.AddResult(nodeinfo.NodeName + "--未指定列表页模板");
                        }
                    }
                }
            }
            catch (Exception ex) { ZLLog.L("生成发布--CreateList," + ex.Message); }
        }
        /// <summary>
        /// 生成静态转换页面地址
        /// </summary>
        public string Gethtmlpageurl(string contents)
        {
            try
            {
                Regex regexObj = new Regex("[\"|\'][-A-Z0-9+&@#/%?=~_|!:,.;]*[A-Z0-9+&@#/%=~_|][\"\']", RegexOptions.IgnoreCase);
                Match matchResults = regexObj.Match(contents);
                string gd = SiteConfig.SiteOption.GeneratedDirectory;
                if (!gd.StartsWith("/"))
                {
                    gd = "/" + gd;
                }
                string GeneratedDirectory = gd;//生成路径
                int maxpagenum = 0;
                int thisnodeid = 0;

                while (matchResults.Success)
                {
                    if (matchResults.ToString().IndexOf(".aspx") > -1)
                    {
                        if (matchResults.ToString().IndexOf("ColumnList.aspx") > -1)
                        {
                            string stringlist = matchResults.ToString();
                            string slist = stringlist;
                            contents = GetPagenurl(ref contents, matchResults, GeneratedDirectory, ref maxpagenum, ref thisnodeid, ref stringlist);
                        }
                    }
                    matchResults = matchResults.NextMatch();
                }

                while (matchResults.Success)
                {

                    if (matchResults.ToString().IndexOf(".aspx") > -1)
                    {
                        if (matchResults.ToString().IndexOf("Content.aspx") > -1)
                        {
                            string stringlist = matchResults.ToString();
                            int infoid = GetInfoID(stringlist, "itemid=");
                            B_Content cll = new B_Content();
                            M_CommonData CommonData = cll.GetCommonData(infoid);
                            int nodeid = CommonData.NodeID;
                            M_Node nodeinfolist = nodeBll.GetNodeXML(nodeid);
                            string HtmlLink = CommonData.HtmlLink;

                            if (!string.IsNullOrEmpty(HtmlLink))
                            {
                                HtmlLink = HtmlLink.Replace("//", "/");
                            }

                            if (!string.IsNullOrEmpty(HtmlLink))
                            {
                                if (nodeinfolist.HtmlPosition == 0)
                                {
                                    stringlist = "../.." + HtmlLink;// stringlist.Replace("Content.aspx", "Content.html");
                                }
                                else
                                {
                                    stringlist = "../.." + GeneratedDirectory + HtmlLink;// stringlist.Replace("Content.aspx", "Content.html");
                                }

                                if (FileSystemObject.IsExist(HttpContext.Current.Server.MapPath(stringlist), FsoMethod.File))
                                {
                                    contents = contents.Replace(matchResults.ToString(), stringlist);
                                }
                            }
                        }

                        if (matchResults.ToString().IndexOf("http://") > -1)
                        {
                            if (matchResults.ToString().IndexOf(SiteConfig.SiteInfo.SiteUrl) > -1)
                            {
                                #region 处理分页
                                if (matchResults.ToString().IndexOf("ColumnList.aspx") > -1)
                                {
                                    string stringlist = matchResults.ToString();
                                    string slist = stringlist;

                                    if (stringlist.IndexOf("&p=") > -1)//处理分页内容
                                    {
                                        string[] stringlistarr = stringlist.Split(new string[] { "&p=" }, StringSplitOptions.None);
                                        stringlist = stringlistarr[0];
                                    }
                                    int infoid = GetInfoID(stringlist, "nodeid=");
                                    M_Node nodeinfo = nodeBll.GetNodeXML(infoid);
                                    string NodeUrl = nodeinfo.NodeUrl;
                                    int pagenum = 0;

                                    if (matchResults.ToString().IndexOf("&p=") > -1)//处理分页内容
                                    {
                                        string pageurl = matchResults.ToString();
                                        pageurl = pageurl.Replace("'", "");
                                        string[] pageurlarr = pageurl.Split(new string[] { "&p=" }, StringSplitOptions.None);

                                        if (pageurlarr.Length > 0)
                                        {
                                            if (pageurlarr[1].IndexOf("&") > -1)
                                            {
                                                string[] pageurlarrarr = pageurlarr[1].Split(new string[] { "&" }, StringSplitOptions.None);
                                                pagenum = DataConverter.CLng(pageurlarrarr[0]);
                                            }
                                            else
                                            {
                                                pagenum = DataConverter.CLng(pageurlarr[1]);
                                            }
                                        }
                                        if (pagenum > maxpagenum) { maxpagenum = pagenum; }
                                        string filename = "_" + infoid.ToString() + "_" + pagenum.ToString();
                                        if (!string.IsNullOrEmpty(NodeUrl))
                                        {
                                            if (NodeUrl.IndexOf(".") > -1)
                                            {
                                                string[] nodearr = NodeUrl.Split(new string[] { "." }, StringSplitOptions.None);
                                                NodeUrl = nodearr[0].ToString() + filename + "." + nodearr[1].ToString();
                                            }
                                        }

                                        string aspxpage = matchResults.ToString();
                                        aspxpage = aspxpage.Replace("'", "");

                                        aspxpage = "/" + aspxpage;//动态地址
                                        string htmlpage = "/" + GeneratedDirectory + NodeUrl;//静态地址
                                        contents = contents.Replace(matchResults.ToString(), htmlpage);
                                    }


                                    if (!string.IsNullOrEmpty(nodeinfo.IndexTemplate))
                                    {
                                        if (!string.IsNullOrEmpty(NodeUrl))
                                        {
                                            if (NodeUrl != "")
                                            {
                                                string tempNodeurl = "../../" + GeneratedDirectory + NodeUrl;
                                                if (nodeinfo.HtmlPosition == 0)
                                                {
                                                    tempNodeurl = "../../" + NodeUrl;
                                                }
                                                if (FileSystemObject.IsExist(HttpContext.Current.Server.MapPath(tempNodeurl), FsoMethod.File))
                                                {
                                                    contents = contents.Replace(matchResults.ToString(), "/" + GeneratedDirectory + NodeUrl);
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                            else if (matchResults.ToString().IndexOf("http://127.0.0.1") > -1)
                            {
                                #region 处理分页
                                if (matchResults.ToString().IndexOf("ColumnList.aspx") > -1)
                                {
                                    string stringlist = matchResults.ToString();
                                    string slist = stringlist;

                                    if (stringlist.IndexOf("&p=") > -1)//处理分页内容
                                    {
                                        string[] stringlistarr = stringlist.Split(new string[] { "&p=" }, StringSplitOptions.None);
                                        stringlist = stringlistarr[0];
                                    }
                                    int infoid = GetInfoID(stringlist, "nodeid=");
                                    M_Node nodeinfo = nodeBll.GetNodeXML(infoid);
                                    string NodeUrl = nodeinfo.NodeUrl;
                                    int pagenum = 0;

                                    if (matchResults.ToString().IndexOf("&p=") > -1)//处理分页内容
                                    {
                                        string pageurl = matchResults.ToString();
                                        pageurl = pageurl.Replace("'", "");
                                        string[] pageurlarr = pageurl.Split(new string[] { "&p=" }, StringSplitOptions.None);

                                        if (pageurlarr.Length > 0)
                                        {
                                            if (pageurlarr[1].IndexOf("&") > -1)
                                            {
                                                string[] pageurlarrarr = pageurlarr[1].Split(new string[] { "&" }, StringSplitOptions.None);
                                                pagenum = DataConverter.CLng(pageurlarrarr[0]);
                                            }
                                            else
                                            {
                                                pagenum = DataConverter.CLng(pageurlarr[1]);
                                            }
                                        }
                                        if (pagenum > maxpagenum) { maxpagenum = pagenum; }
                                        string filename = "_" + infoid.ToString() + "_" + pagenum.ToString();
                                        if (!string.IsNullOrEmpty(NodeUrl))
                                        {
                                            if (NodeUrl.IndexOf(".") > -1)
                                            {
                                                string[] nodearr = NodeUrl.Split(new string[] { "." }, StringSplitOptions.None);
                                                NodeUrl = nodearr[0].ToString() + filename + "." + nodearr[1].ToString();
                                            }
                                        }

                                        string aspxpage = matchResults.ToString();
                                        aspxpage = aspxpage.Replace("'", "");

                                        aspxpage = "/" + aspxpage;//动态地址
                                        string htmlpage = "/" + GeneratedDirectory + NodeUrl;//静态地址
                                        contents = contents.Replace(matchResults.ToString(), htmlpage);
                                    }


                                    if (!string.IsNullOrEmpty(nodeinfo.IndexTemplate))
                                    {
                                        if (!string.IsNullOrEmpty(NodeUrl))
                                        {
                                            if (NodeUrl != "")
                                            {
                                                string tempNodeurl = "../../" + GeneratedDirectory + NodeUrl;
                                                if (nodeinfo.HtmlPosition == 0)
                                                {
                                                    tempNodeurl = "../../" + NodeUrl;
                                                }
                                                if (FileSystemObject.IsExist(HttpContext.Current.Server.MapPath(tempNodeurl), FsoMethod.File))
                                                {
                                                    contents = contents.Replace(matchResults.ToString(), "/" + GeneratedDirectory + NodeUrl);
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                            else if (matchResults.ToString().IndexOf("http://localhost") > -1)
                            {
                                #region 处理分页
                                if (matchResults.ToString().IndexOf("ColumnList.aspx") > -1)
                                {
                                    string stringlist = matchResults.ToString();
                                    string slist = stringlist;

                                    if (stringlist.IndexOf("&p=") > -1)//处理分页内容
                                    {
                                        string[] stringlistarr = stringlist.Split(new string[] { "&p=" }, StringSplitOptions.None);
                                        stringlist = stringlistarr[0];
                                    }
                                    int infoid = GetInfoID(stringlist, "nodeid=");
                                    M_Node nodeinfo = nodeBll.GetNodeXML(infoid);
                                    string NodeUrl = nodeinfo.NodeUrl;
                                    int pagenum = 0;

                                    if (matchResults.ToString().IndexOf("&p=") > -1)//处理分页内容
                                    {
                                        string pageurl = matchResults.ToString();
                                        pageurl = pageurl.Replace("'", "");
                                        string[] pageurlarr = pageurl.Split(new string[] { "&p=" }, StringSplitOptions.None);

                                        if (pageurlarr.Length > 0)
                                        {
                                            if (pageurlarr[1].IndexOf("&") > -1)
                                            {
                                                string[] pageurlarrarr = pageurlarr[1].Split(new string[] { "&" }, StringSplitOptions.None);
                                                pagenum = DataConverter.CLng(pageurlarrarr[0]);
                                            }
                                            else
                                            {
                                                pagenum = DataConverter.CLng(pageurlarr[1]);
                                            }
                                        }
                                        if (pagenum > maxpagenum) { maxpagenum = pagenum; }
                                        string filename = "_" + infoid.ToString() + "_" + pagenum.ToString();
                                        if (!string.IsNullOrEmpty(NodeUrl))
                                        {
                                            if (NodeUrl.IndexOf(".") > -1)
                                            {
                                                string[] nodearr = NodeUrl.Split(new string[] { "." }, StringSplitOptions.None);
                                                NodeUrl = nodearr[0].ToString() + filename + "." + nodearr[1].ToString();
                                            }
                                        }

                                        string aspxpage = matchResults.ToString();
                                        aspxpage = aspxpage.Replace("'", "");

                                        aspxpage = "/" + aspxpage;//动态地址
                                        string htmlpage = "/" + GeneratedDirectory + NodeUrl;//静态地址
                                        contents = contents.Replace(matchResults.ToString(), htmlpage);
                                    }


                                    if (!string.IsNullOrEmpty(nodeinfo.IndexTemplate))
                                    {
                                        if (!string.IsNullOrEmpty(NodeUrl))
                                        {
                                            if (NodeUrl != "")
                                            {
                                                string tempNodeurl = "../../" + GeneratedDirectory + NodeUrl;
                                                if (nodeinfo.HtmlPosition == 0)
                                                {
                                                    tempNodeurl = "../../" + NodeUrl;
                                                }
                                                if (FileSystemObject.IsExist(HttpContext.Current.Server.MapPath(tempNodeurl), FsoMethod.File))
                                                {
                                                    contents = contents.Replace(matchResults.ToString(), "/" + GeneratedDirectory + NodeUrl);
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            #region 处理分页
                            if (matchResults.ToString().IndexOf("ColumnList.aspx") > -1)
                            {
                                string stringlist = matchResults.ToString();
                                string slist = stringlist;

                                if (stringlist.IndexOf("&p=") > -1)//处理分页内容
                                {
                                    string[] stringlistarr = stringlist.Split(new string[] { "&p=" }, StringSplitOptions.None);
                                    stringlist = stringlistarr[0];
                                }
                                int infoid = GetInfoID(stringlist, "nodeid=");
                                M_Node nodeinfo = nodeBll.GetNodeXML(infoid);
                                string NodeUrl = nodeinfo.NodeUrl;
                                int pagenum = 0;

                                if (matchResults.ToString().IndexOf("&p=") > -1)//处理分页内容
                                {
                                    string pageurl = matchResults.ToString();
                                    pageurl = pageurl.Replace("'", "");
                                    string[] pageurlarr = pageurl.Split(new string[] { "&p=" }, StringSplitOptions.None);

                                    if (pageurlarr.Length > 0)
                                    {
                                        if (pageurlarr[1].IndexOf("&") > -1)
                                        {
                                            string[] pageurlarrarr = pageurlarr[1].Split(new string[] { "&" }, StringSplitOptions.None);
                                            pagenum = DataConverter.CLng(pageurlarrarr[0]);
                                        }
                                        else
                                        {
                                            pagenum = DataConverter.CLng(pageurlarr[1]);
                                        }
                                    }
                                    if (pagenum > maxpagenum) { maxpagenum = pagenum; }
                                    string filename = "_" + infoid.ToString() + "_" + pagenum.ToString();
                                    if (!string.IsNullOrEmpty(NodeUrl))
                                    {
                                        if (NodeUrl.IndexOf(".") > -1)
                                        {
                                            string[] nodearr = NodeUrl.Split(new string[] { "." }, StringSplitOptions.None);
                                            NodeUrl = nodearr[0].ToString() + filename + "." + nodearr[1].ToString();
                                        }
                                    }

                                    string aspxpage = matchResults.ToString();
                                    aspxpage = aspxpage.Replace("'", "");

                                    aspxpage = "/" + aspxpage;//动态地址
                                    string htmlpage = "/" + GeneratedDirectory + NodeUrl;//静态地址
                                    contents = contents.Replace(matchResults.ToString(), htmlpage);
                                }


                                if (!string.IsNullOrEmpty(nodeinfo.IndexTemplate))
                                {
                                    if (!string.IsNullOrEmpty(NodeUrl))
                                    {
                                        if (NodeUrl != "")
                                        {
                                            string tempNodeurl = "../../" + GeneratedDirectory + NodeUrl;
                                            if (nodeinfo.HtmlPosition == 0)
                                            {
                                                tempNodeurl = "../../" + NodeUrl;
                                            }
                                            if (FileSystemObject.IsExist(HttpContext.Current.Server.MapPath(tempNodeurl), FsoMethod.File))
                                            {
                                                contents = contents.Replace(matchResults.ToString(), "/" + GeneratedDirectory + NodeUrl);
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        if (matchResults.ToString().IndexOf("NodePage.aspx") > -1)
                        {
                            string stringlist = matchResults.ToString();
                            int infoid = GetInfoID(stringlist, "nodeid=");
                            M_Node nodeinfo = nodeBll.GetNodeXML(infoid);
                            string NodeListUrl = nodeinfo.NodeListUrl;
                            if (!string.IsNullOrEmpty(nodeinfo.ListTemplateFile))
                            {
                                if (!string.IsNullOrEmpty(NodeListUrl))
                                {
                                    if (NodeListUrl != "")
                                    {
                                        string nodelisturl = "../.." + GeneratedDirectory + NodeListUrl;
                                        if (nodeinfo.HtmlPosition == 0)
                                        {
                                            nodelisturl = "../.." + NodeListUrl;
                                        }

                                        if (FileSystemObject.IsExist(HttpContext.Current.Server.MapPath(nodelisturl), FsoMethod.File))
                                        {
                                            contents = contents.Replace(matchResults.ToString(), GeneratedDirectory + NodeListUrl);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    matchResults = matchResults.NextMatch();
                }

                if (thisnodeid > 0)
                {
                    if (maxpagenum > 0)
                    {
                        for (int p = 1; p <= maxpagenum; p++)
                        {
                            string filename = "_" + thisnodeid.ToString() + "_" + p.ToString();
                            if (p == 1) { filename = ""; }
                            string nodepagenuls = "";
                            if (!string.IsNullOrEmpty(nodeBll.GetNodeXML(thisnodeid).NodeUrl))
                            {
                                if (nodeBll.GetNodeXML(thisnodeid).NodeUrl.IndexOf(".") > -1)
                                {
                                    string[] nodearr = nodeBll.GetNodeXML(thisnodeid).NodeUrl.Split(new string[] { "." }, StringSplitOptions.None);
                                    nodepagenuls = nodearr[0].ToString() + filename + "." + nodearr[1].ToString();
                                }
                            }
                            string aspxpage = "/ColumnList.aspx?NodeID=" + thisnodeid.ToString() + "&p=" + p;//动态地址
                            string aspxpages = "ColumnList.aspx?NodeID=" + thisnodeid.ToString() + "&p=" + p;//动态地址
                            string htmlpage = GeneratedDirectory + nodepagenuls;//静态地址
                            string temps = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace(System.Web.HttpContext.Current.Request.RawUrl, "");
                            string temptxt = BaseClass.Sendinfo(temps + aspxpage, "");//获得分页的内容
                            temptxt = HttpContext.Current.Server.UrlDecode(temptxt);
                            temptxt = GetPagenurl(ref temptxt);
                            B_Release.AddResult("栏目列表页", htmlpage);
                            FileSystemObject.WriteFile(SitePhyPath + htmlpage, temptxt);
                        }
                    }
                }
                maxpagenum = 0;
                thisnodeid = 0;
            }
            catch (Exception){}
            return contents;
        }
        /// <summary>
        /// 获得扩展名
        /// </summary>
        public string GetFileEx(int ContentFileEx)
        {
            string contentfex = ".html";
            switch (ContentFileEx)
            {
                case 0:
                    contentfex = ".html";
                    break;
                case 1:
                    contentfex = ".htm";
                    break;
                case 2:
                    contentfex = ".shtml";
                    break;
                case 3:
                    contentfex = ".aspx";
                    break;
            }
            return contentfex;
        }
        /// <summary>
        /// 获得资料ID
        /// </summary>
        public int GetInfoID(string content, string itemlist)
        {
            int infosid = 0;
            if (content.IndexOf("?") > -1)
            {
                string[] contentarr = content.Split(new string[] { "?" }, StringSplitOptions.RemoveEmptyEntries);
                for (int c = 0; c < contentarr.Length; c++)
                {
                    string wenhao = contentarr[c].ToString().Replace("\"", ""); ;

                    if (wenhao.IndexOf("&") > -1)
                    {
                        string[] wenhaarr = wenhao.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int d = 0; d < wenhaarr.Length; d++)
                        {

                            if (wenhaarr[d].ToLower().IndexOf(itemlist) > -1)
                            {
                                infosid = DataConverter.CLng(wenhaarr[d].Replace(itemlist, ""));
                            }
                        }
                    }
                    else
                    {
                        if (wenhao.ToLower().IndexOf(itemlist) > -1)
                        {
                            infosid = DataConverter.CLng(wenhao.ToLower().Replace(itemlist, ""));
                        }
                    }
                }
            }
            return infosid;
        }

        #region  生成发布
        public int m_CreateCount;
        /// <summary>
        /// 发布选定的单页
        /// </summary>
        public void CreateSingleByID(string nodeids)
        {
            DataTable dt = nodeBll.GetCreateSingleByID(nodeids);
            m_CreateCount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                CreateNodePage(DataConverter.CLng(dr["NodeID"]));
            }
        }
        /// <summary>
        /// 发布所有单页
        /// </summary>
        public void CreateSingle()
        {
            DataTable dt = nodeBll.SelByType("2");
            m_CreateCount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                CreateNodePage(DataConverter.CLng(dr["NodeID"]));
            }
        }
        /// <summary>
        /// 发布选定的专题页
        /// </summary>
        public void CreateSpecial(string InfoId)
        {
            string[] specID = InfoId.Split(',');
            for (int i = 0; i < specID.Length; i++)
            {
                DataTable dt = bContent.SelBySpecialID(specID[i]);
                m_CreateCount = dt.Rows.Count;
                foreach (DataRow dr in dt.Rows)
                {
                    CreateInfo(DataConverter.CLng(dr["GeneralID"]), DataConverter.CLng(dr["NodeID"]), DataConverter.CLng(dr["ModelID"]));
                }

            }
        }
        /// <summary>
        /// 发布所有栏目
        /// </summary>
        public void CreateColumnAll()
        {
            DataTable dt = nodeBll.SelByType("1");
            m_CreateCount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    CreateNodePage(DataConverter.CLng(dr["NodeID"]));
                    CreateList(DataConverter.CLng(dr["NodeID"]));
                }
                catch { }
            }
        }
        /// <summary>
        /// 发布选定的栏目页
        /// </summary>
        /// <param name="InfoId"></param>
        public void CreateColumnByID(string InfoId)
        {
            DataTable dt = nodeBll.SelByIDS(InfoId, "1,2");
            if (dt != null && dt.Rows.Count > 0)
            {
                m_CreateCount = dt.Rows.Count;
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CreateNodePage(DataConverter.CLng(dr["NodeID"]));
                        CreateList(DataConverter.CLng(dr["NodeID"]));
                    }
                }
                catch (Exception ex)
                {
                    ZLLog.L(Model.ZLEnum.Log.labelex, "生成栏目出错:" + ex.Message);
                }
            }
        }
        /// <summary>
        /// 发布选定的多个内容页
        /// </summary>
        public void createann(string InfoId)
        {
            if (SafeSC.CheckIDS(InfoId))
            {
                DataTable dt = bContent.SelByIDS(InfoId);
                foreach (DataRow dr in dt.Rows)
                {
                    CreateInfo(DataConverter.CLng(dr["GeneralID"]), DataConverter.CLng(dr["NodeID"]), DataConverter.CLng(dr["ModelID"]));
                }
            }
        }
        /// <summary>
        /// 发布选定的内容页
        /// </summary>
        /// <param name="InfoId"></param>
        public void CreateContentColumn(int InfoId)
        {
            M_CommonData dt = bContent.GetCommonData(InfoId);
            m_CreateCount = 1;
            CreateInfo(DataConverter.CLng(dt.GeneralID), DataConverter.CLng(dt.NodeID), DataConverter.CLng(dt.ModelID));
        }
        /// <summary>
        /// 发布选定的栏目的内容页
        /// </summary>
        /// <param name="InfoId"></param>
        public void CreateInfoColumn(string InfoId)
        {
            DataTable dt = bContent.GetCreateNodeList(InfoId);
            m_CreateCount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                CreateInfo(DataConverter.CLng(dr["GeneralID"]), DataConverter.CLng(dr["NodeID"]), DataConverter.CLng(dr["ModelID"]));
            }
        }
        /// <summary>
        /// 按日期发布内容页
        /// </summary>
        /// <param name="modelId"></param>
        public void CreateInfoDate(string InfoId)
        {
            DateTime ID1 = DataConverter.CDate(InfoId.Split(new char[] { ',' })[0]);
            DateTime ID2 = DataConverter.CDate(InfoId.Split(new char[] { ',' })[1]);
            DataTable dt = bContent.GetCreateDateList(ID1, ID2);
            m_CreateCount = dt.Rows.Count;
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                //---Coffee
                CreateInfo(DataConverter.CLng(dr["GeneralID"]), DataConverter.CLng(dr["NodeID"]), DataConverter.CLng(dr["ModelID"]));
                i++;
            }
            dt = dt.DefaultView.ToTable(true, "NodeID");//生成栏目列表
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                CreateNodePage(Convert.ToInt32(dt.Rows[j]["NodeID"]));
            }
        }
        /// <summary>
        /// 按日期发布内容页
        /// </summary>
        /// <param name="modelId"></param>
        public void CreateInfoDate(string InfoId, int NodeID)
        {
            DataTable newDT = new DataTable();
            newDT.Columns.Add(new DataColumn("NodeID", System.Type.GetType("System.Int32")));
            newDT.Columns.Add(new DataColumn("NodeName", System.Type.GetType("System.String")));
            DataRow dr = newDT.NewRow();
            dr[0] = NodeID;
            dr[1] = "首页";
            newDT.Rows.Add(dr);
            nodeBll.GetColumnList(NodeID, newDT);
            string str = "";
            for (int i = 0; i < newDT.Rows.Count; i++)
            {
                str += newDT.Rows[i]["NodeID"].ToString() + ",";
            }
            if (str.EndsWith(","))
            {
                str = str.Substring(0, str.Length - 1);
            }

            DateTime ID1 = DataConverter.CDate(InfoId.Split(new char[] { ',' })[0]);
            DateTime ID2 = DataConverter.CDate(InfoId.Split(new char[] { ',' })[1]);
            DataTable dt = bContent.GetCreateDateList(ID1, ID2);
            DataRow[] drarr = dt.Select(" NodeID in (" + str + ")");

            m_CreateCount = drarr.Length;
            foreach (DataRow tdr in drarr)
            {
                CreateInfo(DataConverter.CLng(tdr["GeneralID"]), DataConverter.CLng(tdr["NodeID"]), DataConverter.CLng(tdr["ModelID"]));
            }
        }
        /// <summary>
        /// 发布最新个数的内容页
        /// </summary>
        /// <param name="InfoId"></param>
        public void CreateLastInfoRecord(string InfoId)
        {
            DataTable dt = bContent.GetCreateCountList(DataConverter.CLng(InfoId));
            m_CreateCount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    CreateInfo(DataConverter.CLng(dr["GeneralID"]), DataConverter.CLng(dr["NodeID"]), DataConverter.CLng(dr["ModelID"]));
                }
                catch{}
            }
        }
        /// <summary>
        /// 发布所有内容页
        /// </summary>
        public void CreateInfo()
        {
            DataTable dt = bContent.GetCreateAllList();
            m_CreateCount = dt.Rows.Count;
            foreach (DataRow dr in dt.Rows)
            {
                CreateInfo(DataConverter.CLng(dr["GeneralID"]), DataConverter.CLng(dr["NodeID"]), DataConverter.CLng(dr["ModelID"]));
            }
        }
        /// <summary>
        /// 发布指定节点的所有内容页
        /// </summary>
        public void CreateInfo(int NodeID)
        {
            DataTable newDT = new DataTable();
            newDT.Columns.Add(new DataColumn("NodeID", System.Type.GetType("System.Int32")));
            newDT.Columns.Add(new DataColumn("NodeName", System.Type.GetType("System.String")));
            DataRow dr = newDT.NewRow();
            dr[0] = NodeID;
            dr[1] = "首页";
            newDT.Rows.Add(dr);
            nodeBll.GetColumnList(NodeID, newDT);
            string str = "";
            for (int i = 0; i < newDT.Rows.Count; i++)
            {
                str += newDT.Rows[i]["NodeID"].ToString() + ",";
            }
            if (str.EndsWith(","))
            {
                str = str.Substring(0, str.Length - 1);
            }
            CreateInfoColumn(str);
        }
        /// <summary>
        /// 发布主页
        /// </summary>
        public void CreatePageIndex()
        {
            m_CreateCount++;
            string IndexDir = "/" + SiteConfig.SiteOption.IndexTemplate.TrimStart('/');
            IndexDir = SitePhyPath + SiteConfig.SiteOption.TemplateDir + IndexDir;
            if (DataConverter.CLng(SiteConfig.SiteOption.IndexEx) < 3)
            {
                string indexex = GetFileEx(DataConverter.CLng(SiteConfig.SiteOption.IndexEx));
                if (!string.IsNullOrEmpty(IndexDir))
                {
                    CreateIndex(IndexDir, indexex, "/");
                }
                else
                {
                    B_Release.AddResult("未指定主页模板");
                }
            }
            else
            {
                B_Release.AddResult("主页--设置为动态生成略过");
            }
        }
        #endregion

    }
}