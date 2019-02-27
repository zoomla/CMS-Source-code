using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Nodes;
using Winista.Text.HtmlParser.Util;
using Winista.Text.HtmlParser.Visitors;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.Common;
using ZoomLa.Components;

public partial class test_test : System.Web.UI.Page
{
    B_CreateHtml create = new B_CreateHtml();
    RegexHelper regHelper = new RegexHelper();
    HtmlHelper htmlHelp = new HtmlHelper();
    public int Action { get { return DataConverter.CLng(Request.QueryString["Action"]); } }
    public string VPath { get { return Request.QueryString["VPath"]; } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(VPath)) function.WriteErrMsg("路径不能为空");
        string html = SafeSC.ReadFileStr(VPath);
        if (string.IsNullOrEmpty(Request.QueryString["PreView"]))
        {
            //区块编辑,能否解析但不生成?
            //create.IsDesign = 2;
            string realhtml = create.CreateHtml(html);
            //tring designJson = JsonConvert.SerializeObject(create.DesignList); 
            //throw new Exception(designJson);
            //-----
            Regex reg = new Regex(@"(<body)[\s\S]*(/body>)", RegexOptions.IgnoreCase);
            string bak_html = reg.Replace(html, new MatchEvaluator(OutPutMatch));//去除Body
            html = html.Replace("{$CssDir/}", Call.GetLabel("{$CssDir/}"));
            html = html.Replace("{ZL:Boot()/}", Call.GetLabel("{ZL:Boot()/}"));

            string menu = "<div class='contextMenu' id='rmenu'><ul>";
            menu += "<li id='edit'><span class='fa fa-pencil'></span><span>编辑</span></li>";
            menu += "<li id='block'><span class='fa fa-th-large'></span><span>区块编辑</span></li>";
            //menu += "<li id='drag'><span class='fa fa-arrows'></span><span>开启拖动</span></li>";
            //menu += "<li id='enddrag'><span class='fa fa-stop'></span><span>停止拖动</span></li>";
            menu += "</ul></div>";
            StringBuilder builder = new StringBuilder();
            builder.Append("<script src='/JS/jquery-ui.min.js'></script>");
            builder.Append("<script src='/JS/Design/ZL_Tlp.js?ver=1.2'></script>");
            builder.Append("<script src='/JS/jquery.contextmenu.r2.js'></script>");
            builder.Append("<script>parent.SaveHead('" + HttpUtility.UrlEncode(bak_html) + "');</script>");
            //区块编辑
            //<script src='/JS/Controls/ZL_Dialog.js'></script><script src='/JS/Design/ZL_Design_Editor.js'></script>
            //string tlp = "<div id='design_div'><link href='/JS/Design/ZL_Design.css' rel='stylesheet' /></div><script>var labelJson=" + designJson + ";</script>";
            //html = html.Replace("<body>", "<body>" + Call.Boundary);//将flag之前的全截掉再加body
            //Response.Write(html + "<div id='editor_div'>" + menu + builder.ToString() + "</div>" + tlp);
        }
        else//开启预览
        {
            Response.Write(create.CreateHtml(html));
        }
    }
    private string OutPutMatch(Match match)
    {
        //return "<b>" + match.Value + "</b>";
        return Call.Boundary;
    }
}