using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.Page;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.Page;

namespace ZoomLaCMS.Controllers
{
    public class FrontPageController : Controller
    {
        B_CreateHtml createBll = new B_CreateHtml();
        B_PageReg pageBll = new B_PageReg();
        B_Content conBll = new B_Content();
        B_ModelField fieldBll=new B_ModelField();
        B_Model modBll=new B_Model();
        M_PageReg pageMod = new M_PageReg();
        B_Templata tll = new B_Templata();
        B_PageStyle styleBll = new B_PageStyle();
        private int CPage
        {
            get
            {
                int cpage = string.IsNullOrEmpty(base.Request.QueryString["p"]) ? 1 : DataConverter.CLng(base.Request.QueryString["p"]);
                if (cpage <= 0)
                    cpage = 1;
                return cpage;
            }

        }
        private int ItemID { get { return DataConverter.CLng(Request.QueryString["ItemID"]); } }
        //ZL_PageReg
        private int pageID { get { return DataConverter.CLng(Request.QueryString["PageID"]); } }
        public void Default()
        {
            if (pageID < 1) { RepToClient("[产生错误的可能原因：没有找到黄页信息！访问规则：/Page/Default?Pageid=黄页ID]"); return; }
            pageMod = pageBll.SelReturnModel(pageID);
            if (pageMod.ID < 0) { RepToClient("[产生错误的可能原因：您访问的黄页信息不存在！]"); return; }
            if (pageMod.Status != 99) { RepToClient("[产生错误的可能原因：您访问的黄页信息未经过审核！]"); return; }
            if (pageMod.TableName.IndexOf("ZL_Reg_") == -1) { RepToClient("[产生错误的可能原因：您访问的黄页信息不存在！]"); return; }
            string pageuser = pageMod.UserName;
            int Itemid = DataConverter.CLng(pageMod.InfoID);

            DataTable dt = fieldBll.SelectTableName(pageMod.TableName, "UserName = '" + pageuser + "'");
            //pagesmallinfo-黄页详细注册信息
            if (dt.Rows.Count < 1)
            {
                RepToClient("[产生错误的可能原因：您访问的黄页信息不存在！]"); return;
            }
            if (string.IsNullOrEmpty(pageMod.Template))
            {
               RepToClient("[产生错误的可能原因：该黄页未指定模板！]"); return;
            }
            else
            {
                string templateurl = "";
                if (string.IsNullOrEmpty(pageMod.Template))
                {
                }
                else
                {
                    templateurl = pageMod.Template;
                }
                string TemplateDir = "";
                TemplateDir = SiteConfig.SiteOption.TemplateDir + "/" + templateurl;
                int Cpage = 1;
                if (string.IsNullOrEmpty(base.Request.QueryString["p"]))
                {
                    Cpage = 1;
                }
                else
                {
                    Cpage = DataConverter.CLng(base.Request.QueryString["p"]);
                }
                TemplateDir = base.Request.PhysicalApplicationPath + TemplateDir;
                TemplateDir = TemplateDir.Replace("/", @"\").Replace("\\\\", "\\");
                //获取模板html
                string indexstr = FileSystemObject.ReadFile(TemplateDir);
                indexstr = this.createBll.CreateHtml(indexstr, Cpage, pageID, "0"); //黄页最后一个为int类型
                string ContentHtml = indexstr;

                if (!string.IsNullOrEmpty(ContentHtml))
                {
                    /* --------------------判断是否分页 并做处理------------------------------------------------*/
                    string infoContent = ""; //进行处理的内容字段
                    string pagelabel = "";
                    string infotmp = "";
                    string pattern = @"{\#Content}([\s\S])*?{\/\#Content}";  //查找要分页的内容
                    if (Regex.IsMatch(ContentHtml, pattern, RegexOptions.IgnoreCase))
                    {
                        infoContent = Regex.Match(ContentHtml, pattern, RegexOptions.IgnoreCase).Value;
                        infotmp = infoContent;
                        infoContent = infoContent.Replace("{#Content}", "").Replace("{/#Content}", "");
                    }
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
                        }
                        else   //进行内容分页处理
                        {
                            //取分页标签处理结果 返回字符串数组 根据数组元素个数生成几页 
                            string ilbl = pagelabel.Replace("{ZL.Page ", "").Replace("/}", "").Replace(" ", ",");
                            string lblContent = "";
                            int NumPerPage = 500;
                            IList<string> ContentArr = new List<string>();
                            if (string.IsNullOrEmpty(ilbl))
                            {
                                lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                ContentArr = this.createBll.GetContentPage(infoContent, NumPerPage);
                            }
                            else
                            {
                                string[] paArr = ilbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (paArr.Length == 0)
                                {
                                    lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                    ContentArr = this.createBll.GetContentPage(infoContent, NumPerPage);
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
                                    ContentArr = this.createBll.GetContentPage(infoContent, NumPerPage);
                                }
                            }
                            if (ContentArr.Count > 0) //存在分页数据
                            {
                                ContentHtml = ContentHtml.Replace(infotmp, ContentArr[Cpage - 1]);
                                ContentHtml = ContentHtml.Replace(pagelabel, this.createBll.GetPage(lblContent, pageID, Cpage, ContentArr.Count, NumPerPage));
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
                /*--------------------- 分页内容处理结束-------------------------------------------------------------------------*/
                Response.Write(ContentHtml);
            }
        }
        public void PageContent()
        {
            if (ItemID < 1) {RepToClient("[产生错误的可能原因：您访问的内容信息不存在!访问规则：PageContent?ItemID=信息ID]"); return; }
                B_Model bmode = new B_Model();
                B_Node bnode = new B_Node();
                M_CommonData ItemInfo = conBll.GetCommonData(ItemID);
                if (ItemInfo.IsNull)
                {
                    RepToClient("[产生错误的可能原因：内容信息不存在或未开放！]"); return;
                }
                M_ModelInfo modelinfo = bmode.GetModelById(ItemInfo.ModelID);

                M_Templata Nodeinfo = tll.Getbyid(ItemInfo.NodeID);
                string TemplateDir = GetTempPath(ItemInfo, Nodeinfo, pageID);
                if (string.IsNullOrEmpty(TemplateDir))
                {
                    RepToClient("[产生错误的可能原因：该内容所属模型未指定模板！]"); return;
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
                        RepToClient("[产生错误的可能原因：该内容所属模型未指定模板！]"); return;
                    }

                    ContentHtml = this.createBll.CreateHtml(ContentHtml, Cpage, ItemID, 0);

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
                                    //取分页标签处理结果 返回字符串数组 根据数组元素个数生成几页 
                                    string ilbl = pagelabel.Replace("{ZL.Page ", "").Replace("/}", "").Replace(" ", ",");
                                    string lblContent = "";
                                    IList<string> ContentArr = new List<string>();
                                    if (string.IsNullOrEmpty(ilbl))
                                    {
                                        lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                        ContentArr = this.createBll.GetContentPage(infoContent);
                                    }
                                    else
                                    {
                                        string[] paArr = ilbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        if (paArr.Length == 0)
                                        {
                                            lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                            ContentArr = this.createBll.GetContentPage(infoContent);
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
                                            ContentArr = this.createBll.GetContentPage(infoContent);
                                        }
                                    }

                                    if (ContentArr.Count > 0) //存在分页数据
                                    {
                                        ContentHtml = ContentHtml.Replace(infotmp, ContentArr[CPage - 1]);
                                        ContentHtml = ContentHtml.Replace(pagelabel, this.createBll.GetPage(lblContent, ItemID, CPage, ContentArr.Count, ContentArr.Count));
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
                                    //取分页标签处理结果 返回字符串数组 根据数组元素个数生成几页 
                                    string ilbl = pagelabel.Replace("{ZL.Page ", "").Replace("/}", "").Replace(" ", ",");
                                    string lblContent = "";
                                    int NumPerPage = 500;
                                    IList<string> ContentArr = new List<string>();

                                    if (string.IsNullOrEmpty(ilbl))
                                    {
                                        lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                        ContentArr = this.createBll.GetContentPage(infoContent, NumPerPage);
                                    }
                                    else
                                    {
                                        string[] paArr = ilbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                        if (paArr.Length == 0)
                                        {
                                            lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                            ContentArr = this.createBll.GetContentPage(infoContent, NumPerPage);
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
                                            ContentArr = this.createBll.GetContentPage(infoContent, NumPerPage);
                                        }
                                    }
                                    if (ContentArr.Count > 0) //存在分页数据
                                    {
                                        ContentHtml = ContentHtml.Replace(infotmp, ContentArr[Cpage - 1]);
                                        ContentHtml = ContentHtml.Replace(pagelabel, this.createBll.GetPage(lblContent, ItemID, CPage, ContentArr.Count, NumPerPage));
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
                    Response.Write(ContentHtml);
                }
        }
        public void Pagelink()
        {
            if (ItemID < 1) { RepToClient("[产生错误的可能原因：内容信息不存在或未开放！访问规则：Pagelink?ItemID=信息ID]"); return; }
            M_PageReg ItemInfo = pageBll.SelReturnModel(ItemID);
            if (ItemInfo.IsNull) { RepToClient("[产生错误的可能原因：内容信息不存在或未开放！]"); return; }
            M_ModelInfo modelinfo = modBll.GetModelById(ItemInfo.ModelID);
            string TempNode = "";
            string TempContent = ItemInfo.Template;
            string TemplateDir = modelinfo.ContentModule;

            //风格
            string pageuser = ItemInfo.UserName;
            DataTable cmdinfo = fieldBll.SelectTableName("ZL_PageReg", "TableName like 'ZL_Reg_%' and UserName='" + pageuser + "'");

            string templateurl = "/" + modelinfo.ContentModule;

            string styleurl = "";
            int lastindexOf = 0;
            if (templateurl.IndexOf('/') > -1)
            {
                lastindexOf = templateurl.LastIndexOf('/');
            }
            templateurl = styleurl + templateurl.Substring(lastindexOf, templateurl.Length - lastindexOf);

            TemplateDir = templateurl;

            if (!string.IsNullOrEmpty(TempContent))
            {
                int lastindexOfs = 0;
                if (TempContent.IndexOf('/') > -1)
                {
                    lastindexOfs = TempContent.LastIndexOf('/');
                }
                TempContent = styleurl + TempContent.Substring(lastindexOfs, TempContent.Length - lastindexOfs);

                TemplateDir = TempContent;
            }
            if (string.IsNullOrEmpty(TempContent))
            {
                if (!string.IsNullOrEmpty(TempNode))
                    TemplateDir = TempNode;
            }

            if (string.IsNullOrEmpty(TemplateDir))
            {
                RepToClient("[产生错误的可能原因：该内容所属模型未指定模板！]"); return;
            }
            else
            {
                TemplateDir = base.Request.PhysicalApplicationPath + SiteConfig.SiteOption.TemplateDir + TemplateDir;
                TemplateDir = TemplateDir.Replace("/", @"\");
                string ContentHtml = FileSystemObject.ReadFile(TemplateDir);
                ContentHtml = this.createBll.CreateHtml(ContentHtml, 0, ItemID, 0);
                Response.Write(ContentHtml);
            }
        }
        public void Pagelist()
        {
            int ItemID = DataConverter.CLng(Request.QueryString["NodeID"]);
            if (ItemID < 1) { RepToClient("[产生错误的可能原因：没有指定栏目ID！访问规则：Pagelist?NodeID=节点ID&Pageid=黄页ID]"); return; }
            int Pageid = DataConverter.CLng(base.Request.QueryString["Pageid"]);

            M_PageReg pageinfo = pageBll.SelReturnModel(Pageid);
            M_Templata tempinfo = tll.Getbyid(ItemID);
            string tablename = pageinfo.TableName;
            if (pageinfo.IsNull)
            {
                RepToClient("[产生错误的可能原因：您访问的黄页信息不存在！]"); return;
            }
            if (tablename.IndexOf("ZL_Reg_") == -1)
            {
                RepToClient("[产生错误的可能原因：黄页信息不存在！]"); return;
            }
            int Styleid = DataConverter.CLng(tempinfo.UserGroup);
            int userid = tempinfo.UserID;

            DataTable nodeinfo = tll.Getinputinfo("TemplateID", ItemID.ToString());
            if (nodeinfo.Rows.Count == 0) { RepToClient("[产生错误的可能原因：您访问的栏目信息不存在！]"); return; } 
            if (DataConverter.CLng(nodeinfo.Rows[0]["IsTrue"]) != 1) { RepToClient("[产生错误的可能原因：您访问的信息不可用！]"); return; }

            int nodetype = DataConverter.CLng(nodeinfo.Rows[0]["TemplateType"]);
            string opentype = nodeinfo.Rows[0]["OpenType"].ToString();
            if (nodetype == 3)
            {
                Response.Redirect(nodeinfo.Rows[0]["linkurl"].ToString());return;
            }
            //---获取路径

            M_PageStyle styleMod = new M_PageStyle();
            if (pageinfo.NodeStyle == 0) { RepToClient("[产生错误的可能原因：未为该黄页栏目指定样式!]"); return; }
            //-----获取该黄页所绑定的样式,将栏目模板与样式模板路径组合,UserGroup即为其所绑定的样式ID
            styleMod = styleBll.SelReturnModel(Convert.ToInt32(pageinfo.NodeStyle));
            string TemplateDir = Server.MapPath(styleMod.StylePath + tempinfo.TemplateUrl);
            string ContentHtml = FileSystemObject.ReadFile(TemplateDir);
            ContentHtml = this.createBll.CreateHtml(ContentHtml, CPage, ItemID, Pageid);

            string identifiers = nodeinfo.Rows[0]["identifiers"].ToString();
            if (!string.IsNullOrEmpty(identifiers))
            {
                ContentHtml = ContentHtml.Replace(identifiers, ItemID.ToString());
            }
            Response.Write(ContentHtml);
        }
        //----------------------------
        public void RepToClient(string msg)
        {
            Response.Clear(); Response.Write(msg); Response.Flush(); Response.End(); return;
        }
                /// <summary>
        /// 返回该黄页的模板物理路径
        /// </summary>
        /// <param name="cData">ZL_CommonModel</param>
        /// <param name="tempMod">ZL_PageTemplate:栏目</param>
        private string GetTempPath(M_CommonData cData, M_Templata tempMod, int pageID)
        {
            if (pageID == 0) { function.WriteErrMsg("未为该黄页栏目指定样式!"); return ""; }
            B_PageReg pageBll = new B_PageReg();
            M_PageReg pageMod = pageBll.SelReturnModel(pageID);
            B_PageStyle styleBll = new B_PageStyle();
            M_PageStyle styleMod = new M_PageStyle();
            M_CommonData ItemInfo = conBll.GetCommonData(ItemID);//获取栏目信息
            M_Templata Nodeinfo = tll.Getbyid(ItemInfo.NodeID);
            if (pageMod == null) { function.WriteErrMsg("未找到黄页信息" + pageID);return ""; }
            if (pageMod.NodeStyle != 0)//样式优先读取客户自己设定的,再读取我们后台设定的
            {
                styleMod = styleBll.SelReturnModel(pageMod.NodeStyle);
            }
            else if (DataConverter.CLng(Nodeinfo.UserGroup) != 0)
            {
                styleMod = styleBll.SelReturnModel(DataConverter.CLng(Nodeinfo.UserGroup));
            }
            else { function.WriteErrMsg("该栏目未指定样式!!");return ""; }
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
}
