using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using ZoomLa.BLL;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;

public class FrontPage : System.Web.UI.Page
{
    public B_Node nodeBll = new B_Node();
    public B_CreateHtml bll = new B_CreateHtml();
    public int ItemID
    {
        get
        {
            return DataConverter.CLng(B_Route.GetParam("ID", Page));
        }
    }
    /// <summary>
    /// 允许值为0页面判断,为0显示全部或==1
    /// </summary>
    public int Cpage { get { int cpage = DataConverter.CLng(B_Route.GetParam("CPage", Page)); return cpage < 0 ? 0 : cpage; } }
    public void ErrToClient(string str)
    {
        string html = SafeSC.ReadFileStr("/Prompt/error.html");
        html = html.Replace("@msg", str);
        Response.Clear(); Response.Write(html); Response.Flush(); Response.End();
    }
    //前端获取了模板后,传参给其,用于返回
    public void HtmlToClient(string templateUrl)
    {
        if (string.IsNullOrEmpty(templateUrl)) { ErrToClient("[产生错误的可能原因：未指定模板!]"); return; }
        if (templateUrl.StartsWith("可视设计_"))
        {
            string spage = Regex.Split(templateUrl, "可视设计_")[1];
            Server.Transfer("/design/spage/preview.aspx?pname=" + HttpUtility.UrlDecode(spage) + "&itemid=" + ItemID + "&cpage=" + Cpage, true);
        }
        else
        {
            Response.Clear(); Response.Write(TemplateToHtml(templateUrl)); Response.Flush(); Response.End();
        }
    }
    /// <summary>
    /// 每个页面必须实现,用于将模板解析为html
    /// </summary>
    public virtual string TemplateToHtml(string templateUrl)
    {
        string html = "";
        string vpath = (SiteConfig.SiteOption.TemplateDir + "/" + templateUrl);
        string TemplateDir = function.VToP(vpath);
        if (!File.Exists(TemplateDir)) { return "[产生错误的可能原因：(" + vpath + ")文件不存在]"; }
        html = FileSystemObject.ReadFile(TemplateDir);
        html = bll.CreateHtml(html, Cpage, ItemID, "1");
        if (SiteConfig.SiteOption.IsSensitivity == 1) { html = B_Sensitivity.Process(html); }
        return html;
    }
}