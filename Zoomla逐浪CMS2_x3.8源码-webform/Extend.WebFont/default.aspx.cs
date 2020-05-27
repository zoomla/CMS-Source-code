using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper.Addon;
using ZoomLa.Model;
using System.Runtime.InteropServices;
using ZoomLa.SQLDAL;
using ZoomLa.BLL.CreateJS;
using System.Data;
using ZoomLa.Common;
using ZoomLa.BLL.Helper;
using ZoomLa.Components;

public partial class test_test : System.Web.UI.Page
{
    B_CodeModel codeBll = new B_CodeModel("ZL_Font_Apply");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CreateFontHandler handler = CreateFont;
            //var result = handler.BeginInvoke(null, null);
            //handler.EndInvoke(result);
        }
    }
    protected void CreateFont_Btn_Click(object sender, EventArgs e)
    {
        string fontsrc = Font_DP.SelectedValue + ".ttf";
        string tlp = SafeSC.ReadFileStr("/WebFont/tlp.html");
        DataRow dr = codeBll.NewModel();
        dr["Text"] = StringHelper.SubStr(T1.Text, 500, "");
        dr["FlowCode"] = DateTime.Now.ToString("yyyyMMddHHmm") + function.GetRandomString(6);
        dr["CDate"] = DateTime.Now;
        tlp = tlp.Replace("{Text}", dr["Text"].ToString());
        string fontdir = "/WebFont/Users/" + dr["FlowCode"].ToString() + "/";
        dr["FontDir"] = function.VToP(fontdir);
        SafeSC.WriteFile(fontdir + "tlp.html", tlp);
        dr["ZStatus"] = 0;
        //1,将选定的字体拷入
        string src = "/WebFont/Fonts/" + fontsrc;
        string tar = fontdir + "pen.ttf";
        Copy(src, tar);
        int id = codeBll.Insert(dr);
        result_div.Visible = true;
        result_t_div.InnerText = T1.Text;
        r_quote_t.Text = GetQuote(dr["FlowCode"].ToString(), "pen");
        font_css.InnerHtml = GetQuote(dr["FlowCode"].ToString(), "pen");
        //2,运行命令开始生成(命令行中已处理)
        //3,拷贝完成后回发指令,表示完成,客户端可预览或下载
        while (Response.IsClientConnected)
        {
            System.Threading.Thread.Sleep(1000);
            dr = codeBll.SelByID(id);
            if (dr["ZStatus"].ToString().Equals("1")) { break; }
        }
    
    }
    private void Copy(string src, string target)
    {
        string srcPath = function.VToP(src);
        string tarPath = function.VToP(target);
        File.Copy(srcPath, tarPath);
    }
    private string GetQuote(string flow, string font)
    {
        //最终决定   网址/t/ig098xinklnmi
        //需要路由,否则无法跨域处理
        string vpath = (SiteConfig.SiteInfo.SiteUrl).Replace("http:", "") + "/WebFont/Users/" + flow + "/" + font;
        string quote = "@font-face {font-family: 'demofont';"
            + "src:url('" + vpath + ".eot');"
            + "src:url('" + vpath + ".eot') format('embedded-opentype'), url('" + vpath + ".woff') format('woff'), url('" + vpath + ".ttf') format('truetype'), url('" + vpath + ".svg') format('svg');"
            + "font-weight: normal;font-style: normal;}";
        return quote;
    }
    //private string GetQuote(string flow, string font)
    //{
    //    //最终决定   网址/t/ig098xinklnmi
    //    //需要路由,否则无法跨域处理
    //    string vpath = (SiteConfig.SiteInfo.SiteUrl).Replace("http:","") + "/t/" + flow + "/" + font;
    //    string quote = "@font-face {font-family: 'demofont';"
    //        + "src:url('" + vpath + ".eot');"
    //        + "src:url('" + vpath + ".eot') format('embedded-opentype'), url('" + vpath + ".woff') format('woff'), url('" + vpath + ".ttf') format('truetype'), url('" + vpath + ".svg') format('svg');"
    //        + "font-weight: normal;font-style: normal;}";
    //    return quote;
    //}
}