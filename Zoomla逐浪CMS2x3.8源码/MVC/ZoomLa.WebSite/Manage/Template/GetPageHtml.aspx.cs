using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using ZoomLa.BLL;


namespace ZoomLaCMS.Manage.Template
{
    public partial class GetPageHtml : System.Web.UI.Page
    {
        private string PageUrl = "";
        protected System.Web.UI.WebControls.Button GetText = new Button();
        protected void Page_Load(object sender, EventArgs e)
        {
            Call.SetBreadCrumb(Master, "<li>工作台</li><li>系统设置</li><li>模板风格</li><li class=\"active\">源码探测速</li>");
            this.GetText.Click += new System.EventHandler(this.GetText_Click);
        }
        protected void WebClientButton_Click(object sender, EventArgs e)
        {
            PageUrl = UrlText.Text;
            WebClient wc = new WebClient();
            wc.Credentials = CredentialCache.DefaultCredentials;

            ///方法一：
            Byte[] pageData = wc.DownloadData(PageUrl);
            ContentHtml.Text = Encoding.Default.GetString(pageData);


            /// 方法二：
            /// ***************代码开始**********
            /// Stream resStream = wc.OpenRead(PageUrl);
            /// StreamReader sr = new StreamReader(resStream,System.Text.Encoding.Default);
            /// ContentHtml.Text = sr.ReadToEnd();
            /// resStream.Close();
            /// **************代码结束********
            ///  
            wc.Dispose();

        }
        protected void WebRequestButton_Click(object sender, EventArgs e)
        {
            PageUrl = UrlText.Text;
            WebRequest request = WebRequest.Create(PageUrl);
            WebResponse response = request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(resStream, System.Text.Encoding.Default);
            ContentHtml.Text = sr.ReadToEnd();
            resStream.Close();
            sr.Close();
        }
        private void GetText_Click(object sender, System.EventArgs e)
        {
            PageUrl = UrlText.Text;
            WebRequest request = WebRequest.Create(PageUrl);
            WebResponse response = request.GetResponse();
            Stream resStream = response.GetResponseStream();
            StreamReader sr = new StreamReader(resStream, System.Text.Encoding.Default);
            ContentHtml.Text = sr.ReadToEnd();
            resStream.Close();
            sr.Close();
            ContentHtml.Text = Regex.Replace(ContentHtml.Text, "<[^>]*>", "");
            //替换空格
            ContentHtml.Text = Regex.Replace(ContentHtml.Text, "\\s+", " ");
        }
    }
}