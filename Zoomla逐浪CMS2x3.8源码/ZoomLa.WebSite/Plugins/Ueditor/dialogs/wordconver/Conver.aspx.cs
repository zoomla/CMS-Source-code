using Aspose.Words;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;

public partial class Plugins_Ueditor_dialogs_wordconver_Conver : System.Web.UI.Page
{
    RegexHelper regHelper = new RegexHelper();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Add_Btn_Click(object sender, EventArgs e)
    {
        if (File_UP.PostedFile.ContentLength < 100) { function.Script(this, "alert('请先选择文件');"); return; }
        string vpath = "/UploadFiles/Admin/Temp/WordConver/";
        string exname = Path.GetExtension(File_UP.FileName).ToLower().Replace(".", "");
        //string fpath = SafeSC.SaveFile(vpath, File_UP.PostedFile);
        string fpath = vpath + Path.GetFileName(File_UP.FileName);
        File_UP.SaveAs(fpath);
        if ("doc,docx,rtf".Split(',').Contains(exname))
        {
            Document doc = new Document(Server.MapPath(fpath));
            vpath = vpath + function.GetRandomString(3) + "/";
            string dirpath = Server.MapPath(vpath);
            string htmlpath = dirpath + function.GetRandomString(3) + ".html";
            if (!Directory.Exists(dirpath)) Directory.CreateDirectory(dirpath);
            doc.Save(htmlpath, SaveFormat.Html);
            string text = SafeSC.ReadFileStr(htmlpath);
            text = regHelper.GetValueBySE(text, "<body>", "</body>", false);
            text = text.Replace("<img", " <img");
            MatchCollection matchs = regHelper.GetValuesBySE(text, "<img", "/>");
            foreach (Match mc in matchs)
            {
                if (string.IsNullOrEmpty(mc.Value)) continue;
                string src = regHelper.GetValueBySE(mc.Value, "src=\"", "\"", false);
                string newvalue = "";
                newvalue = mc.Value.Replace(src, vpath + src);
                text = text.Replace(mc.Value, newvalue);
            }
            text = text.Replace("&#xa0;", "");
            Content_Div.InnerHtml = text;
            function.Script(this, "SetContent();");
        }
        else if (exname.Equals("txt"))
        {
            string text = SafeSC.ReadFileStr(Server.MapPath(fpath),true);
            Content_Div.InnerHtml = text;
            function.Script(this, "SetContent();");
        }
        else if (exname.Equals("rtf"))
        {
 
        }
        else
        {
            function.Script(this, "alert('请上传doc,docx文件!!');"); return;
        }
    }
}