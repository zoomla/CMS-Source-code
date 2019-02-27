namespace ZoomLa.WebSite.Admin.Prompt
{
    using System;
    using ZoomLa.Web;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using ZoomLa.SQLDAL;
    using ZoomLa.Common;

    public partial class ShowSuccess : CustomerPageAction
    {
        protected string link = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            this.LtrSuccessMessage.Text = HttpContext.Current.Items["Message"] as string;
            string url = HttpContext.Current.Items["ReturnUrl"] as string;
            int time = DataConvert.CLng(HttpContext.Current.Items["autoSkipTime"]);
            if (string.IsNullOrEmpty(url))
            {
                if (base.Request.UrlReferrer == null)
                {
                    LnkReturnUrl.Text = "<span class='fa fa-home'></span>首页";
                    LnkReturnUrl.NavigateUrl = "/";
                }
                else
                {
                    function.Script(this, "SetUrl('javascript:history.back();');");
                }
            }
            else
            {
                if (time > 0) function.Script(this, "AutoReturn('" + url + "'," + time + ");");
                LnkReturnUrl.Text = "<span class='fa fa-repeat'></span>立即跳转";
                function.Script(this, "SetUrl('" + url + "');");
            }
        }
    }
}