namespace ZoomLa.WebSite.Manage
{
    using System;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Xml;
    using ZoomLa.Components;
    using ZoomLa.Common;
    using System.Web.Configuration;
    using ZoomLa.Web;
using ZoomLa.BLL;

    public partial class index : System.Web.UI.Page
    {
        /// <summary>
        /// 页面装载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected B_Admin badmin = new B_Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.badmin.CheckMulitLogin();
            this.Header.Title = SiteConfig.SiteInfo.SiteName + "--后台管理";
        }
    }
}