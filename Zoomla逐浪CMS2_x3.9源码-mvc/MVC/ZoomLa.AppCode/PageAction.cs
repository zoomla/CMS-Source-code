using System;
using System.IO;
using System.Web;
using System.Web.UI;

/// <summary>
/// Get the CustomPath from URLRewrite.config and Rewrite form's target action
/// </summary>
public class CustomerPageAction : Page

{
    /// <summary>
    /// ~/customPath/:Used for CS
    /// </summary>
    public static string customPath;
    /// <summary>
    /// /customPath/:Used for JS and Aspx page
    /// </summary>
    public static string customPath2;
    /// <summary>
    /// Admin:BasePath
    /// </summary>
    public static string baseCustomPath;
    /// <summary>
    /// Read the latest configuration from UrlRewrite.config
    /// </summary>
    public static void SetCustomPath()
    {
        //baseCustomPath = URLRewriter.Config.RewriterConfiguration.GetBaseConfig().LookFor;
        baseCustomPath = "admin";
        customPath = "~/" + baseCustomPath + "/";
        customPath2 = "/" + baseCustomPath + "/";
    }
    static CustomerPageAction()
    {
        baseCustomPath = "admin";
        //baseCustomPath = URLRewriter.Config.RewriterConfiguration.GetBaseConfig().LookFor;
        customPath = "~/" + baseCustomPath + "/";
        customPath2 = "/" + baseCustomPath + "/";
    }
   
    #region Render

        /// <summary>
        ///  重写默认的HtmlTextWriter方法，修改form标记中的value属性，使其值为重写的URL而不是真实URL。
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {

            if (writer is System.Web.UI.Html32TextWriter)
            {
                writer = new FormFixerHtml32TextWriter(writer.InnerWriter);
            }
            else
            {
                writer = new FormFixerHtmlTextWriter(writer.InnerWriter);
            }

            base.Render(writer);
        }
        #endregion

    #region FormFixers

    #region FormFixerHtml32TextWriter
    internal class FormFixerHtml32TextWriter : System.Web.UI.Html32TextWriter
    {
        private string _url; // 假的URL

        internal FormFixerHtml32TextWriter(TextWriter writer):base(writer)
        {
            _url = HttpContext.Current.Request.RawUrl;
        }

        public override void WriteAttribute(string name, string value, bool encode)
        {
            // 如果当前输出的属性为form标记的action属性，则将其值替换为重写后的虚假URL
            if (_url != null && string.Compare(name, "action", true) == 0)
            {
                value = _url;
            }
            base.WriteAttribute(name, value, encode);
        }
    }
    #endregion
    
    #region FormFixerHtmlTextWriter
    internal class FormFixerHtmlTextWriter : System.Web.UI.HtmlTextWriter
    {
        private string _url;
        internal FormFixerHtmlTextWriter(TextWriter writer):base(writer)
        {
            _url = HttpContext.Current.Request.RawUrl;
        }

        public override void WriteAttribute(string name, string value, bool encode)
        {
            if (_url != null && string.Compare(name, "action", true) == 0)
            {
                value = _url;
            }

            base.WriteAttribute(name, value, encode);
        }
    }
    #endregion

    #endregion

}