using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;

/// <summary>
/// Repeater分页公共类,用于支持/Plat/目录
/// </summary>
public class PageCommon
{
    /// <summary>
    /// Page
    /// </summary>
    public static int GetCPage()
    {
    
        int cPage = DataConverter.CLng(HttpContext.Current.Request["Page"]);
        cPage = cPage < 1 ? 1 : cPage;
        return cPage;
    }
    public static int GetPageCount(int pageSize, int itemCount)
    {
        int pageCount = 0;
        if (pageSize > 0 && itemCount > 0)
            pageCount = itemCount / pageSize + ((itemCount % pageSize > 0) ? 1 : 0);
        return pageCount;
    }
    public static object GetPageDT(int psize, int cpage, DataTable dt, out int pcount)
    {
        return PageHelper.GetPageDT(psize, cpage, dt, out pcount);
    }
    /// <summary>
    /// 公用分页方法,BootStrap
    /// </summary>
    /// <param name="pcount">超过页则隐藏</param>
    /// <param name="hrefTlp">超链接模板,默认以url跳转,可依需要扩展为ajax等</param>
    public static string CreatePageHtml(int pageCount, int cPage, int pcount = 10, string hrefTlp = "")
    {
        if (string.IsNullOrEmpty(hrefTlp)) hrefTlp = "<a href='@querypage=@page' title='@title'>@text</a>";
        if (pageCount < 2) return "";
        int i = 1, maxPage = cPage / pcount > 0 ? pcount * (cPage / pcount) + pcount : pcount;
        string url = HttpContext.Current.Request.Url.Query.Contains("?") ? HttpContext.Current.Request.Url.Query + "&" : HttpContext.Current.Request.Url.Query + "?";
        string query = Regex.Split(url, "page=")[0];
        cPage = cPage < 1 ? 1 : (cPage > pageCount ? pageCount : cPage);
        string pageHtml = "<ul class='pagination' style='margin:0 0 0 0;position:relative;top:3px;'>"
                        + "<li " + (cPage > 1 ? "" : "class='disabled'") + ">" + TlpRep(hrefTlp, query, 1, "&laquo;") + "</li>";
        if (maxPage > pcount) { i = (maxPage / pcount) * pcount - pcount; }//处理i值,让i值保持准确
        if (cPage >= pcount)
            pageHtml += "<li>" + TlpRep(hrefTlp, query, (maxPage - pcount * 2), "...<span class='sr-only'>(current)</span>") + "</li>";
        for (; i <= pageCount && i < maxPage; i++)
            pageHtml += "<li " + (cPage != i ? "" : "class='active'") + ">" + TlpRep(hrefTlp, query, i, i.ToString() + "<span class='sr-only'>(current)</span>") + "</li>";
        if (pageCount > maxPage)
            pageHtml += "<li>" + TlpRep(hrefTlp, query, maxPage, "...<span class='sr-only'>(current)</span>") + "</li>";
        pageHtml += "<li>" + TlpRep(hrefTlp, query, pageCount, "&raquo;", "尾页") + "</li></ul>";
        return pageHtml; 
    }
    //替换HrefTlp模板
    private static string TlpRep(string hrefTlp,string query,int page,string text,string title="") 
    {
        return hrefTlp.Replace("@query", query).Replace("@page", page.ToString()).Replace("@text",text).Replace("@title", title);
    }
    /// <summary>
    /// 用于Url重写后分页,后期整合
    /// </summary>
    public static string CreatePageHtml(int pageCount, int cPage, string query, int pcount = 10)
    {
        if (pageCount < 2) return "";
        int i = 1, maxPage = cPage / pcount > 0 ? pcount * (cPage / pcount) + pcount : pcount;
        cPage = cPage < 1 ? 1 : (cPage > pageCount ? pageCount : cPage);
        string pageHtml = "<ul class='pagination' style='margin:0 0 0 0;position:relative;top:3px;'>"
                        + "<li " + (cPage > 1 ? "" : "class='disabled'") + "><a href='" + string.Format(query, 1) + "'>&laquo;</a></li>";
        if (maxPage > pcount) { i = (maxPage / pcount) * pcount - pcount; }
        if (cPage >= pcount)
            pageHtml += "<li><a href='" + string.Format(query, (maxPage - pcount * 2)) + "';>...<span class='sr-only'>(current)</span></a></li>";
        for (; i <= pageCount && i < maxPage; i++)
            pageHtml += "<li " + (cPage != i ? "" : "class='active'") + "><a href='" + string.Format(query, i) + "';>" + i + "<span class='sr-only'>(current)</span></a></li>";
        if (pageCount > maxPage)
            pageHtml += "<li><a href='" + string.Format(query, pageCount) + "';>...<span class='sr-only'>(current)</span></a></li>";
        pageHtml += "<li><a href='" + string.Format(query, pageCount) + "' title='尾页'>&raquo;</a></li></ul>";
        return pageHtml;
    }
    public static string GetTlpDP(string name,bool ispre=false,string preurl="")
    {
        string url = CustomerPageAction.customPath2 + "Template/TemplateEdit.aspx?setTemplate=/template/" + SiteConfig.SiteOption.TemplateName + "/&filepath=/";
        StringBuilder builder = new StringBuilder();
        builder.Append("<div class='btn-group Template_btn' data-bind='" + name + "' id='" + name + "_body'>");
        builder.Append("<button type='button' class='btn btn-default dropdown-toggle' style='min-width:300px;' data-toggle='dropdown' aria-expanded='false'>");
        builder.Append("<span class='gray_9'><i class='fa fa-warning'></i>点击选择模板!</span> <span class='pull-right'><span class='caret'></span></span></button>");
        builder.Append("<ul class='dropdown-menu Template_files' role='menu'></ul>");
        //builder.Append("<input type='hidden' id='Tlp_Hid' name='Tlp_Hid' />");
        builder.Append("</div>");
        builder.Append("<input type='button' value='手工绑定' class='btn btn-info' onclick='Tlp_ShowSel(\"" + name + "\");' style='margin-left:5px;'/>");
        builder.Append("<a href='javascript:;' onclick='Tlp_EditHtml(\"" + url + "\",\"" + name + "\");' title='编辑模板' class='btn btn-info' style='margin-left:5px;'>编辑模板</a>");
        if (ispre)
        { builder.Append("<a href='" + preurl + "' target='_blank' style='margin-left:5px;'><i class='fa fa-eye'></i></a>"); }
        return builder.ToString();
    }
}