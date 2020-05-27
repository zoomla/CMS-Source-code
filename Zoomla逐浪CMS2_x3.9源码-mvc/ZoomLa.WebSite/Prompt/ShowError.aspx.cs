using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZoomLaCMS.Prompt
{
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using ZoomLa.Web;
    public partial class ShowError : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LtrSuccessMessage.Text = HttpContext.Current.Items["Message"] as string;
            string url = HttpContext.Current.Items["ReturnUrl"] as string;
            string title = HttpContext.Current.Items["MessageTitle"] as string;
            if (!string.IsNullOrEmpty(title)) { title_h3.InnerHtml = title; }
            if (string.IsNullOrEmpty(url))
            {
                if (base.Request.UrlReferrer == null)
                {
                    this.LnkReturnUrl.Text = "<span class='fa fa-remove'></span>关闭";
                    this.LnkReturnUrl.NavigateUrl = "#";
                    this.LnkReturnUrl.Attributes.Add("onclick", "window.close();");
                    this.LnkReturnUrl.ToolTip = "关闭此页";
                }
                else
                {
                    this.LnkReturnUrl.NavigateUrl = "javascript:history.back();";// Request.UrlReferrer.ToString();
                }
            }
            else if ((url.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase) || url.StartsWith("https://", StringComparison.CurrentCultureIgnoreCase)) || url.StartsWith("javascript:", StringComparison.CurrentCultureIgnoreCase))
            {
                if (url.ToLower().IndexOf("../") == 0) { url = url.Replace("../", CustomerPageAction.customPath).Replace("~/", ""); }
                this.LnkReturnUrl.NavigateUrl = url;
            }
            else
            {
                if (url.ToLower().IndexOf("../") == 0) { url = url.Replace("../", "/" + CustomerPageAction.customPath).Replace("~/", ""); }
                this.LnkReturnUrl.NavigateUrl = url;
            }
        }
    }
}