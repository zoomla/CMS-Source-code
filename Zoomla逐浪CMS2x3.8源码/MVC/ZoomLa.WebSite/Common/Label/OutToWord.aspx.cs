namespace ZoomLaCMS.Common.Label
{
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
    public partial class OutToWord : System.Web.UI.Page
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
                SafeSC.DownFile(OfficeHelper.W_HtmlToWord("<html>" + Html + "</html>", wordPath));
            }
        }
    }
}