using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZoomLaCMS.Prompt
{
    public partial class ShowMessage : System.Web.UI.Page
    {
        //protected HtmlForm form1;
        //protected Label LblMessageTitle;
        //protected HyperLink LnkReturnUrl;
        //protected Literal LtrMessage; 
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Link1.Visible = false;
            //if (Request.Cookies["MyUrlCook"] != null)
            //{
            //    this.Link1.Text = "确认付费浏览 ";
            //    this.Link1.Visible = true;
            //}
            this.LtrMessage.Text = HttpContext.Current.Items["Message"] as string;
            string str = HttpContext.Current.Items["ReturnUrl"] as string;
            this.LblMessageTitle.Text = HttpContext.Current.Items["MessageTitle"] as string;
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
            else if (!string.IsNullOrEmpty(this.Page.Request.RawUrl))
            {
                string rawUrl = this.Page.Request.RawUrl;
                string str3 = rawUrl.Substring(0, rawUrl.LastIndexOf("/"));
                string str4 = rawUrl.Substring(rawUrl.LastIndexOf("/"));
                if (str.ToLower().IndexOf("../") == 0) { str = str.Replace("../", CustomerPageAction.customPath); }
                this.LnkReturnUrl.NavigateUrl = str;
            }
        }

        protected void Link1_Click1(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("MyCook");//初使化并设置Cookie的名称
            DateTime dt = DateTime.Now;
            TimeSpan ts = new TimeSpan(0, 0, 0, 20, 0);//过期时间为1分钟
            cookie.Expires = dt.Add(ts);//设置过期时间
            cookie.Values.Add("return", "1");
            Response.AppendCookie(cookie);
            // ClientScript.RegisterStartupScript(typeof(string), "script", "<script language=javascript>location.href='/Item/29.aspx'; </script>");
            //string rawUrl = this.Page.Request.RawUrl;
            //throw new Exception(rawUrl);
            HttpCookie cok = Request.Cookies["MyUrlCook"];
            if (cok != null)
            {
                string str = Request.Cookies["MyUrlCook"]["Url"];
                Response.Redirect(str);
                TimeSpan tss = new TimeSpan(-1, 0, 0, 0);
                cok.Expires = DateTime.Now.Add(tss);//删除整个Cookie，只要把过期时间设置为现在
                Response.AppendCookie(cok);
            }
            else
            {
                Response.Redirect(this.Page.Request.RawUrl);
            }
        }
    }
}