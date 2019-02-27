namespace ZoomLa.WebSite.Admin.Prompt
{
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using ZoomLa.Web;

    public partial class ShowError : System.Web.UI.Page
    {
        //protected HtmlForm form1;
        //protected HyperLink LnkReturnUrl;
        //protected Literal LtrErrorMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LtrErrorMessage.Text = HttpContext.Current.Items["ErrorMessage"] as string;
            string str = HttpContext.Current.Items["ReturnUrl"] as string;
            if (string.IsNullOrEmpty(str))
            {
                if (base.Request.UrlReferrer == null)
                {
                    this.LnkReturnUrl.Text = "关闭";
                    this.LnkReturnUrl.NavigateUrl = "javascript:window.close();";
                }
                else
                {
                    this.LnkReturnUrl.NavigateUrl = "javascript:history.back();";
                }
            }
            else if ((str.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase) || str.StartsWith("https://", StringComparison.CurrentCultureIgnoreCase)) || str.StartsWith("javascript:", StringComparison.CurrentCultureIgnoreCase))
            {
                this.LnkReturnUrl.NavigateUrl = str;
            }
            else
            {
                this.LnkReturnUrl.NavigateUrl = str;
            }
        }
    }
}