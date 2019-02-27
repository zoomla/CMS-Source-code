using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using ZoomLa.BLL.Page;
using ZoomLa.Model.Page;

public partial class PageContent : System.Web.UI.Page
{
    B_CreateHtml bll = new B_CreateHtml();
    B_PageReg b_PageReg = new B_PageReg();
    B_Content bcontent = new B_Content();
    B_Templata tll = new B_Templata();
    B_Sensitivity sell = new B_Sensitivity();
    public int ItemID { get { return DataConverter.CLng(Request.QueryString["ItemID"]); } }
    //ZL_PageReg
    public int pageID { get { return DataConverter.CLng(Request.QueryString["PageID"]); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(base.Request.QueryString["ItemID"]))
        {
            B_Model bmode = new B_Model();
            B_Node bnode = new B_Node();
            B_ModelField mfll = new B_ModelField();

            int CPage = string.IsNullOrEmpty(base.Request.QueryString["p"]) ? 1 : DataConverter.CLng(base.Request.QueryString["p"]);
            if (CPage <= 0)
                CPage = 1;
            M_CommonData ItemInfo = bcontent.GetCommonData(ItemID);
            if (ItemInfo.IsNull)
            {
                Response.Write("[产生错误的可能原因：内容信息不存在或未开放！]");
            }
            M_ModelInfo modelinfo = bmode.GetModelById(ItemInfo.ModelID);

            M_Templata Nodeinfo = tll.Getbyid(ItemInfo.NodeID);
            string TemplateDir = GetTempPath(ItemInfo, Nodeinfo, pageID);
            if (string.IsNullOrEmpty(TemplateDir))
            {
                Response.Write("[产生错误的可能原因：该内容所属模型未指定模板！]");
            }
            else
            {
                int Cpage = 1;
                if (string.IsNullOrEmpty(base.Request.QueryString["p"]))
                {
                    Cpage = 1;
                }
                else
                {
                    Cpage = DataConverter.CLng(base.Request.QueryString["p"]);
                }
                string ContentHtml = "";
                try
                {
                     ContentHtml = FileSystemObject.ReadFile(TemplateDir);
                }
                catch
                {
                    Response.Write("[产生错误的可能原因：该内容所属模型未指定模板！]");
                }

                ContentHtml = this.bll.CreateHtml(ContentHtml, Cpage, ItemID, 0);

                /* --------------------判断是否分页 并做处理------------------------------------------------*/
                if (!string.IsNullOrEmpty(ContentHtml))
                {
                    string infoContent = ""; //进行处理的内容字段
                    string pagelabel = "";
                    string infotmp = "";
                    #region 分页符分页
                    string pattern = @"{\#PageCode}([\s\S])*?{\/\#PageCode}";  //查找要分页的内容
                    if (Regex.IsMatch(ContentHtml, pattern, RegexOptions.IgnoreCase))
                    {
                        infoContent = Regex.Match(ContentHtml, pattern, RegexOptions.IgnoreCase).Value;
                        infotmp = infoContent;
                        infoContent = infoContent.Replace("{#PageCode}", "").Replace("{/#PageCode}", "");
                        //查找分页标签
                        //bool flag = false;
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
                            }
                            else   //进行内容分页处理
                            {
                                //文件名
                                string file1 = "Content.aspx?ID=" + ItemID.ToString();
                                //取分页标签处理结果 返回字符串数组 根据数组元素个数生成几页 
                                string ilbl = pagelabel.Replace("{ZL.Page ", "").Replace("/}", "").Replace(" ", ",");
                                string lblContent = "";
                                IList<string> ContentArr = new List<string>();
                                if (string.IsNullOrEmpty(ilbl))
                                {
                                    lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                    ContentArr = this.bll.GetContentPage(infoContent);
                                }
                                else
                                {
                                    string[] paArr = ilbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (paArr.Length == 0)
                                    {
                                        lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                        ContentArr = this.bll.GetContentPage(infoContent);
                                    }
                                    else
                                    {
                                        string lblname = paArr[0].Split(new char[] { '=' })[1].Replace("\"", "");
                                        B_Label blbl = new B_Label();
                                        lblContent = blbl.GetLabelXML(lblname).Content;
                                        if (string.IsNullOrEmpty(lblContent))
                                        {
                                            lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                        }
                                        ContentArr = this.bll.GetContentPage(infoContent);
                                    }
                                }

                                if (ContentArr.Count > 0) //存在分页数据
                                {
                                    ContentHtml = ContentHtml.Replace(infotmp, ContentArr[CPage - 1]);
                                    ContentHtml = ContentHtml.Replace(pagelabel, this.bll.GetPage(lblContent, ItemID, CPage, ContentArr.Count, ContentArr.Count));
                                }
                                else
                                {
                                    ContentHtml = ContentHtml.Replace(infotmp, infoContent);
                                    ContentHtml = ContentHtml.Replace(pagelabel, "");
                                }
                            }
                        }
                        else  //没有分页标签
                        {
                            //如果设定了分页内容字段 将该字段内容的分页标志清除
                            if (!string.IsNullOrEmpty(infoContent))
                                ContentHtml = ContentHtml.Replace(infotmp, infoContent);
                        }
                    }
                    #endregion

                    pattern = @"{\#Content}([\s\S])*?{\/\#Content}";  //查找要分页的内容
                    if (Regex.IsMatch(ContentHtml, pattern, RegexOptions.IgnoreCase))
                    {
                        infoContent = Regex.Match(ContentHtml, pattern, RegexOptions.IgnoreCase).Value;
                        infotmp = infoContent;
                        infoContent = infoContent.Replace("{#Content}", "").Replace("{/#Content}", "");

                        //查找分页标签
                        //bool flag = false;
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
                            }
                            else   //进行内容分页处理
                            {
                                //文件名
                                string file1 = "Content.aspx?ID=" + ItemID.ToString();
                                //取分页标签处理结果 返回字符串数组 根据数组元素个数生成几页 
                                string ilbl = pagelabel.Replace("{ZL.Page ", "").Replace("/}", "").Replace(" ", ",");
                                string lblContent = "";
                                int NumPerPage = 500;
                                IList<string> ContentArr = new List<string>();

                                if (string.IsNullOrEmpty(ilbl))
                                {
                                    lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                    ContentArr = this.bll.GetContentPage(infoContent, NumPerPage);
                                }
                                else
                                {
                                    string[] paArr = ilbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (paArr.Length == 0)
                                    {
                                        lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                        ContentArr = this.bll.GetContentPage(infoContent, NumPerPage);
                                    }
                                    else
                                    {
                                        string lblname = paArr[0].Split(new char[] { '=' })[1].Replace("\"", "");
                                        if (paArr.Length > 1)
                                        {
                                            NumPerPage = DataConverter.CLng(paArr[1].Split(new char[] { '=' })[1].Replace("\"", ""));
                                        }
                                        B_Label blbl = new B_Label();
                                        lblContent = blbl.GetLabelXML(lblname).Content;
                                        if (string.IsNullOrEmpty(lblContent))
                                        {
                                            lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                        }
                                        ContentArr = this.bll.GetContentPage(infoContent, NumPerPage);
                                    }
                                }
                                if (ContentArr.Count > 0) //存在分页数据
                                {
                                    ContentHtml = ContentHtml.Replace(infotmp, ContentArr[CPage - 1]);
                                    ContentHtml = ContentHtml.Replace(pagelabel, this.bll.GetPage(lblContent, ItemID, CPage, ContentArr.Count, NumPerPage));
                                }
                                else
                                {
                                    ContentHtml = ContentHtml.Replace(infotmp, infoContent);
                                    ContentHtml = ContentHtml.Replace(pagelabel, "");
                                }
                            }
                        }
                        else  //没有分页标签
                        {
                            //如果设定了分页内容字段 将该字段内容的分页标志清除
                            if (!string.IsNullOrEmpty(infoContent))
                                ContentHtml = ContentHtml.Replace(infotmp, infoContent);
                        }
                    }
                }
                string patterns = @"{ZL\.Page([\s\S])*?\/}";
                string pagelabels = Regex.Match(ContentHtml, patterns, RegexOptions.IgnoreCase).Value;
                if (pagelabels != "")
                {
                    ContentHtml = ContentHtml.Replace(pagelabels, "");
                }
                /*--------------------- 分页内容处理结束-------------------------------------------------------------------------*/
                ContentHtml = sell.ProcessSen(ContentHtml);
                Response.Write(ContentHtml);
            }
        }
        else
        {
            Response.Write("[产生错误的可能原因：您访问的内容信息不存在!访问规则：PageContent.aspx?ItemID=信息ID]");
        }
    }

    /// <summary>
    /// 获得缓存
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetCatch(string key)
    {
        return Cache.Get(key).ToString();
    }

    /// <summary>
    /// 设置文件缓存
    /// </summary>
    /// <param name="key"></param>
    /// <param name="dirvalue"></param>
    private void SetCatch(string key, string dirvalue)
    {
        if (Cache.Get(key) == null || Cache.Get(key).ToString() == "")
        {
            Cache.Insert(key, FileSystemObject.ReadFile(dirvalue), new System.Web.Caching.CacheDependency(dirvalue));
        }
    }
    /// <summary>
    /// 返回该黄页的模板物理路径
    /// </summary>
    /// <param name="cData">ZL_CommonModel</param>
    /// <param name="tempMod">ZL_PageTemplate:栏目</param>
    private string GetTempPath(M_CommonData cData, M_Templata tempMod, int pageID)
    {
        if (pageID == 0) function.WriteErrMsg("未为该黄页栏目指定样式!");
        B_PageReg pageBll = new B_PageReg();
        M_PageReg pageMod = pageBll.SelReturnModel(pageID);
        B_PageStyle styleBll = new B_PageStyle();
        M_PageStyle styleMod = new M_PageStyle();
        M_CommonData ItemInfo = bcontent.GetCommonData(ItemID);//获取栏目信息
        M_Templata Nodeinfo = tll.Getbyid(ItemInfo.NodeID);
        if (pageMod == null) { function.WriteErrMsg("未找到黄页信息" + pageID); }
        if (pageMod.NodeStyle != 0)//样式优先读取客户自己设定的,再读取我们后台设定的
        {
            styleMod = styleBll.SelReturnModel(pageMod.NodeStyle);
        }
        else if (DataConverter.CLng(Nodeinfo.UserGroup) != 0)
        {
            styleMod = styleBll.SelReturnModel(DataConverter.CLng(Nodeinfo.UserGroup));
        }
        else { function.WriteErrMsg("该栏目未指定样式!!"); }
        string modelist = tempMod.Modelinfo;
        string tempUrl = "";
        string tempPath = "";

        //------TempUrl为栏目所指定的Html模板路径.
        if (!string.IsNullOrEmpty(modelist))//38,内容页/招聘内容页
        {
            if (modelist.IndexOf("|") > 0 && modelist.IndexOf(",") > 0)//如绑定多个模型则以|分隔,每个模型,可绑定不同的黄页
            {
                string[] modearr = modelist.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < modearr.Length; i++)
                {
                    string[] arr = modearr[i].Split(new char[] { ',' });
                    if (cData.ModelID == Convert.ToInt32(arr[0]))
                    {
                        tempUrl = arr[1]; break;
                    }
                }
            }
            else if (modelist.IndexOf(",") > 0)
            {
                tempUrl = modelist.Split(new char[] { ',' })[1];
            }
        }
        //-----获取该黄页所绑定的样式,将栏目模板与样式模板路径组合,UserGroup即为其所绑定的样式ID
        tempPath = Server.MapPath(styleMod.StylePath + tempUrl);
        return tempPath;
    }
}