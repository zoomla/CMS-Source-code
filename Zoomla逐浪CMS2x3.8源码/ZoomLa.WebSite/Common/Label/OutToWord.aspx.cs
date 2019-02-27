using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL;
using ZoomLa.Common;
/*
 * 导出至Word功能,推荐导出为word
 * base64编码,Post提交,建议弹新窗口,以div为单位
 */
public partial class Common_Label_OutToWord : System.Web.UI.Page
{
    public string Html { get { return HttpUtility.UrlDecode(Request.Form["html_toword_hid"] ?? ""); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string wordName = HttpUtility.UrlDecode(Request["Name"] ?? "");
            string wordDir = "/UploadFiles/auto/outToWord/";
            if (string.IsNullOrEmpty(wordName)) { wordName = function.GetRandomString(6); }
            SafeSC.CreateDir(wordDir);
            string wordPath = wordDir + wordName + ".docx";
            SafeSC.DownFile(OfficeHelper.W_HtmlToWord("<html>" + Html + "</html>",wordPath));
        }
    }
}