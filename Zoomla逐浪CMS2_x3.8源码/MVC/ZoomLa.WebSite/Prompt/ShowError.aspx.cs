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
    using ZoomLa.Common;
    using ZoomLa.Web;
    public partial class ShowError : CustomerPageAction
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //避免报错出现循环
            try
            {
                this.LtrSuccessMessage.Text = HttpContext.Current.Items["Message"] as string;
                string url = HttpContext.Current.Items["ReturnUrl"] as string;
                string title = HttpContext.Current.Items["MessageTitle"] as string;
                if (!string.IsNullOrEmpty(title)) { title_h3.InnerHtml = title; }
                if (string.IsNullOrEmpty(url))
                {
                    //传入url为空,且是直接进入,则只显示关闭此页按钮,否则返回上一页
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
                    //传入外网地址或js,则点击执行
                    this.LnkReturnUrl.NavigateUrl = url;
                }
                else
                {
                    function.Script(this, "SetUrl('" + url + "');");
                }
            }
            catch (Exception ex) { Response.Clear(); Response.Write(ex.Message); Response.End(); }
        }
    }
}