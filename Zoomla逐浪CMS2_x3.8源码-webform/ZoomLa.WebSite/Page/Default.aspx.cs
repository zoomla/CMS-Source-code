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

public partial class Pageindex : System.Web.UI.Page
{
    protected B_CreateHtml b_CH = new B_CreateHtml();
    protected B_PageReg b_PageReg = new B_PageReg();
    protected B_Content b_C = new B_Content();
    protected M_CommonData m_CD = new M_CommonData();
    protected B_ModelField b_FM = new B_ModelField();
    protected DataTable dt = new DataTable();

    protected M_PageReg m_PageReg = new M_PageReg();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(base.Request.QueryString["Pageid"]))
        {
            int Pageid = DataConverter.CLng(base.Request.QueryString["Pageid"]);
            m_PageReg = b_PageReg.SelReturnModel(Pageid);
            //pageinfo 黄页注册信息
            if (m_PageReg.ID < 0)
            {
                Response.Write("[产生错误的可能原因：您访问的黄页信息不存在！]");
                Response.End();
            }
            if (m_PageReg.Status != 99)
            {
                Response.Write("[产生错误的可能原因：您访问的黄页信息未经过审核！]");
                Response.End();
            }
            string tablename = m_PageReg.TableName;
            if (tablename.IndexOf("ZL_Reg_") == -1)
            {
                Response.Write("[产生错误的可能原因：您访问的黄页信息不存在！]");
                Response.End();
            }
            string pageuser = m_PageReg.UserName;
            int Itemid = DataConverter.CLng(m_PageReg.InfoID);

            DataTable dt = b_FM.SelectTableName(tablename, "UserName = '" + pageuser + "'");
            //pagesmallinfo-黄页详细注册信息
            if (dt.Rows.Count < 1)
            {
                Response.Write("[产生错误的可能原因：您访问的黄页信息不存在！]");
                Response.End();
            }
            if (string.IsNullOrEmpty(m_PageReg.Template))
            {
                Response.Write("[产生错误的可能原因：该黄页未指定模板！]");
                Response.End();
            }
            else
            {
                string templateurl = "";
                if (string.IsNullOrEmpty(m_PageReg.Template))
                {
                }
                else
                {
                    templateurl = m_PageReg.Template;
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
                indexstr = this.b_CH.CreateHtml(indexstr, Cpage, Pageid, "0"); //黄页最后一个为int类型
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
                            //文件名
                            string file1 = "index.aspx?pageid=" + Pageid.ToString();
                            //取分页标签处理结果 返回字符串数组 根据数组元素个数生成几页 
                            string ilbl = pagelabel.Replace("{ZL.Page ", "").Replace("/}", "").Replace(" ", ",");
                            string lblContent = "";
                            int NumPerPage = 500;
                            IList<string> ContentArr = new List<string>();
                            if (string.IsNullOrEmpty(ilbl))
                            {
                                lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                ContentArr = this.b_CH.GetContentPage(infoContent, NumPerPage);
                            }
                            else
                            {
                                string[] paArr = ilbl.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (paArr.Length == 0)
                                {
                                    lblContent = "{loop}<a href=\"{$pageurl/}\">{$pageid/}</a>$$$<b>[{$pageid/}]</b>{/loop}"; //默认格式的分页导航
                                    ContentArr = this.b_CH.GetContentPage(infoContent, NumPerPage);
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
                                    ContentArr = this.b_CH.GetContentPage(infoContent, NumPerPage);
                                }
                            }
                            if (ContentArr.Count > 0) //存在分页数据
                            {
                                ContentHtml = ContentHtml.Replace(infotmp, ContentArr[Cpage - 1]);
                                ContentHtml = ContentHtml.Replace(pagelabel, this.b_CH.GetPage(lblContent, Pageid, Cpage, ContentArr.Count, NumPerPage));
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
                M_PageReg mpage = b_PageReg.SelReturnModel(Pageid);
                if (mpage != null && mpage.ID > 0)
                {
                    HttpContext.Current.Response.Cookies["users"]["u"] = mpage.UserID.ToString();
                    HttpContext.Current.Response.Cookies["users"]["pageid"] = mpage.ID.ToString();
                }
                Response.Write(ContentHtml);
            }
        }
        else
        {
            Response.Write("[产生错误的可能原因：没有找到黄页信息！访问规则：Default.aspx?Pageid=黄页ID]");
            Response.End();
        }
    }
    /// <summary>
    /// 获得缓存
    /// </summary>
    public string GetCatch(string key)
    {
        return Cache.Get(key).ToString();
    }

    /// <summary>
    /// 设置文件缓存
    /// </summary>
    private void SetCatch(string key, string dirvalue)
    {
        if (Cache.Get(key) == null || Cache.Get(key).ToString() == "")
        {
            Cache.Insert(key, FileSystemObject.ReadFile(dirvalue), new System.Web.Caching.CacheDependency(dirvalue));
        }
    }
}